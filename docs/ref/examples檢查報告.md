# Examples 章節範例程式碼檢查報告

> 檢查對象:`fetched/examples/*.htm`(依 `fetched/examples_list.txt` 順序)。
> 範例程式碼一律引用原文,不做修改。所有「官方」證據皆指 `fetched/` 底下的官方頁面或 `docs/ref/` 的整理文件。
> 暫存與驗證腳本放在 `/tmp/claude-0/-home-user/08029163-3a91-551c-a68b-7c8f0a2e6ace/scratchpad/E_work/`(例如 `cleanstock.js`、`cs_test.js`、`cs_json.js`、`jsoncheck.py`)。

## 統計

| 項目 | 數量 |
|---|---|
| 檢查頁數 | **54 頁**(說明檔寫 53 頁,但 `examples_list.txt` 與 `fetched/examples/` 實際都是 54 個檔) |
| 其中含程式碼的頁面 | 40 頁 |
| 無程式碼的頁面(目錄頁 / 只有截圖 / 只有說明) | 14 頁 |
| 檢查的程式碼段數 | **65 段**(連續的一段程式碼算一段;同一段內多個函式不另計) |
| **確定錯誤** | **9** |
| **與官方不一致** | **28** |
| **存疑** | **37** |
| **手冊已記錄** | **25**(含 2 項「手冊已記錄(補充)」) |
| 有問題的頁面 | 33 頁 |

驗證方式:

- 語法成對:逐段人工核對 `global function/end global`、`function/end function`、`if/end if`、`for/next`、`[label]/goto`、括號與引號。
- JSON:以 `python3 json.loads` 驗證(佔位符先代入樣本值),全部可解析(見各頁說明)。
- JavaScript(`examplecleanstock`):`node --check` 語法檢查,並以 mock 物件實際執行一次。
- 官方定義:對照 `fetched/beasscript/`、`fetched/bsl/`、`fetched/missing*/` 官方頁面,`docs/ref/*.md`,以及 `database.txt`。

---

## 各頁問題(依頁面順序)

### add_a_button_and_click_event.htm(E: Add a button and click event)

| # | 等級 | 位置 | 問題 | 證據 | 建議 |
|---|---|---|---|---|---|
| 1 | 與官方不一致 | 第 2 段 `global function dw_master_item_button_testhtml_click` | 按鈕用 `create=button=...` 建立,但事件名帶有 `button_`。官方兩頁寫法不同 | `beasscript/bsbuttoncreate.htm`:`create=button` 的事件是 `global function dw_master_item_mpos2_click`(無 `button_`),`button_` 只出現在 `create=rightclick` 的 `dw_master_item_button_boyum_hello_click`;但手冊整理的視窗事件表(`Beas範例操作手冊.md` 第 3137 行)寫 `dw_master_item_button_[object]_click`「點擊按鈕或右鍵選單」 | 以系統資訊(System Information)Protocol 頁籤顯示的實際事件名為準;手冊第 125 行「事件命名規則:`dw_master_item_button_<按鈕名稱>_click`」與 `bsbuttoncreate.htm` 不同,建議加註 |

### bsl-example-placeholder.htm(E: BSL: Place holder and function call)

| # | 等級 | 位置 | 問題 | 證據 | 建議 |
|---|---|---|---|---|---|
| 1 | 與官方不一致 | 佔位符行與完整範例 `<bsl.WorkorderPos([...]\|[...]).QuantityProduced/$value>` | 屬性前用 `.`,官方用 `/` | `beasscript/bs-bsl.htm`:`<bsl.WorkorderPos([ll_DocEntry]\|[ll_LineNumber])/QuantityProduced/$value>`;同頁其他例 `<bsl.item("0815")/ItemName/$value>` | 改用 `/QuantityProduced/$value` 並實測 |
| 2 | 與官方不一致 | `if <b.Status> = locked then`(兩處) | 比對值 `locked` 不在官方列出的狀態值中 | `bsl/bslworkorderbomcheckminmax2.htm`(CheckMinMaxBom):「status = ok -> all ok, question, blocked」 | 應比對 `blocked`(或同時處理 `question`),實測回傳值 |
| 3 | 存疑 | 完整範例第一個 `if` | 註解寫「Bill of Material position must be defined」,條件卻是 `<dw_1.item.pos_id.value> n= 0`(BOM 位置必須為 0) | 原文:`// Work order - Bill of Material position must be defined` / `... and <dw_1.item.pos_id.value> n= 0 and ...` | 註解與條件相反,確認意圖(收貨組件時 pos_id=0)後修正註解 |

### create_button.htm(E: Create Button)

| # | 等級 | 位置 | 問題 | 證據 | 建議 |
|---|---|---|---|---|---|
| 1 | 確定錯誤 | `windowevent itempressed beas_lyt` | 建立的按鈕名稱是 `mybutton`,但點擊事件攔的是 `beas_lyt`,按下 `mybutton` 不會觸發此事件 | 原文:`create button=name=mybutton,...` 與 `windowevent itempressed beas_lyt`;官方 `beasscript/windowevent_open.htm` 範例 `create=button=name=beas_kalk` 對應 `windowevent itempressed beas_kalk`(名稱一致) | 改為 `windowevent itempressed mybutton` |
| 2 | 手冊已記錄 | `windowevent open` / `windowevent itempressed` | 使用 `windowevent`,與 SAP UI-API 頁「不要使用 windowevent」NOTE 矛盾 | 手冊 2.1 第 529 行 | — |

### create_right_click_entry.htm(E: Create Right Click Entry)

| # | 等級 | 位置 | 問題 | 證據 | 建議 |
|---|---|---|---|---|---|
| 1 | 確定錯誤 | 兩段的 `beasgui=rightclickmenu=1=<spr:My Entry=myentry1=...` | `<spr:` 佔位符沒有對應的 `>`,角括號不成對;後面的 `=myentry1=...` 會被吃進佔位符 | 官方 `beasscript/rightclickmenu_sbo.htm` caption 範例為完整佔位符 `<translate:My Menu Entry>` | 改為 `<spr:My Entry>=myentry1=...` |
| 2 | 存疑 | `windowevent premmenuclick myentry1` | 事件名 `premmenuclick` 在所有官方頁面(beasscript / bsl / missing*)搜尋 0 筆,無法證實;拼法(兩個 m)可疑 | `grep -i menuclick` 於 `fetched/` 官方頁只命中本範例 | 以除錯視窗(Level 1)實際觸發的事件名為準 |
| 3 | 手冊已記錄 | 全頁 | `windowevent` 用法與 NOTE 矛盾 | 手冊 2.2 第 585 行 | — |

