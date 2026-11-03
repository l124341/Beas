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
  - `bom/create`、`bom/change`、`bom/create-version`(`ue_api_bom`)
  - `routing/add`(`ue_api_routing`)
  - `workorder/import-single`、`workorder/import-more`(`ue_api_wo`;建立後工單號用 `<mywo.docentry>` 取得,屬性名稱為推測)
  - `bom/check`、`bom/max-level`:唯讀 SQL 查 `BEAS_STL`(`Services/IBeasQueryClient.cs`,只允許單一 SELECT、值參數化;欄位名稱取自 api_bom 說明,未對真實資料庫驗證;Mock 模式只回傳將執行的 SQL)
- 其餘端點仍是 Mock,文件沒有對應語法:BOM 刪除、check-version、search-other-version、routing change/delete、unit、scrap、scrapdetail、version、workorder 的 import-with-machine / change-bom / del-result-back、各 sql-result-back。
- `Beas:UseMock=true` 時只回傳產生的 script,不寫庫;改 `false` 後才真的寫入。
- `Beas:Script:OdbcConnectionString` 要與原 `connection()` 一致(預設是 HANA 的猜測值)。

## 待確認
- `BomItem`、`WorkOrderItem`、`ChangeBomItem` 的欄位是推測的,Excel 沒有定義,請依 DLL 調整。
- `appsettings.json` 內密碼勿提交真實值。

## 選擇 Beas 接口
- `appsettings.json` 的 `Beas:Interface`(`Script` 或 `Bsl`)設定全域預設;單一請求可用 `Interface` 欄位覆寫。
- 目前所有功能只有 `Script` 版(BeasScript 內建物件)。選 `Bsl` 會回 HTTP 501,待取得 Beas Service Layer 的實體文件後逐一補上。
- 輸入含換行/Tab/`?` 時回 HTTP 400。
