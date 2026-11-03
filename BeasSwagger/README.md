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

- 已實作:`POST /api/production/issue`(發料,來自原 `issue` 函數)。
- 其他端點目前仍是 Mock,需要各功能的 script 範例才能補上(見 `Services/BeasScriptBuilder.cs`)。
- `Beas:UseMock=true` 時只回傳產生的 script,不寫庫;改 `false` 後才真的寫入。
- `Beas:Script:OdbcConnectionString` 要與原 `connection()` 一致(預設是 HANA 的猜測值)。

## 待確認
- `BomItem`、`WorkOrderItem`、`ChangeBomItem` 的欄位是推測的,Excel 沒有定義,請依 DLL 調整。
- `appsettings.json` 內密碼勿提交真實值。