### e-close-all-documents.htm(E: Close all documents)

| # | 等級 | 位置 | 問題 | 證據 | 建議 |
|---|---|---|---|---|---|
| 1 | 與官方不一致 | 迴圈內 `vdoc.close` | 只呼叫 `close`,沒有 `update` | 官方 `missing_b/di-api-close.htm`:`d.close` 之後「// update」`d.update`,再 `destroy d` | 實測;若無效,依官方加 `vdoc.update` |
| 2 | 與官方不一致 | `vdoc.getbykey=<r.docentry>` | 以 `r.select "ORDR"."DocEntry" ...` 建立 datastore,欄位名可能是 `ordr_docentry` 而非 `docentry` | `beasscript/dtw.htm`:`lds=select "OITM"."ItemCode","OWHS"."WhsName" ...` 會「create fields oitm_itemcode and owhs_whsname」;只有不帶表名時(`beasscript/bs-sql.htm` 的 `d.select "OnHand"` → `d.onhand`)才是單純欄位名 | 改用 `select "DocEntry" from "ORDR" ...` 或讀 `<r.ordr_docentry>`,實測 |
| 3 | 存疑 | `vdoc.getbusinessobject=17;` | 行尾多了 `;`,BeasScript 指令不以分號結尾,值可能變成 `17;` | 同頁其他行與官方 `di-api-getbusinessobject.htm`(`d.getbusinessobject=oOrders`)都沒有分號 | 移除分號 |

### e-get-price-from-item.htm(E: Get Price from Item)

| # | 等級 | 位置 | 問題 | 證據 | 建議 |
|---|---|---|---|---|---|
| 1 | 手冊已記錄 | DI-API 段 | `destroy=itemprice` 出現兩次(官方 `missing/diapi_itemprice.htm` 同樣如此) | 手冊第 2288 行 | — |
| 2 | 手冊已記錄 | 最後一行 `messagebox=&ltb.price>` | 原始 HTML 為 `=&amp;ltb.price&gt;`,顯示成 `&ltb.price>`,不是佔位符 | 手冊第 2288 行;`fetched/examples/e-get-price-from-item.htm` 第 205 行原始碼 | 應為 `<b.price>` |
| 3 | 手冊已記錄 | `b.get=Item(<itemcode>,<whscode>,<cardcode>,<quantity>,"K")` | 少了 `/GetPrice`,字串參數也沒用 `,bsl` 格式 | 手冊第 2288 行;官方 `bsl/bslitemgetgetprice.htm`:`b.get=Item/GetPrice(<itemcode,bsl>,<whscode,bsl>,<cardcode,bsl>,<quantity>,"K")` | 依官方頁寫法 |

### e-print-with-crystal.htm(E: Print with Crystal)

| # | 等級 | 位置 | 問題 | 證據 | 建議 |
|---|---|---|---|---|---|
| 1 | 與官方不一致 | 第 1 段 `printobject=crreport=selectprinter=<value_printerdriver>` | 只傳了 driver(第 1 個參數),沒有印表機名稱 | `beasscript/crystal-crreport.htm`:`selectprinter=[printerdriver]=[printername]=[port]`,「You need only the printer name」,短語法 `selectprinter==[printername]`;`beasscript/printer.htm` 範例 `printobject=selectprinter=<value_printerdriver>=<value_printername>=<value_printerport>` | 改為三個參數或 `selectprinter==<value_printername>` |
| 2 | 與官方不一致 | 第 2 段 `printobject=copies=2` | 少了 `crreport=` | `beasscript/crystal-crreport.htm`:`printobject=crreport=copies=3` | 改為 `printobject=crreport=copies=2` 並實測 |
| 3 | 存疑 | 第 3 段最後兩行 | 註解「// and export」放在 `printobject=crreport=printout`(列印)之前,實際匯出在前一行 `export=true`;註解與程式不對應 | 原文:`printobject=crreport=export=true` / `// and export` / `printobject=crreport=printout`;`crystal-crreport.htm`:`export` = save/export,`printout` = print the document | 調整註解(先匯出、再列印) |

### e-print-work-order.htm(E: Print Work order)

| # | 等級 | 位置 | 問題 | 證據 | 建議 |
|---|---|---|---|---|---|
| 1 | 與官方不一致 | `global function dw_master_item_button_printworkorder_click` | 同 add_a_button 第 1 項:`create=button` 的事件名是否帶 `button_`,官方兩頁不同 | `beasscript/bsbuttoncreate.htm` vs 手冊第 3137 行視窗事件表 | 實測 |
| 2 | 存疑 | B1UP 巨集 `cast(... as varchar(20))+'\|'+cast(...)` | 用 `+` 串接字串、`top 1`,是 MSSQL 寫法,HANA 上 `+` 不能串字串 | `beasscript/sqlruntimeconverter.htm`:「use "\|\|" if you add a string, but use "+" for adding numbers」(此規則針對 Beas 的 SQL 轉換器;B1UP 巨集的 SQL 是否經過轉換,官方未說明) | HANA 環境改用 `\|\|` 或 `concat` |
| 3 | 存疑 | B1UP `BeasFunction(PrintWorkorder\|@store1)`、Pool List `beasfunction=PrintWorkorder=<str_parm.belnr_id>=<str_parm.belpos_id>` | 標題是「Print」,但沒有傳 `print` 參數,依官方是預覽 | `missing_b/beasfunctionprintworkorder.htm`:「print = emtpy/not defined=view, print=print to printer」 | 要直接列印就加 `\|print` / `=print` |

### e-purchase-order.htm(E: Purchase order)

