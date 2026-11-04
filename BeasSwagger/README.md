# SAP B1 / Beas Swagger 測試介面

ASP.NET Core 8 + Swashbuckle,把 `Beas 匯入工單DLL說明.xlsx` 的每個 sheet 對應成一組 API。

## 執行
```
cd BeasSwagger
dotnet run        # 開啟 http://localhost:5000/swagger (實際埠號看 console)
```
預設 `Beas:UseMock=true`,所有端點回傳收到的參數(不連資料庫),可先驗證介面。

## 運作方式(不需要 DLL)
Swagger 端點 → 組出 Beas Script → `INSERT INTO <db>."BEAS_COMMON_INPUT"(COMMONTYP,TEXTPARAMETER,REQUESTID)` → 輪詢 `BEAS_COMMON_OUTPUT`(依 REQUESTID,COUNTID desc 取 ANSWERTEXT)。

- 已實作(依 `beasScript.pdf` 與 `fetched/beasscript` 說明撰寫,**尚未在實際 Beas 驗證**):
  - `production/issue`(發料,來自原 `issue` 函數,最早的實作)
  - `production/finished-receipt`、`production/return-good`(入庫/半成品退回,`ue_reservation=receiptwo`,base_linenum2=0 為父項)
  - `production/finished`(完工回報,`ue_api_time`)
  - **BOM(照原 DLL 的 `bom` 類別)**:版本不是 `ItemVersion`,而是 `BEAS_STL.VersionArea`,標記在同一張 BOM 的每一行上。
    - `bom/create`(`CreateM`):`ue_api_bom` 的 `new` → 每品項一組欄位 → `update`,`BomItem` 欄位沿用原 DLL(Quanity、PosText、Uom、Length、QuantityPerPiece、FaterVersion、WorkRout、Version、Stap);`"''"` 視為空;`WorkRout` 空白預設 `01`。REQUESTID=`Bom`+CreateTime。
    - `bom/create-version`(`CreateMultVer`):同上,但所有行的 versionarea 設為 `Version`。`bom=new` 在 BOM 已存在時會失敗。
    - `bom/change`(`ChangeBom`):明細數為 0 → 走 create;否則 → 取代該版本的明細(同 `search-other-version`)。`Version="-1"` 視為空白版本。
    - `bom/search-other-version`(`Searchotherversionbom`):名稱容易誤解,實際是「取代指定版本的明細」:讀出其他版本既有明細 → 在**同一交易**刪除 `BEAS_STL_HEADER` 與該版本的 `BEAS_STL` 行並寫入 INPUT → script 以「新明細 + 其他版本既有明細」重建整張 BOM。**直接刪除 Beas 資料表,需 `Beas:Script:AllowDirectSqlWrite=true`**,只有 Script 版。
    - `bom/delete`(`DeleteBom`):直接刪除 `BEAS_STL_HEADER` 與 `BEAS_STL`(同一交易),同樣需 `AllowDirectSqlWrite`。原 DLL 的 SQL 少了「.」且兩句塞同一命令,在 HANA 應跑不起來,這裡拆成兩句。
  - `routing/add`(`ue_api_routing`;REQUESTID=`Rot:`+Mes+CreateDate,結果回傳新行 pos_id)、`routing/sql-result-back`(`Rot:`+Mes+CreateTime)
  - `routing/change`、`routing/delete`:照原 DLL **直接對 `BEAS_APL_WKZ`、`BEAS_APL` 下 DELETE**(整條途程),`change` 是「先刪整條途程再建立只有這一個工序的新途程」(刪除與寫入 INPUT 同一交易,**會移除該料號其他所有工序**)。預設關閉:需 `Beas:Script:AllowDirectSqlWrite=true`,否則回 HTTP 403;只有 Script 版。SQL 固定在程式內且值參數化(原 DLL 是字串串接)。
  - `workorder/change-bom`(`ue_api_wo` 的 `bom_replaceitem`:取代工單 BOM 某行料號,欄位 Woid/Woposition/Woitemposition/NewItem 來自原 DLL;Bsl 版為 `PUT WorkorderBom`)
  - `workorder/import-single`、`workorder/import-more`(`ue_api_wo`;建立後工單號用 `<mywo.docentry>` 取得,屬性名稱為推測)
  - 各 `sql-result-back`、`workorder/del-result-back`:對應原 DLL 的 `sqlresultback`/`delresultback`,只讀一次 `BEAS_COMMON_OUTPUT`(依 REQUESTID 取 COUNTID 最大且 ANSWERTEXT 非空的一筆),查不到回「尚無結果」由呼叫端重試;`del-result-back` 預設 REQUESTID=`wobominsert`+CreateTime(原 DLL 寫死),`sql-result-back` 預設 Mes+CreateTime(推測),兩者都可用 `RequestId` 直接覆寫
  - `bom/check`(`checkbom`,明細行數)、`bom/check-version`(`checkbomversion`:版本空白=VersionArea 為 NULL,否則 `like %版本%`)、`bom/max-level`:唯讀 SQL(`Services/IBeasQueryClient.cs`,只允許單一 SELECT、值參數化;Mock 模式只回傳將執行的 SQL)。`max-level` 原 DLL 對字串取 MAX(字典序),這裡改取數值最大值。`bom/sql-result-back` 的 REQUESTID=`Bom`+CreateTime(原 DLL 會把 mes 清成空字串,Mes 不參與)。
  - `unit/create`、`unit/update`(原 `CreateUom`):**直接 INSERT / UPDATE `BEAS_ME`**(原 DLL 如此,沒有 Script/BSL 路徑;BSL 的 `UoM` 實體只有 GET)。Create:`ERFTSTAMP`=今天(僅日期)、`ERFUSER`=`MES` 固定,`ANDTSTAMP`/`ANDUSER` 取自請求;Update:`ANDTSTAMP`=今天;影響 0 筆回 `NO`,成功回 `OK`。需 `AllowDirectSqlWrite=true`。`unit/sql-result-back` 只是確認單位是否存在(存在回單位代碼,否則 `no`),不需開關。欄位名不加引號,讓 HANA 轉成大寫。
