# fetched/ 文件範例檢查結果

檢查範圍:`fetched/beasscript/`(306 檔)、`fetched/bsl/`(344 檔)。行號為 HTML 原始檔行號。
標記「存疑」的項目不確定是否為錯誤,修改前須先對照原文判斷,不確定就不改。
HTML 原始檔中 `<` `>` `&` 以 `&lt;` `&gt;` `&amp;` 表示,修改時須維持轉義。

## 一、SQL / sqlca / filter(beasscript)

### sqlsetfilter.htm
- 186:`sqlfilter=abgkz='N'` → `sqlsetfilter=abgkz='N'`

### sqladdlike.htm
- 158:語法列 `sqllike=<Fieldname>` → `sqladdlike=<fieldname>`

### sqllike.htm
- 167:`like 'entered value>%'` → `'<entered value>%'`

### sql4filter.htm
- 161:`globa function form_preload` → `global function`

### sql2.htm
- 163:`beas_myfuction` → `beas_myfunction`(兩處,DROP 對象一致)
- 168、179:`return datetime` → `RETURNS datetime`
- 162–175:`[sql]` 與 `#sql` 的收尾符號混用,改為成對(`[sql]…[/sql]` 或 `#sql…#end`)

### sqlca-select.htm
- 169:`into ls_itemname,ls_itemcode` → `into ls_itemcode,ls_itemname`

### sqlca-into.htm
- 174:標示「or without ':'」的範例不應帶 `:` → `into ls_itemcode,ls_itemname`
- 225:`='ls_itemcode,dbstring>` → `=<ls_itemcode,dbstring>`
- 225、244:`correntstock` → `currentstock`
- 存疑 178 vs 190–193:`sum()` 結果加 `:#0` 格式與說明矛盾

### sqlca-format-placeholders.htm
- 196:`<itemcode,dbstring>` → `<ls_itemcode,dbstring>`
- 208:`dbnum(6)` → `num(6)`(bsl/bsluomconversion.htm:192 同)

### sqlca_getdate.htm
- 159:`<sqlca.today>` → `<sqlca.getdate>`
- 162:`today()` → `getdate()`

### sqlca_getwithnolock.htm
- 166:`withnolock()` → `WITH (NOLOCK)`
- 存疑 163:屬性名 `sqlca.withnolock` vs 頁名 getwithnolock

### sqlca_ishana.htm
- 157:說明「ms-sql server」→ HANA
- 164:see also 連回自己 → 改連 `sqlca.ismssql`

### sqlruntimeconverter.htm
- 182、185:`IfNull("ItemCode")`、`isnull(itemcode)` 補第二參數,例如 `IfNull("ItemCode",'')`
- 195:`select max(5) "ItemCode"` → `select top 5 "ItemCode"` 或 `… limit 5`
- 244:`'hello'||'world` 補結尾引號
- 246:`concat('hello'||'world')` → `concat('hello','world')`
- 252:`rpad(50)` → `rpad('',50)`
- 214:補 `from "BEAS_DUMMY"`
- 存疑 303:`dateadd(yy,getdate())` 少參數

### sqlscript.htm
- 180/184:`ld_today` 與 `<ld_date,dbdate>` 名稱統一
- 199:`BEAS_DUMMMY` → `BEAS_DUMMY`
- 存疑 190、171

### sqltransport.htm
- 198:`read=<table>=<file>` 不接受 where,範例 `=typ='beleg'` 應改用 `newread`
- 存疑:頁名 sqltransport vs 指令 `tools=sqltransfer`

### ifwert1operatorwert2.htm
- 248:`1 = Error` → `-1 = Error`
- 234:括號未閉合
- 272:`n=>` → `n>=`
- 存疑 221、234:`select 1 where` 無 FROM、`isnull` 為 MSSQL 專用

### ifwert1wert2.htm
- 211:範例缺 `if=<value1>=Peter=salutation()=you_not()`
- 243–249:V 對應 "Detailed" 但程式碼設為 "Compacted";245 缺 `!` → `[!setvar=…]`

### dropdown-sql-format.htm
- 174:缺 `(field=` 開頭
- 181、183:結尾多一個 `)`;183 多 `'`
- 246:`width formula` → `weight`