| # | 等級 | 位置 | 問題 | 證據 | 建議 |
|---|---|---|---|---|---|
| 1 | 存疑 | `if <di_doc.ret_code> = -1 then return failure` | 失敗時直接 return,沒有先 `destroy di_doc` | `missing_b/api_sbo.htm`:「After using the DI-API object: Destroy it! Otherwise it can generate subsequent errors.」 | return 前先 destroy |

### example-bsl-get-receipt-qty.htm(E: BSL: Get receipt quantity)

| # | 等級 | 位置 | 問題 | 證據 | 建議 |
|---|---|---|---|---|---|
| 1 | 與官方不一致 | `<bsl.WorkorderPos([ll_DocEntry]\|[ll_LineNumber]).QuantityProduced/$Value>` | 官方同一段範例用 `/QuantityProduced/$value`;本頁用 `.` 與 `$Value` | `beasscript/bs-bsl.htm` 有一字不差的同一段範例,唯一差別是 `/QuantityProduced/$value` | 依官方改用 `/` |
| 2 | 存疑 | `instance bsl b` | 宣告了 `b` 但沒有使用,也沒有 destroy(本例只用佔位符) | 原文全段無 `b.` 呼叫 | 刪除或補 `destroy=b` |

### example-fieldchanged.htm(E: Field changed, Dropdown)

| # | 等級 | 位置 | 問題 | 證據 | 建議 |
|---|---|---|---|---|---|
| 1 | 手冊已記錄 | `form_loaded` 以 `end event` 結束;`dw_1_item_u_match_itemchanged` 缺 `end global` | 結尾不成對 | 手冊第 392 行 | — |
| 2 | 與官方不一致 | `dw_1."ITEM"."U_beas_din"."DROPDOWN".select ...` | 物件路徑的關鍵字被加上雙引號並轉大寫 | `beasscript/bsdropdownselect.htm`:`dw_1.item.udf1.dropdown.select "OITM"."ItemCode" from ...` | 改為 `dw_1.item.<欄位>.dropdown.select ...` |
| 3 | 存疑 | 下拉選單欄位 vs itemchanged 欄位 | 下拉建在 `U_beas_din`,但事件與賦值用 `u_din`(`dw_1_item_u_din_itemchanged`、`dw_1.item.u_din.value`),同一範例內視窗欄位名稱不一致 | 原文兩處;同頁註解只說 SQL 裡的 UDF 名稱會自動轉換 | 確認 dw_1 的實際欄位名後統一 |
| 4 | 確定錯誤 | `messagebox=error$**$The match code "<dw_1.item.u_match.value> is current in use. ...` | 訊息文字中雙引號共 5 個,不成對(`"<dw_1.item.u_match.value>` 沒有收尾引號);只影響顯示內容 | 原文該行 | 補上 `"` |
| 5 | 存疑 | 同上 `error$**$` | `$**$` 形式不在官方 messagebox 語法中 | `beasscript/meldung2.htm`:`messagebox=<typ>$$<text>` 或 `messagebox=<typ>$id=..<tab>text=..`(`**` 只出現在 `id=**`) | 改用 `error$$...` 或 `error$id=**<tab>text=...` |

### example-item-structure-tool-tip.htm(E: Item Structure tool tip)

| # | 等級 | 位置 | 問題 | 證據 | 建議 |
|---|---|---|---|---|---|
| 1 | 存疑 | `dw_1.item.info_t.text=<itemcode><cr_lf><itemname><cr_lf><sqlca.result.1>` | 同一函式的 SQL 用 `<dw_1.item.itemcode.value,dbstring>`,文字卻用 `<itemcode>`、`<itemname>`;`<itemcode>` 是否等於被點擊那一列的值,頁面沒有說明 | 原文兩行 | 統一用 `<dw_1.item.itemcode.value>` / `<dw_1.item.itemname.value>` |

### example-print-work-order-automatically.htm(E: Print Work order automatically)

| # | 等級 | 位置 | 問題 | 證據 | 建議 |
|---|---|---|---|---|---|
| 1 | 手冊已記錄 | `<e_WoLineNum>` | 官方事件變數是 `e_WoLineNumber` | 手冊第 2072 行;`missing/ue_workordermanagement_createworkorderpositioncomplete.htm` 變數表 | — |
| 2 | 手冊已記錄 | 全函式 | 沒有檢查 `e_Refresh` | 手冊第 2074 行 | — |
| 3 | 存疑 | `// preview work order` / `beasfunction=PrintWorkorder=<e_WoDocEntry>=<e_line>` | 頁面說明「the system has to print directly」,程式只做預覽(沒有 `print` 參數) | `missing_b/beasfunctionprintworkorder.htm`:不帶 print = view | 要直接列印就加 `=print` |

### example_add_button_for_print.htm(E: Add Button for Print)

| # | 等級 | 位置 | 問題 | 證據 | 建議 |
|---|---|---|---|---|---|
| 1 | 與官方不一致 | `// Create button on right side` / `create=button=name=MyPrint,text=Print,position=1` | 註解說右側,官方定義 `position=1` 是左側 | `beasscript/bsbuttoncreate.htm`:「position 0=bottom, 1=left, 2=top」,範例中 `position=1` 標在「// Left」下;但同頁開頭又說按鈕區域是「Top, right and bottom」,9.3 PL2 起可寫 `position=right`(官方自身不一致) | 實測;要右側可改 `position=right` |
| 2 | 與官方不一致 | `global function dw_master_item_myprint_click` | 與 `bsbuttoncreate.htm` 一致,但與同章其他範例(`dw_master_item_button_..._click`)及視窗事件表不同 | 見 add_a_button 第 1 項 | 實測 |

### example_batch-number-reservation.htm(E: Batch number reservation)