- 其餘端點仍是 Mock,尚無原始碼/文件依據:scrap、scrapdetail、workorder 的 import-with-machine。
- `Beas:UseMock=true` 時只回傳產生的 script,不寫庫;改 `false` 後才真的寫入。
- `Beas:Script:OdbcConnectionString` 預設已與原 DLL 的 `connection()` 一致:`Driver=HDBODBC32;ServerNode=…;uid=…;pwd=…;DATABASE=…`(HANA;`HDBODBC32` 為 32 位元驅動,64 位元環境請改 `HDBODBC`)。

## 待確認
- `BomItem`、`WorkOrderItem`、`ChangeBomItem` 的欄位是推測的,Excel 沒有定義,請依 DLL 調整。
- `appsettings.json` 內密碼勿提交真實值(`Beas:Connection:Password`、`Beas:Bsl:Password` 皆為 `CHANGE_ME`;建議用環境變數或 user-secrets)。

## 選擇 Beas 接口
- `appsettings.json` 的 `Beas:Interface`(`Script` 或 `Bsl`)設定全域預設;單一請求可用 `Interface` 欄位覆寫。
- `Script`:BeasScript 內建物件(`ue_api_*`、`ue_reservation`)。`Bsl`:在 BeasScript 內以 `ue_bsl`(`instance bsl b` + `b.post=Entity=<json>`)呼叫 Beas Service Layer(`Services/BeasBslBuilder.cs`,依 `fetched/bsl`)。兩者都是寫入 `BEAS_COMMON_INPUT` 的同一流程。
- **`BslHttp`:直接呼叫 Beas Service Layer(不經資料庫)**。依官方範例(`BSL-CSharpExample`、`fetched/bsl/bsl_and_php.htm`、`bsl_authorization.htm`):`POST {BaseUrl}/Login`(`{"User","Pwd"}`,另可帶 `ServicePwd`、`StationId`、`StationPwd`、`LocationId`、`ProgramId`)→ 回應的 `beas-sessionid` 以 `Cookie: beas-sessionid=…` 帶在後續請求 → `POST`/`PUT {BaseUrl}/<Entity>` → `POST {BaseUrl}/Logout`。每個請求各自登入、登出;多筆呼叫(如 `workorder/import-more`)依序送出,**任何一步失敗就停止**並仍登出。
  - 設定:`Beas:Bsl`(`BaseUrl`、`User`、`Password`、`TimeoutSeconds`…);`Beas:Interface` 設為 `BslHttp` 或單一請求用 `Interface` 欄位指定。需先在 Beas Tools > WEB Server 啟動服務,並在人員主檔 Login 頁籤設定帳密(文件的 PHP/C# 範例預設 port 8080 / 8081,以實際為準)。
  - 與 `Bsl` 共用同一份 builder,**送出的 JSON 與 `Bsl` 的 script 版相同**,所以有 `Bsl` 版的端點(含只有 Bsl 版的 `version/*`)都能用;沒有 `Bsl` 版的端點回 HTTP 501。
  - 結果:`Data` 直接是 BSL 的回應 JSON(單一呼叫為該回應,多個呼叫為陣列),**不需輪詢、不需 `*/sql-result-back`**。成功條件:HTTP 2xx 且回應若含 `ret_code` 則為 0;失敗時 `Message` 為 `<步驟>: (<ret_code>) <ret_text>`,`Data` 為原始回應。連不上、逾時、登入失敗都回 `Success=false`(不是 HTTP 500)。
  - **尚未對真實 BSL 驗證**:只用假的 BSL 伺服器測過登入/Cookie/順序/失敗停止/登出。需確認:資源路徑是否需要版本段(文件的 `bslissue.htm` 範例寫 `odata4/v1/issue`,C# 範例是 `/odata4/Reservation`;若需要,把 `/v1` 放進 `BaseUrl`)、POST 回應裡單據編號所在欄位(目前原樣回傳整份 JSON)、`PUT WorkorderBom(...)` 的 HTTP 寫法。
- 已有 `Bsl` 版:`production/issue`(IssueWo)、`finished-receipt` / `return-good`(ReceiptWo)、`finished`(TimeReceipt)、`bom/create` / `bom/create-version`(BillOfMaterial POST;欄位對應:PosText→Position、Quanity→Input_Quantity、Uom→Input_Unit、QuantityPerPiece→QuantityPer、Stap→ScrapPercent、WorkRout→UDF1、FaterVersion→VersionFilter〔文件:資料庫欄位即 BEAS_STL.VersionArea〕、Version→IVersionId〔推測〕)、`routing/add`(Routing)、`workorder/import-single` / `import-more`(Workorder)、`version/create` / `version/update`(ItemVersion,**只有 Bsl 版**,未指定接口時自動用 Bsl;明確指定 Script 回 501;版本已存在時 create 會更新、update 只送有值的欄位,`Remark` 無對應欄位不送出)。其餘選 `Bsl` 會回 HTTP 501。
- 行為差異:`Bsl` 的 `bom/create` 使用 `replace=false`(加入明細,BOM 已存在時**不會失敗**,也不會清掉其他版本的行);`bom/change` 的「取代版本」分支與 `bom/search-other-version`、`bom/delete`、`routing/change`、`routing/delete` 只有 Script 版(BSL 的 script 介面沒有 delete);`Bsl` 版 `Finished` 若有 `DoTime` 會以 `Duration`(分鐘)送出。
- 尚未在實際 Beas 驗證,需確認:`b.ret_value` 是否為單據編號、`Routing` POST 在途程不存在時是否自動建立表頭、`VersionFilter` 的值格式(BSL 文件寫成 CSV 加引號 `"3","5"`,原 DLL 存的是去掉單引號的原值)。
- 輸入含換行/Tab/`?` 時回 HTTP 400。