### SQL 注入與字串格式化
| 檔案:行號 | 修正 |
|---|---|
| beasscript/create_delivery.htm:243 | `where docnum=<col_docnum>` → `<col_docnum,num(0)>` |
| beasscript/parallel_connection.htm:195 | `'<mydata>'` → `<mydata,dbstring>` |
| beasscript/platzhalter.htm:177 | `'<myitem>'` → `<myitem,dbstring>` |
| beasscript/bs_string.htm:405、437 | 補 `update mytable set mycolumn=… where primarycol=<ls_id,dbstring>`;移除未使用的 `ls_id`(若確實未用) |
| 存疑 beasscript/del.htm:174、186、190 | 改用 dbstring / num |

## 二、BeasScript 一般範例(beasscript)

### 關鍵字、函式名
- accepttext.htm:171、globalfunction.htm:174,180、bsquestion.htm:289:`funktion` → `function`;accepttext 範例補 `end global`
- bsbatch.htm:180,183,188:`endglobal` → `end global`
- destroy.htm:176:`destroy=function helloworld` 與語法 `destroy=function=[name]` 一致化
- token.htm:161:語法與範例(`tools=token=…`)一致
- setsetup.htm:177:`<setup2=[variable]>` → 對照 setsetup2.htm:162 的 `<setup2:…>`

### 變數名、物件名不一致
- arrays.htm:175 `mycounter1` → `mycounter`;187 `myArray[myCounter)` 括號與大小寫
- crypt.htm:196 `cypt` → `crypt`
- ftp.htm:162–163 `lftp`/`lu_ftp` 統一;375 `lft.disconnect` → `lftp.disconnect`
- calendar.htm:205 `declare=ue_tab=etab` → `declare=etab=ue_tab`
- item_dw.htm:999 `<ls_values>` → `<ls_items>`
- setvar.htm:166 註解變數名改 ls_itemcode;170 `=>str_parm.s_parm2>` → `=<str_parm.s_parm2>`
- tracerreport.htm:165 第三參數 `[distnumber]` → `[sysnumber]`;272 `trancer_maxlevel` → `tracer_maxlevel`
- ue_produmex.htm:186,198,209 `proposal_picking_*` → `proposalpicking_*`
- window.htm:369–377 x/y 互換修正;389 `is_clickobjectname` → `is_clickobjecttype`
- system.htm:1751/1752 `artikel_speggew`/`artikel_spezgew` 統一;1306 `is_widnowuser` → `is_windowuser`;1586 補引號;1739–1740 說明與邏輯相反處對齊
- xml2array.htm:178 "First Name" → "First item"

### 語法錯誤
- for-next.htm:185–187:`sqlca.select … into dw_1.item.mycolumn "OITM"` 補 `from`;移除多餘 `next`
- print-with-crystal.htm:288–291 `for … end if` → 以 `next` 結束;264 `paremater_set` → `parameter_set`
- dtw.htm:690 運算子壞掉;468 `daastore` → `datastore`;474–475 物件改 `destination`;264–275 `owhs_whscode` → `owhs_whsname`
- bsdropdownscript.htm:164 → `dw_1.item.udf1.dropdown.script=`;175 補 `"`;186 `.script.modify=` → `.script=modify=`;242 補 `set=` 並去重 `<cr_lf>`;246 補 `&`
- bsdropdownfromfield.htm:163 `=mycolumn2` → `=mycolumn1`
- bsplaceholdermiscellaneous.htm:179 補 `)`
- crystal-crreport.htm:355、bsl/printservicepost.htm:391 `[port}` → `[port]`
- meldung2.htm:388 `"world"/body>` → `"world"</body>`
- ue_api_wobom2.htm:307 `<belpos_id` → `<belpos_id>`;219 註解與程式碼不符
- ue_form.htm:160,169 `declare=lform.ue_form` → `declare=lform=ue_form`;184 `form.return.ret_code`;371 `destry` → `destroy`;374–375 `lform.` → `mynewform.`;471 `dwname=mywindow.psr` → `psr=`;500–503 補 `lform.show`;569 補逗號;657 補引號並統一表名 `@U_ColorTable`/`@U_Color_Table`
- ue_json.htm:192 `a.parse {…}` 補 `=`;218 補 `b.get=`
- zip.htm:216 `unzipfile=` → `unzipwebfile`;239 目標檔改 `.zip`
- file2datastore.htm:389,393 `<attribute …>` 補 `/>`