| # | 等級 | 位置 | 問題 | 證據 | 建議 |
|---|---|---|---|---|---|
| 1 | 確定錯誤 | 說明文字 `workordermangement.GetBatchNumber` | 事件模組名拼錯(少 a),官方為 `workordermanagement` | 官方頁 `missing/usereventworkordermanagement.htm`、`missing_b/ue_workordermanagement_getbatchnumber.htm`;同頁程式碼的檔名寫 `workordermanagement.src`(正確) | 改為 `workordermanagement` |
| 2 | 存疑 | `setvar=e_DistNumber=<e_DocEntry><e_LineNumber><today,yyyymmdd><now,hhmm>` | 說明寫「Work order Document Entry + Current Date yyyymmddhhmm」,程式還多串了 `<e_LineNumber>` | 原文說明與程式 | 修正說明或程式 |
| 3 | 存疑 | `function CreateWorkOrderPositionEnd` | 沒有檢查 `e_Refresh`;更新模式下工單是暫時、未存檔的,仍會執行 `update "BEAS_FTPOS"` | `missing_b/ue_workordermanagement_createworkorderpositionend.htm`:`e_Refresh` true = refresh mode,「this work order is not saved in the database」 | 開頭加 `if <e_Refresh> = true then return success` 並實測 |

### example_change_wms_prodline.htm(E: Change WMS Production line)

| # | 等級 | 位置 | 問題 | 證據 | 建議 |
|---|---|---|---|---|---|
| 1 | 手冊已記錄 | `"BEAS_APLATZ"."WMS_PRODLINE"` | PDF 中 `BEAS_APLATZ` 沒有此欄位 | 手冊第 1059、3067 行 | — |
| 2 | 與官方不一致 | `// we want to check only WMS Warehouse (typ 3)` / `if <ls_binType> <> 3 then return success` | 官方 `WhsBinType` 的 3 是 Produmex 且「current not supported」,資料庫運算式只會回 0/1/2;依官方定義此條件永遠成立,函式永遠提前結束 | `bsl/warehouseget.htm`:「0=None 1=beas 2=SAP 3=Produmex (current not supported)」,`Database: case when ... then 1 when "OWHS"."BinActivat" = 'Y' then 2 else 0 end` | 確認 WMS 倉庫在新版 BSL 的實際值後修改 |
| 3 | 存疑 | 全函式 | 同 batch-number-reservation 第 3 項,沒有檢查 `e_Refresh` 就 `update "BEAS_FTPOS"` | `missing_b/ue_workordermanagement_createworkorderpositionend.htm` | 同上 |

### example_check_max_qty.htm(E: Manual Issue: Check Max. Quantity)

| # | 等級 | 位置 | 問題 | 證據 | 建議 |
|---|---|---|---|---|---|
| 1 | 手冊已記錄 | 第一個 `if ... then` | 缺 `end if`(2 個 `if`、1 個 `end if`) | 手冊第 1501 行 | — |
| 2 | 手冊已記錄(補充) | `CheckMinMaxBom` 第 4 個參數 `<dw_1.item.pos_id.value>:<dw_1.item.menge.value,dbnum>` | 手冊視為筆誤;但官方格式就是 `"pos_id:quantity"`,內容其實符合,只是沒有加引號(官方註明 defined as string,另兩頁範例用 `""`) | `bsl/bslworkorderbomcheckminmax2.htm`:「IssueQuantityString ... "pos_id:quantity,pos_id:quantity" Note: defined as string」 | 加上雙引號後實測;手冊第 1501 行可補充此點 |
| 3 | 與官方不一致 | `if <b.Status> = locked then` | 官方狀態值是 `blocked` | `bsl/bslworkorderbomcheckminmax2.htm`:「ok, question, blocked」 | 改比對 `blocked` |

### example_close_work_orders.htm(E: Close work orders)

| # | 等級 | 位置 | 問題 | 證據 | 建議 |
|---|---|---|---|---|---|
| 1 | 手冊已記錄(補充) | `closeworkorderpos` 中 `b.get=Workorder(<ll_DocEntry,#0>)/Closed/$value` | 檢查的是工單層級,不是位置層級。官方 WorkorderPos 本身就有 `Closed` 欄位 | 手冊第 1325 行;`bsl/bslworkorderposget.htm`:`Closed Edm.Boolean`(工單或位置任一結案即為 1) | 改用 `WorkorderPos(<ll_DocEntry,#0>,<ll_LineNumber,#0>)/Closed/$value` |
| 2 | 手冊已記錄 | `function closeworkorder` / `closeworkorderpos` 寫在 `global function ... end global` 內 | 巢狀結構鬆散 | 手冊第 1325 行 | — |
| 3 | 與官方不一致 | `if <b.ret_status> n= -1 then`(兩處) | 官方沒有 `ret_status` 屬性;Close 回傳的欄位名是 `status` | `bsl/bslworkorderclose.htm`、`bsl/bslworkorderposclose.htm` 回傳 `{"DocEntry":..,"status": -1, "error": ...}`;`beasscript/bsue_bsl.htm`:`<b.[fieldname]>` 讀欄位;`ret_status` 在所有官方頁面搜尋 0 筆(手冊第 3035 行速查表列了 `b.ret_status`,無出處) | 改為 `<b.status>` 並實測 |
| 4 | 與官方不一致 | `if <b.ret_code> = -1 then`(兩處) | BSL 物件的 `ret_code` 官方定義 0 = ok、400/404/其他 = 錯誤,不是 -1 | `beasscript/bs-bsl.htm`:「<b.ret_code> -> 0 = ok, 400 = Syntax error, 404 = not found, all other messages are error messages」(但 `bsl/bslworkorder.htm` 官方範例也用 `= -1`,官方頁面互相不一致) | 改為 `<b.ret_code> n<> 0` 並實測 |
| 5 | 存疑 | 迴圈內 `if <dw_1.item.zeilentyp.value> n= 0` / `n= 10` | 迴圈以 `ll_row` 處理選取列,但 `zeilentyp` 沒加列號,讀的是目前列;同函式其他欄位都用 `:[ll_row]` | 原文 `<dw_1.item.belnr_id:[ll_row].value>`;`beasscript/dw_1.htm`:`<dw_1.isselect:[ll_loop]>` 列號寫法 | 改為 `<dw_1.item.zeilentyp:[ll_row].value>` |
| 6 | 存疑 | `if <b.ret_value> = 1 then`(Workorder/Closed) | 註解說 1 或 0,但官方 Workorder 的 `Closed` 欄位寫 Edm.Int32 卻列「Yes=J No=N」 | `bsl/bslworkorderget.htm`:`Closed Edm.Int32 ... Default Value: N Constraints: Yes=J No=N` | 實測回傳值 |
| 7 | 與官方不一致 | `create=button=name=CloseOrder,...,position=1`、事件 `dw_master_item_closeorder_click` | 同 add_button_for_print 第 1、2 項(position=1 官方為 left;事件名是否帶 `button_`) | `beasscript/bsbuttoncreate.htm` | 實測 |
| 8 | 存疑 | 說明 vs 程式 | 說明寫「close the current and all marked work orders」,程式只處理 `isselect` 為 true 的列 | 原文說明與 `if <dw_1.isselect:[ll_row]> = true` | 修正說明或補處理目前列 |

