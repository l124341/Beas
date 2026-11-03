# API 方法與屬性(參考文件)

> 來源:Beas 官方說明文件(Beas Script 2026.09)。本文件為原文頁面的繁體中文翻譯整理,只收錄頁面上的內容;範例程式碼逐字保留原文(含錯字)。
> 「⚠ 原文疑似不一致」為整理時標註的原文問題,未做任何更正。
> 原文頁面中的圖片(截圖)無法轉成文字,以「〔圖片:alt 文字〕」標示其位置。

## 目錄

- **Boyum Usability Package**
  - [Boyum Usability Package](#boyum-usability-packageb1uphtm)(`b1up.htm`)
  - [B1UP: Dynamic Code](#b1up-dynamic-codeb1up-dynamic-codehtm)(`b1up-dynamic-code.htm`)
  - [B1UP: execute Beas Script](#b1up-execute-beas-scriptb1upexecutebeasscripthtm)(`b1upexecutebeasscript.htm`)
  - [B1UP: open beas form](#b1up-open-beas-formb1upopenformhtm)(`b1upopenform.htm`)
  - [CalculateAPS](#calculateapsbeasfunctioncalculateapshtm)(`beasfunctioncalculateaps.htm`)
  - [CalculateMRP](#calculatemrpbeasfunctioncalculatemrphtm)(`beasfunctioncalculatemrp.htm`)
  - [PrintWorkorder](#printworkorderbeasfunctionprintworkorderhtm)(`beasfunctionprintworkorder.htm`)
- **SAP DI API / DI-API Methods**
  - [DI-API Methods](#di-api-methodsdi-api-methodshtm)(`di-api-methods.htm`)
  - [DI-API: cancel](#di-api-canceldi-api-cancelhtm)(`di-api-cancel.htm`)
  - [DI-API: savetofile](#di-api-savetofiledi-api-savetofilehtm)(`di-api-savetofile.htm`)
  - [DI-API: savexml](#di-api-savexmldi-api-savexmlhtm)(`di-api-savexml.htm`)
  - [DI-API: getasxml](#di-api-getasxmldi-api-getasxmlhtm)(`di-api-getasxml.htm`)
  - [DI-API: getbusinessobjectfromxml](#di-api-getbusinessobjectfromxmldi-api-getbusinessobjectfromxmhtm)(`di-api-getbusinessobjectfromxm.htm`)
  - [DI-API: connect_extern](#di-api-connect_externconnect_externhtm)(`connect_extern.htm`)
  - [DI-API: startTransaction](#di-api-starttransactiondi-api_starttransactionhtm)(`di-api_starttransaction.htm`)
  - [DI-API: endTransaction](#di-api-endtransactiondi-api-endtransactionhtm)(`di-api-endtransaction.htm`)
  - [DI-API: debug](#di-api-debugdi-api-debughtm)(`di-api-debug.htm`)
- **SAP DI API / DI-API Properties**
  - [Di-API: connect](#di-api-connectdi-api-connecthtm)(`di-api-connect.htm`)
  - [DI-API: companyinfo](#di-api-companyinfodi-api-companyinfohtm)(`di-api-companyinfo.htm`)
  - [DI-API: admininfo](#di-api-admininfodi-api-admininfohtm)(`di-api-admininfo.htm`)
- **SAP DI API / DI-API: Service Types**
  - [DI-API: Service Types](#di-api-service-typesservicetypeshtm)(`servicetypes.htm`)

---

## Boyum Usability Package

### Boyum Usability Package(`b1up.htm`)

**Working with**

〔圖片:b1up〕

可以直接開啟各種 B1UP 視窗,或直接執行 B1UP 的 universal functions(通用功能)。

在其中你可以讀取/變更 Beas 資訊,並執行 Beas 函式。

- 執行 b1up 函式:`object.ue_b1up.uf=<uf-name>`
- 在 B1UP 函式中讀取 Beas 變數:`[BEAS: beas Variable]`
- 在 B1UP-UF 中執行 Beas 函式:`ExecutreBeasScript(<SomeBeasScript - in one line>);`(此行在原文中連結到 [B1UP: execute Beas Script](#b1up-execute-beas-scriptb1upexecutebeasscripthtm))

> ⚠ 原文疑似不一致:此處寫 `ExecutreBeasScript`,而 `b1upexecutebeasscript.htm` 頁面的指令為 `ExecuteBeasScript`。
> ⚠ 原文疑似不一致:此處讀取 Beas 變數的寫法為 `[BEAS: beas Variable]`(無 `$`),而本頁表格範例與 `b1up-dynamic-code.htm` 使用 `$[BEAS:...]`。
> ⚠ 原文疑似不一致:此處執行 b1up 函式寫作 `object.ue_b1up.uf=<uf-name>`(以 `.` 分隔),而下方表格範例皆寫作 `object=ue_b1up=uf=...`(以 `=` 分隔)。

透過物件 `ue_b1up`,你有以下用法:

| Property | Read | Write | 說明 |
|---|---|---|---|
| `search` | | x | 開啟 B1UP Search(搜尋)視窗並搜尋該字串。<br>範例:搜尋 "S001"<br>`object=ue_b1up=search=S001` |
| `uf`, `universalfunction` | | x | 執行 universal function。<br>範例:執行 universal function UF001<br>〔圖片:beasscript-logo-small〕<br>`object=ue_b1up=uf=UF001`<br>**Note:**<br>無法傳送參數給 universal function。<br>但你可以從 universal function 讀取 BeasScript 變數。<br>範例:SQL Report<br>`SELECT DocNum,CardCode FROM OINV WHERE CardCode = '$[BEAS:dw_1.item.knd_id.value]'`<br>請參閱<br>https://help.boyum-it.com/B1UP/index.html?the-beas-syntax.html |
| `ufedit` | | x | 編輯 universal function。<br>範例:編輯 universal function UF001<br>`object=ue_b1up=ufedit=UF001`<br>以搜尋模式開啟 Universal function<br>`object=ue_b1up=ufedit` |
| `common` | | x | 執行 b1up 中所有既有的指令<br>`object=ue_b1up=ActionCode,ActionText,Parameter` |

(原文表格中四個屬性的「x」皆標在 Write 欄,Read 欄皆空白。)

### B1UP: Dynamic Code(`b1up-dynamic-code.htm`)

〔圖片:b1up〕

在每一種通訊中,你都可以使用 Dynamic code(動態代碼)與 Beas 溝通。

若要從 Beas 表單讀取資訊,請使用 `$[BEAS: xxxx]`

xxxx = 目前表單中的 Beas 資訊。

**範例:**

以目前 structure 變數 "str_parm" 中的資訊顯示訊息

〔圖片:b1up_dynamicCode〕(原文此範例僅以截圖呈現,無文字程式碼)

**範例:**

在 Work order structure(工單結構)檢視中,我想針對目前 Business Partner 的所有工單開啟一個 SQL Report。

Business Partner 儲存在欄位 "KND_ID"。

此情況下的 Dynamic code 為 `$[BEAS:knd_id]` 或 `$[BEAS:dw_1.item.knd_id.value]`

〔圖片:macrosqlreport〕

在 Work order Structure 中插入按鈕

〔圖片:b1up_dynamicCode2〕

現在你就可以直接從 Work order structure 檢視開啟該 SQL report:

〔圖片:b1up_dynamicCode3〕

### B1UP: execute Beas Script(`b1upexecutebeasscript.htm`)

〔圖片:b1up〕

在 B1UP UF 中,你可以用以下指令執行 BeasScript:

```
ExecuteBeasScript( <... the script ...> ) ;
```

這會在目前視窗中執行。

Beas Script:

```
ExecuteBeasScript( messagebox=hello world ) ;
```

只能全部定義在一行之內。

系統會在執行前轉換佔位符(placeholders)。如此即可透過佔位符 `<cr_lf>` 處理多行。

**範例:**

開啟工單。編號儲存在 `@store1` 中

```
@store1=6002452;

ExecuteBeasScript(openform=beas_workorder_edit|@store1);

// alternative: openform(beas_workorder_edit|@store1);
```

### B1UP: open beas form(`b1upopenform.htm`)

〔圖片:b1up〕

可以開啟任何 Beas 表單

```
openform(BEAS:[formname]|Parameter|Parameter);
```

你可以用 Beas-Id 或 PSR-File 名稱來指定視窗

```
@store1=6002452;

openform(BEAS:ItemHistory|@store1);
```

若要刪除或建立新項目,請將 "new" 或 "delete" 定義為最後一個參數。

為料號 FP 建立新的 Bill of Materials(物料清單)項目

```
openform(BEAS:BOM_Pos_Edit|FP|new);
```

從 Bill of Materials FP 刪除 Position 20

```
openform(BEAS:BOM_Pos_Edit|FP|10|del);
```

> ⚠ 原文疑似不一致:說明寫最後一個參數為 "delete",範例卻使用 `del`;說明寫刪除 Position 20,範例參數卻是 `10`。

主要視窗清單請參閱 Beas Window Names(原文連結 `beasform-id.htm`)

關於開啟表單的更多資訊請參閱 Open a window(原文連結 `open-a-window.htm`)

**範例:**

使用 B1UP 直接從 SAP 選單開啟 Work list(工作清單)

(原文此範例標題之後無任何內容。)

### CalculateAPS(`beasfunctioncalculateaps.htm`)

計算所定義的 APS Scenario(情境)

參數:ScneraioId(String)

範例:計算 Standard 情境

```
beasfunction(CalculateAPS|Standard);
```

〔圖示:formel_bscript〕

```
 beasfunction=CalculateAPS=Standard
```

此函式會:

- 檢查目前使用者是否有 APS 授權
- 檢查此情境目前是否正在執行 APS 計算
- 詢問你是否要開始計算
- 在背景中開始此情境的計算

它一律使用所用 APS Scenario 中定義的第一個計算類型(calculation type)。

在此範例中,使用的計算類型為 "forward"

〔圖片:APS_calc_Forward〕

> 註:原文參數名稱拼寫為 `ScneraioId`(CalculateMRP 頁為 `ScenarioId`),照錄。原文中 `beasfunction(CalculateAPS|Standard);` 一行前沒有圖示,`beasfunction=CalculateAPS=Standard` 一行前為 formel_bscript 圖示。

### CalculateMRP(`beasfunctioncalculatemrp.htm`)

計算所定義的 MRP Scenario。

參數:ScenarioId(Number)

範例:計算 MRP Scenario 1

〔圖示:formel_bup〕

```
 beasfunction(CalculateMRP|1);
```

〔圖示:formel_bscript〕

```
 beasfunction=CalculateMRP=1
```

此函式會:

- 檢查目前使用者是否有 Beas Basic 授權
- 檢查此情境目前是否正在執行 MRP 計算
- 詢問你是否要開始計算
- 在背景中開始此情境的計算

### PrintWorkorder(`beasfunctionprintworkorder.htm`)

預覽或列印所定義的工單位置(Work order position)

參數:`DocEntry,[LineNumber ,print,printername,setprintFlag]`

- `DocEntry` = 工單編號
- `LineNumber` = 工單位置;若未定義,則為所有工單位置
- `print` = 空白/未定義=檢視(view),print=列印到印表機(原文:emtpy/not defined=view, print=print to printer)
- `printername` = 印表機名稱
- `setprintFlag` = true=將工單與工單位置設為已列印

範例:以 BeasScript 預覽某張工單的所有位置

Note:系統會為每個工單位置開啟一個單獨的檢視。

〔圖示:formel_bscript〕

```
 beasFunction=PrintWorkorder=600344
```

以 Boyum Usability Package 列印

〔圖示:formel_bup〕

```
 beasFunction(PrintWorkorder|600344|10|print);
```

以 BUP 在印表機 HS-30421 上列印

〔圖示:formel_bup〕

```
 beasFunction(PrintWorkorder|600344|10|print|HS-30421);
```

列印並將工單設為已列印

〔圖示:formel_bup〕

```
 beasFunction(PrintWorkorder|600344|10|print|HS-30421|true);
```

> 註:本頁函式名寫作 `beasFunction`(大寫 F),CalculateAPS / CalculateMRP 頁寫作 `beasfunction`,照錄。

---

## SAP DI API / DI-API Methods

### DI-API Methods(`di-api-methods.htm`)

〔圖片:sap-sdk〕

本頁為 DI-API 方法的連結清單(括號內為原文連結目標):

- DI-API: add(`diapi_add.htm`)
- DI-API: cancel(`di-api-cancel.htm`)
- DI-API: close(`di-api-close.htm`)
- DI-API: savetofile(`di-api-savetofile.htm`)
- DI-API: savexml(`di-api-savexml.htm`)
- DI-API: getasxml(`di-api-getasxml.htm`)
- DI-API: getbusinessobject(`di-api-getbusinessobject.htm`)
- DI-API: getbusinessobjectfromxml(`di-api-getbusinessobjectfromxm.htm`)
- DI-API: getbykey(`di-api-getbykey.htm`)
- DI-API: update(`di-api-update.htm`)
- DI-API: connect_extern(`connect_extern.htm`)
- DI-API: startTransaction(`di-api_starttransaction.htm`)
- DI-API: endTransaction(`di-api-endtransaction.htm`)
- DI-API: debug(`di-api-debug.htm`)
- DI-API: silent(`di-api-silent.htm`)

(本頁除清單外無其他說明文字。)

### DI-API: cancel(`di-api-cancel.htm`)

〔圖片:sap-sdk〕

取消(cancel)一張單據

```
// cancel sales order 3000

setvar=ll_docentry=3000

declare d=ue_api_sbo

d.getbusinessobject=oOrders

// Load

d.getbykey=<ll_docentry>

// set to "cancel"

d.cancel

// update

d.update

destroy d
```

### DI-API: savetofile(`di-api-savetofile.htm`)

〔圖片:sap-sdk〕

將目前物件存為 XML 資料

```
savetofile=filename
```

```
// cancel sales order 3000

setvar=ll_docentry=3000

declare d=ue_api_sbo

d.getbusinessobject=oOrders

// Load

d.getbykey=<ll_docentry>

// save to xml file

d.savetofile=c:\output\document.xml

destroy=d
```

> ⚠ 原文疑似不一致:範例註解寫 `// cancel sales order 3000`,但範例實際內容是載入訂單並存成 XML 檔,並未取消。另本範例以 `destroy=d` 結尾,cancel 頁範例寫 `destroy d`。

### DI-API: savexml(`di-api-savexml.htm`)

〔圖片:sap-sdk〕

將目前物件存為 XML 資料

此指令的反向操作為 GetBusinessObjectFromXML

```
savexml=filename
```

```
// cancel sales order 3000

setvar=ll_docentry=3000

declare d=ue_api_sbo

d.getbusinessobject=oOrders

// Load

d.getbykey=<ll_docentry>

// save to xml file

d.savexml=c:\output\document.xml

destroy=d
```

**範例:**

儲存並載入一張發票

```
declare d=ue_api_sbo

d.GetBusinessObject=oInvoices

// Retrieve an invoice document from the database

d.GetByKey=1023

// Save the object as an xml file

d.savexml=C:\Program Files\SAP\XML\Invoice.xml

destroy d

// Create new instance and load the object from xmldeclare d=ue_api_sbo

d.GetBusinessObject=oInvoices

d.GetBusinessObjectFromXML=C:\Program Files\SAP\XML\Invoice.xml"=0
```

> ⚠ 原文疑似不一致:(1) 第一個範例的註解寫 `// cancel sales order 3000`,但內容是存成 XML,未取消。(2) 第二個範例中,註解 `// Create new instance and load the object from xml` 與 `declare d=ue_api_sbo` 在原始 HTML 中位於同一行(中間無換行),依字面看 `declare` 會成為註解的一部分。(3) 最後一行結尾為 `Invoice.xml"=0`,有一個不成對的 `"` 及 `=0`,原文未說明其意義。

### DI-API: getasxml(`di-api-getasxml.htm`)

〔圖片:sap-sdk〕

從 XML 檔取得物件(更多資訊請參閱 SAP SDK)

> ⚠ 原文疑似不一致:方法名稱為 getasxml(字面為「取得為 XML」),說明卻寫「從 XML 檔取得物件」;本頁無範例可供對照。

### DI-API: getbusinessobjectfromxml(`di-api-getbusinessobjectfromxm.htm`)

〔圖片:sap-sdk〕

從 xml 讀取 business object

savexml 的反向函式

你可以載入先前以 savexml 儲存的單據。

可藉此以簡單的方式建立副本。

Note:Beas 的料號建立一律會建立範本料號的複本。為此 Beas 使用 savexml/gebusinessobjectfromxml。

> 註:Note 中的 `gebusinessobjectfromxml` 為原文拼寫(少一個 t),照錄。本頁無範例程式碼;用法範例見 savexml 頁。

### DI-API: connect_extern(`connect_extern.htm`)

〔圖片:sap-sdk〕

將 di-api 連線到另一個資料庫。

```
declare=myapi=ue_api_sbo

myapi=connect_extern=db=training<tab>username=manager<tab>password=manager
```

| 參數 | 說明 |
|---|---|
| `companydb`(或 `db`、`database`) | 設定資料庫名稱。Primary(主要) |
| `server` | 設定伺服器。預設為目前的伺服器 |
| `dbpassword` | 設定資料庫密碼。預設為目前的密碼 |
| `dbuser` | 設定資料庫使用者。預設為目前的資料庫使用者 |
| `username` | 設定 SAP 使用者名稱(SAP-Username)。Primary(主要) |
| `password` | 設定 SAP 密碼(SAP-Password)。Primary(主要) |
| `servertype` | SQL-Server 的類型,以數字表示——請參閱 SAP 文件。預設:目前的 Servertype<br>1=automatic, 4=sql2005, 6=sql2008 |

(原文「Primary」未進一步說明其意義。)

```
declare=myapi=ue_api_sbo

myapi=connect_extern=db=training<tab>username=manager<tab>password=manager

myapi=connectinfo

if <value> = -1 then

  destroy=myapi

  return failure

end if

myapi=getbusinessobject=oitem

myapi=getbykey=A11896

myapi=itemname=Gas Nozzle Subassembly, 65-50264

myapi=update

destroy=myapi
```

> 註:範例中使用的 `connectinfo` 在本頁及本批頁面中均無說明;原文僅示範其後以 `<value> = -1` 判斷失敗。

### DI-API: startTransaction(`di-api_starttransaction.htm`)

〔圖片:sap-sdk〕

開始一個交易(transaction),讓你可以對多個 business objects 執行資料操作。

使用 EndTransaction 方法結束交易並釋放被鎖定的記錄,讓其他使用者可以存取它們。

**Remarks(備註):**

當你要對多個 business objects 執行資料操作時,請使用此方法:

- 若操作成功,可以 commit(提交)交易以將資料儲存到資料庫,或 roll back(回滾)以捨棄變更。
- 若其中一個操作失敗,DI API 會 roll back 該交易,捨棄變更。在交易中某個 DI API 呼叫失敗後,你必須立即退出交易(請見下方範例)。

```
declare=di_item=ue_api_sbo

// Start transaction process

// Add first item

di_item=starttransaction

di_item=getbusinessobject=oitems

di_item=itemcode=NewItemCode

di_item=itemname=NewItemName

di_item=add

// Add second item

di_item=getbusinessobject=oitems

di_item=itemcode=NewItemCode2

di_item=itemname=NewItemName2

di_item=add

// If anything is wrong - create rollback

if <anyerror> = true then

  di_item=endtransaction=-1

// all ok, commit complete process

else

  di_item=endtransaction=0

end if
```

**Note:**

交易流程只適用於 SAP SDK 連線。

Beas 會自行開啟到資料庫的連線,在 commit 之前看不到 SAP 交易流程中所做的變更。

```
declare=di_item=ue_api_sbo

// Start transaction process

// Add first item

di_item=starttransaction

di_item=getbusinessobject=oitems

di_item=itemcode=NewItemCode

di_item=itemname=NewItemName

di_item=add

// follow return 100 (not found) because the transaction process not closed

select "ItemCode" from "OITM" where ItemCode='NewItemCode'

di_item=endtransaction=0

// now it's possible to read

select "ItemCode" from "OITM" where ItemCode='NewItemCode'
```

> ⚠ 原文疑似不一致:本頁範例以 `endtransaction=-1` 做 rollback,而 endTransaction 頁寫 `1=Rollback`。
> 註:原文中「EndTransaction method」的超連結目標指向本頁自身(`di-api_starttransaction.htm`),而非 endTransaction 頁。範例中的 `<anyerror>` 在本頁未說明。

### DI-API: endTransaction(`di-api-endtransaction.htm`)

〔圖片:sap-sdk〕

結束以 StartTransaction 方法開始的全域交易(global transaction)。

```
endTransacton=endType
```

- 0=commit
- 1=Rollback

只有在與資料庫的連線為作用中時,才能使用 StartTransaction 與 EndTransaction 方法。

若呼叫 EndTransaction 時發生例外(exception),變更不會被 commit。要修正此情況:

- 排查並修正該例外。
- 呼叫 StartTransaction。
- 重新提交變更(呼叫 Add、Update 與 Delete 方法)。
- 呼叫 EndTransaction。

> ⚠ 原文疑似不一致:本頁 `1=Rollback`,但 startTransaction 頁範例的 rollback 寫法為 `endtransaction=-1`。另語法行原文拼寫為 `endTransacton`(少一個 i),照錄。

### DI-API: debug(`di-api-debug.htm`)

〔圖片:sap-sdk〕

啟用延伸除錯記錄(extended debug log)

顯示更多細節

```
declare d=ue_api_sbo

d.debug=true
```

---

## SAP DI API / DI-API Properties

### Di-API: connect(`di-api-connect.htm`)

〔圖片:sap-sdk〕

檢查 DI-API 是否已連線

回傳 1=已連線,或 -1=未連線(原文:return 1=connect or -1=not connected)

〔圖片:beasscript-logo-small〕

```
declare d=ue_api_sbo

if <d.connect> = -1 then

  messagebox=API not connected

  return -1

end if
```

### DI-API: companyinfo(`di-api-companyinfo.htm`)

〔圖片:sap-sdk〕

從 DI-API company 物件回傳資訊

| 屬性 | 說明 |
|---|---|
| `companyname` | 回傳公司名稱 |
| `blockstocknegativequantity` | 回傳屬性 blockstocknegativequantity |
| `version` | 回傳公司的 Version(版本) |

```
declare d=ue_api_sbo

messagebox=<d.companyinfo.companyname>
```

(本頁原文僅上列 3 個屬性。)

### DI-API: admininfo(`di-api-admininfo.htm`)

〔圖片:sap-sdk〕

回傳 Admin 資訊 ^1

```
declare d=ue_api_sbo

messagebox=<d.AccountSegmentsSeparator>
```

下表共 257 個屬性,依原文順序列出。原文許多屬性的說明只寫「property <屬性名>」,表中以「屬性 <屬性名>(原文僅此說明)」表示。原文的「Field name: X」譯為「欄位名稱:X」、「Length: N characters」譯為「長度:N 字元」;「Country-specific for …」譯為「…特定」。

| 屬性 | 說明 |
|---|---|
| `Account` | 屬性 Account(原文僅此說明) |
| `AccountSegmentsSeparator` | 設定或回傳科目區段之間顯示的分隔字元(例如:"-" 或 "_")。美國特定(Country-specific for US)。<br>欄位名稱:`ActSep`<br>長度:1 字元 |
| `AccuracyofQuantities` | 設定或回傳數量顯示的小數位數。此設定只影響顯示。系統一律精確計算到小數第六位。<br>欄位名稱:`QtyDec` |
| `ActionWhenDeviateFromBAForAccounting` | 屬性 ActionWhenDeviateFromBAForAccounting(原文僅此說明) |
| `ActionWhenDeviateFromBAForGRPO` | 屬性 ActionWhenDeviateFromBAForGRPO(原文僅此說明) |
| `ActionWhenDeviateFromBAForPO` | 屬性 ActionWhenDeviateFromBAForPO(原文僅此說明) |
| `AdditionalIdNumber` | 設定或回傳公司的附加 ID 號碼。例如,稅務機關以此號碼識別該公司為某公司集團的一部分。<br>長度:32 字元<br>欄位名稱:`FreeZoneNo` |
| `Address` | 設定或回傳以本地語言表示的公司地址。<br>長度:254 字元<br>欄位名稱:`CompnyAddr` |
| `AddressinForeignLanguage` | 回傳以外語表示的公司地址。<br>長度:254 字元<br>欄位名稱:`CmpnyAddrF` |
| `AdressFromWH` | 決定是否在採購單據中顯示完整的倉庫地址。<br>欄位名稱:`AdrsFromWH` |
| `AdvancesonCorpIncomeTax` | 設定或回傳因公司所得稅而必須預繳的百分比。<br>以色列特定(Country-specific for Israel)。<br>欄位名稱:`DpsitPrcnt` |
| `AlertbyWarehouse` | 決定系統檢查庫存時,是檢查特定倉庫,還是檢查存放該料號的所有倉庫。<br>欄位名稱:`WarnByWhs`<br>1 - 輸入銷售單據時,系統檢查指定倉庫的庫存水準。若目前交易使此水準低於設定的最小值,即顯示警告訊息——即使該料號在所有倉庫的庫存大於最小值也一樣。<br>0 - 檢查存放該料號的所有倉庫的庫存水準。 |
| `AlertTypeforWHStock` | 決定當銷售單據(例如交貨單或發票)導致庫存水準低於此最小值時,系統的回應類型。欄位名稱:LevelWarn。 |
| `AliasName` | 屬性 AliasName(原文僅此說明) |
| `AllowBPWithNoOwner` | 屬性 AllowBPWithNoOwner(原文僅此說明) |
| `AllowClosedSalesQuotations` | 決定銷售報價單在(完整)複製到後續單據之後,保持開啟或關閉。<br>欄位名稱:`ClosedQuot` |
| `AllowFuturePostingDate` | 決定是否在公司層級允許未來的過帳日期。欄位名稱:AllowFuPos。 |
| `AllowInBoundPostingWithZeroPrice` | 屬性 AllowInBoundPostingWithZeroPrice(原文僅此說明) |
| `AllowMultipleBAOnSamePeriod` | 屬性 AllowMultipleBAOnSamePeriod(原文僅此說明) |
| `AltNameForApInvoice` | 設定或回傳 A/P invoice(應付發票)單據的替代名稱。<br>長度:20 字元<br>欄位名稱:`A/P Invoice` |
| `AltNameforCreditMemo` | 設定或回傳 A/P credit memo(應付貸項通知單)單據的替代名稱。<br>長度:20 字元<br>欄位名稱:`RpcName` |
| `AltNameForGoodsReceipt` | 設定或回傳 goods receipt PO(採購收貨單)單據的替代名稱。<br>長度:20 字元<br>欄位名稱:`PdnName` |
| `AltNameForGoodsReturn` | 設定或回傳 goods returns(退貨)單據的替代名稱。<br>長度:20 字元<br>欄位名稱:`RpdName` |
| `AltNameForPurchase` | 設定或回傳 Purchase(採購)單據的替代名稱。<br>長度:20 字元<br>欄位名稱:(原文空白) |
| `ApplicationOfIFRS` | 屬性 ApplicationOfIFRS(原文僅此說明) |
| `ApplyBaseInactiveStatusToPeriodVolumeDiscounts` | 屬性 ApplyBaseInactiveStatusToPeriodVolumeDiscounts(原文僅此說明) |
| `ApplyBaseInactiveStatusToPriceLists` | 屬性 ApplyBaseInactiveStatusToPriceLists(原文僅此說明) |
| `ApplyBaseInactiveStatusToSpecialPrices` | 屬性 ApplyBaseInactiveStatusToSpecialPrices(原文僅此說明) |
| `AutoAddPackage` | 表示是否在每次建立新料號時加入所有包裝定義,或自動將新的包裝定義加入既有料號。<br>欄位名稱:`AutoAddPkg` |
| `AutoAddUoM` | 表示是否在每次建立新料號時加入所有 UoM(Unit of Measurement,計量單位)群組定義,或自動將新的 UoM 群組定義加入既有料號。<br>欄位名稱:`AutoAddUoM` |
| `AutoAssignOnlyValidAPBA` | 屬性 AutoAssignOnlyValidAPBA(原文僅此說明) |
| `AutoAssignOnlyValidARBA` | 屬性 AutoAssignOnlyValidARBA(原文僅此說明) |
| `BankCountry` | 設定或回傳預設往來銀行(公司銀行)的國家,如 Banks 物件中所定義。<br>長度:3 字元<br>欄位名稱:`BankCountr` |
| `BankStatementInstalled` | 回傳布林值,指出 BankStatementService 是否已啟用。<br>Yes = 服務已啟用(2006 A 新安裝的預設值)<br>No = 服務未啟用(升級公司的預設值)。 |
| `BaseField` | 決定銷售單據 Remarks(備註)區要包含哪個參考號碼:Base Document Number(基礎單據號碼)(1),或 Customer/Vendor Reference Number(客戶/供應商參考號碼)(0;若基礎單據中不存在 Customer/Vendor Reference Number,銷售單據的 Remarks 欄位保持空白)。<br>欄位名稱:`BaseFld` |
| `BlockBookkeeping` | 決定在涉及與預算相關之 G/L 科目的會計交易中,是否封鎖偏離預算的情況。<br>適用於 CalculateBudget 的有效值為 1,且 BlockBudget 的有效值為 bb_Block 或 bb_MonthlyAlertOnly 時(系統會對任何偏離預算的情況顯示警告)。<br>欄位名稱:`BdgtAcctng` |
| `BlockBudget` | 決定系統如何處理單據中偏離預算的情況。<br>適用於 CalculateBudget 的有效值為 1 時。<br>欄位名稱:`BgtBlock` |
| `BlockDelNotesforPurchase` | 決定在 goods receipt PO(採購收貨單)中是否封鎖偏離預算的情況。<br>適用於 CalculateBudget 的有效值為 1,且 BlockBudget 的有效值為 bb_Block 或 bb_MonthlyAlertOnly 時(系統會對任何偏離預算的情況顯示警告)。<br>欄位名稱:`BdgtPDNDo`<br>LDiscTotal ⚠ 原文疑似不一致:此儲存格末尾多出一個單獨的「LDiscTotal」,未附任何說明(LDiscTotal 在原文中是 CalculateRowDiscount 的欄位名稱)。 |
| `BlockMultipleBAOnSameAPDocument` | 屬性 BlockMultipleBAOnSameAPDocument(原文僅此說明) |
| `BlockMultipleBAOnSameARDocument` | 屬性 BlockMultipleBAOnSameARDocument(原文僅此說明) |
| `BlockPostingDateEditing` | 決定是否封鎖 Journal Entry(日記帳分錄)單據中個別列的過帳日期編輯。<br>欄位名稱:`RefDNoEdit` |
| `BlockPurchaseOrders` | 決定在採購訂單中是否封鎖偏離預算的情況。<br>適用於 CalculateBudget 的有效值為 1,且 BlockBudget 的有效值為 bb_Block 或 bb_MonthlyAlertOnly 時(系統會對任何偏離預算的情況顯示警告)。<br>欄位名稱:`BdgtPORDoc` |
| `BlockStockNegativeQuantity` | 屬性 BlockStockNegativeQuantity(原文僅此說明) |
| `BlockSystemCurrencyEditing` | 決定是否封鎖 Journal Entry 單據中以系統貨幣表示之總金額的編輯。<br>欄位名稱:`SysCNoEdit` |
| `BlockTaxDate` | 決定是否封鎖 Journal Entry 單據中每列的單據日期編輯。<br>欄位名稱:`TaxDNoEdit` |
| `BoletoFolderPath` | 屬性 BoletoFolderPath(原文僅此說明) |
| `BPTypeCode` | 決定付款單(payment slips)中使用的 Payment Reference(付款參考)字串長度。<br>北歐國家特定(Country-specific for Nordic countries)。<br>欄位名稱:`CaredType` |
| `BudgetAlert` | 決定偏離預算時的警示類型。<br>適用於 CalculateBudget 的有效值為 1,且 BlockBudget 的有效值為 bb_Block 或 bb_MonthlyAlertOnly 時。<br>欄位名稱:`BgtWarning` |
| `CalculateBudget` | 決定是否啟用預算管理。欄位名稱:DoBudget。 |
| `CalculateGrossProfitperTra` | 決定是否為銷售單據啟用毛利計算。<br>欄位名稱:`SaleProfit` |
| `CalculateInWhseQtyBasedOnPostingDate` | 屬性 CalculateInWhseQtyBasedOnPostingDate(原文僅此說明) |
| `CalculateRowDiscount` | 設定或回傳一個有效值,決定是否計算列折扣。<br>欄位名稱:`LDiscTotal` |
| `CalculateTaxinSalesQuotati` | 決定是否在銷售報價單中計算稅額。設為 0 時,系統不在報價單中計算或顯示稅額;但若使用者建立參照報價單的訂單,系統會依客戶與料號的規則計算稅額。<br>欄位名稱:`AddVat` |
| `CertificateNo` | 設定或回傳適用於銷售單據之預扣稅(withholding tax)的憑證號碼。適用於 SHandleWT 設為 1 時。<br>長度:20 字元<br>欄位名稱:`CrtfcateNO` |
| `ChangeDefReconAPAccounts` | 決定是否允許為不同供應商指定不同的統制科目(control accounts)。可透過 BusinessPartners 物件為各供應商指定統制科目。預設值為 0,表示所有供應商都使用 Control Accounts - Accounts Payable 視窗中定義的統制科目。欄位名稱:ChCtrAPAct。 |
| `ChangeDefReconARAccounts` | 決定是否允許為不同客戶指定不同的統制科目。可透過 BusinessPartners 物件為各客戶指定統制科目。預設值為 0,表示所有客戶都使用 Control Accounts - Accounts Receivable 視窗中定義的統制科目。欄位名稱:ChCtrARAct。 |
| `ChangedExistingOrders` | 決定是否允許變更既有的銷售訂單。預設值為 1。若設為 0,銷售訂單一經建立即不可修改。欄位名稱:ChangeRdr。 |
| `ChartofAccountsTemplate` | 決定公司的科目表。可用的科目表範本依國家在地化(country localization)而定。預設值為 U,表示使用者自訂的科目表。只有在建立新的公司資料庫時才允許修改範本;一旦套用科目表,就不允許修改範本。欄位名稱:CompType。 |
| `CloseCountedRowsWithoutConfirmation` | 屬性 CloseCountedRowsWithoutConfirmation(原文僅此說明) |
| `CloseCountedRowsWithZeroDifference` | 屬性 CloseCountedRowsWithZeroDifference(原文僅此說明) |
| `Code` | 這組管理設定的鍵值。<br>供內部使用。<br>欄位名稱:`Code` |
| `CommitmentRestriction` | 決定是否依客戶指定的 MaxCommitment 限制為該客戶建立銷售單據並提示警告訊息。承諾限額(commitment limit)適用於指定類型的單據(A/R Invoice、Delivery 與 Sales Order)。<br>欄位名稱:`ObligLimit` |
| `CompanyColor` | 設定或回傳使用中視窗背景色的編號。<br>欄位名稱:`Color` |
| `CompanyName` | 回傳公司名稱(可為縮寫)。此名稱顯示在 SAP Business One 選單頂端,但不用於列印單據。欄位名稱:CompnyName。<br>長度:100 字元<br>欄位名稱:`CompnyName` |
| `ConsiderDelNotesinSalesR` | 決定是否將尚未開立發票的交貨單計入指定限制的總額。欄位名稱:AddDlnBlnc。 |
| `ConsumeForecast` | 決定要從預測中扣除(1)或加上(0)銷售訂單與反向發票(reverse invoices)的料號數量。欄位名稱:ConsumeFCT。 |
| `ConsumptionMethod` | 設定或回傳預測消耗的預設方法:Backward-to-Forward 或 Forward-to-Backward。欄位名稱:ConsumeMtd。 |
| `ContinuousStockManagement` | 決定是否以永續盤存(Perpetual Inventory)方式管理庫存。<br>欄位名稱:`ContInvnt` |
| `ContinuousStockSystem` | 設定或回傳預設的庫存評價方法:Moving Average、Standard Price 或 FIFO。適用於 ContinuousStockManagement 的值為 1 時。欄位名稱:InvntSystm。 |
| `CopyExchangeRateInCopyTo` | 表示是否將匯率複製到目標單據。<br>欄位名稱:`CpyExhRate` |
| `CopyOpenRowsToDelivery` | 屬性 CopyOpenRowsToDelivery(原文僅此說明) |
| `CopySingleCounterToIndividualCounter` | 表示是否將單一盤點人(single counter)複製到個別盤點人(多位盤點人)。<br>欄位名稱:`INCSingToV` |
| `Country` | 設定或回傳公司所在國家。<br>欄位名稱:`Country)`<br>這是指向 Countries 資料表(OCRY,未透過 DI API 公開)的外鍵。 |
| `CreateAutoVATLineinJDT` | 決定是否依科目定義的 VAT 群組自動計算 VAT。欄位名稱:AutoVat。 |
| `CreateOnlineQuotation` | 屬性 CreateOnlineQuotation(原文僅此說明) |
| `CreditBalancewithMinusSign` | 決定 G/L 科目與業務夥伴的貸方餘額以負數(1)或正數(0)顯示。預設設定為 1,即 G/L 科目與業務夥伴的貸方餘額以負數顯示。<br>欄位名稱:`DispPosDeb` |
| `CreditDepositType` | 決定信用卡憑單(credit vouchers)是自動或手動提交。若手動提交,系統將憑單日期記錄為過帳日期;若自動提交,則在存款時由系統輸入,並以存款日期作為信用憑單的過帳日期。欄位名稱:CreditDpst。 |
| `CreditRestriction` | 決定是否依客戶指定的 CreditLimit;(原文如此)限制為該客戶建立銷售單據並提示警告訊息。信用額度適用於指定類型的單據(A/R Invoice、Delivery 與 Sales Order)。<br>欄位名稱:`CreditLimit` |
| `CustomerIdNumber` | 設定預設的客戶 ID 號碼(僅適用於 ISRType 為 BISR 時)。由瑞士的自動付款系統使用。欄位名稱:CustmrDdct。⚠ 原文疑似不一致:此欄位名稱與 CustomersDeductionatSource 相同。 |
| `CustomersDeductionatSource` | 決定是否對客戶支付的金額預扣稅款。以色列特定。欄位名稱:CustmrDdct。 |
| `DataOwnershipManageBy` | 屬性 DataOwnershipManageBy(原文僅此說明) |
| `DateSeparator` | 設定日期欄位之間顯示的字元。欄位名稱:DateSep。 |
| `DateTemplate` | 決定日期的顯示方式。此設定不影響日期的輸入方式。DD = 日,兩位數。MM = 月,兩位數。Month = 月,以文字表示。YY = 年,兩位數。YYYY = 年,四位數。CCYY = 年,以 20__ 開頭(21 世紀)。欄位名稱:DateFormat。 |
| `DaysBackward` | 設定或回傳搜尋可消耗之預測時往回的天數。欄位名稱:DaysBack。 |
| `DaysForward` | 設定或回傳搜尋可消耗之預測時往後的天數。欄位名稱:DaysFwrd。 |
| `DecimalSeparator` | 決定用來分隔整數與小數的字元。美國與英國使用點;德語系國家一般使用逗號。欄位名稱:DecSep。 |
| `DeductionFileNo` | 設定並回傳 Deduction File No.(扣繳檔案編號)。<br>長度:50 字元<br>欄位名稱:(原文空白) |
| `DefaultAccountCurrency` | 決定是否為目前科目設定預設 AcctCurrency。<br>欄位名稱:(原文空白) |
| `DefaultBankAccount` | 設定或回傳預設的 BankAccount 號碼。欄位名稱:DflBnkAcct。 |
| `DefaultBankAccountKey` | 設定或回傳預設銀行帳戶的鍵值。欄位名稱:DflBnkCode。⚠ 原文疑似不一致:此欄位名稱與 DefaultBankNo 相同。 |
| `DefaultBankNo` | 設定或回傳預設的銀行號碼。欄位名稱:DflBnkCode。 |
| `DefaultBranch` | 設定或回傳預設的銀行分行編號。欄位名稱:DflBranch。<br>欄位名稱:`DflBranch` |
| `DefaultBudgetCostAssessMt` | 設定或回傳預設預算方法的鍵值。欄位名稱:BdgtDflt。 |
| `DefaultCustomerPaymentTerms` | 設定或回傳輸入新客戶主檔時顯示的預設付款條件。欄位名稱:DfCustTerm。 |
| `DefaultDunningTerm` | 設定或回傳輸入新客戶主檔時顯示的預設 DunningTerm(催款條件)。<br>欄位名稱:(原文空白) |
| `DefaultforBatchStatus` | 設定或回傳預設的批次狀態。有效值為:Released、Not Accessible 與 Locked。<br>欄位名稱:`BtchStatus` |
| `DefaultTaxCode` | 設定或回傳預設的銷售稅 Code。這是指向 SalesTaxCodes 物件之 Code 的外鍵。<br>長度:8 字元<br>欄位名稱:`DflTaxCode` |
| `DefaultVendorPaymentTerms` | 設定或回傳輸入新供應商主檔時顯示的預設付款條件。欄位名稱:DfVendTerm。 |
| `DefaultWarehouse` | 設定或回傳新料號要使用的 DefaultWarehouse(預設倉庫)。欄位名稱:DfltWhs。 |
| `DeferredTax` | 決定系統是否允許 DeferredTax(遞延稅)。遞延稅代表針對當期帳務年度盈餘、將於未來年度支付的稅。欄位名稱:DeferrTax。 |
| `DeferredTaxforVendors` | 決定系統是否允許供應商使用 Deferred Tax 科目。欄位名稱:defTaxVend。 |
| `DirectIndirectRate` | 決定匯率過帳方式:Direct 或 Indirect,也就是以本國貨幣與外幣計算交易金額的方法。<br>一旦過帳日記帳分錄後,此設定即不可修改。<br>欄位名稱:`DirectRate` |
| `DisplayBatchQtyUoMBy` | 屬性 DisplayBatchQtyUoMBy(原文僅此說明) |
| `DisplayBookkeepingWindow` | 決定是否依統制科目顯示科目餘額。欄位名稱:DspBokpWin。 |
| `DisplayCancelDocInReport` | 表示是否在報表中呈現已取消的單據及取消單據(cancellation documents)。<br>欄位名稱:`CmdDisBoth` |
| `DisplayCurrencyontheRight` | 決定貨幣符號顯示在金額的哪一側。<br>欄位名稱:`CurOnRight` |
| `DisplayInactivePriceListInDocuments` | 屬性 DisplayInactivePriceListInDocuments(原文僅此說明) |
| `DisplayInactivePriceListInReports` | 屬性 DisplayInactivePriceListInReports(原文僅此說明) |
| `DisplayInactivePriceListInSettings` | 屬性 DisplayInactivePriceListInSettings(原文僅此說明) |
| `DisplayPriceforPriceOnly` | 決定銷售報價單中是否包含 VAT 金額。欄位名稱:TreePricOn。 |
| `DisplayRoundingRemark` | 決定當外幣發票的折扣金額因四捨五入而與折扣百分比不同時,是否在 Remarks 欄位顯示註記。欄位名稱:RoundRmrk。 |
| `DocConfirmation` | 決定系統是否驗證採購與銷售單據的授權。欄位名稱:useDocWrf。 |
| `eMail` | 設定或回傳使用者的預設電子郵件地址。<br>長度:100 字元<br>欄位名稱:`E_Mail` |
| `EmployerReference` | 設定或回傳 Employer's Reference(雇主參考號)。英國特定(Country-Specific for UK)。<br>欄位名稱:`EmployerRf`<br>長度:32 字元 |
| `EnableAdvancedGLAccountDetermination` | 是否依彈性且集中的方法管理庫存 G/L 科目判定(G/L account determination)。<br>欄位名稱:`NewAcctDe` |
| `EnableApprovalProcedureInDI` | 決定 DI 中是否啟用核准程序(approval procedure)。<br>欄位名稱:`EnbApprDI` |
| `EnableBranches` | 屬性 EnableBranches(原文僅此說明) |
| `EnableCentralizedIncomingPayments` | 屬性 EnableCentralizedIncomingPayments(原文僅此說明) |
| `EnableCentralizedOutgoingPayments` | 屬性 EnableCentralizedOutgoingPayments(原文僅此說明) |
| `EnableMultipleSchedulings` | 屬性 EnableMultipleSchedulings(原文僅此說明) |
| `EnablePaymentDueDates` | 屬性 EnablePaymentDueDates(原文僅此說明) |
| `EnableSeparatePriceMode` | 屬性 EnableSeparatePriceMode(原文僅此說明) |
| `EnableUpdateBAPriceAndPlannedAmount` | 屬性 EnableUpdateBAPriceAndPlannedAmount(原文僅此說明) |
| `EnableUpdateDocAfterApproval` | 屬性 EnableUpdateDocAfterApproval(原文僅此說明) |
| `EnableUpdateDraftDuringApproval` | 屬性 EnableUpdateDraftDuringApproval(原文僅此說明) |
| `ExcelFolderPath` | 設定或回傳從 SAP Business One 應用程式匯出之 Microsoft Excel 文件的路徑。<br>欄位名稱:`ExcelPath`<br>長度:16 字元 |
| `ExpirationDate` | 設定或回傳適用於銷售單據之預扣稅憑證的到期日。適用於 SHandleWT 設為 1 時。<br>欄位名稱:`DdctExpire` |
| `ExtendedAdminInfo` | 提供存取延伸管理屬性(extended administration properties)。 |
| `FaxNumber` | 設定或回傳收件者傳真號碼。<br>長度:20 字元<br>欄位名稱:`Fax` |
| `FaxNumberForeignLang` | 設定或回傳以外語表示的預設傳真號碼。<br>欄位名稱:`FaxF`<br>長度:20 字元 |
| `FCCheckAccount` | 決定是否允許使用 Foreign Currency Check Account(外幣支票科目)。<br>欄位名稱:`FcNoBlnc` |
| `FederalTaxID` | 設定或回傳公司的主要聯邦稅籍編號(federal tax ID)。<br>長度:32 字元<br>另請參閱:Documents Object、Warehouses Object、BPAddresses Object。<br>欄位名稱:`TaxIdNum` |
| `FederalTaxID2` | 設定或回傳公司的第二個聯邦稅籍編號(若相關)。欄位名稱:TaxIdNum2。 |
| `FederalTaxID3` | 設定或回傳公司的第三個聯邦稅籍編號(若相關)。<br>欄位名稱:`TaxIdNum3` |
| `FileNumberinIncomeTax` | 設定或回傳公司的 IRS 檔案編號。<br>僅限美國(Country-specfic for US only)。<br>欄位名稱:`IRSFileNo` |
| `GeneralManager` | 總經理(本地語言)。<br>欄位名稱:`Manager1`<br>長度:100 字元 |
| `GeneralManagerForeignLanguage` | 總經理(外語)。<br>欄位名稱:`Manager1F`<br>長度:100 字元 |
| `GLMethod` | 設定或回傳 BoGLMethods 類型的有效值,指定用於過帳與該料號相關交易的預設 G/L 科目。<br>來源:Warehouses、ItemGroups,或在料號層級指定。<br>欄位名稱:`GLMethod` |
| `GrossProfitAfterSale` | 決定是否為銷售單據啟用毛利計算。<br>另請參閱 CalculateGrossProfitperTra<br>欄位名稱:`GrossBySal` |
| `GrossProfitPercentForServiceDocuments` | 服務類型銷售單據計算毛利時使用的預設毛利百分比。欄位名稱:GPPrcntSrv |
| `GTSDefaultChecker` | 屬性 GTSDefaultChecker(原文僅此說明) |
| `GTSDefaultPayee` | 屬性 GTSDefaultPayee(原文僅此說明) |
| `GTSInboundFolder` | 屬性 GTSInboundFolder(原文僅此說明) |
| `GTSMaxAmount` | 屬性 GTSMaxAmount(原文僅此說明) |
| `GTSOutboundFolder` | 屬性 GTSOutboundFolder(原文僅此說明) |
| `GTSResponseToExceeding` | 屬性 GTSResponseToExceeding(原文僅此說明) |
| `GTSSeparateCode` | 屬性 GTSSeparateCode(原文僅此說明) |
| `HolidaysName` | 設定或回傳指向 OHLD 資料表的外鍵,該資料表定義一年的假日。<br>欄位名稱:`HldCode` |
| `IEMandatoryValidation` | 屬性 IEMandatoryValidation(原文僅此說明) |
| `InstitutionCode` | 屬性 InstitutionCode(原文僅此說明) |
| `InventoryCountingHighlightCountersDifference` | 屬性 InventoryCountingHighlightCountersDifference(原文僅此說明) |
| `InventoryCountingHighlightMaxVariance` | 屬性 InventoryCountingHighlightMaxVariance(原文僅此說明) |
| `InventoryCountingHighlightVariance` | 屬性 InventoryCountingHighlightVariance(原文僅此說明) |
| `InventoryPostingHighlightVariance` | 屬性 InventoryPostingHighlightVariance(原文僅此說明) |
| `InventoryPostingReleaseOnlySerialAndBatch` | 屬性 InventoryPostingReleaseOnlySerialAndBatch(原文僅此說明) |
| `IsPrinterConnected` | 屬性 IsPrinterConnected(原文僅此說明) |
| `ISRBillerID` | 設定或回傳 ISRBillerId。(僅限瑞士)。<br>長度:9 字元<br>欄位名稱:`ISRBillerID` |
| `IsRemoveUnpricedValue` | 屬性 IsRemoveUnpricedValue(原文僅此說明) |
| `ISRType` | 設定或回傳 ISRType 值。(僅限瑞士)。<br>另請參閱:UseTax 欄位名稱:ISRBillerI。⚠ 原文疑似不一致:ISRType 的欄位名稱寫為 ISRBillerI,而 UseTax 的欄位名稱寫為 ISRType。 |
| `IssuePrimarilyBy` | 指定要依儲位(bin locations)或依序號/批次資訊來挑選要發料的序號或批次料號。 |
| `LetterHeaderinForeignLangu` | 設定或回傳列印單據中的頁首(以外語表示的公司全名)。<br>長度:100 字元<br>欄位名稱:`PrintHdrF` |
| `LocalCurrency` | 公司代碼的貨幣(國家貨幣),本地分類帳以此貨幣管理。公司使用本國貨幣向當地稅務機關申報。<br>Note:一旦在 SAP Business One 中為料號或其主檔記錄輸入了日記帳分錄,本國貨幣即不可修改。欄位名稱:MainCurncy。 |
| `ManagingDirector` | 設定或回傳 Managing Director(董事總經理)的姓名。<br>長度:100 字元<br>欄位名稱:`Manager` |
| `ManagingDirectorForeignLan` | 設定或回傳以外語表示的 Managing Director 姓名。<br>欄位名稱:`ManagerF`<br>長度:100 字元 |
| `MaxDaysForCancel` | 行銷單據過帳後可取消的最大天數。<br>欄位名稱:`CnclMaxDay` |
| `MaxHistory` | 決定記錄檔中保存的最大記錄數(預設:99)。記錄檔保存主檔記錄(料號、業務夥伴與 G/L 科目)及付款的變更歷史。<br>欄位名稱:`MaxHistory` |
| `MaximumNumberOfDaysForDueDate` | 屬性 MaximumNumberOfDaysForDueDate(原文僅此說明) |
| `MeasuringAccuracy` | 決定計量單位顯示的小數位數。此設定只影響顯示。系統一律精確計算到小數第六位。欄位名稱:MeasureDec。 |
| `MinimumAmountfor347Report` | 設定納入 347 Report 的最低申報門檻金額。西班牙特定。欄位名稱:MinAmnt347。 |
| `MultiCurrencyCheck` | 決定是否封鎖多幣別日記帳分錄(Multi Currency Journal Entry)。<br>欄位名稱:`MultiCurr` |
| `MultiLanguageSupportEnable` | 決定是否啟用多語言支援。<br>另請參閱:MultiLanguageTranslations。⚠ 原文疑似不一致:本屬性欄位名稱 MultiCurr 與 MultiCurrencyCheck 相同。<br>欄位名稱:`MultiCurr` |
| `NationalInsuranceNo` | 設定或回傳適用於銷售單據的國民保險號碼(national insurance number)。適用於 SHandleWT 設為 1 時。<br>長度:20 字元<br>欄位名稱:`NINum` |
| `OrderBlock` | 決定是否封鎖會自動產生日記帳分錄之單據(發票、貸項通知單、存款與付款)的過帳。欄位名稱:OrderBlock。 |
| `OrderingParty` | 設定 Payment Engine 附加元件(add-on)的 ordering party ID。欄位名稱:OrderParty。 |
| `OrganizationNumber` | 決定組織編號,用於付款產生器(payment generator)(部分歐盟國家特定)。<br>欄位名稱:`OrgNumber`<br>長度:100 字元 |
| `ParamFolderPath` | 設定或回傳 Parameters Folder Path(參數資料夾路徑)。欄位名稱:ParamPath。<br>長度:16 字元<br>欄位名稱:`ParamPath` |
| `PBSGroupNumber` | >決定 PBS 使用的客戶群組編號(丹麥特定)。(原文開頭即有「>」字元)<br>欄位名稱:`PBSGroupNo` |
| `PBSNumber` | 決定 PBS 使用的客戶識別號碼。(丹麥特定。)欄位名稱:PBSNumber。 |
| `PDefaultWTCode` | 設定或回傳適用於採購單據的預設預扣稅代碼。這是指向 WithholdingTaxCodes 物件的外鍵。適用於 WithholdingTax 設為 1 時。<br>長度:4 字元<br>欄位名稱:`PDfltWT` |
| `PDfltITWT` | 設定或回傳採購單據的預設 Income Tax WTax Code(所得稅預扣稅代碼)。⚠ 原文疑似不一致:本屬性欄位名稱 CrdCommUse 與 SDfltITWT、SetCommissionbyCustomer 相同。<br>欄位名稱:`CrdCommUse` |
| `PercentageAccuracy` | 設定或回傳百分比精確度。<br>欄位名稱:`PercentDec` |
| `PeriodStatusAutoChange` | 決定是否自動將 Period Status 變更為 'Closing Period'。<br>欄位名稱:`PStatAutCh` |
| `PeriodStatusChangeDelay` | 設定或回傳將 Period Status 變更為 'Closing Period' 的延遲天數。適用於 PeriodStatusAutoChange 屬性設為 Y 時。<br>欄位名稱:`PStatDelay` |
| `PhoneNumber1` | 設定或回傳公司的第一個預設電話號碼。<br>長度:20 字元<br>欄位名稱:`Phone1` |
| `PhoneNumber1ForeignLang` | 設定或回傳以外語表示的第一個預設電話號碼。長度:20 字元。<br>欄位名稱:`Phone1F` |
| `PhoneNumber2` | 設定或回傳第二個預設電話號碼。長度:20 字元。<br>欄位名稱:`Phone2` |
| `PhoneNumber2ForeignLang` | 設定或回傳以外語表示的第二個預設電話號碼。長度:20 字元。<br>欄位名稱:`Phone2F` |
| `PickList` | 表示建立揀貨清單(pick lists)時是否強制執行信用額度/承諾限額(credit\commitment limit)檢查。<br>欄位名稱:`PickLimit` |
| `PriceAccuracy` | 設定或回傳價格顯示的小數位數。此設定只影響顯示。系統一律精確計算到小數第六位。欄位名稱:PriceDec。 |
| `PriceListforCostPrice` | 決定毛利計算所依據的價格清單。<br>適用於 CalculateGrossProfitperTra 設為 1 時。<br>欄位名稱:`CostPrcLst` |
| `PriceProceedMethod` | 屬性 PriceProceedMethod(原文僅此說明) |
| `PriceSystem` | 決定是否允許選擇特定倉庫進行庫存重估(stock revaluation)。<br>此欄位僅適用於 ContinuousStockManagement 設為 1 時。<br>欄位名稱:`PriceSys` |
| `PrintingHeader` | 設定或回傳列印單據中的頁首(以本地語言表示的公司全名)。<br>長度:100 字元<br>欄位名稱:`PrintHeadr` |
| `PurchaseOrderConfirmed` | 決定是否允許將採購訂單帶入(drawing)目標單據。欄位名稱:PriceDec。⚠ 原文疑似不一致:此欄位名稱與 PriceAccuracy 相同。 |
| `QueryAccuracy` | 決定查詢中計算欄位的小數位數。<br>欄位名稱:`QueryDec` |
| `RateAccuracy` | 決定匯率顯示的小數位數。此設定只影響顯示。系統一律精確計算到小數第六位。<br>欄位名稱:`RateDec` |
| `RefreshInWhseQtyInDI` | 屬性 RefreshInWhseQtyInDI(原文僅此說明) |
| `RemoveUpdatePricesBasedOnNonStandardPriceLists` | 屬性 RemoveUpdatePricesBasedOnNonStandardPriceLists(原文僅此說明) |
| `ReportAccordingTo` | 屬性 ReportAccordingTo(原文僅此說明) |
| `RestrictDelNotesPO` | 決定當客戶套用限額且已超過時,是否封鎖交貨單據。欄位名稱:DlnLimit。 |
| `RestrictOrders` | 決定當客戶套用限額且已超過時,是否封鎖訂單。欄位名稱:OrderLimit。 |
| `RestrictSales` | 決定當客戶套用限額且已超過時,是否封鎖所有銷售單據。欄位名稱:SalesLimit。 |
| `ReuseDocumentNum` | 屬性 ReuseDocumentNum(原文僅此說明) |
| `ReuseNotaFiscalNum` | 屬性 ReuseNotaFiscalNum(原文僅此說明) |
| `RoundingMethod` | 決定行銷單據中的金額依貨幣或依單據進行四捨五入。欄位名稱:RoundMthd。 |
| `RoundTaxAmounts` | 決定行銷單據中的金額依貨幣或依單據進行四捨五入。欄位名稱:RoundMthd。⚠ 原文疑似不一致:說明與欄位名稱和 RoundingMethod 完全相同。 |
| `SalesOrderConfirmed` | 決定是否允許將銷售訂單帶入(drawing)目標單據。欄位名稱:RdrConfrmd。 |
| `SDefaultWTCode` | 設定或回傳適用於銷售單據的預設預扣稅代碼。這是指向 WithholdingTaxCodes 物件的外鍵。適用於 SHandleWT 設為 1 時。<br>欄位名稱:`SDfltWT`<br>長度:4 字元 |
| `SDfltITWT` | 設定或回傳銷售單據的預設 Income Tax WTax Code(所得稅預扣稅代碼)。⚠ 原文疑似不一致:本屬性欄位名稱 CrdCommUse 與 PDfltITWT、SetCommissionbyCustomer 相同。<br>欄位名稱:`CrdCommUse` |
| `SEPACreditorID` | 屬性 SEPACreditorID(原文僅此說明) |
| `Series` | 屬性 Series(原文僅此說明) |
| `ServiceCode` | 決定顯示機密資料時是否加密資料。資料包括:G/L 科目餘額、客戶與供應商帳戶餘額、價格清單,以及交易的毛利。選取時(Y),系統會顯示 Encoding Table 按鈕。使用者可按此按鈕開啟 Encoding 視窗設定編碼。可為表中每個字元定義不同的顯示字元。欄位名稱:UseCode。⚠ 原文疑似不一致:ServiceCode、ServicePassword、UserConversionCode 三者說明幾乎相同且欄位名稱皆為 UseCode(啟用值分別寫為 Y、1、Y)。 |
| `ServicePassword` | 決定顯示機密資料時是否加密資料。<br>資料包括:G/L 科目餘額、客戶與供應商帳戶餘額、價格清單,以及交易的毛利。<br>設為 1 時,系統會顯示 Encoding Table 按鈕。使用者可按此按鈕開啟 Encoding 視窗設定編碼。可為表中每個字元定義不同的顯示字元。<br>欄位名稱:`UseCode` |
| `SetCommissionbyCustomer` | 決定是否依單據中指定的客戶計算佣金。<br>欄位名稱:`CrdCommUse` |
| `SetCommissionbyItem` | 決定是否依料號計算佣金。<br>欄位名稱:`ItmCommUse` |
| `SetCommissionbySE` | 決定是否依銷售員計算佣金<br>欄位名稱:`SlpCommUse` |
| `SetItemsWarehouses` | 決定是否在使用者每次建立新料號時加入所有倉庫;並自動將新定義的倉庫加入既有料號。<br>欄位名稱:`AutoITW` |
| `SetResourcesWarehouses` | 屬性 SetResourcesWarehouses(原文僅此說明) |
| `SHandleWT` | 決定是否在銷售單據中啟用預扣稅。欄位名稱:SHandleWT。 |
| `SirenNo` | 屬性 SirenNo(原文僅此說明) |
| `SplitPO` | 決定是否拆分與多個倉庫相關的採購訂單。<br>若設為 yes,系統允許為每個倉庫建立個別的採購訂單。<br>欄位名稱:`RevisionPo` |
| `StandardUnitofLength` | 設定料號的預設長度計量單位。<br>在 SAP Business One 中定義新料號時,這些計量單位會自動顯示。另請參閱:Items。<br>欄位名稱:`DefLengthU` |
| `StartingInFiscalYear` | 屬性 StartingInFiscalYear(原文僅此說明) |
| `State` | 設定或回傳公司的州/省代碼。<br>欄位名稱:`State`<br>長度:3 字元<br>這是指向 States 資料表(OCST)的外鍵,該資料表未透過 DI API 公開。 |
| `SystemCurrency` | 系統與本國貨幣平行管理所有交易所使用的貨幣。<br>若系統貨幣與本國貨幣不同,必須定義匯率。在 Accounting 中,每張單據都會同時以系統貨幣與本國貨幣過帳。公司報表可同時以本國貨幣與系統貨幣產生。欄位名稱:BankCountr。⚠ 原文疑似不一致:此欄位名稱與 BankCountry 相同。 |
| `TaxCollection` | 設定或回傳一個有效值,決定是否徵收 VAT 稅。<br>欄位名稱:`VatCharge` |
| `TaxDefinition` | 決定此公司是否繳納 VAT。<br>欄位名稱:`PayOutVat` |
| `TaxDefinitionforVatitem` | 決定銷售料號時使用的預設稅務群組。<br>欄位名稱:`DfSVatItem` |
| `TaxDefinitionforVatservice` | 決定銷售服務時使用的預設稅務群組。<br>欄位名稱:`DfSVatServ` |
| `TaxGroupforPurchaseItem` | 決定採購料號時使用的預設稅務群組。<br>欄位名稱:`DfPVatItem` |
| `TaxGroupforServicePurchase` | 決定採購服務時使用的預設稅務群組。<br>欄位名稱:`DfPVatServ` |
| `TaxOffice` | 設定或回傳稅務機關名稱。<br>欄位名稱:`Revoffice` |
| `TaxPercentage` | 設定公司必須繳納與徵收的稅率百分比。以色列特定。<br>欄位名稱:`VatPrcnt` |
| `TaxRateDetermination` | 屬性 TaxRateDetermination(原文僅此說明) |
| `ThousandsSeparator` | 設定或回傳用來分隔千位的字元。美國與英國使用逗號;德語系國家一般使用點。<br>欄位名稱:`ThousSep` |
| `TimeTemplate` | 決定系統以 12 小時制或 24 小時制顯示時間。<br>欄位名稱:`TimeFormat` |
| `TotalsAccuracy` | 決定金額顯示的小數位數。此設定只影響顯示。系統一律精確計算到小數第六位。<br>欄位名稱:`SumDec` |
| `UniqueSerialNo` | 決定系統依哪個值將序號設為唯一。選項為:<br>0 - None<br>2 - Manufacturer Serial Number<br>3 - Serial Number<br>4 - Lot Number<br>欄位名稱:`SriUniqFld` |
| `UniqueTaxPayerReference` | 設定或回傳 Unique Taxpayer Reference(UTR)。英國特定。<br>欄位名稱:`TaxPayerRf`<br>長度:32 字元 |
| `UseNegativeAmounts` | 決定在沖銷交易中使用負數金額或借/貸切換(debit/credit switch)。此設定可隨時變更。<br>資料庫中的欄位名稱:NegAmount。 |
| `UseParentWIPInComponents` | 屬性 UseParentWIPInComponents(原文僅此說明) |
| `UsePASystem` | 決定是否在與永續盤存(continuous stock)相關的日記帳分錄中建立採購科目記錄。Note:一旦已產生日記帳分錄,此設定即不可變更。<br>欄位名稱:`UsePaSys` |
| `UseProductionProfitAndLossAccount` | 應用程式在庫存與生產之間元件交易的日記帳分錄中,使用 WIP 科目、庫存科目、WIP 抵銷科目與庫存抵銷科目。<br>欄位:UseProdPL。 |
| `UserConversionCode` | 決定顯示機密資料時是否加密資料。<br>資料包括:G/L 科目餘額、客戶與供應商帳戶餘額、價格清單,以及交易的毛利。<br>選取時(Y),系統會顯示 Encoding Table 按鈕。使用者可按此按鈕開啟 Encoding 視窗設定編碼。可為表中每個字元定義不同的顯示字元。<br>欄位名稱:`UseCode` |
| `UseTax` | 決定要列印在發票上的欄位。由瑞士的自動付款系統使用。選項包括:ISR = 含預印金額。<br>BISR = 含預印金額。<br>ISR+ = 金額欄位空白。<br>BISR+ = 金額欄位空白。<br>欄位名稱:`ISRType` |
| `WeightUnitDefault` | 設定 Item Master Data 表單中料號的預設重量計量單位。在 SAP Business One 中定義新料號時,這些重量單位會自動顯示。<br>欄位名稱:`DefWeightU` |
| `WholdingTaxDedHierarchy` | 決定是否使用預扣稅扣除層級(Withholding Tax deduction hierarch)。<br>以色列特定。<br>欄位名稱:`useDdctTrc` |
| `WithholdingTaxDdctExpired` | 設定或回傳預扣稅扣除的到期日。<br>欄位名稱:`DdctExpire` |
| `WithholdingTaxDdctOffice` | 設定或回傳預扣稅扣除辦公室的地址。<br>長度:100 字元<br>欄位名稱:`DdctOffice` |
| `WithholdingTaxPHandle` | 決定是否在採購單據中啟用預扣稅。將此欄位設為 'Y' 或 'N'。<br>欄位名稱:`PHandleWT`<br>長度:1 字元 |
| `WithholdingTaxTdctPercnt` | 設定或回傳預扣稅扣除百分比。<br>欄位名稱:`DdctPercnt` |
| `WithholdingTaxVendorDdct` | 決定是否存在供應商扣除(vendor deduction)。<br>欄位名稱:`VendorDdct` |
| `WithTax` | 設定必須從支付給公司之金額中預扣的稅率百分比。<br>欄位名稱:`IncomeTax` |
| `WTAccumAmountAP` | 設定以本國貨幣表示、由政府規定的付款(outgoing payments)WTax 過帳最低限額。<br>欄位名稱:`WTAccumAmt` |
| `WTAccumAmountAR` | 設定以本國貨幣表示、由政府規定的收款(incoming payments)WTax 過帳最低限額。<br>欄位名稱:`WTAccAmtAR` |
| `WTLiableExpense` | 未使用。欄位名稱:ExWTLiab。 |
| `XMLFileFolderPath` | 設定或回傳以 Export Form to XML 功能建立之 XML 檔案的路徑。<br>欄位名稱:`XmlPath` |

(^1) 來源(Source):SAP Business One SDK

> ⚠ 原文疑似不一致(admininfo 表格內,已於各列標註):
> - `BlockDelNotesforPurchase` 儲存格末尾多出單獨的「LDiscTotal」。
> - 欄位名稱重複:`CustomerIdNumber` 與 `CustomersDeductionatSource`(CustmrDdct);`DefaultBankAccountKey` 與 `DefaultBankNo`(DflBnkCode);`MultiLanguageSupportEnable` 與 `MultiCurrencyCheck`(MultiCurr);`PDfltITWT`、`SDfltITWT` 與 `SetCommissionbyCustomer`(CrdCommUse);`PurchaseOrderConfirmed` 與 `PriceAccuracy`(PriceDec);`SystemCurrency` 與 `BankCountry`(BankCountr);`ISRType` 的欄位名稱為 ISRBillerI,而 `UseTax` 的欄位名稱為 ISRType。
> - `RoundTaxAmounts` 的說明與欄位名稱與 `RoundingMethod` 完全相同;`ServiceCode`、`ServicePassword`、`UserConversionCode` 說明幾乎相同、欄位名稱皆為 UseCode。
> - 欄位名稱留白(原文為「Field name: .」):`AltNameForPurchase`、`DeductionFileNo`、`DefaultAccountCurrency`、`DefaultDunningTerm`。`AltNameForApInvoice` 的欄位名稱寫為「A/P Invoice」。

---

## SAP DI API / DI-API: Service Types

### DI-API: Service Types(`servicetypes.htm`)

〔圖片:sap-sdk〕

Service Types(^1)

原文表格有 Member / Description / Number 三欄,但 Description 欄全部空白。共 127 列,依原文順序列出:

| Member | Description | Number |
|---|---|---|
| `DepositsService` |  | 25 |
| `ExternalReconciliationsService` |  | 26 |
| `CheckLinesService` |  | 27 |
| `SeriesService` |  | 35 |
| `FormPreferencesService` |  | 41 |
| `BusinessPartnerPropertiesService` |  | 44 |
| `TransactionCodesService` |  | 45 |
| `ProfitCentersService` |  | 61 |
| `DistributionRulesService` |  | 62 |
| `ProjectsService` |  | 63 |
| `LandedCostsService` |  | 69 |
| `CreditLinesService` |  | 72 |
| `AlertManagementService` |  | 80 |
| `MessagesService` |  | 81 |
| `SalesOpportunityInterestsSetupService` |  | 98 |
| `SalesOpportunitySourcesSetupService` |  | 100 |
| `SalesOpportunityReasonsSetupService` |  | 102 |
| `AlternativeItemsService` |  | 107 |
| `PartnersSetupsService` |  | 108 |
| `SalesOpportunityCompetitorsSetupService` |  | 109 |
| `BranchesService` |  | 118 |
| `DepartmentsService` |  | 119 |
| `ApprovalStagesService` |  | 120 |
| `ApprovalTemplatesService` |  | 121 |
| `ApprovalRequestsService` |  | 122 |
| `CountriesService` |  | 129 |
| `StatesService` |  | 130 |
| `CashDiscountsService` |  | 133 |
| `PaymentBlocksService` |  | 159 |
| `ServiceCallStatusService` |  | 167 |
| `ServiceCallTypesService` |  | 168 |
| `ServiceCallProblemTypesService` |  | 169 |
| `EmployeeRolesSetupService` |  | 172 |
| `EmployeeStatusService` |  | 173 |
| `TerminationReasonService` |  | 174 |
| `ReportFilterService` |  | 180 |
| `BOELinesService` |  | 181 |
| `ServiceCallSolutionStatusService` |  | 188 |
| `ServiceCallOriginsService` |  | 192 |
| `DunningTermsService` |  | 196 |
| `EmployeePositionService` |  | 210 |
| `PredefinedTextsService` |  | 215 |
| `ReportLayoutsService` |  | 232 |
| `AccountCategoryService` |  | 238 |
| `BankChargesAllocationCodesService` |  | 239 |
| `CashFlowLineItemsService` |  | 242 |
| `DimensionsService` |  | 251 |
| `ServiceGroupsService` |  | 255 |
| `MaterialGroupsService` |  | 256 |
| `NCMCodesSetupService` |  | 257 |
| `BOEDocumentTypesService` |  | 267 |
| `BOEPortfoliosService` |  | 268 |
| `BOEInstructionsService` |  | 269 |
| `CustomsDeclarationService` |  | 283 |
| `AccountsService` |  | 1001 |
| `BusinessPartnersService` |  | 1002 |
| `MaterialRevaluationFIFOService` |  | 1006 |
| `ActivitiesService` |  | 1007 |
| `UserMenuService` |  | 2002 |
| `BankStatementsService` |  | 10000003 |
| `ExternalCallsService` |  | 10000020 |
| `BatchNumberDetailsService` |  | 10000044 |
| `SerialNumberDetailsService` |  | 10000045 |
| `InventoryPostingsService` |  | 10000071 |
| `FinancialYearsService` |  | 10000073 |
| `SectionsService` |  | 10000074 |
| `CertificateSeriesService` |  | 10000075 |
| `NatureOfAssesseesService` |  | 10000077 |
| `ReportTypesService` |  | 10000196 |
| `UnitOfMeasurementGroupsService` |  | 10000197 |
| `UnitOfMeasurementsService` |  | 10000199 |
| `BinLocationFieldsService` |  | 10000203 |
| `BinLocationAttributesService` |  | 10000204 |
| `WarehouseSublevelCodesService` |  | 10000205 |
| `BinLocationsService` |  | 10000206 |
| `ChangeLogsService` |  | 130000000 |
| `DNFCodeSetupService` |  | 140000041 |
| `ElectronicFileFormatsService` |  | 410000005 |
| `EmployeeTransfersService` |  | 480000001 |
| `DeductionTaxSubGroupsService` |  | 530000000 |
| `TaxCodeDeterminationsService` |  | 540000005 |
| `CostCenterTypesService` |  | 540000042 |
| `AccrualTypesService` |  | 540000048 |
| `NFModelsService` |  | 540000056 |
| `BrazilStringIndexersService` |  | 540000065 |
| `BrazilNumericIndexersService` |  | 540000066 |
| `BrazilFuelIndexersService` |  | 540000067 |
| `BrazilBeverageIndexersService` |  | 540000068 |
| `LegalDataService` |  | 540000073 |
| `TaxWebSitesService` |  | 1160000000 |
| `CockpitsService` |  | 1210000000 |
| `DashboardPackagesService` |  | 1210000008 |
| `GTIsService` |  | 1230000004 |
| `BlanketAgreementsService` |  | 1250000025 |
| `KPIsService` |  | 1320000000 |
| `TargetGroupsService` |  | 1320000002 |
| `ExtendedTranslationsService` |  | 1320000005 |
| `CampaignsService` |  | 1320000012 |
| `RetornoCodesService` |  | 1320000028 |
| `OccurrenceCodesService` |  | 1320000032 |
| `DepreciationTypesService` |  | 1470000000 |
| `FAAccountDeterminationsService` |  | 1470000002 |
| `DepreciationAreasService` |  | 1470000003 |
| `DepreciationTypePoolsService` |  | 1470000004 |
| `AssetClassesService` |  | 1470000032 |
| `IntrastatConfigurationService` |  | 1470000035 |
| `AssetGroupsService` |  | 1470000046 |
| `DeterminationCriteriasService` |  | 1470000048 |
| `AssetCapitalizationService` |  | 1470000049 |
| `GLAccountAdvancedRulesService` |  | 1470000057 |
| `AssetCapitalizationCreditMemoService` |  | 1470000060 |
| `BarCodesService` |  | 1470000062 |
| `InventoryCountingsService` |  | 1470000065 |
| `FixedAssetItemsService` |  | 1470000074 |
| `AssetManualDepreciationService` |  | 1470000075 |
| `EnhancedDiscountGroupsService` |  | 1470000077 |
| `AssetTransferService` |  | 1470000090 |
| `AssetRetirementService` |  | 1470000094 |
| `AssetDepreciationGroupsService` |  | 1470000220 |
| `AttributeGroupsService` |  | 1470000226 |
| `IntegrationPackagesConfigureService` |  | 1470000264 |
| `ServiceTaxPostingService` |  | 1520000001 |
| `EmployeeIDTypeService` |  | 1560000000 |
| `JournalEntryDocumentTypeService` |  | 1560000001 |
| `BrazilMultiIndexersService` |  | 1720000001 |
| `MaterialRevaluationSNBService` |  | 1980000001 |
| `TrackingNotesService` |  | 1980000009 |

(^1) 來源(原文德文 Quelle):SAP Business One SDK