### 邏輯、輸出與說明不符
- bscriptoperators.htm:188–194 `*` → `+`;230–234 輸出 "John Doe" → Martin
- bscriptvariables.htm:226 "incremented by 6" → 3
- bscriptstatements.htm:172 分號說明與範例一致
- bs_number.htm:198–199 `#0.0000` 輸出 → 5.5500 / 5,5500
- bs_string.htm:304–305 `mid 4 1` 結果 "l";324 "hallo" → "hello";624 去重 `setvar=ls_test=`;667–669 hex2int 範例修正
- bsstringfunctions.htm:254–255 `ls_reslult` 拼字、長度 10 → 11;270 "right letters" → "left letters"
- bsdatetimefunctions.htm:300–302 lastdayofweek 範例呼叫修正;268 週數 "2011/01" → "2011/08"
- globalreplace.htm:167–169 `myvar=<itemname>`、`setitem=itemname=<myvar>`
- link2url.htm:165 寫死網址 → `<myurl>`
- bsquestion.htm:279–280 註解 "Goto end" → 對應 `ende`
- file.htm:216,228 filemove 說明與訊息文字改為 move

### bs-bsl.htm(bsl/bsl_and_beas_script.htm 內容相同,兩頁都改)
- 232 `ret_vale` → `ret_value`;286 `red_code` → `ret_code`;242 `Itemcode("a001")` → `Item("a001")`;211 `.QuantityProduced/$Value` → `/QuantityProduced/$value`
- bsue_bsl.htm:256 `b.code/b.text/b.value` → `ret_code/ret_text/ret_value`

## 三、BSL(bsl)

### 不合法 JSON
- bsl_and_web_service.htm:223–225、bslexpand.htm:161–163:鍵名加引號、補逗號
- bsl_authorization.htm:248 多餘引號
- bsl_odata_interface.htm:436、450 缺引號;437 括號不對稱;410/422 結尾逗號(bslinlinecount.htm:178,186 同)
- bsldocumentpost.htm(bslsalesdeliverypost.htm 同):255–256、273–274 多餘逗號/缺逗號;268 `BatchGroup` 加引號;189 vs 195 `"Allocation":true`
- 缺逗號:bslitempost.htm:187–188、bslreservationpost.htm:239、bslsalesdelivery.htm:189–190、bslqcordertransfer.htm:317、bslworkorderget.htm:1083
- bslqcordertransfer.htm:185 `{DistNumber"`、`ToDistNumber"` 缺引號;`"ToItemCode",` 無值;`Reijected` → `Rejected`
- bslworkorderposclose.htm:173 `"LineNumber: 10` 引號與重複鍵;177、bslworkorderclose.htm:176 註解 `/` → `//`
- bslworkorderpospost.htm:161 `Quantity:3` 鍵加引號
- bslworkorderbomput.htm:160,165 括號;163 標題與範例一致
- 結尾多餘逗號:bslitemgetgetdocuments.htm:214(213 `FileNAme` → `FileName`)、bslinterruptionpost.htm:351、bsl_timereceipt.htm:208
- bsl_timereceipt.htm:205–206 補 `&`、`hh:mm"` 補 `>`
- bsltransferrequest.htm:186 補 `}`
- external_production.htm:187/192 重複 `DistNumber` 鍵;224 補逗號
- qualitycontrol.htm:245 `{"transfer",{…}}` → `{"transfer":{…}}`

### OData 查詢語法
- 缺 `$`:bsl_and_web_service.htm:231,240、bsl_odata_interface.htm:433、bslitemversion.htm:195、bslskip.htm:162、top.htm:161
- 缺 `=`:bslselect.htm:199、bslodata_functions.htm:188,226
- bslorder.htm:161 `$groupby=` → `$orderby=`
- bslfilter.htm:299 `neg` → `neq`;294–296 範例改用非數字字串(如 "abc")並保留 `and`
- external_production.htm:168 `bg 0` → `gt 0`;216 → `PurchaseOrderLines/LineStatus eq 'O'`
- bslodata_functions.htm:194 `conact` → `concat` 並補括號
- bslitemuomlist.htm:160 `?$mask1` → `?$mask=1`