### example_configuration-wizzard.htm(E: Configuration Wizard example)

| # | 等級 | 位置 | 問題 | 證據 | 建議 |
|---|---|---|---|---|---|
| 1 | 確定錯誤 | 「File system_opionen.ini in project folder」 | 設定檔名拼錯(少 t) | `missing/custom_config_wizzard.htm`:「Example "system_optionen.ini" for standard Configuration wizard」、`s_parm1=system_optionen.ini` | 改為 `system_optionen.ini` |
| 2 | 手冊已記錄 | `2→G→helptext=...→T→Count of days→demodays=0` | `demodays=0` 的意義官方未說明 | 手冊第 1795 行 | — |

### example_di-api_goodspofromorder.htm(E: Goods Receipt Po on Purchase order)

| # | 等級 | 位置 | 問題 | 證據 | 建議 |
|---|---|---|---|---|---|
| 1 | 存疑 | `return failure` | 失敗時沒有先 destroy | `missing_b/api_sbo.htm`「After using the DI-API object: Destroy it!」 | return 前 destroy |

### example_goods-receipt-with-uf.htm(E: Goods Receipt with UF)

| # | 等級 | 位置 | 問題 | 證據 | 建議 |
|---|---|---|---|---|---|
| 1 | 手冊已記錄 | `<di_get.getnewobjectkey>` | 物件名應為 `di_doc` | 手冊第 2197 行 | — |
| 2 | 手冊已記錄 | `di_doc.lines=userfield=U_test2=N` | 官方找不到 `userfield=` 寫法 | 手冊第 2199 行 | — |
| 3 | 存疑 | `return failure` | 失敗時沒有先 destroy | `missing_b/api_sbo.htm` | return 前 destroy |

### example_inventory_from_request.htm(E: Inventory transfer with batchnum)

| # | 等級 | 位置 | 問題 | 證據 | 建議 |
|---|---|---|---|---|---|
| 1 | 手冊已記錄 | `lines=batchnumber=batchnum=...` | `batchnum` 屬性與 batchnumber/batchnumbers 單複數官方未證實 | 手冊第 2161、2163 行 | — |
| 2 | 存疑 | 調撥單 `di_doc=lines=fromwhscode=01` | 官方只列出表頭屬性 `fromwarehouse`(僅限調撥),行屬性表沒有 `fromwhscode` | `docs/ref/API_物件.md` document 頁:「fromwarehouse | 僅限調撥(transfer):來源倉庫」;line 屬性表無 fromwhscode(但 `api_sbo.htm` 說可存取幾乎所有屬性,不做檢查) | 實測;不行就改用表頭 `di_doc=fromwarehouse=01` |
| 3 | 存疑 | 第一段 `return -1`、第二段 `return failure` | 失敗時沒有先 destroy | `missing_b/api_sbo.htm` | return 前 destroy |

### example_manreceiptcheckminmax.htm(E: Manual Receipt: Check Min. and Max. Quantity)

| # | 等級 | 位置 | 問題 | 證據 | 建議 |
|---|---|---|---|---|---|
| 1 | 與官方不一致 | `.QuantityProduced/$value` | 同 bsl-example-placeholder 第 1 項 | `beasscript/bs-bsl.htm` | 改用 `/` |
| 2 | 與官方不一致 | `if <b.Status> = locked then` | 官方狀態值是 `blocked` | `bsl/bslworkorderbomcheckminmax2.htm` | 改比對 `blocked` |
| 3 | 存疑 | 第一個 `if` | 註解「Bill of Material position must be defined」與條件 `pos_id n= 0` 相反 | 原文 | 修正註解 |
| 4 | 存疑 | 說明「execute the check with BSL WorkorderBoM/CheckMinMax」 | 程式呼叫的是 `CheckMinMaxBom`;官方另有一個 CheckMinMax 頁,實為 `CheckMinMaxRouting`,是不同函式 | `bsl/bslworkorderbomcheckminmax.htm`(標題 CheckMinMaxRouting)vs `bsl/bslworkorderbomcheckminmax2.htm`(CheckMinMaxBom) | 說明改為 `CheckMinMaxBom` |

### example_opensalesorder.htm(E: Open Sales Order from Workorder)

| # | 等級 | 位置 | 問題 | 證據 | 建議 |
|---|---|---|---|---|---|
| 1 | 手冊已記錄 | 說明「the system opens the linked Work order」 | 程式實際開啟銷售訂單(`openwindow=fo_Order=...`) | 手冊第 1564 行 | — |

### example_print_work_order.htm(E: Print Work order (4 Variants))

本頁內容與 `e-print-work-order.htm` 相同,問題也相同。

| # | 等級 | 位置 | 問題 | 證據 | 建議 |
|---|---|---|---|---|---|
| 1 | 與官方不一致 | `dw_master_item_button_printworkorder_click` | 同 e-print-work-order 第 1 項 | `beasscript/bsbuttoncreate.htm` | 實測 |
| 2 | 存疑 | B1UP 巨集 SQL 的 `+` 字串串接 | 同 e-print-work-order 第 2 項 | `beasscript/sqlruntimeconverter.htm` | 同上 |
| 3 | 存疑 | `BeasFunction(PrintWorkorder\|@store1)`、`beasfunction=PrintWorkorder=..=..` | 同 e-print-work-order 第 3 項(沒有 print 參數 = 預覽) | `missing_b/beasfunctionprintworkorder.htm` | 同上 |

