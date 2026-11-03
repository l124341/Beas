# Beas 範例操作手冊(繁體中文)

> 來源:`fetched/examples/`(Beas Script 2026.09 官方文件的 Examples 章節,共 53 頁)。
> 補充來源:`fetched/missing/`(14 頁:使用者事件、Configuration Wizard、DI-API itemprice、視窗事件等),用來把原先的推測改成依官方說明。標示「(官方說明)」的段落即來自這些頁面。
> 編排方式:依原文章節順序整理。**程式碼一律保留原文,不做修改**;說明文字翻譯為繁體中文。
> 原文頁面裡的截圖未收錄,手冊中以「(原文附截圖)」標示。
> 標示「⚠ 原文疑似筆誤」的地方,是翻譯時發現原文內容前後不一致之處,程式碼仍保持原樣,使用前請自行確認。

---

## 目錄

0. [使用前須知](#0-使用前須知)
1. [Beas 視窗(Window - Beas)](#1-beas-視窗)
2. [SAP 視窗(Window - SAP)](#2-sap-視窗)
3. [交易(Transactions)](#3-交易)
4. [生產(Production)](#4-生產)
5. [品質管制(Quality Control)](#5-品質管制)
6. [工具(Tools)](#6-工具)
7. [列印功能(Print functions)](#7-列印功能)
8. [連接器(Connectors)](#8-連接器)
9. [DI-API 範例](#9-di-api-範例)
10. [BSL(Beas Service Layer)範例](#10-bslbeas-service-layer範例)
11. [使用者事件(User events)說明](#11-使用者事件user-events說明)
12. [附錄:語法速查](#12-附錄語法速查)

---

## 0. 使用前須知

- 本手冊的範例都是**客製化範例**。
- 官方**不保證**範例在你使用的 Beas 版本中的功能或相容性,範例為求簡單,內容都盡量精簡。
- 客製化**沒有免費支援**,有問題請使用官方的 Q&A 論壇。
- 開啟 BeasScript 編輯器的方式(本手冊多次用到):
  - 選單:**工具(Tools)> Extension for Clients**,或
  - 快捷鍵:**CTRL+N+U**。
- 查看欄位名稱的方式:開啟 **檢視(View)> 系統資訊(System Information)**,按住 **CTRL** 並將滑鼠移到欄位上。

---

## 1. Beas 視窗

### 1.1 存取品項(欄位)值 — Accessing item values

**用途**:讀取並修改作業(Operation)的某個欄位值。

> 注意:這只是改變畫面上的值,**並未更新(儲存)**。若要儲存,必須搭配會觸發視窗更新的事件。

**操作步驟**

1. 從 **管理(Administration)> 設定(Setup)> 生產(Production)> 作業目錄(Operation catalog)** 開啟作業目錄。
2. 開啟任一作業。
3. 開啟 **檢視(View)> 系統資訊(System Information)**,切換到 **Item** 頁籤。
4. 按住 **CTRL**,將滑鼠移到要修改的欄位上。
5. 畫面會顯示該欄位需要存取的名稱。本例為 data window 1 的 `bez` 欄位。(原文附截圖)
6. 以 **工具 > Extension for Clients** 或 **CTRL+N+U** 開啟 BeasScript 編輯器。
7. 在右側面板找到該欄位,**按兩下**將它加入編輯器。
8. **讀取**:執行選取的那一行即可讀出欄位值。(原文附截圖)
9. **寫入**:執行選取的那一行即可寫入欄位值。(原文附截圖)

---

### 1.2 建立彈出訊息 — Creating a popup message

**用途**:開啟作業目錄時,由腳本跳出一則訊息。

**操作步驟**

1. 開啟 **作業目錄**(路徑同 1.1)。
2. 開啟 BeasScript 編輯器。
3. 在右側面板找到需要的事件,按兩下插入編輯器。(原文附截圖)
4. 在事件中加入以下程式碼:

   ```
   messagebox=Hello World!
   ```

   - 也可使用 **Script Assistant(腳本精靈)** 產生程式碼:開啟精靈 → 選擇指令 `message` → 勾選彈出訊息框並輸入文字。(原文附截圖)
5. 儲存並關閉編輯器。

開啟作業目錄時就會顯示訊息。訊息框的更多選項請參閱 `messagebox` 指令說明。

---

### 1.3 新增按鈕與點擊事件 — Add a button and click event

**用途**:在作業目錄上建立新按鈕,按下時顯示 HTML 格式的彈出訊息。

**操作步驟**

1. 開啟 **作業目錄**。
2. 開啟 BeasScript 編輯器。
3. 在右側面板找到 `form_loaded` 事件並按兩下,輸入:

   ```
   global function form_loaded

   create=button=name=testhtml,text=Test

   end global
   ```

   按鈕即被建立。(原文附截圖)

4. 為按鈕加入點擊事件:
   1. 開啟 **檢視 > 系統資訊**,然後按下新建立的按鈕,事件會顯示在 **Protocol** 頁籤。
   2. 編輯器開啟的狀態下,在該事件上按兩下,會打開正確的事件。
   3. 按 **OK**,事件即加入腳本編輯器。
   4. 加入要執行的程式碼(HTML 格式選項請參閱 `messagebox`):

   ```
   global function dw_master_item_button_testhtml_click

   messagebox=error$id=1234<tab>text=<html><h1>Message</h1>&

   <ul><li>List item 1</li><li>List item 2</li><li>List item 3</li></ul></html>&

   <tab>popup<tab>

   end global
   ```

按下按鈕後,會顯示 HTML 格式的訊息。(原文附截圖)

> 事件命名規則:`dw_master_item_button_<按鈕名稱>_click`,按鈕名稱需與 `create=button=name=...` 一致(小寫)。

---

### 1.4 欄位驗證 — Validate field

**用途**:在欄位變更時驗證「比對碼(matchcode)」只能是 3 個字元。

**操作步驟**

1. 開啟 **作業目錄** → 開啟任一作業。
2. 開啟 BeasScript 編輯器。
3. 在右側面板找到 **update 事件**,按兩下加入編輯器。
4. 加入程式碼:

   ```
   // ******* Event dw_1_update ********
   // Created 2019/01/29 by manager
   global function dw_1_update

   setvar=ll_length=%len(<dw_1.item.matchcode.value>)

   if <ll_length> <> 3 then

     messagebox=Lenght is not 3 characters.

   end if

   end global
   ```

按下 **更新(Update)** 按鈕且欄位長度不是 3 時,會顯示訊息。(原文附截圖)

- 變數建立方式請參閱 `setvar`。
- Data window 請參閱 `dw_1`(及 dw_2、dw_3…dw_7、dw_edit)。

> ⚠ 原文疑似筆誤:訊息文字 `Lenght` 應為 `Length`;程式碼保持原樣。

---

### 1.5 品項結構的提示框 — Item Structure tool tip

**情境**:在「結構檢視」中無法插入額外欄位。

**構想**:建立自訂提示框,使用者點擊某一列時顯示資訊。

**做法**

1. 這是列表型表單,必須建立與列表**重疊**的文字欄位,可用 `band=foreground`:

   ```
   dw_1.create=text=name=info_t<tab>text=Hello<tab>band=foreground<tab>width=600<tab>height=300
   ```

2. 變更背景。BeasScript 屬性在此無法使用,改用 **data window 語法**:

   ```
   dw_1.modify=info_t.background.color='<rgb:190:190:190>' info_t.background.mode=0 info_t.border=2 info_t.visible='0'
   ```

3. 攔截 Click 事件,依列類型 `<zeilentyp>` 顯示資訊。更多資訊請參閱「品項結構視窗(item structure window)」。

**完整客製化原始碼**

```
global function dw_1_load

dw_1.create=text=name=info_t<tab>text=Hello<tab>band=foreground<tab>width=600<tab>height=300

dw_1.modify=info_t.background.color='<rgb:190:190:190>' info_t.background.mode=0 info_t.border=2 info_t.visible='0'

end global

// Catch click event from the structure list

global function dw_1_click

dec x,y

x=<form.click.x> + 90

y=<form.click.y> + 60

dw_1.item.info_t.x=<x>

dw_1.item.info_t.y=<y>

// Click on normal item

if <dw_1.item.zeilentyp.value> = 0 then

  dw_1.item.info_t.visible=1

  sqlca.select "SuppCatNum" from "OITM" where "ItemCode" = <dw_1.item.itemcode.value,dbstring>

  dw_1.item.info_t.text=<itemcode><cr_lf><itemname><cr_lf><sqlca.result.1>

  return 1

end if

// all other: make unvisible

dw_1.item.info_t.visible=0

end global
```

**行為說明**:點到一般品項(`zeilentyp = 0`)時,在滑鼠位置右下方顯示提示框,內容為品項編號、品名與供應商目錄號;點到其他類型的列則隱藏提示框。

**品項結構視窗的列類型 `<dw_1.item.zeilentyp.value>`(官方說明)**

這個視窗(視窗檔 `artikel_struktur_browse`)是「結構檢視」,同一個列表會顯示不同來源的資料:

| 值 | 說明 |
|---|---|
| 0 | 品項主檔 |
| 3 | 物料清單(BOM)行 |
| 4 | 途程位置(Routing position) |
| 50 | 品項版本 |
| 51 | BOM 表頭 |
| 1001 | 平行資源;此時 `<dw_1.item.c_typ.value>` 可區分:`W`=工具、`H`=輔助工具、`R`=替代、`A`=平行 |

**開啟此視窗的方式(官方說明)**

```
openform=beas_ItemStructure
openwindow=artikel_struktur_browse
```

參數:`s_parm1`(或第一個參數)= 篩選類型,`s_parm2`(或第二個參數)= 該類型的引數。

| 篩選類型 | 說明 |
|---|---|
| `filter` | 設定起始篩選,使用者可在欄位標題上按右鍵覆寫 |
| `filterfix` | 設定固定篩選,使用者不能更改 |
| `item` | 開啟指定品項的結構 |

例:開啟單一品項

```
openform=beas_ItemStructure<tab>item<tab>FP
```

---

### 1.6 新增列印按鈕 — Add Button for Print

**用途**:在工單結構視窗右側加入「列印」按鈕,透過 **列印巨集(Print Macro)** 列印工單。

**前提**:必須先知道要列印哪一個巨集報表。請參閱 BeasScript 的 `MacroPrint` 與 `Create Button`。

```
// we use the loaded event to initialize Buttons
global function form_loaded

// Create button on right side. See Create
create=button=name=MyPrint,text=Print,position=1

end global

// the Button has object name "myPrint". for this i register this event
global function dw_master_item_myprint_click

// in this case create only preview. More see MacroPrint
printobject=makrostart=preview=fert_stuktur_browse=1000

return true

end global
```

**說明**:`position=1` 表示按鈕建立在右側;本例只產生預覽(`preview`)。

---

### 1.7 欄位變更與下拉選單 — Field changed, Dropdown

**情境**:品項主檔視窗的欄位變更處理。本範例包含:

- 在 DIN 欄位插入**下拉選單**,並檢查、轉成大寫。
- 檢查比對碼(match code)是否**唯一**。
- 處理 MSSQL 與 HANA 欄位名稱不同的情況。

使用的事件:`form_loaded`、`dw_1_item_xx_itemchanged`;使用 `sql`、`messagebox`。

```
global function form_loaded

// we create the dropdown and read the information if user click on dropdown icon

dw_1."ITEM"."U_beas_din"."DROPDOWN".select "U_beas_din" from "OITM" Group by "U_beas_din" order by "U_beas_din"

end event

global function dw_1_item_u_din_itemchanged

// Convert u_din automatically to upper case

// example: insert "abc", converted autoamtically to "ABC"

dw_1.item.u_din.value=<dw_1.item.u_din.value,upper>

end global

global function dw_1_item_u_match_itemchanged

// Match code changed. System check, if this matchcode is in use (but not in own itemcode)

// The field "U_beas_match" exists only in HANA. In MSSQL the name is "U_match"

// but in SQL statements the system convert it always in correct format

// we use "select ... into :variable to read the result

// Note: use always "<..,dbstring> for convert string in correct SQL format.

select count(*) into :ll_count from "OITM" where "U_beas_match" =<dw_1.item.u_match.value,dbstring> and "ItemCode" <> <str_parm.s_parm1,dbstring>

if <ll_count> n> 0 then

  // if entry found: search for first entry with item name

  select top 1 "ItemCode","ItemName" into :ls_itemcode,:ls_itemname from "OITM" where "U_beas_match" =<dw_1.item.u_match.value,dbstring> and "ItemCode" <> <str_parm.s_parm1,dbstring>

  messagebox=error$**$The match code "<dw_1.item.u_match.value> is current in use. First item with this match code: "<ls_itemcode>" "<ls_itemname>"

  dw_1.item.u_match.value=""

end if

global function dw_1_item_u_znr_itemchanged

// third letter must be always a "-"

// if user insert "abc", the system convert it automtacally to "ab-c"

if  <dw_1.item.u_znr.value,mid 3 1> <> "-" then

  dw_1.item.u_znr.value=<dw_1.item.u_znr.value,left 2>-<dw_1.item.u_znr.value,mid 3>

end if

end global
```

**重點說明**

| 事件 | 作用 |
|---|---|
| `form_loaded` | 建立下拉選單,內容為 `OITM.U_beas_din` 的不重複值(使用者點下拉圖示時讀取) |
| `dw_1_item_u_din_itemchanged` | 輸入 `abc` 自動轉成 `ABC` |
| `dw_1_item_u_match_itemchanged` | 檢查比對碼是否被**其他品項**使用,若是則顯示錯誤並清空欄位 |
| `dw_1_item_u_znr_itemchanged` | 第 3 個字元必須是 `-`,輸入 `abc` 會自動變成 `ab-c` |

**事件觸發時機(官方說明)**

| 事件 | 時機與用途 |
|---|---|
| `form_loaded` | 在視窗開啟、**資料載入之後**、視窗**顯示之前**執行。最適合用來新增或變更按鈕,或新增、變更欄位屬性(參考 BeasScript 的 `create` 指令) |
| `dw_1_item_[object]_itemchanged` | 欄位內容變更時呼叫,為 **Post Event**(變更之後才觸發) |

**注意**:

- 欄位 `U_beas_match` 只存在於 HANA,MSSQL 中叫 `U_match`;但在 SQL 語句中系統會自動轉成正確格式。
- SQL 內的字串請**一律使用 `<…,dbstring>`** 轉成正確的 SQL 格式。

> ⚠ 原文疑似筆誤:原文程式碼中第一個函式以 `end event` 結尾、`dw_1_item_u_match_itemchanged` 缺少 `end global`;`autoamtically`、`automtacally` 為英文拼字錯誤。程式碼保持原樣,實際使用時請補齊結尾。

---

### 1.8 選擇框 — ChooseBox

**用途**:開啟簡易的選擇視窗並取回輸入的值。(詳見 `choosebox` 指令)

```
tools=choosebox=title=my ChooseBox/&

   PreName=S/&

   SecondName=S/&

   FromDate,!From Date=D/&

   Item=W==select "ItemCode","ItemName" from "OITM"

// Canceled?

if <return> = -1 then

  return -1

end if

// Display value

messagebox=result: <aa:PreName>, <aa:SecondName>
```

**欄位型別說明(依範例推得)**:`S` = 文字、`D` = 日期、`W` = 以 SQL 查詢結果作為選項。
**取回值**:`<aa:欄位名稱>`。使用者取消時 `<return>` 為 -1。

---

### 1.9 UDF 當作數字或日期 — UDF as Number or Date

- UDF(使用者自訂欄位)**永遠是字串(String)型別**。可以當作日期欄位使用,但系統只會把輸入的日期格式化為區域設定格式。

在 `itemchanged` 事件中:

```
dw_1.item.udf1.value=<dw_1.item.udf1.value,date>
```

這會把字串轉成日期,再轉回依區域格式顯示的字串。

要與其他日期比較時,可轉成數字,格式 `yyyymmdd`(例如 2021/10/05 = `"20211005"`):

```
if <dw_1.item.udf1.value,yyyymmdd> n< <today,yyyymmdd> then

  messagebox=Date must be in future

  return -1

end if
```

運算子 `n<`(以及 `n>`、`n=`、`n>=`、`n<=`)會**把兩邊都轉成數字**再比較,可據此建立邏輯判斷。

---

### 1.10 禁止手動輸入但保留下拉 — Disable editing without lock

**情境**:欄位有下拉選單,想禁止手動輸入、**只允許選下拉項目**。

Beas 沒有對應的屬性,但可以用 **DataWindow 語法**:

```
dw_1.modify=udf1.edit.displayonly=Yes
```

結果:下拉選單仍可使用,但鍵盤手動輸入被鎖定。

**`DisplayOnly` 與條件式控制(官方說明)**

- `Yes`:不允許使用者輸入資料,欄位僅供顯示。
- `No`:(預設)允許使用者輸入。
- 若要依條件控制欄位能不能編輯,請改用 **`Protect`** 屬性。

**`column.edit` 可用屬性一覽(官方說明,內容摘自 PowerBuilder 原始文件)**

| 屬性 | 說明(值為 Yes / No 者,Yes 表示啟用) |
|---|---|
| `AutoHScroll` | 輸入或刪除資料時,編輯控制項是否自動水平捲動 |
| `AutoSelect` | 取得焦點時是否自動選取全部內容 |
| `AutoVScroll` | 輸入或刪除資料時,是否自動垂直捲動 |
| `Case` | 文字大小寫:`Any`(任意)、`Upper`(轉大寫)、`Lower`(轉小寫) |
| `CodeTable` | 欄位是否有代碼表 |
| `DisplayOnly` | 欄位是否僅供顯示(見上) |
| `FocusRectangle` | 欄位取得焦點時,是否在目前列周圍顯示虛線焦點框 |
| `Format` | 編輯控制項的顯示格式字串,可為 DataWindow 運算式 |
| `HScrollBar` | 是否顯示水平捲軸 |
| `Limit` | 最多可輸入字元數(0 到 32,767,0 = 不限制) |
| `Name` | 欄位所用的預先定義編輯樣式名稱 |
| `NilIsNull` | 使用者留空時,是否把值設為 null |
| `Password` | 是否採用密碼顯示(輸入的字元顯示為 `*`);變更時建議同時調整 `Format` |
| `Required` | 欄位是否必填 |
| `Style` | (僅限查詢)回傳欄位的編輯樣式 |
| `UseEllipsis` | 資料太長時是否截斷並顯示省略號(欄位有焦點時不顯示) |
| `ValidateCode` | 是否用代碼表驗證使用者輸入的值 |
| `VScrollBar` | 是否顯示垂直捲軸 |

> 修改方式為 `dw_1.modify=<欄位>.edit.<屬性>=<值>`,例如本節的 `udf1.edit.displayonly=Yes`。

---

## 2. SAP 視窗

### 2.1 建立按鈕 — Create Button

```
windowevent open

create button=name=mybutton,text=My Button,x=200,y=305,width=100

end event

windowevent itempressed beas_lyt

messagebox=info$$You clicked my Button

end event
```

- `open` 事件中建立按鈕,指定名稱、文字、位置(x, y)與寬度。
- `itempressed` 事件在按下按鈕時顯示訊息。

---

### 2.2 建立右鍵選單項目 — Create Right Click Entry

**用途**:建立右鍵選單項目(**僅限 SAP 表單**)。函式說明請參閱 `Rightclickmenu_SBO`。

```
windowevent prerightclick

beasgui=rightclickmenu=1=<spr:My Entry=myentry1=<global:system_home>bmp\formel.png=3===message=Click on my entry

end event

// Example: Work with Event

windowevent prerightclick

beasgui=rightclickmenu=1=<spr:My Entry=myentry1=<global:system_home>bmp\formel.png=3

end event

windowevent premmenuclick myentry1

message=Click on my Entry

end event
```

**兩種寫法**

1. 在 `prerightclick` 中直接帶入點擊後要執行的指令(`...===message=Click on my entry`)。
2. 在 `prerightclick` 只建立項目,另外用 `premmenuclick <項目名稱>` 事件處理點擊。

---

## 3. 交易

### 3.1 清空庫存 — Clean stock(JBS 範例)

此 JBS 範例可清空庫存。可將它存成專案資料夾內的原始檔,從不同地方呼叫。

```
#jbs

#include "issuetools.jbs"; // Load from this file

cleanStock(String Warehouse,String ItemCode); // execute the function
```

**參數**

- `Warehouse`:要清空的倉庫(**必填**)。
- `ItemCode`:要清空的品項;留空則清空該倉庫所有品項。

> **注意**:Beas 會刪除 Beas 的保留(Reservation),但**無法刪除 SAP 的保留**。若存在 SAP 保留,系統無法為該品項建立發料,並會回傳錯誤訊息。

**呼叫範例**

```
#jbs

#include "issuetools.jbs";

cleanStock("01","RM_S_N");
```

**完整函式原始碼**

```
// Clean up Stock for warehouse or/and itemcode

// 3.6.2020 M.Heigl

// ***********************************************

// Declare as local instance

let cleanStock=function cleanStock(e_WhsCode,e_ItemCode ) {

if (e_WhsCode == "" ) {

   alert("deleteStock(): Please define warehouse");

   return -1;

}

// ItemCode is empty: Load stock from all items from this warehose

// Note: The system delete reservation by SQL statement. Not the best solution, but current no other function available

if (e_ItemCode != "" ) {

   let stock=bsl.get ("WareHouseStock(:e_WhsCode)?$filter=ItemCode = :e_ItemCode and OnHand > 0");

   SQLStatement.execute("delete from \"BEAS_RESERVATION_LINE\" where \"ItemCode\"=:e_ItemCode and \"WhsCode\"=:e_WhsCode");

   }

// Item is defined. Load only for this item

else {

   let stock=bsl.get ("WareHouseStock(:e_WhsCode)?$filter=OnHand > 0");

   SQLStatement.execute("delete from \"BEAS_RESERVATION_LINE\" where \"ItemCode\"=:e_ItemCode ");

   }

if (stock.rowcount() == 0) return 1;

// Create JSON as String (not object)

// alternative it's possible to create a JSON Object.

// Note: You don't need to group the items for Batch/Serial numbers. The system will do this automatically.

let ls_transaction="";

for (let ll_stock=1;ll_stock <= stock.rowcount();ll_stock++){

   stock.setrow(ll_stock);

   if (ll_stock > 1)      ls_transaction+=",";

   ls_transaction += '{"ItemCode":"'+stock.itemcode+'","Quantity":'+stock.onhand.toString()+',"Whscode":"'+e_WhsCode+'",'+

         '"SysNumber":'+stock.sysnumber+',"DistNumber":"'+stock.batch+'","BinCode":"'+stock.bincode+'","IVersionId":"'+stock.iversionid+'"}';

   }

ls_transaction='{"DocumentLines":['+ls_transaction+']}';

// Execute BSL Post command

// Second post parameter can be a json string or a variable

let b=bsl.post("Issue",ls_transaction);

if (b.returnCode) != 0

  alert(b.returnMessage+"  "+b.returnResult);

return 1;

};
```

**運作流程**

1. 檢查倉庫是否有填;沒填則提示並結束。
2. 用 BSL 的 `WareHouseStock` 讀出庫存(有指定品項則只讀該品項,且 `OnHand > 0`)。
3. 以 SQL 刪除 Beas 保留資料表 `BEAS_RESERVATION_LINE` 內的對應記錄(原文註明這不是最佳解法,但目前沒有其他函式)。
4. 組出 JSON 字串(批次/序號不需先分組,系統會自動處理)。
5. 以 BSL `Issue` 的 POST 發料;失敗時顯示錯誤。

> ⚠ 原文疑似問題:
> - 清空**全部品項**的分支(`else`)中,`delete` 語句使用了 `:e_ItemCode`,但此時品項為空,條件應為倉庫 `:e_WhsCode`。
> - `if (b.returnCode) != 0` 缺少外層括號,語法上不完整。
> 以上程式碼均保持原文,正式使用前請先測試。

---

### 3.2 收貨後處理 — after receipt

**用途**:收貨完成後 (1) 變更批次狀態 (2) 列印報表。

使用的使用者事件(User event):

- `materialmangement.receipt`、`workordermanagement.receipt`
- `materialmanagement.receiptend`、`workordermangement.receiptend`
- 相關 BSL:`BatchNumber`

**`ReceiptEnd` 事件的可用變數(官方說明,皆為唯讀)**

`workordermanagement.ReceiptEnd`(文件建立之後執行):

| 變數 | 說明 |
|---|---|
| `e_WoDocEntry` | 工單號 |
| `e_WoLineNumber` | 工單位置 |
| `e_ReceiptDocEntry` | 收貨單 Document Entry |
| `e_ReceiptDocNumber` | 收貨單單號 |
| `e_Transaction` | 日記帳交易編號(`OJDT.Transid`) |
| `e_TimeReceiptEntry` | 連結的時間回報編號(`BEAS_ARBZEIT.BUCHNR_ID`) |

`stockmanagement.ReceiptEnd`:

| 變數 | 說明 |
|---|---|
| `e_DocEntry` | 建立的文件 Document Entry |
| `e_DocNumber` | 建立的文件單號 |
| `e_Transaction` | 日記帳交易編號(`OJDT.Transid`) |

**範例 1:僅當品項群組 9 設為「N」時,把批次狀態改為 0**

```
function ReceiptEnd

instance bsl b

instance datastore dtw

dtw=(v2)select  "IBT1"."ItemCode","OBTN"."SysNumber" &

   from "IBT1"  &

   inner join "OBTN" on "OBTN"."ItemCode"="IBT1"."ItemCode" and "OBTN"."DistNumber"="IBT1"."BatchNum"  &

   inner join "OITM" on "OITM"."ItemCode"="IBT1"."ItemCode"  and "OITM"."QryGroup9" <>'Y' &

   where "IBT1"."BaseType"=59 and "IBT1"."BaseEntry"=<e_ReceiptDocEntry> ;

for loop = 1 to <dtw.rowcount>

   dtw.setrow=<loop>

   b.put=BatchNumber("<dtw.itemcode>",<dtw.sysnumber>)<tab>{"Status":"1"}

   if <b.ret_code> <> 1 then

       messagebox=error$$<b.ret_text>

   end if

next

destroy=dtw

end function
```

**範例 2:把工單位置的 UDF 2 存入批次屬性 1(`OBTN.MnfSerial`)**(檔案 `workordermanagement.src`)

```
function ReceiptEnd

instance bsl b

instance datastore dtw

select "UDF2" into :mnfSerialInfo from "BEAS_FTPOS" where "BELNR_ID"=<e_belnr_id> and "BELPOS_ID"=<e_belpos_id>

dtw=(v2)select  "IBT1"."ItemCode","OBTN"."SysNumber" &

   from "IBT1"  &

   inner join "OBTN" on "OBTN"."ItemCode"="IBT1"."ItemCode" and "OBTN"."DistNumber"="IBT1"."BatchNum"  &

   inner join "OITM" on "OITM"."ItemCode"="IBT1"."ItemCode"  and "OITM"."QryGroup9" <>'Y' &

   where "IBT1"."BaseType"=59 and "IBT1"."BaseEntry"=<e_ReceiptDocEntry> ;

for loop = 1 to <dtw.rowcount>

   dtw.setrow=<loop>

   b.put=BatchNumber("<dtw.itemcode>",<dtw.sysnumber>)<tab>{"BatchAttribute1":<mnfSerialInfo,bsl>}

   if <b.ret_code> <> 1 then

       messagebox=error$$<b.ret_text>

   end if

next

destroy=dtw

end function
```

> ⚠ 原文疑似不一致:範例 1 的說明寫「設為 0」,程式碼卻是 `{"Status":"1"}`;兩處皆保持原文,請依實際需求(Beas 批次狀態代碼)確認。

> ⚠ 變數名稱:範例 2 使用 `e_belnr_id`、`e_belpos_id`,但官方事件頁列出的工單變數是 `e_WoDocEntry`、`e_WoLineNumber`。官方使用者事件說明提到舊版名稱仍然有效(相容性超過 99%),所以 `e_belnr_id` 很可能是舊名稱;新寫的腳本建議使用官方列出的變數名。這是依文件推論,未實測。

---

## 4. 生產

### 4.1 工單序號/批號產生 — Create Wo Serial/Batch Numbers

在使用者事件 `workordermangement.GetBatchNumber` 與 `workordermangement.GetSerialNumber` 中,可撰寫腳本來產生工單**收貨/序號保留**時所用的批號或序號。

**範例 1:批號 = 今天日期(yyyymmdd)+ "-" + 當日流水號**
例:`20200501-0001`、`20200501-0002`……

```
function GetBatchNumber

select coalesce(max(substring("DistNumber",10,4)),0) into myCounter from "OBTN" where left("DistNumber",9) = '<today,yyyymmdd>-'

setvar=myCounter=%numadd(myCounter,1)

setvar=myCounter=000<myCounter,#0>

setvar=e_DistNumber=<today,yyyymmdd>-<myCounter,right 4>

end function
```

**範例 2:前綴 `B05C` + 5 位數自訂計數器;物料群組為 `ZPT` 的品項使用獨立計數器**

```
function GetBatchNumber

// define instance from beas Service Layer

instance bsl boy_b

// Read Material Group from item

boy_b.get=Item(<e_itemcode,bsl>)/MaterialGroupId/$value

// if Material Group is ZPT then read Counter G otherwise C

if <boy_b.ret_value> = "ZPT" then

  setvar=boy_counter=G

else

  setvar=boy_counter=C

end if

// new serial Number is B05C + Counter with 5 digits

setvar=boy_counter=00000<system.counter.boy_counter_[boy_counter]>

setvar=e_DistNumber=B05C<boy_counter,right 5>

return success

end function
```

**範例 3:物料群組為 20 時,手動詢問序號;其餘使用日期+流水號**

```
function GetSerialNumber

instance bsl boy_b

// Read Material Group from item

boy_b.get=Item(<e_itemcode,bsl>)/MaterialGroupId/$value

// if Material Group is ZPT then read Counter G otherwise C

if <boy_b.ret_value> = "20" then

  input=SerialNumber=e_DistNumber=<e_DistNumber>

  return true

end if

// for all other use a item based counter

select coalesce(max(substring("DistNumber",10,4)),0) into myCounter from "OBTN" where left("DistNumber",9) = '<today,yyyymmdd>-'

setvar=myCounter=%numadd(myCounter,1)

setvar=myCounter=000<myCounter,#0>

setvar=e_DistNumber=<today,yyyymmdd>-<myCounter,right 4>

end function
```

**範例 4:每張工單一個序號,格式為「工單號(8 碼)+ "-" + 該工單計數器(4 碼)」**
計數器存放在工單的 **UDF4**。

```
// ******* Event GetSerialNumber ********

// Created 2021/08/24 by Martin Heigl

function GetSerialNumber

// Get UDF4 from work order

instance bsl b

b.get=Workorder(<e_DocEntry>)/UDF4/$value

setvar=ls_counter=%numadd(b.ret_value,1)

// save new counter back (using SQL, because BSL current not available

sql=update "BEAS_FTHAUPT" set "UDF4"='<ls_counter,#0>'

// create new serial number

setvar=e_DocEntry=000000<e_DocEntry>

setvar=e_Counter=000000<ls_counter,#0>

setvar=e_DistNumber=<e_DocEntry,right 8>-<e_Counter,right 4>

end function
```

**重點**:這些函式透過設定變數 `e_DistNumber` 回傳新的批號/序號;`<變數,right N>` 取右邊 N 碼,用來補零。

> ⚠ 原文疑似問題:範例 3 的程式碼註解沿用範例 2 的文字(ZPT / G / C);範例 4 的 `sql=update "BEAS_FTHAUPT" set "UDF4"=...` 沒有 `where` 條件,正式使用前請確認會不會更新到所有工單。程式碼保持原文。

---

### 4.2 建立工單 — Create work order

以 BSL 建立工單:

```
instance bsl b

b.post=Workorder={"WorkorderPos":[{"ItemCode":"FP","Quantity":2}]}

if <b.ret_code> = -1 then

   messagebox=Error create work order : <b.ret_value>

else

  messagebox=Work order <b.ret_value> created

end if
```

建立品項 `FP`、數量 2 的工單;失敗時顯示錯誤,成功時顯示新工單編號(`b.ret_value`)。

---

### 4.3 變更 WMS 生產線 — Change WMS Production line

**情境**:在 `CreateWorkOrderPositionEnd` 事件中,把工單位置的 WMS 生產線,改為「第一個途程(Routing)位置所用資源」上定義的生產線。

**原文建議**

- 能用**倉庫規則(Warehouse Rules)**就用。
- 能用 **Beas Service Layer** 就用。
- 若有 Beas Service Layer 指令可用,**不要直接改標準欄位**;若對 Beas 資料表使用 update,官方**無法提供任何保證**。

```
function CreateWorkOrderPositionEnd

// Get Warehouse from Work order Position. We use always BSL if possible,

instance bsl b

b.get=WorkorderPos(<e_WoDocEntry>,<e_WoLineNumber>)/WhsCode/$value

setvar=ls_whscode=<b.ret_value>

// Then we get the warehouse type. BSL is perfect for this work

b.get=WareHouse(<ls_whscode,bsl>)/WhsBinType/$value

setvar=ls_binType=<b.ret_value>

// we want to check only WMS Warehouse (typ 3)

if <ls_binType> <> 3 then

  return success

end if

// Get WMS Produdction Line from resource from first Routing position

// Note: You must sort by "SortId", not "POS_ID"

// we use SQL, because not all fields available in BeasServiceLayer

sql.select top 1 "BEAS_APLATZ"."WMS_PRODLINE" into ls_wmsprodline &

   from "BEAS_FTAPL" inner join "BEAS_APLATZ" on "BEAS_APLATZ"."APLATZ_ID"="BEAS_FTAPL"."APLATZ_ID" &

   where "BEAS_FTAPL"."BELNR_ID"=<e_WoDocEntry> and "BEAS_FTAPL"."BELPOS_ID"=<e_WoLineNumber> order by "SortId"

// Note: Update on Standard fields not good

// But in this case: This field don't have additional functionality in Beas and BSL PUT is not available

sql.update "BEAS_FTPOS" set "WMS_PRODLINE"=<ls_wmsprodline,dbstring> where "BELNR_ID"=<e_WoDocEntry> and "BELPOS_ID"=<e_WoLineNumber>

end function
```

**流程**

1. 用 BSL 取得工單位置的倉庫代碼。
2. 用 BSL 取得該倉庫的儲位類型(`WhsBinType`)。
3. 只處理 **WMS 倉庫(類型 3)**,其他直接結束。
4. 用 SQL 取第一個途程位置(依 **`SortId`** 排序,**不是** `POS_ID`)的資源之 `WMS_PRODLINE`(因為 BSL 沒有提供此欄位)。
5. 用 SQL 更新工單位置的 `WMS_PRODLINE`(原文說明:此欄位在 Beas 中沒有額外功能,且沒有 BSL PUT 可用,所以例外使用 update)。

---

### 4.4 批號保留 — Batch number reservation

**用途**:在**建立工單**時預留新的批號;**收貨**時使用該預留批號。

- 批號 = 工單 Document Entry + 目前日期時間(`yyyymmddhhmm`)。
- 步驟:(a) 建立工單時把新批號存進工單位置 (b) 收貨流程取用預留批號。

需要兩個事件:

- `CreateWorkOrderPositionEnd`:位置建立後呼叫。
- `workordermangement.GetBatchNumber`:回傳下一個批號。

使用者事件定義方式請參閱 `UserEvent`;相關 BSL:`Workorder`。檔案:`workordermanagement.src`

```
// Change GetBatchNumber function

function GetBatchNumber

// BatchNumber defined?

select "CHARGE_ID" into :e_DistNumber from "BEAS_FTPOS"  where "BELNR_ID"=<e_DocEntry> and "BELPOS_ID"=<e_LineNumber>

if <e_DistNumber> <> "" then

 return true

end if

// Create new Batch number

setvar=e_DistNumber=<e_DocEntry><e_LineNumber><today,yyyymmdd><now,hhmm>

return true

end function

// Work order creation: Get BatchNumber and save it in work order position

function CreateWorkOrderPositionEnd

// get next BatchNumber

instance bsl b

// Get next batch number

b.get=WorkorderPos/GetNextBatchNumber(<e_WoDocEntry>,<e_WoLineNumber>)

// save in work order position Batch number field

sql.update "BEAS_FTPOS" set "CHARGE_ID"=<b.value,dbstring> where "BELNR_ID"=<e_WoDocEntry> and "BELPOS_ID"=<e_WoLineNumber>

end function
```

**流程**

- `CreateWorkOrderPositionEnd`:呼叫 BSL `GetNextBatchNumber`(它會觸發 `GetBatchNumber`)取得新批號,寫入 `BEAS_FTPOS.CHARGE_ID`。
- `GetBatchNumber`:若工單位置已有批號(`CHARGE_ID` 非空)就直接沿用;否則依「工單號+行號+日期+時間」產生新批號。

---

### 4.5 自訂視窗建立工單 — Own Window Create Work order

**用途**:建立自己的**強制回應(modal)視窗**,並在其中建立工單:

- 定義新視窗。
- 變更視窗屬性。
- 建立工單。

**兩種實作方式**

1. **單一腳本**:所有功能都定義在同一個腳本中(目標視窗的所有函式)。
2. **兩個腳本**:第 1 個腳本負責開啟視窗,第 2 個腳本放在新視窗內處理邏輯。

本範例說明的是「單一腳本」做法。相關項目:BSL `Workorder`、WindowEvents `form_opened`、JBScript 的 `openwindow`、`SQL`、`messagebox`。

> 說明:原文頁面此處**沒有附程式碼**,僅有概述。

**`form_opened` 事件(官方說明)**:在視窗**框架開啟之後**、**文件開啟之前**執行,用來管理視窗上方的頁籤(新增、變更等)。

---

### 4.6 結案工單 — Close work orders

**用途**:在工單結構視窗右側建立「Close(結案)」按鈕。按下後,會結案**目前及所有已勾選**的工單與工單列。

本範例涵蓋:建立按鈕、使用 SQL、使用 BSL 的 GET 與 POST、讀取選取的列。(相關:BeasServiceLayer、DataWindow、setvar functions)

**完整腳本**

```
global function form_loaded

// create button on right side with text "Close"

create=button=name=CloseOrder,text=Close order,position=1

end global

global function dw_master_item_closeorder_click

for ll_row=1 to <dw_1.rowcount>

  if <dw_1.isselect:[ll_row]> = true then

      if <dw_1.item.zeilentyp.value> n= 0 then

           closeworkorder()

      end if

      if <dw_1.item.zeilentyp.value> n= 10 then

           closeworkorderpos()

      end if

      if <return> n= -1 then

         return -1

      end if

  end if

next

retrieve

function closeworkorder

// read work order number from defined row

setvar=ll_DocEntry=<dw_1.item.belnr_id:[ll_row].value>

// Work with SQL: with into you can define the target Variable

// in bracket you can define the number. #0 => no decimal places

//select coalesce("ABGKZ",'N') into :ls_closed from "BEAS_FTHAUPT" where "BELNR_ID"=<ll_DocEntry,#0>

//if <ls_closed> = J then

//  return 1

//end if

// Better way: BSL- check close flag. is 1 or 0

instance bsl b

b.get=Workorder(<ll_DocEntry,#0>)/Closed/$value

if <b.ret_value> = 1 then

  return 1

end if

// Close work order

b.get=Workorder/Close(<ll_DocEntry,#0>)

if <b.ret_code> = -1 then

  messagebox=error$<b.ret_text>

  return -1

end if

// Close return separate status. If -1, then error.

if <b.ret_status> n= -1 then

  messagebox=error$<b.error>

  return -1

end if

dw_1.item.abgkz:[ll_row].value=J

end function

function closeworkorderpos

instance bsl b

// read work order pos number from defined row

setvar=ll_DocEntry=<dw_1.item.belnr_id:[ll_row].value>

setvar=ll_LineNumber=<dw_1.item.belpos_id:[ll_row].value>

// if position closed, then ignore

b.get=Workorder(<ll_DocEntry,#0>)/Closed/$value

if <b.ret_value> = 1 then

  return 1

end if

// Close work order Position

b.get=WorkorderPos/Close(<ll_DocEntry,#0>,<ll_LineNumber,#0>)

if <b.ret_code> = -1 then

  messagebox=error$<b.ret_text>

  return -1

end if

// Close return separate status. If -1, then error.

if <b.ret_status> n= -1 then

  messagebox=error$<b.error>

  return -1

end if

dw_1.item.abgkz:[ll_row].value=J

end function

end global
```

**運作說明**

1. `form_loaded`:在右側建立「Close order」按鈕。
2. 按鈕被按下時,逐列檢查是否被選取(`dw_1.isselect`):
   - 列類型 `zeilentyp = 0` → 結案**工單**(`closeworkorder`)。
   - 列類型 `zeilentyp = 10` → 結案**工單位置**(`closeworkorderpos`)。
   - 任一步回傳 -1 就中止。
3. 全部處理完呼叫 `retrieve` 重新整理畫面。
4. 兩個子函式都先用 BSL 檢查是否已結案(`Closed = 1` 則略過),再呼叫 BSL 的 `Close`;出錯會顯示訊息並回傳 -1,成功則把該列 `abgkz` 設為 `J`。

> ⚠ 原文疑似問題:`closeworkorderpos` 檢查「是否已結案」時讀的是 `Workorder(...)/Closed`(工單層級),不是位置層級;此外 `closeworkorder`、`closeworkorderpos` 及 `end global` 的巢狀結構在原文中也較為鬆散。程式碼保持原文。

---

### 4.7 時間回報規則 — TimeReceipt rules

**情境**:**若物料尚未完整發料,不允許建立時間回報(time receipt)。**

時間回報可從很多地方建立:TimeReceipt、桌面終端機(Desktop Terminal)的 LogOff、桌面應用程式、WEB 應用程式,或直接透過 Beas Service Layer。逐一修改所有視窗要耗費大量時間,因此可以直接在**功能本身**內插入自己的腳本,與前端畫面無關 —— 這就是**使用者事件(User event)**。最適合的事件是 **`TimeRecording`**。(原文附說明影片)

**`TimeRecording` 事件(官方說明)**:系統要建立時間回報時觸發,可以**取消流程**,你可以在這個事件中加入自己的規則。可用變數(皆為唯讀):

| 變數 | 說明 |
|---|---|
| `e_WoDocEntry` | 工單號(僅數字) |
| `e_WoLineNumber` | 工單位置行號 |
| `e_WoLineNumber2` | 作業(Operation)行號 |
| `e_Employee` | 人員編號(`pers_id`) |
| `e_Quantity` | 良品數量(倉庫單位) |
| `e_Scrap` | 報廢數量(倉庫單位) |

另外,`ReceiptStart` 事件是在**建立工單收貨文件之前**執行,可用 `return failure` 取消該工單位置的處理(變數:`e_WoDocEntry`、`e_WoLineNumber`)。注意:若這是**倒沖(backflushing)**流程的一部分,取消收貨時,系統**無法回復**倒沖流程中已完成的發料。

**行為**

- 允許建立時間回報**只有在物料完整發料**時(**不支援部分發料**)。
- **伺服器模式**(server mode):只回傳錯誤訊息並取消流程。
- **桌面模式**(例如終端機):以 HTML 訊息框列出所有尚未發完的行,並取消流程。

> **前提工作流程**:在建立**第一筆**時間回報之前,必須先手動完整發出所有物料清單(BOM)的料。
> 此腳本**不檢查手動收貨**。另外可再檢查是否為最後一個位置(參考 `bsl.workorderrouting`),或改在「組裝收貨」事件中檢查。

使用技術:datastore values、Beas Service Layer、BSL 與 BeasScript。檔案:`workordermanagement.src`

```
function TimeRecording

// check count of entries which not booked completely

// we use beas service layer

instance datastore d

d.bsl=WorkorderBom?$select=ItemCode,QuantityWhsUoM,UoMWarehouseId,QuantityBooked&$filter=DocEntry eq <e_WoDocEntry> and LineNumber eq <e_WoLineNumber> and QuantityWhsUoM gt QuantityBooked and Closed eq false

// if no entry found, then all is ok and we can continue

if <d.rowcount> n= 0 then

  // don't forget to destroy the created datastore

  destroy=d

  return success

end if

// if server mode (example Beas service layer or  WEB App, we return only an error message

if <system.servermodus> = true then

  messagebox=error$$Not all lines booked completely!

  destroy=d

  return failure

end if

// we create a messagebox with all lines, which not booked as html message

setvar=ls_html=<html><body>Follow lines not booked completely:</br></br><table><tr><td>Item Number</td><td>Planned Quantity</td><td>Booked Quantity</td></tr>

// create the lines

// Note: All field names must be lower case.

for ll_line=1 to <d.rowcount>

  d.setrow=<ll_line>

  addvar=ls_html=<tr><td><d.itemcode></td><td><d.quantitywhsuom> <d.uomwarehouseid></td><td><d.quantitybooked>  <d.uomwarehouseid></td></tr>

next

// don't forget to destroy the created datastore

destroy=d

addvar=ls_html=</table></br>It is not possible to create a time receipt before complete material is booked!</body></html>

// Display the message

messagebox=info$popup=true<tab>text=<ls_html>

// Cancel the process

return failure

end function
```

**重點**

- 用 `datastore` 以 BSL 的 `$filter` 找出「計畫量 > 已發量」且未結案的物料行。
- 沒有未發完的行 → `return success`,繼續流程。
- 有未發完的行 → 伺服器模式回傳錯誤並 `return failure`;桌面模式顯示 HTML 表格,再 `return failure`。
- datastore 用完要記得 `destroy`。
- datastore 取出的欄位名稱一律使用**小寫**(例如 `<d.itemcode>`)。

---

### 4.8 手動發料:檢查最大數量 — Manual Issue: Check Max. Quantity

**情境**:工單的「手動發料」視窗,對工單 BOM 位置**不會**檢查最大允許數量(此檢查只在「物料發料視窗」有效)。可用 BSL `WorkorderBoM/CheckMinMaxBom` 進行檢查。

**構想**:使用者變更數量時就檢查。檔案:`mw_manuell_buchen.src`

```
// ******* Event dw_1_item_menge_itemchanged ********

global function dw_1_item_menge_itemchanged

// Work order - Bill of Material position must be defined

if <dw_1.item.belnr_id.value> n> 0 and <dw_1.item.belpos_id.value> n> 0 and <dw_1.item.pos_id.value> n> 0 then

// Execute BSL function

instance bsl b

b.get=WorkorderBom/CheckMinMaxBom(<dw_1.item.belnr_id.value>,<dw_1.item.belpos_id.value>,&

  <dw_1.item.pos_id.value>,<dw_1.item.pos_id.value>:<dw_1.item.menge.value,dbnum>,0,false)

// if locked, then set quantity to 0

if <b.Status> = locked then

  dw_1.item.menge.setvalue=0

end if

destroy=b

return true

end global
```

**說明**:當工單號、BOM 位置與 pos_id 都已指定時,呼叫 BSL 檢查;回傳狀態為 `locked`(超出限制)時,把數量重設為 0。

> ⚠ 原文疑似筆誤:`CheckMinMaxBom` 的第 4 個參數寫成 `<dw_1.item.pos_id.value>:<dw_1.item.menge.value,dbnum>`,第 1 個 `if` 缺 `end if`。程式碼保持原文。

---

### 4.9 手動收貨:檢查最小與最大數量 — Manual Receipt: Check Min. and Max. Quantity

**情境**:工單「手動收貨」視窗不會檢查組裝收貨的**最小數量**(此檢查只在「物料發料視窗」有效)。可用 BSL `WorkorderBoM/CheckMinMax`(程式碼中實際呼叫 `CheckMinMaxBom`)進行檢查。

**構想**:輸入收貨數量時,檢查發料數量。

**檢查內容**

- 需求數量 = 本次輸入數量 + 過去所有收貨數量。
- 依據此數量,系統要換算出計畫數量,再據以算出最小與最大數量。

檔案:`mw_manuell_buchen.src`

```
global function dw_1_item_menge_itemchanged

dec lc_bookedQty

// Work order - Bill of Material position must be defined

if <dw_1.item.belnr_id.value> n> 0 and <dw_1.item.belpos_id.value> n> 0 and <dw_1.item.pos_id.value> n= 0 and <dw_1.item.menge.value> n> 0 then

   // Get booked Quantity

   // Parameter with [] because inside placeholder

   // comma in placeholder not allowed. Use | as comma

   lc_bookedQty=<bsl.WorkorderPos([dw_1.item.belnr_id.value]|[dw_1.item.belpos_id.value]).QuantityProduced/$value>

   lc_bookedQty=<lc_bookedQty,dbnum> + <dw_1.item.menge.value,dbnum>

   // Execute BSL function

   instance bsl b

   b.get=WorkorderBom/CheckMinMaxBom(<dw_1.item.belnr_id.value>,<dw_1.item.belpos_id.value>,0,"",<lc_bookedQty,dbnum>,true)

   // if locked, then set quantity to 0

   if <b.Status> = locked then

     dw_1.item.menge.setvalue=0

   end if

   destroy=b

end if

end global
```

(此範例與 [10.3 佔位符與函式呼叫](#103-bsl佔位符與函式呼叫) 內容相同,該節有較完整的逐點說明。)

---

### 4.10 從工單開啟銷售訂單 — Open Sales Order from Workorder

**用途**:在工單文件中放入一個「金色箭頭」。使用者點擊後,開啟連結的銷售訂單。(原文寫「opens the linked Work order」,依程式碼實際是開啟銷售訂單。)

**限制**:

- 僅適用於由「銷售訂單」或「銷售訂單轉生產單」建立的工單。
- **不支援**「集合訂單(Collective order)」、「合併工單(Merge work order)」。

```
global function form_loaded

item.auftrag.detailbitmap=opensalesorder()

end global

global function opensalesorder

// Read from work order position the linked Document

// Note: This is only working for SalesOrder to Production orders.

// Pre Invoice or other base documents not supported in this example

select top 1 "BEAS_FTPOS"."DocEntry" into :ll_docEntry &

  from "BEAS_FTPOS" where "BEAS_FTPOS"."BELNR_ID" = <dw_1.item.belnr_id.value,#0> and "BEAS_FTPOS"."DocEntry">0

if <ll_docEntry> n> 0 then

  openwindow=fo_Order=<ll_docEntry>

end if

end global
```

**說明**:`form_loaded` 時在 `auftrag` 欄位旁放上箭頭圖示並綁定函式;點擊後從工單位置讀取連結的單據號碼,再用 `openwindow=fo_Order=<單號>` 開啟。

---

### 4.11 列印工單 — Print Work order

參見 [7.3 列印工單](#73-列印工單--print-work-order)與 [7.4 工單建立後自動列印](#74-工單建立後自動列印)。

---

## 5. 品質管制

### 5.1 自動設定報廢 — Set Scrap automatically

**情境**:在「量測樣本 QC」視窗中,若量測值超出範圍,自動設定「封鎖原因(Blockage reason)」。

本例只針對 QC 步驟「**Humidity**」檢查;若超出定義的量測範圍,自動把報廢原因設為 `OOR`。

```
global function dw_2_item_messwertc_itemchanged

// we ignore all, if not Humidty

if <dw_1.item.qs_id.value> <> Humidty then

  return 1

end if

// read maesurement

setvar=lc_value=<dw_2.item.messwertc.value,#.000000>

// Delete block reason

dw_2.item.sperrgrundid.setvalue=

// check if outside the rang

if <lc_value,#.000000> n< <dw_1.item.minimal.value,#.000000> or <lc_value,#.000000> n> <dw_1.item.maximal.value,#.000000> then

  // check only, if minimal not 0 and value not 9

  if <lc_value,#.000000> n> 0 and <dw_1.item.minimal.value,#.000000> <> 0 then

    dw_2.item.sperrgrundid.setvalue=OOR

  end if

end if

end global
```

**邏輯**

1. QC 步驟不是 Humidity → 結束。
2. 讀量測值(以 6 位小數格式),先清除封鎖原因。
3. 量測值**小於最小值**或**大於最大值** → 再確認「量測值 > 0 且最小值不為 0」才設定封鎖原因 `OOR`。

> ⚠ 原文疑似筆誤:`Humidty` 應為 `Humidity`(原文程式碼與說明文字拼法不同);註解 "check only, if minimal not 0 and value not 9" 與實際條件(`value > 0`)不同。程式碼保持原文,請以實際 QC 步驟名稱為準。

---

## 6. 工具

### 6.1 設定精靈範例 — Configuration Wizard example

**用途**:在「設定精靈(Configuration wizard)」中建立自己的設定項目(自訂 Customizing Configuration wizard)。

檔案:專案資料夾中的 `system_opionen.ini`

```
1→U→status→bitmap=menu_administration→Demo

2→G→status→V→Work order type→demoworkordertype→→select:fttyp::emptyrow=true

2→G→status→J→Allow to change the type→demoallowchange→N

2→I→In next line you can insert the lines

2→G→helptext=Count of days for my special setting→T→Count of days→demodays=0

2→IA→visible=demodays=0   height=280   It's important to define count of days

2→B→buttontext=Items with standard prices   View   opennewwindow=artikel_struktur_browse.psr<wintab>s_parm1=filterfix<wintab>s_parm2="OITM"."EvalSystem"='S'
```

> 其中的 `→` 代表**定位字元(Tab,字元 9)**;原始檔以 Tab 分隔欄位(官方說明),文件頁面把它顯示成 `→`。實際寫檔時請使用真正的 Tab 字元。

**在腳本中取用某個設定**:

```
if <setup:demoallowchange> <> J then

  messagebox=Changes not allowed

end if
```

更多屬性資訊請參閱官方說明中的「properties」。

**定義檔格式(官方說明)**

設定精靈(視窗 `system_option_struktur`)可用來:

- 在標準設定精靈或其他標準設定視窗(例如站台設定)中**新增參數**。
- 建立**完全自訂**的設定視窗。

定義檔**每一行定義一個設定**,分隔符號為 **Tab(字元 9)**。每行分成兩個區域:前半是擴充功能,後半是值與下拉選項。

*基本欄位*

| 欄位 | 說明 |
|---|---|
| Level(層級) | 1 = 主層級 |
| Type(類型) | 設定的種類(見下表) |

| Type | 說明 |
|---|---|
| `U` | 標題 |
| `G` | 全域設定(Global) |
| `E` | 站台設定(Station) |
| `I` | 資訊框 |
| `IA` | 資訊框,紅色邊框,表示要注意 |
| `B` | 按鈕 |

*擴充設定(Extended settings)*

| 項目 | 說明 |
|---|---|
| `status` | 顯示狀態圖示 |
| `topic` | 連結到標準說明 |
| `helptext` | 顯示在頁尾區域的文字 |
| `buttontext` | 按鈕文字(類型為 `B` 時使用) |
| `bitmap` | 圖片(僅限主層級、類型 `U`)。圖片需存放在 `...bmp\[設計類型]` 資料夾,例如 `bmp\goldenthread`;寫法 `bitmap=menu_administration` |
| `height` | 文字高度 |
| `visible` | 顯示條件,見下方 |

`visible` 的寫法(`設定名稱=x` 或 `設定名稱<>x`,x 可為值、`empty`,或用 `;` 分隔的多個值):

| 寫法 | 意義 |
|---|---|
| `visible=myVariable=empty` | 僅當 `myVariable` **未定義**時顯示 |
| `visible=myVariable=A` | 變數必須是 `A` |
| `visible=myVariable=A;B;C` | 變數是 `A`、`B` 或 `C` 其中之一 |

*輸入類型(Input type)*

| Input type | 說明 |
|---|---|
| `T` | 文字、數字(自由輸入欄位) |
| `V` | 值清單(List of Values) |
| `J` | 核取方塊:是(J)或否(N) |

後面依序是 Description(說明)、setup name(設定變數名稱)、Default Value(預設值)、Values(值清單)。Values 可以是 select 指令或值清單,例如:

| 寫法 | 說明 |
|---|---|
| `select:fttyp::emptyrow=true` | 可選擇工單類型 |
| `select:whscode::emptyrow=true` | 可選擇倉庫代碼 |
| `select "ItemCode","Description" from "BEAS_APL_HEADER"` | 可選擇途程計畫 |

**開啟設定視窗的方式(官方說明)**

```
opennwewindow=system_option_struktur<tab>s_parm1=system_optionen.ini
```

> ⚠ 官方頁面此處寫成 `opennwewindow`,應為 `opennewwindow`(本文件其他範例用的是 `opennewwindow`),程式碼保持原文。

| 參數 | 說明 |
|---|---|
| `s_parm1` | 設定檔名稱。標準設定精靈用 `system_optionen.ini`。系統會掃描標準資料夾,再掃描所有專案與 Add-on 資料夾 |
| `as_variable` | 若有定義,直接跳到這個變數 |
| `s_parm3` | 視窗標題 |
| `s_parm5` | 以字串直接給定設定定義(此時 `s_parm1` 要設成 `""`) |

**對照本範例各行**

| 列 | 意義 |
|---|---|
| `1→U→status→bitmap=menu_administration→Demo` | 第 1 層標題 `Demo`,顯示狀態圖示,圖片 `menu_administration` |
| `2→G→status→V→Work order type→demoworkordertype→→select:fttyp::emptyrow=true` | 全域設定,值清單,說明「Work order type」,設定名稱 `demoworkordertype`,選項來自工單類型,允許空白列 |
| `2→G→status→J→Allow to change the type→demoallowchange→N` | 全域設定,核取方塊,設定名稱 `demoallowchange`,預設 `N` |
| `2→I→In next line you can insert the lines` | 資訊框(說明文字) |
| `2→G→helptext=Count of days for my special setting→T→Count of days→demodays=0` | 全域設定,頁尾說明文字,文字輸入;設定名稱欄位在原文顯示為 `demodays=0` |
| `2→IA→visible=demodays=0   height=280   It's important to define count of days` | 紅框注意訊息,高度 280;依 `visible` 規則,條件是 `demodays=0` |
| `2→B→buttontext=Items with standard prices   View   opennewwindow=artikel_struktur_browse.psr<wintab>s_parm1=filterfix<wintab>s_parm2="OITM"."EvalSystem"='S'` | 按鈕,文字「Items with standard prices」,按下後開啟品項結構視窗並套用固定篩選 `EvalSystem='S'` |

> 說明:最後一行按鈕的欄位順序(buttontext、顯示文字、動作)官方頁面沒有進一步拆解,以上為原文照列;`demodays=0` 中 `=0` 的確切意義(預設值或條件)官方頁面也沒有說明,請自行測試確認。

---

## 7. 列印功能

### 7.1 概述 — Print functions

Beas 使用 **Crystal Report** 列印報表與標籤。支援的列印方式:

- 直接使用 Crystal。
- 透過**列印巨集(Print Macros)**使用 Crystal —— 使用簡單,可減少腳本行數並支援更多變化。
- 簡易的**工單列印**函式。

在 WEB 解決方案中也可使用 Beas 內部的列印方案。

目前使用的印表機在 **站台精靈(station wizard)→ 位置設定(location settings)** 中定義,也有相同的可能性。

使用**列印服務(print service)**可透過 **Beas Common Service** 列印,不會阻塞你的 HTTP 伺服器。

---

### 7.2 用 Crystal 列印 — Print with Crystal

在 BeasScript 中用 `printobject` 建立報表很簡單。

> 注意:所有腳本內都有「選擇印表機」的範例。
> 注意:若要使用內部 **Crystal 11 檢視器**,必須連接報表(connectioninfo)。
> 在 Web 環境中,系統會自動採用 **站台精靈 → 位置** 中定義的印表機。

**範例 1:設定印表機並列印**(同時使用了 `crreport` 指令)

```
printobject=openreport=myreport.rpt

// Define parameter

printobject=setvar=myparam1=<paramvalue1>

printobject=setvar=myparam2=<paramvalue2>

// set printer (exact that name, which is defined in the Windows printer definitions)

tools=printer=getdriversport=name

printobject=crreport=selectprinter=<value_printerdriver>

// and print out

printobject=crreport=printout
```

**範例 2:列印單一報表、2 份**

```
// open the report

printobject=openreport=<myfile.prt>

// define all parameters

printobject=setvar=myparam1=<paramvalue1>

printobject=setvar=myparam2=<paramvalue2>

// Define count of copies (need beas 9.3 PL 3 HF 1 or newer)

printobject=copies=2

// Print out

printobject=crreport=printout
```

> 設定份數(`copies`)需要 **Beas 9.3 PL 3 HF 1 或更新版本**。

**範例 3:匯出並列印**

```
// load report from variable "filename"

printobject=openreport=<filename>

// and save (export with question about format)

printobject=crreport=exportoption=destinationtype=disk

printobject=crreport=exportoption=diskfilename=c:\temp\myreport.pdf

printobject=crreport=exportoption=formattype=pdf

printobject=crreport=export=true

// and export

printobject=crreport=printout
```

---

### 7.3 列印工單 — Print Work order

列印工單有多種方式:

1. 從銷售訂單用 **B1UP 巨集**列印
2. 在**工作清單(Pool List)**用 BeasScript 列印
3. 工單**建立後自動列印**(使用者事件)
4. **WEB APP** 透過 Beas Service Layer 列印

**範例:用目前的印表機列印工單,並把工單標示為已列印**(三種呼叫寫法)

```
 beasFunction(PrintWorkOrder|DocEntry|LineNumber|print||true);

 application.beasFunction("PrintWorkOrder",DocEntry,LineNumber,"print","",true);

 beasFunction=PrintWorkOrder=DocEntry=LineNumber=print==true
```

更多資訊請參閱 `beasFunction`。

**`PrintWorkorder` 參數(官方說明)**

呼叫格式:B1UP 為 `beasFunction(FunctionName|Parameter);`,BeasScript 為 `beasfunction=functionname=parameter`。

| 函式 | 參數 | 說明 |
|---|---|---|
| `PrintWorkorder` | `DocEntry,LineNumber [,print,printername,setprintFlag]` | 預覽或列印指定工單位置 |

依官方的參數順序,範例 `print||true` 的意思是:第 3 個參數 `print` = 直接列印(省略則為預覽)、第 4 個參數印表機名稱留空(使用目前印表機)、第 5 個參數 `true` = 設定「已列印」旗標。(第 3、5 個參數各值的確切意義,官方頁面只給了參數名稱,以上為依範例的解讀。)

Beas 標準函式(`beasFunction`)清單:

| 函式 | 參數 | 說明 |
|---|---|---|
| `CalculateAPS` | APSScenario-Id | 計算指定的 APS 情境 |
| `CalculateMRP` | MRP-Id | 計算指定的 MRP 情境 |
| `PrintWorkorder` | 見上 | 預覽或列印工單位置 |

#### 方式 1:從銷售訂單用 B1UP 巨集

通用函式的原始碼:

```
@STORE1=SQL(select top 1 cast("BELNR_ID" as varchar(20))+'|'+cast("BELPOS_ID" as varchar(20)) from "BEAS_FTPOS" where "BaseDocNum"=$[$8.0.NUMBER])

IF(@STORE1='') BEGIN

  MessageBox(No work order linked!);

END

IF(@STORE1<>'') BEGIN

    BeasFunction(PrintWorkorder|@store1);

END
```

**說明**:查出此銷售訂單連結的工單(工單號|位置號);沒有則提示「No work order linked!」,有則呼叫 `PrintWorkorder`。

#### 方式 2:Pool List 用 BeasScript

操作:**Pool list → 開啟(Open)→ 視窗「Operation Edit」→ 新增按鈕「Print Work order」**。檔案:`fert_Afo_anmelden.src`

```
global function form_loaded

create=button=name=printworkorder,text=Print Work order

end global

global function dw_master_item_button_printworkorder_click

beasfunction=PrintWorkorder=<str_parm.belnr_id>=<str_parm.belpos_id>

end global
```

#### 方式 3:建立後自動列印(使用者事件)

構想:工單建立後,系統直接列印工單文件。詳見下一節 [7.4](#74-工單建立後自動列印)。

#### 方式 4:WEB APP 透過 Beas Service Layer

WEB 應用程式(或其他服務)要列印工單時:

```
odata4/WorkorderPos/Print(6002479,10,"print","",false)
```

機器端可以這樣送出請求(範例):

```
woPrint/6002482/10
```

---

### 7.4 工單建立後自動列印

**構想**:工單建立之後,系統直接列印工單文件。

**最佳方案**:Beas 使用者事件 **`CreateWorkOrderPositionComplete`**。這個事件會對**每一個新的主工單位置**呼叫。列印時必須讀出所有子位置:BSL `WorkorderPos/GetSubPositions`。

**`CreateWorkOrderPositionComplete` 事件(官方說明)**

- 執行時機:建立新的工單位置**並完成時間計算之後**。此時位置、途程與物料清單都已存進資料庫。
- **只對主層級呼叫,不包含子位置**(所以才需要用 BSL 另外讀出子位置)。

| 變數 | 讀 | 說明 |
|---|---|---|
| `e_WoDocEntry` | ✔ | 建立的工單號 |
| `e_WoLineNumber` | ✔ | 行號 |
| `e_Project` | ✔ | 專案編號 |
| `e_ItemCode` | ✔ | 此工單位置的品項代碼 |
| `e_Refresh` | ✔ | 是否為**更新(refresh)模式**。更新模式下,系統會為了同步變更而**並行建立一份暫時的工單,不存進資料庫**;`true` = 更新模式、`false` = 建立模式 |

檔案:`workordermanagement.src`

```
function CreateWorkOrderPositionComplete

// Use BSL for get Work order positions

instance bsl b

b.get=WorkorderPos/GetSubPositions(<e_WoDocEntry>,<e_WoLineNum>)

// Read positions

setvar=e_positions=<b.value>

[loop]

// Get next Work order position

tools=token=e_positions=,

setvar=e_line=<value>

if <e_line> = "" then

  goto=exitloop

end if

// preview work order

beasfunction=PrintWorkorder=<e_WoDocEntry>=<e_line>

goto=loop

[exitloop]

// Destroy

destroy=b

end function
```

**流程**:用 BSL 取得所有子位置清單(逗號分隔)→ 用 `tools=token` 逐個取出 → 每一個位置呼叫 `PrintWorkorder` → 取完離開迴圈並 `destroy`。

> ⚠ 變數名稱:範例中的 `<e_WoLineNum>` 在官方事件頁列出的變數是 **`e_WoLineNumber`**(官方事件頁與本範例頁都有這個不一致)。程式碼保持原文,使用前請改成 `e_WoLineNumber` 並實際測試。
>
> ⚠ 更新模式:官方變數說明指出,`e_Refresh = true` 時處理的是「暫時、未存進資料庫」的工單。範例沒有檢查 `e_Refresh`;若你的環境會觸發更新模式,建議自行在腳本開頭判斷(例如 `e_Refresh` 為 true 就直接結束),避免對暫時的工單執行列印。這是依官方變數說明的建議,範例本身未處理,也未實測。

---

## 8. 連接器

**Connectors**:此區放的是「第三方應用程式與 Beas 之間通訊」的各種範例。

> 說明:原文頁面僅有此標題與一句簡介,**未提供具體範例內容**。

---

## 9. DI-API 範例

這些範例使用 **SAP DI-API**。通用寫法:

1. `declare=<名稱>=ue_api_sbo` 宣告物件。
2. `getbusinessobject=<物件類型>` 取得 SAP 商業物件(例如 59 = 收貨、67 = 庫存移轉、20 = 採購收貨 PO、22 = 採購訂單)。
3. 設定欄位、明細(`lines=…`)。
4. `add` 新增(或 `update` 更新),檢查 `ret_code`。
5. `destroy` 釋放物件。

### 9.1 含使用者自訂欄位的收貨 — Goods Receipt with UF

> 注意:能用 Beas Service Layer 建立收貨就盡量使用它。

下例建立一張收貨,並把文字寫入使用者自訂欄位 `u_test2`:

```
//Create goods receipt with UserFields

declare di_doc=ue_api_sbo

di_doc.getbusinessobject=59

di_doc.lines=itemcode=RM

di_doc.lines=quantity=1

di_doc.lines=userfield=U_test2=N

di_doc.add

if <di_doc.ret_code> = -1 then

  return failure

end if

messagebox=info$document created: <di_get.getnewobjectkey>

destroy=di_doc
```

> ⚠ 原文疑似筆誤:最後的訊息使用了 `<di_get.getnewobjectkey>`,物件名稱應為 `di_doc`。程式碼保持原文。

---

### 9.2 取得品項價格 — Get Price from Item

Beas 有多種方式取得品項價格:

1. 僅使用 **SAP DI-API**
2. 使用具擴充功能的 **Beas 物件**
3. **Beas Service Layer**

#### 方式 1:僅用 SAP DI-API

使用 SAP B1 標準功能讀取價格。這是 SAP 原始提供的函式,功能有限(請參閱 DI-API `itemprice`)。

```
declare=itemprice=ue_api_sbo

itemprice=getitemprice=itemcode=FP<tab>cardcode=110441

destroy=itemprice

message=<currency> <price> <discount>

destroy=itemprice
```

**`itemprice` 物件說明(官方說明)**

- 回傳品項的**價格、幣別與折扣**。
- 使用的是 SAP DI-API 原生函式,**沒有 Beas 額外功能**。
- 支援價格表、特殊價格表、**一般協議(blanket agreement)**與 **UoM 群組**。
- 執行後可從三個字串變數取回結果:`price`、`currency`、`discount`。
- 需要完整的 Beas 功能時,請改用 Beas Service Layer 的 `item.GetPrice()`。

可設定的屬性:

| 屬性 | 說明 |
|---|---|
| `itemcode` | 品項代碼 |
| `cardcode` | 業務夥伴 |
| `uomentry` | UoM(官方頁面此列說明為「quantity in uom: uomquantity」) |
| `uomquantity` | 數量所使用的 UoM(官方頁面此列說明為「UoM for which quantity is defined」) |
| `inventoryquantity` | 以庫存單位表示的數量 |
| `currency` | 希望取得的幣別 |
| `date` | 價格日期 |
| `pricelist` | 價格表 |
| `blankedagreementnumber` | 協議編號 |
| `blankedagreementline` | 協議行 |

> ⚠ 官方屬性表中 `uomentry` 與 `uomquantity` 的兩段說明看起來互相對調(`uomentry` 應為 UoM、`uomquantity` 應為數量);此處照原文列出。

#### 方式 2:Beas 物件(擴充功能)

這是以 SAP DI-API `GetItemPrice` 為基礎、具備完整 Beas 功能的 BeasScript 物件,功能更多。請參閱 `ue_artikelstamm.pricing`。

```
object=ue_artikelstamm=pricing=itemcode=A001
```

#### 方式 3:Beas Service Layer

可以透過 Beas Service Layer 取得含 Beas 擴充功能的價格。它以 `ue_artikelstamm.pricing` 為基礎,但可以用簡單的服務方式使用。請參閱 `Item/GetPrice`。

```
Item/GetPrice(ItemCode,WhsCode,CardCode,QuantityInStockUnit,Valuation)
```

回傳:

```
{

    "price": "20.9814"

}
```

```
// beas Script Example: Return calculation price

instance bsl b

b.get=Item(<itemcode>,<whscode>,<cardcode>,<quantity>,"K")

messagebox=&ltb.price>
```

> ⚠ 原文疑似筆誤:方式 1 範例中 `destroy=itemprice` 出現兩次;最後一行 `&ltb.price>` 應為 `<b.price>`;BSL 範例的呼叫方式與上面列出的 `Item/GetPrice(...)` 簽章不完全一致。程式碼保持原文。

---

### 9.3 含批號的庫存移轉 — Inventory transfer with batchnum

先做一次收貨,再建立含批號的庫存移轉單(從倉庫 01 移到 02):

```
// First receipt

declare=di_doc=ue_api_sbo

di_doc=getbusinessobject=59

di_doc=lines=itemcode=RM_B

di_doc=lines=quantity=1

di_doc=lines=whscode=01

di_doc=lines=batchnumber=batchnum=20190209

di_doc=lines=batchnumber=quantity=1

di_doc=add

if <di_doc.ret_code> = -1 then

  return -1

end if

destroy=di_doc

//Create Inventory transfer

declare=di_doc=ue_api_sbo

di_doc=getbusinessobject=67

di_doc=lines=itemcode=RM_B

di_doc=lines=quantity=1

di_doc=lines=batchnumber=batchnum=20190209

di_doc=lines=batchnumber=quantity=1

di_doc=lines=fromwhscode=01

di_doc=lines=whscode=02

di_doc=add

if <di_doc.ret_code> = -1 then

  return failure

end if

messagebox=Created document: <di_doc.getnewobjectkey>

destroy di_doc
```

---

### 9.4 採購訂單 — Purchase order

用 DI-API 建立採購訂單(物件類型 22):

```
// Work with Purchase order

declare=di_doc=ue_api_sbo

di_doc=getbusinessobject=22

di_doc=cardcode=P001

di_doc=lines=itemcode=RM

di_doc=lines=quantity=1

di_doc=add

if <di_doc.ret_code> = -1 then

  return failure

end if

messagebox=Created document: <di_doc.getnewobjectkey>

destroy di_doc
```

---

### 9.5 依採購訂單建立採購收貨 — Goods Receipt Po on Purchase order

以採購訂單為基礎,建立採購收貨 PO(物件類型 20)。

```
//Create Goods receipt PO based on a purchase order

declare=di_doc=ue_api_sbo

di_doc=getbusinessobject=20

di_doc=lines=itemcode=RM

di_doc=lines=quantity=2

di_doc=lines=basetype=22

// Base Docentry is document 1371

di_doc=lines=baseentry=1371

di_doc=lines=baseline=0

di_doc=add

if <di_doc.ret_code> = -1 then

  return failure

end if

messagebox=Created document: <di_doc.getnewobjectkey>

destroy di_doc

return 1
```

**基礎單據欄位**:`basetype=22`(採購訂單)、`baseentry=1371`(基礎單號)、`baseline=0`(基礎單的行號,從 0 起算)。

---

### 9.6 業務夥伴目錄編號 — Business Partner Catalog Number

在「業務夥伴目錄編號」中建立一筆(物件類型 73):

```
declare di_doc=ue_api_sbo

di_doc.getbusinessobject=73

di_doc.CardCode=2000

di_doc.ItemCode=FP

di_doc.Substitute=Hello2

di_doc.add

destroy=di_doc
```

---

### 9.7 處理日記帳分錄 — Working with journal entries

把日記帳分錄第 2 行的 `linememo` 改成 "Hello3":

```
setvar=ll_docentry=10744

setvar=ll_linenum=1

setvar=ls_memo="Hello 3"

declare jdt=ue_api_sbo

jdt.getbusinessobject=oJournalEntries

jdt.getbykey=<ll_docentry>

jdt.lines.setcurrentline=<ll_linenum>

jdt.lines.linememo=<ls_memo>

jdt.update

destroy=jdt
```

**說明**:行號從 0 起算,`ll_linenum=1` 即第 2 行。先 `getbykey` 讀取,再設定目前行並修改,最後 `update`。

---

### 9.8 結案所有單據 — Close all documents

結案所有單據,例如所有銷售訂單:

```
declare=vdoc=ue_api_sbo

// Create Sales order object

vdoc.getbusinessobject=17;

// Read all open documents

instance datastore r

r.select "ORDR"."DocEntry" from "ORDR" where "DocStatus"='O'

// Close all

for i=1 to <r.rowcount>

  r.setrow=<i>

  vdoc.getbykey=<r.docentry>

  vdoc.close

next

destroy=r

destroy=vdoc
```

**說明**:用 datastore 查出所有未結(`DocStatus = 'O'`)的銷售訂單,逐筆讀取並結案。

---

### 9.9 變更品項價格表 — Change Item Pricelist

為品項「RM」在**第二個價格表**定義新價格 25:

```
declare itemprice=ue_api_sbo

itemprice.getbusinessobject=oitm

// Read item

itemprice.getbykey=RM

// use second price list (0-based)

itemprice.pricelist.setcurrentline=1

itemprice.pricelist.price=25

itemprice.update

destroy=itemprice
```

價格表索引**從 0 起算**,所以 `setcurrentline=1` 是第二個價格表。

---

## 10. BSL(Beas Service Layer)範例

使用 Beas Service Layer 的範例。

### 10.1 BSL:建立收貨單 — create Receipt document

用 Beas Service Layer 建立一張含 **5 個新序號**的收貨單。此範例同時示範如何使用佔位符(place holder)。

```
// Create a receipt document with 5 new serial numbers

// in this example you can see, how to work with place holder

instance bsl b

setvar=ls_json=

for ll_loop=1 to 5

   addvar=ls_json={"ItemCode":"RM_S","WhsCode":"01","DistNumber":"<time,hhmmss><ll_loop>","Quantity":1}

   if <ll_loop> <> 5 then

      addvar=ls_json=,

   end if

next

setvar=ls_json={"DocumentLines":[<ls_json>]}

// in post command use only variable.

// follow is not working: b.post=receipt={"DocumentLines":[<ls_json>]}

// because the system convert placeholder only, if the string don't start with {

b.post=receipt=<ls_json>

messagebox=<b.ret_code> <b.ret_text> <b.ret_value>

destroy b
```

**重點**

- 先用 `addvar` 逐筆累積 JSON 明細,最後再包上 `{"DocumentLines":[...]}`。
- **`post` 指令請只傳入變數**:`b.post=receipt=<ls_json>` 可以;`b.post=receipt={"DocumentLines":[<ls_json>]}` **不行**,因為系統**只有在字串不是以 `{` 開頭時**才會轉換佔位符。
- 結果存於 `b.ret_code`、`b.ret_text`、`b.ret_value`。

---

### 10.2 BSL:取得收貨數量 — Get receipt quantity

傳回某張工單的收貨數量:

```
instance bsl b

setvar=ll_DocEntry=6002479

setvar=ll_LineNumber=10

// Note: use Pipeline, becaus comman in place holder not supported

setvar=lc_bookedQty=<bsl.WorkorderPos([ll_DocEntry]|[ll_LineNumber]).QuantityProduced/$Value>

messagebox=booked quantity: <lc_bookedQty>
```

**重點**:佔位符內**不支援逗號**,多個參數要用管線符號 `|` 分隔,且參數用 `[]` 包起來。

---

### 10.3 BSL:佔位符與函式呼叫 — Place holder and function call

示範以佔位符進行 BSL 呼叫,以及 BSL 函式呼叫。

**情境**:在工單「手動收貨」視窗,組裝收貨時系統**不會檢查最小數量**;此檢查只在「物料發料視窗」有效。可用 BSL `WorkorderBoM/CheckMinMaxBom` 執行。

**構想**:輸入收貨數量時檢查發料數量。

**檢查內容**:

- 需求數量 = 輸入數量 + 過去所有收貨。
- 依此數量換算出計畫數量,再據以算出最小與最大數量。

檔案:`mw_manuell_buchen.src`

**BSL 佔位符**

```
lc_bookedQty=<bsl.WorkorderPos([dw_1.item.belnr_id.value] | [dw_1.item.belpos_id.value]).QuantityProduced/$value>
```

- 佔位符中**不允許**使用逗號與 `<>`。
- 逗號:用管線符號(`|`)代替。
- 佔位符中的佔位符:用 `[]`。

**函式呼叫**:必須先宣告 BSL 物件,才能呼叫 BSL 函式。

```
instance bsl b

b.get=WorkorderBom/CheckMinMaxBom(<dw_1.item.belnr_id.value>,&

<dw_1.item.belpos_id.value>,0,"",<lc_bookedQty,dbnum>,true)
```

回傳 JSON 物件(結構請參閱該函式的說明):

```
   if <b.Status> = locked then

     dw_1.item.menge.setvalue=0

   end if
```

**完整範例**

```
global function dw_1_item_menge_itemchanged

dec lc_bookedQty

// Work order - Bill of Material position must be defined

if <dw_1.item.belnr_id.value> n> 0 and <dw_1.item.belpos_id.value> n> 0 and <dw_1.item.pos_id.value> n= 0 and <dw_1.item.menge.value> n> 0 then

   // Get booked Quantity

   // Parameter with [] because inside placeholder

   // comma in placeholder not allowed. Use | as comma

   lc_bookedQty=<bsl.WorkorderPos([dw_1.item.belnr_id.value]|[dw_1.item.belpos_id.value]).QuantityProduced/$value>

   lc_bookedQty=<lc_bookedQty,dbnum> + <dw_1.item.menge.value,dbnum>

   // Execute BSL function

   instance bsl b

   b.get=WorkorderBom/CheckMinMaxBom(<dw_1.item.belnr_id.value>,<dw_1.item.belpos_id.value>,0,"",<lc_bookedQty,dbnum>,true)

   // if locked, then set quantity to 0

   if <b.Status> = locked then

     dw_1.item.menge.setvalue=0

   end if

   destroy=b

end if

end global
```

---

## 11. 使用者事件(User events)說明

(本節內容皆來自官方 `User events` 說明頁。)

### 11.1 什麼是使用者事件

使用者事件是 Beas 的特殊事件,例如物料過帳、生產控制。這些事件**不依附於視窗**,完全在背景執行。你可以在事件中變更或擴充標準功能,例如工單建立、收貨等。

呼叫來源:各種視窗、Integration hub、Beas Service Layer。

### 11.2 建立使用者事件

1. 開啟 **Beas AddOn**。
2. 開啟第一行。
3. 選擇 **User event**。
4. 在右側選擇事件。
5. 插入腳本。
6. 儲存(update)。變更**立即生效,不需重啟**;但**其他電腦必須重新啟動**才會生效。

系統會自動插入函式框架:

```
function CreateWorkOrder

// Script

end function
```

編輯器右側會列出所有可讀取或寫入的屬性。

### 11.3 一般注意事項

- 宣告的任何物件,用完都必須 `destroy`。
- 若要讀取視窗相關資訊,必須先確認這個使用者函式是否真的在該視窗內執行。
- 使用者事件在**沙箱(Sandbox)**中執行:**只有「可變更(changeable)」的變數才能在事件外看到變更**。
- 函式**之外**的所有定義與宣告都會被**忽略**。
- 需要額外的函式時,必須宣告在**事件函式內部**。

### 11.4 除錯(Debug)

在腳本中加入除錯點即可看到所有變數或更多細節:

```
system.debug=mirror  //  insert all variables in debug window

system.debug=level=2 // go level 2

system.debug=level=1 // go back to level 1
```

更多資訊請參閱官方 `System > Debug`。

### 11.5 相容性(Compatibility)

- **名稱已更改**:舊版使用過其他變數名稱與事件名稱;所有舊名稱仍可運作(相容性超過 99%),但程式邏輯有改變,可能造成問題。
  例:Beas 9.1 及更早版本只有單一收貨函式,無法在同一份文件收不同批號、儲位或品項;現在可以(新的 PO 收貨、新的品項發料/收貨功能、所有 WEB APP)。因此事件由單純的 receipt 改為 `ReceiptStart` 與 `ReceiptLine`(與舊的 `receipt` 相同),可能造成不相容。
- **`end function` 為必要**:使用 `function` 宣告使用者事件時,必須以 `end function` 結尾。過去只支援 `end`,現在**必須**寫 `end function`。
- **忽略事件函式之外的宣告**:

  ```
  function ReceiptEnd

  test()

  end function

  function test

  messagebox=Hello world

  end function
  ```

  過去會顯示訊息,現在**不會**,因為 `test` 宣告在 `ReceiptEnd` 之外。正確寫法(子函式宣告在事件函式內,且子函式以 `end` 結尾,**不是** `end function`):

  ```
  function ReceiptEnd

  test()

  function test

  messagebox=Hello world

  end

  end function
  ```

- **每個使用者事件都是沙箱**:用 `setvar` 定義的變數,在該事件之外看不到。例如 `ReceiptLine` 與 `ReceiptEnd` 屬於同一個視窗,系統先執行 `ReceiptLine`、再執行 `ReceiptEnd`:

  ```
  function ReceiptLine

  setvar=myVariable="Test"

  end function

  function ReceiptEnd

  messagebox=<myVariable>

  end function
  ```

  舊版會顯示 `myVariable` 的內容;目前版本因為該變數未宣告,會顯示字面的 `<myVariable>`。`ReceiptLine` 在沙箱中執行,系統會還原變數。只能變更文件中標示為 `changeable` 的變數(例如範例中的 `e_DistNumber`)。

### 11.6 事件清單(官方目錄)

| 群組 | 事件 |
|---|---|
| `addon` | install、start |
| `beasmenu` | start、poststart、initmenu、stopbeasgui、menu[id] |
| `aps` | StartCalculation、StartWorkorderPosition、StartWorkorderPositionStep、(<<) Start/Middle/EndCalcRoutingPositionBackward、(>>) Start/Middle/EndCalcRoutingPositionForward、CalcRoutingOverlapping、EndWorkorderPositionStep、EndWorkorderPosition、EndCalculation |
| `Configurator` | CheckConfigBeforeNewLoad |
| `fdc` | additionalholiday |
| `mrp` | Start、End |
| `sales` | rightclick |
| `stockmanagement` | GoodsReceiptPo、Allocation、Transfer、Issue、Receipt |
| `workordermanagement` | Calc.、creation、manage、Preassign、issue process、receipt process、time recording、wip - Work in Process |
| `qcmanagement` | QCPlanDetermination、QCorderCreate、QCorderCreateEnd、Print |

> 各事件的完整變數表在官方各事件頁面;目前 repo 只有本手冊用到的 5 個事件頁:`CreateWorkOrderPositionComplete`、`workordermanagement.ReceiptStart`、`workordermanagement.ReceiptEnd`、`stockmanagement.ReceiptEnd`、`TimeRecording`。其餘見 `docs/缺漏頁面清單.md`。

---

## 12. 附錄:語法速查

以下整理自各範例,方便查詢(僅為使用範例歸納,詳細定義請以官方指令說明為準)。

| 項目 | 寫法 / 說明 |
|---|---|
| 事件函式 | `global function <事件名>` … `end global` |
| 變數 | `setvar=<名稱>=<值>`;累加 `addvar=<名稱>=<值>`;宣告十進位 `dec 名稱` |
| 讀取變數 / 佔位符 | `<變數>`、`<控制項.item.欄位.value>` |
| 格式轉換 | `<值,upper>`、`<值,date>`、`<值,yyyymmdd>`、`<值,left 2>`、`<值,right 4>`、`<值,mid 3 1>`、`<值,#0>`(無小數)、`<值,dbstring>`(轉 SQL 字串)、`<值,dbnum>`(轉 SQL 數字)、`<值,bsl>`(轉 BSL 參數) |
| 數值比較運算子 | `n=`、`n<`、`n>`、`n>=`、`n<=`:兩邊都轉成數字再比較 |
| 訊息框 | `messagebox=文字`;`messagebox=error$文字`;`messagebox=info$文字` |
| 建立控制項 | `create=button=name=…,text=…[,position=1]`、`dw_1.create=text=name=…<tab>…` |
| 修改 DataWindow | `dw_1.modify=…`(使用 DataWindow 原生語法) |
| SQL 取值 | `select … into :變數 from …` |
| 宣告 BSL | `instance bsl b`;`b.get=…`、`b.post=…`、`b.put=…` |
| BSL 回傳 | `b.ret_code`、`b.ret_text`、`b.ret_value`、`b.ret_status` |
| 佔位符內的 BSL | `<bsl.實體(參數1|參數2).欄位/$value>`:佔位符內不能有逗號,用 `|`;巢狀佔位符用 `[]` |
| 宣告 DI-API | `declare=<名稱>=ue_api_sbo`;`<名稱>.getbusinessobject=<類型>`;`<名稱>.add / update / close`;`<名稱>.ret_code` |
| 資料集 | `instance datastore d`;`d.setrow=<n>`;`<d.欄位>`(欄位小寫);用完 `destroy=d` |
| 流程控制 | `if … then … end if`;`for 變數=1 to <n> … next`;`[標籤]` + `goto=標籤` |
| 使用者事件常見變數 | 以 `e_` 開頭,如 `e_WoDocEntry`、`e_WoLineNumber`、`e_DistNumber`、`e_ReceiptDocEntry` |

### 常見使用者事件(Userevent)

| 事件 | 時機 |
|---|---|
| `CreateWorkOrderPositionEnd` | 工單位置建立後 |
| `CreateWorkOrderPositionComplete` | 每個新的主工單位置完成後 |
| `workordermangement.GetBatchNumber` / `GetSerialNumber` | 工單收貨/序號保留需要新批號/序號時 |
| `ReceiptEnd`(`materialmanagement` / `workordermangement`) | 收貨完成後 |
| `TimeRecording` | 建立時間回報時 |

> 使用者事件的建立方式、沙箱規則與相容性說明,見[第 11 節](#11-使用者事件user-events說明)。