### 函式名、呼叫方式
- bslitemgetgetprice.htm:175 補 `/GetPrice`、字串參數補 `,bsl`;176 `&ltb.price>` → `<b.price>`
- bslqcordercreateqcorderfromsapdocument.htm:166、170 `CreateQcOrderFromSAPDocment` → `…Document`;169–170 改為 `instance bsl b`、`b.get=`
- bslqcordermeasurementsetok.htm / setrelease.htm / setvalue.htm 159 行:`/` 呼叫與 `.` 簽章一致
- bslqcordermeasurementsetvalue.htm:169 補 `")`;170 訊息文字
- bslqcordermeasurement.htm:171 `QCOrdermesaurement`
- bslqcorderput.htm:171 `BlockReasonText` → `BlockageReasonText`
- bslqcordergetinsprectionplan.htm:145 `Insprection` → `Inspection`
- bsl_system.htm:399 `System.ReadSetup` → `System/ReadSetup`
- bslworkorderposclose.htm:159 補 `/Close`
- bslworkorderposrecalcualtetime.htm:158 `WororderPos` → `WorkorderPos`
- bsluomconversionconversion.htm:159、…itemconversion.htm:160、…qtyconversion.htm:160 `UoMConverion` → `UoMConversion`
- bsluomconversion.htm:175(qtyconversion.htm:178 同)呼叫與說明一致;188–192 `WhsUnitQty` 改為已宣告變數、`dbnum` → `num(6)`
- bslpoolreport.htm:162 / bslpoolreportcalculate.htm:159,168 一致化;163 補 `//`
- bslnewfunction.htm:292 `sqljoin` → `sqljson`
- bslnewobject.htm:317 → `"BEAS_FTPOS"."ItemCode"`;388 補 `name=`;396 補 `join=`
- bslworkorderserialnumberreservatiion.htm:162 補引號與右括號
- warehouseruledel.htm:160、warehouserulelinesdel.htm:160,163 `DEL` → `DELETE`
- bsl_printservice.htm:187、printservicepost.htm:176 `CurrentSessoinId` → `CurrentSessionId`;printservicepost.htm:185 補 `&`
- bslprotocolpost.htm:338 `destroy mywo` 對象

### 說明與範例不符
- bsl_issuewo.htm:202–204 Transaction `receiptwo` → issue;220–231 `IssueWo` 與說明一致、`BaseDocEntry` / `Base_DocEntry` 統一
- bslissue.htm:158 "receipt" → "issue"
- bslserialnumberput.htm:158 "batch number" → "serial number"
- bslexpand.htm:169 `ItemWareouse`;184–185 "more than one" → "only one",刪多餘 `]`
- bslqcordercreateqcorderfromgoodsreceiptp.htm:157,165 "Purchase order" → "Goods Receipt PO"
- json-format.htm:183 `28\"` 與 "Wheel 20\"" 一致
- 拼字:bslbillofmateriallinedelete.htm:160 `BillOfMatrialId`、bslreservationdelete.htm:159 `Rerservation`、bslresourceput.htm:159 `ResourceIe`
- bslresourcecalendarrecalculatecalendar.htm:166,169 `"2021-11-20,` 補引號

### bsl_and_php.htm
- 212:一次設定完整 header 陣列,避免覆蓋 `Content-Type: application/json`
- 186:`echo $items;` 改為可讀輸出(如 `print_r`/`json_encode`)
- 存疑 205:PUT 分支缺 body

## 四、存疑 / 僅記錄,預設不修改
- `ret_code` 判斷方式不一致(`<> 1`、`= -1`、0 = ok)
- 「doNotClosePosition / Receipt」說明被複製到約 15 個非 receipt 頁(`*post.htm`,約 339–355 行)
- `empoyee-Id`(可能是 API 本身拼錯)
- scrapreason.htm:477,500 佔位符遺失
- bsltransfer.htm:362/389 `ToWareHouse`/`ToWhsCode`、`BaseLineNum`/`BaseLineNumber`
- bslpurchaseorder.htm:172 `wo` 未定義
- formresizemode.htm:163,171
- json-format.htm:220 `yyyy-mmThh:mm`

## 五、純拼字(低嚴重度)
DateTiem、Siee、canalways、currentlí、allways、propertie(s)、delimieter、formular、sysntax、modfiy、definnition、dropodown、Descrption、diysplay、Defintion、intance、returnvaule、functinoname、Procress、Encripted/Decripted、Statemend、execite、Didplay、dropown、Imporant、currnent、adress、sendet、choosed、junp、tilte、sqlwehre、Msster、otherweise、mateiral、numberic、varialbe、varliable、swithces、deletehistogran、Work oder、wizzard、Numerc、PurchaOrderLines、Purchaserder、ExternalDocEnrty、Booelan、reasion、Messurements、EntiyName、toQuantiy、Produtio、closse、Assemlby、Persnnell、StarDate、Autenticate 等。
僅修正位於程式碼範例、函式名、屬性名內者;一般敘述文字的拼字視情況修正。