### example_qc_-set-scrap-automatically.htm(E: Set Scrap automatically)

| # | 等級 | 位置 | 問題 | 證據 | 建議 |
|---|---|---|---|---|---|
| 1 | 手冊已記錄 | `Humidty`;註解「value not 9」與條件 `n> 0` | 拼法與說明不同;註解與條件不同 | 手冊第 1659 行 | — |

### example_serialnumberbywo.htm(E: Create Wo Serial/Batch Numbers)

| # | 等級 | 位置 | 問題 | 證據 | 建議 |
|---|---|---|---|---|---|
| 1 | 手冊已記錄 | 範例 3 註解沿用範例 2(ZPT/G/C);範例 4 `sql=update "BEAS_FTHAUPT" set "UDF4"=...` 沒有 where | 註解錯誤;update 會更新全部工單 | 手冊第 964 行 | — |
| 2 | 確定錯誤 | 說明「workordermangement.GetBatchNumber and workordermangement.GetSerialNumber」 | 事件模組名拼錯,官方為 `workordermanagement` | `missing_b/ue_workordermanagement_getbatchnumber.htm`、`missing/usereventworkordermanagement.htm` | 改為 `workordermanagement` |
| 3 | 與官方不一致 | 範例 2 `<system.counter.boy_counter_[boy_counter]>` | 官方計數器語法用 `=`,不是 `.` | `beasscript/system.htm`:「<system.counter=[value]> creates and reads a counter」、`messagebox=<system.counter=mycounter1>` | 改為 `<system.counter=boy_counter_[boy_counter]>` 並實測 |
| 4 | 存疑 | 範例 3 `// for all other use a item based counter` | 註解說依品項計數,程式卻是與範例 1 相同的「每日」計數(`left("DistNumber",9) = '<today,yyyymmdd>-'`),與品項無關 | 原文範例 1 與範例 3 的 SQL 相同 | 修正註解或加上品項條件 |
| 5 | 存疑 | 範例 3 `GetSerialNumber` 從 `"OBTN"` 取最大號 | 序號應在序號表;官方 BSL 的資料來源把批號放 OBTN、序號放 OSRN | `bsl/bslwarehousestock.htm`:`Batch ... coalesce("OBTN"."DistNumber","OSRN"."DistNumber",'')` | 序號改查 `"OSRN"`,實測 |
| 6 | 存疑 | 範例 1、3 `coalesce(max(substring("DistNumber",10,4)),0)` | `max(substring(...))` 是字串,`0` 是數字,`coalesce` 型別混用,在 HANA 上可能出錯 | 原文;`beasscript/sqlruntimeconverter.htm` 要求以 HANA 語法撰寫 | 改為 `coalesce(max(substring(...)),'0')` 或先 cast |
| 7 | 存疑 | 範例 4 `set "UDF4"='<ls_counter,#0>'` | 手動加單引號,沒有用 `<…,dbstring>` | `beasscript/sqlca_update.htm`:「if you work with placeholders, you must convert them to the correct format」;`bs-sql.htm` 字串一律用 `,dbstring` | 改為 `<ls_counter,dbstring>`(若需先格式化為整數,先 setvar 再 dbstring) |

### example_timereceipt-rules.htm(E: TimeReceipt rules)

| # | 等級 | 位置 | 問題 | 證據 | 建議 |
|---|---|---|---|---|---|
| 1 | 存疑 | 說明第一句 | 寫「do not allow ... if no material is booked」,程式與後文是「未完全發料就不允許」 | 原文說明 vs `$filter=... QuantityWhsUoM gt QuantityBooked ...` 與後文「only if material is booked completely」 | 修正說明 |
| 2 | 存疑 | `setvar=ls_html=...</br></br>...` | 用 `</br>`(結束標籤形式)換行 | `beasscript/meldung2.htm`:「Line Feed ... html: <br />」 | 改為 `<br />` |

### exampleafterreceipt.htm(E: after receipt)

| # | 等級 | 位置 | 問題 | 證據 | 建議 |
|---|---|---|---|---|---|
| 1 | 手冊已記錄 | 範例 1 註解「change the Batch status to 0」vs `{"Status":"1"}` | 註解與程式不同 | 手冊第 826 行 | — |
| 2 | 手冊已記錄 | 範例 2 `e_belnr_id`、`e_belpos_id` | 官方事件變數是 `e_WoDocEntry`、`e_WoLineNumber` | 手冊第 828 行 | — |
| 3 | 確定錯誤 | 說明 `workordermangement.receiptend` | 模組名拼錯,官方為 `workordermanagement` | `missing/ue_workordermanagement_receiptend.htm` | 改為 `workordermanagement` |
| 4 | 與官方不一致 | 說明 `materialmangement.receipt`、`materialmanagement.receiptend`;程式用 `<e_ReceiptDocEntry>` | 官方沒有 `materialmanagement` 模組,對應的是 `stockmanagement`;且 `e_ReceiptDocEntry` 只存在於 workordermanagement.ReceiptEnd,stockmanagement.ReceiptEnd 的變數是 `e_DocEntry` | `missing/ue_stockmanagement_receiptend.htm`(變數 e_DocEntry、e_DocNumber、e_Transaction);`missing/ue_workordermanagement_receiptend.htm`(e_ReceiptDocEntry);官方 userevent 頁只有 `usereventstockmanagement` | 說明改為 stockmanagement;若用在 stockmanagement 須改用 `e_DocEntry` |
| 5 | 與官方不一致 | `if <b.ret_code> <> 1 then`(兩段) | `bs-bsl.htm` 定義 `ret_code` 0 = ok;照此定義,成功時也會顯示錯誤訊息。但官方 `bslbatchnumberput.htm` 範例同樣寫 `<> 1`,官方頁面互相矛盾 | `beasscript/bs-bsl.htm`:「0 = ok, 400 = Syntax error, 404 = not found」;`bsl/bslbatchnumberput.htm` 範例 `if <b.ret_code> <> 1` | 實測 PUT 成功時的 ret_code |
| 6 | 存疑 | `dtw=(v2)select "IBT1"."ItemCode","OBTN"."SysNumber" ...` 後讀 `<dtw.itemcode>`、`<dtw.sysnumber>` | 帶表名、未用 `as` 的欄位在 (v2) 下的欄位名官方說明不清 | `beasscript/dtw.htm`:(v2) 範例「creates the following fields: myfield and owhs_whscode」(未加 as 的欄位仍帶表名前綴) | 加 `as "ItemCode"`、`as "SysNumber"` 或實測欄位名 |
| 7 | 存疑 | 說明「After receipt process - change Batch Status - print report」 | 兩段程式都沒有列印 | 原文 | 修正說明或補列印範例 |

### examplechoosebox.htm(E: ChooseBox)

| # | 等級 | 位置 | 問題 | 證據 | 建議 |
|---|---|---|---|---|---|
| 1 | 與官方不一致 | `tools=choosebox=title=my ChooseBox/&` 等 4 行 | 用 `&` 換行;官方 choosebox 頁明說不允許 | `beasscript/auswahl.htm`:「// NOTE: You must write all in one line. & Signs are not allowed」(本範例與官方 `missing_b/custom_choosebox.htm` 相同,官方頁面互相矛盾) | 實測;不行就寫成一行 |
| 2 | 與官方不一致 | `title=my ChooseBox` | 官方標題關鍵字是 `titel` | `beasscript/auswahl.htm`:「special is "titel". With this key-word you can set the title」,範例 `tools=choosebox=titel=Choose Window/`(同頁又寫「Example title=Hello world」,官方自身不一致) | 實測 `titel` / `title` |

### examplecleanstock.htm(E: Clean stock,JBS / JavaScript)

| # | 等級 | 位置 | 問題 | 證據 | 建議 |
|---|---|---|---|---|---|
| 1 | 手冊已記錄 | `if (b.returnCode) != 0` | 語法錯誤 | 手冊第 710 行;`node --check` 結果:`SyntaxError: Unexpected token '!='`(第 36 行) | — |
| 2 | 手冊已記錄 | else 分支 `delete ... where "ItemCode"=:e_ItemCode` | 清全部品項時條件應為倉庫 | 手冊第 710 行 | — |
| 3 | 確定錯誤 | `let stock=bsl.get(...)` 寫在 `if {...}` 與 `else {...}` 區塊內 | `let` 是區塊範圍,離開區塊後 `stock` 不存在,下一行 `stock.rowcount()` 會拋 ReferenceError | 修正第 1 項語法後以 node 實際執行(mock `bsl`/`SQLStatement`):`ReferenceError stock is not defined`(腳本 `E_work/cs_test.js`)。此為標準 JavaScript 語意;JBS 引擎的作用域規則官方無文件 | 在 if 之前宣告 `let stock;`,區塊內改為 `stock=bsl.get(...)` |
| 4 | 確定錯誤 | `// ItemCode is empty: Load stock from all items ...` 與 `// Item is defined. Load only for this item` | 兩個註解位置互換:前者放在 `if (e_ItemCode != "")`(品項有值)之上,後者放在 `else`(品項為空)之上 | 原文程式碼 | 對調註解 |
| 5 | 與官方不一致 | `$filter=ItemCode = :e_ItemCode and OnHand > 0` | 官方 `$filter` 運算子沒有 `=`(等於是 `eq`;Beas 另支援 `<, >, >=, <=, <>`);字串值官方範例加引號 | `bsl/bslfilter.htm`:「eq Equal ... Note: Beas supports <, >, >=, <= and <> as well」,範例 `City eq 'Redmond'` | 改為 `ItemCode eq ':e_ItemCode'` 類寫法並實測 |
| 6 | 存疑 | JSON 鍵 `"Whscode"` | 官方欄位名是 `WhsCode`(大小寫不同);其餘鍵 `ItemCode`、`Quantity`、`SysNumber`、`DistNumber`、`BinCode`、`IVersionId` 與官方一致 | `bsl/bslissuepost.htm`、`bsl/bslissue.htm` 範例 `"WhsCode":"01"` | 改為 `"WhsCode"` |
| 7 | 存疑 | `alert("deleteStock(): Please define warehouse")` | 訊息中的函式名與實際函式 `cleanStock` 不同 | 原文 | 改為 `cleanStock()` |

其他:以 mock 資料執行(修正 1、3 項後)組出的 JSON 字串可被 `JSON.parse` 解析(`E_work/cs_json.js`)。

### examplecreateworkorder.htm(E: Create work order)

| # | 等級 | 位置 | 問題 | 證據 | 建議 |
|---|---|---|---|---|---|
| 1 | 與官方不一致 | `if <b.ret_code> = -1 then messagebox=Error create work order : <b.ret_value>` | `ret_code` 官方定義 0 = ok、400/404/其他 = 錯誤;錯誤訊息應在 `ret_text`。本範例與官方 `bsl/bslworkorder.htm` 的範例一字不差,官方頁面互相不一致 | `beasscript/bs-bsl.htm`:「<b.ret_code> -> 0 = ok, 400 ..., 404 ...」「<b.ret_text> = Error message」 | 改為 `if <b.ret_code> n<> 0` 並顯示 `<b.ret_text>`,實測 |
| 2 | 與官方不一致 | JSON 鍵 `"WorkorderPos"` | Workorder POST 屬性表定義工單位置的屬性名是 `Line`,沒有 `WorkorderPos`;但 `bslworkorder.htm`、`bslasynch.htm` 範例都用 `WorkorderPos` | `bsl/bslworkorderpost.htm`:「Line String BEAS_FTPOS.* Define Work order positions (the assemblies) as object-Array [{...}]」 | 實測兩種寫法(JSON 本身可解析) |

### validate_field.htm(E: Validate field)

| # | 等級 | 位置 | 問題 | 證據 | 建議 |
|---|---|---|---|---|---|
| 1 | 手冊已記錄 | 訊息 `Lenght` | 拼字(僅訊息文字) | 手冊第 161 行 | — |
| 2 | 存疑 | 說明「validated on change」vs 程式 `global function dw_1_update` | 開頭說欄位變更時驗證,程式是在更新(Update)時驗證,且只顯示訊息、沒有 `return -1` 阻止更新 | 原文說明;步驟 4 與結尾說明則寫 update | 統一說明;若要阻止存檔,加 `return -1` 並實測 |

---

## 無問題的頁面

含程式碼、檢查後未發現問題:

- `creating_a_popup_message.htm`(`messagebox=Hello World!`,符合 `meldung2.htm` 簡易語法)
- `e-change-item-pricelist.htm`(與官方 `items_prices.htm` 範例相同)
- `e-working-with-journal-entries.htm`
- `example-bsl-receipt.htm`(JSON 代入樣本值後可解析;與官方 `bsl/bslreceipt.htm` 範例相同)
- `example_diapi-bbcatnum.htm`(物件 73 的屬性官方沒有對應頁面,見「未能檢查的項目」)
- `example_onlydisplay.htm`(`displayonly` 見 `missing/dw-column-edit.htm`)
- `example_udf_beas.htm`

沒有程式碼(目錄頁、只有說明或只有截圖):

- `accessing_item_values.htm`(程式碼只在截圖中)
- `e-print-functions.htm`、`e_window_beas.htm`、`example-bsl.htm`、`example-quality-control.htm`、`example_connectors.htm`、`example_di_api.htm`、`example_tools.htm`、`examplecreatewofromownwindow.htm`(原文只有概述與截圖)、`examplenote.htm`、`exampleproduction.htm`、`examples.htm`、`exampletransactions.htm`、`window-sap.htm`

---

## 總結

1. **確定錯誤(9 項)**,大多是名稱前後不一致或成對問題:
   - `create_button`:按鈕名 `mybutton`,點擊事件卻攔 `beas_lyt`。
   - `create_right_click_entry`:`<spr:My Entry` 角括號沒有閉合。
   - `examplecleanstock`:`let stock` 的區塊作用域會讓 `stock.rowcount()` 拋 ReferenceError(node 實測);兩個註解位置互換。
   - `example-fieldchanged`:訊息文字雙引號不成對。
   - `example_configuration-wizzard`:`system_opionen.ini` 應為 `system_optionen.ini`。
   - `example_serialnumberbywo`、`example_batch-number-reservation`、`exampleafterreceipt`:說明中的模組名 `workordermangement` 應為 `workordermanagement`。
2. **反覆出現的「與官方不一致」**,建議以實測決定後統一修正:
   - BSL `ret_code` 的成功值:`bs-bsl.htm` 寫 0 = ok,但 `bslworkorder.htm`、`bslbatchnumberput.htm` 等官方範例用 `= -1` / `<> 1`(影響 examplecreateworkorder、example_close_work_orders、exampleafterreceipt)。
   - `CheckMinMaxBom` 的狀態值:範例比 `locked`,官方是 `blocked`(3 頁)。
   - BSL 佔位符路徑用 `.QuantityProduced`,官方是 `/QuantityProduced`(3 頁)。
   - 按鈕事件名要不要帶 `button_`、`position=1` 是左還是右:官方 `bsbuttoncreate.htm` 與視窗事件表不同(5 頁)。
   - `example_close_work_orders` 的 `<b.ret_status>` 在官方中不存在(應為 `status`)。手冊第 3035 行速查表也列了 `b.ret_status`,建議一併修正。
3. **手冊已記錄的 25 項**都確認仍存在。其中兩項另有補充:
   - `example_check_max_qty` 的第 4 個參數其實符合官方 `"pos_id:quantity"` 格式,只缺引號。
   - `example_close_work_orders` 可改用 `WorkorderPos(...)/Closed`。
4. JSON 片段全部可解析。JavaScript 只有 `examplecleanstock` 一段:有 1 個語法錯誤(手冊已記錄)和 1 個執行期作用域錯誤(新發現)。

## 未能檢查的項目

- `accessing_item_values.htm`:讀寫欄位的程式碼只在截圖(`read_field_value.png`、`change_field_value.png`)中,沒有文字可檢查。
- `examplecreatewofromownwindow.htm`:原文頁面沒有程式碼(只有截圖 `examplemodalwindow.png` 與概述)。
- DI-API 屬性:官方 `API_物件.md` 沒有 Alternate Catalog Number(物件 73,`Substitute`)與 Journal Entries(`linememo`)的屬性頁,`getbusinessobject` 的數字代碼(17、20、22、59、67、73)也沒有官方對照表,無法核對。`api_sbo.htm` 說明可存取 SAP SDK 的幾乎所有屬性,且不做檢查。
- B1UP 巨集語法(`@STORE1=SQL(...)`、`$[$8.0.NUMBER]`、`IF(...) BEGIN ... END`)官方 Beas 文件中沒有語法頁,只核對了 BeasFunction 參數。
- JBS(`#jbs`、`#include`、`bsl.get`、`bsl.post` 的 `returnCode/returnMessage/returnResult`、`SQLStatement.execute`、字串中的 `:變數` 代入)在官方頁面沒有說明,只能以標準 JavaScript 語意與 node 檢查。
- SAP 標準資料表欄位(`OBTN`、`IBT1`、`OITM.QryGroup9`、`OITM.U_beas_din` 等)不在 `database.txt`(Beas 資料表)範圍內。Beas 資料表欄位已核對:`BEAS_FTPOS.BaseDocNum/DocEntry/CHARGE_ID/UDF2/WMS_PRODLINE`、`BEAS_FTAPL.SortId/APLATZ_ID`、`BEAS_FTHAUPT.UDF4` 等都存在。只有 `BEAS_APLATZ.WMS_PRODLINE` 不存在,手冊已記錄。
- 佔位符格式 `<now,hhmm>`、`<time,hhmmss>`、`<…,#.000000>`、`<…,bsl>` 的完整定義未在官方頁面找到(手冊第 3042 行已說明),未判定對錯。
- 無法實機執行 BeasScript,所有「與官方不一致 / 存疑」項目都需要在 Beas 環境實測確認。
