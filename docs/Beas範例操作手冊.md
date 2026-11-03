# Beas 範例操作手冊(繁體中文)

> 來源:`fetched/examples/`(Beas Script 2026.09 官方文件的 Examples 章節,共 53 頁)。
> 補充來源:`fetched/missing/`(14 頁)與 `fetched/missing_b/`(214 頁):使用者事件、Configuration Wizard、DI-API、視窗事件與視窗客製化等,用來把原先的推測改成依官方說明。標示「(官方說明)」的段落即來自這些頁面;`missing_b` 共 214 頁已全部整理:與本手冊範例直接相關的由我逐頁閱讀並寫入各節,其餘 194 頁整理成參考文件;`fetched/missing_c/` 的 64 頁(WEB 函式庫、資料庫、SAP UI-API、關鍵字等)也已整理。`docs/ref/` 共 8 份參考文件(見[第 11.11 節](#1111-參考文件索引)),`fetched/missing_b/` 與 `fetched/missing_c/` 合計 278 頁都已處理。參考文件由 AI 子代理整理,我已用腳本核對頁數、程式碼逐行與表格內容,但翻譯措辭未逐句審閱。
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

**官方對 SAP 表單腳本的說明(SAP UI-API 頁)**

- 要在 SAP 表單加入自己的功能:先開啟除錯視窗(Level 1),再開啟 SAP 表單,除錯視窗會顯示 SAP 觸發的所有事件;在其中的 global function 上按兩下,會開啟 script 視窗讓你直接寫程式碼。
- 要**新增或變更元素**,請使用事件 `form_clicked()`。
- 官方 NOTE:**不要使用 `windowevent`**,它只為相容性而保留,請改用 `global function`。

> ⚠ 本節兩個範例(2.1、2.2)以及官方「BS Create objects」範例都使用 `windowevent`,與上述 NOTE 互相矛盾;官方沒有說明矛盾的原因。範例程式碼保持原文,新寫的腳本建議依官方 NOTE 改用 `global function`,並實測。

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

**`beasgui=rightclickmenu` 參數(官方「BS Right Click」頁)**

```
beasgui=rightclickmenu=type=caption=uid=[image=[position=[enabled=[checked=[command, if not windowevent...]]]]]
```

| 參數 | 說明 |
|---|---|
| `type` | `1` = 一般項目 |
| `caption` | 選單中顯示的文字,例:`<translate:My Menu Entry>` |
| `uid` | 識別碼(物件名稱) |
| `image` | 圖片,需完整路徑,例:`<global:system_home>bmp\project_red.png` |
| `position` | 在選單清單中的位置 |
| `enabled` | boolean:啟用 / 停用 |
| `checked` | boolean:設定 check 屬性 |
| `command` | 直接設定指令;也可以改用 windowevent 處理 |

對照本節範例 `beasgui=rightclickmenu=1=<spr:My Entry=myentry1=<global:system_home>bmp\formel.png=3===message=Click on my entry`:`type=1`、`caption=<spr:My Entry`、`uid=myentry1`、`image=…\formel.png`、`position=3`、`enabled` 與 `checked` 留空、`command=message=Click on my entry`。

官方限制:僅適用於 SAP 表單;此函式**只能在 `windowevent prerightclick` 中呼叫**。

> ⚠ 這條「只能在 `windowevent prerightclick` 呼叫」的規定,與 SAP UI-API 頁「不要使用 windowevents」的 NOTE 互相矛盾(見 2.1)。

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

> ⚠ 變數名稱:範例 2 使用 `e_belnr_id`、`e_belpos_id`,但官方事件頁列出的工單變數是 `e_WoDocEntry`、`e_WoLineNumber`。官方使用者事件總說明提到舊版名稱仍然有效(相容性超過 99%);工單事件的總覽頁(`usereventworkordermanagement.htm`)也確實出現 `e_belnr_id`、`e_belpos_id`、`e_pos_id`、`e_row`、`e_pers_id` 這類名稱。`beasscript/wotime.htm` 的 `workordermanagement:timerecordingstart` 事件說明也用同樣的 `e_belnr_id,e_belpos_id,e_pos_id` 描述該事件的變數。但**官方沒有任何頁面提供新舊名稱的對照**,所以 `e_belnr_id` 是不是 `e_WoDocEntry` 的舊名,官方文件**無法證實**。新寫的腳本建議使用各事件頁列出的變數名,範例 2 的行為請實測。

---

## 4. 生產

### 4.1 工單序號/批號產生 — Create Wo Serial/Batch Numbers

在使用者事件 `workordermangement.GetBatchNumber` 與 `workordermangement.GetSerialNumber` 中,可撰寫腳本來產生工單**收貨/序號保留**時所用的批號或序號。

**事件變數(官方說明)**:`GetBatchNumber` 與 `GetSerialNumber` 可用的變數相同,**只有 `e_DistNumber` 可寫**(放入新的批號/序號;沒定義時系統使用設定精靈的標準計數),`e_ItemCode`、`e_DocEntry`、`e_LineNumber`、`e_LineNumber2` 皆為唯讀。完整說明見 [11.8](#118-getbatchnumber--getserialnumber官方說明)。

> 說明:官方 `GetBatchNumber` 頁面把範例 3、4(其實是 `GetSerialNumber`)也列在一起,與本節相同。

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

> 資料表欄位查證:官方資料庫文件(`docs/ref/資料庫.md`)**沒有說明** `BEAS_FTPOS.WMS_PRODLINE`,也沒有 `BEAS_APLATZ.WMS_PRODLINE`。這個範例對 Beas 資料表做 `update`,官方自己的說明已提醒無法保證(見本節開頭)。欄位的完整對照見[附錄:資料表欄位查證](#範例用到的資料表欄位查證)。

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

> 資料表欄位查證:`BEAS_FTPOS.CHARGE_ID` 在官方資料庫文件中**沒有說明**(只有品管表 `qs_fthaupt.Charge_id` 被提到)。本範例直接 `update` 這個欄位,無法由官方文件證實;使用前請實測,並留意官方對「直接更新 Beas 資料表」的保證聲明。見[附錄:資料表欄位查證](#範例用到的資料表欄位查證)。

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

**工單結構視窗(官方說明)**:視窗檔 `fert_stuktur_browse`,不需參數即可開啟;可選擇只開啟某一個工單位置。

```
openwindow=dw=beas_WorkOrder_Structur
openwindow=dw=beas_WorkOrder_Structur<tab>belnr_id=6002253<tab>belpos_id=10
```

參數:`DocEntry` / `belnr_id`(基礎文件)、`LineNumber` / `belpos_id`(基礎文件行)。官方頁面**沒有列出 `zeilentyp` 各值的意義**(已查閱 `fetched/missing_b/` 全部頁面,包括視窗客製化各頁,仍找不到),所以下面程式碼中 `zeilentyp = 0`(工單)與 `= 10`(工單位置)的對應,仍是依範例上下文的解讀,未獲官方確認。

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

**手動過帳視窗 `mw_manuell_buchen`(官方說明)**

「手動收貨、發料、移轉」是**同一個視窗**。開啟參數:

| 參數 | 說明 |
|---|---|
| `s_parm1` | 品項代碼 |
| `s_parm2` | `F` = 針對工單、`B` = 分配(Allocation)、`U` = 目前不支援 |
| `s_parm3` | `einbuchen` = 收貨、`ausbuchen` = 發料、`umbuchen` = 移轉 |
| `s_parm4` | `close` = 交易後關閉視窗 |

另可帶入預設值:`e_batchnum`(批號)、`e_quantity`(數量)、`e_reference`(參考)、`e_info`、`e_whscodedfl`(預設倉庫)、`e_whscode_to`(移轉目標倉庫)、`e_cardcode`(業務夥伴)、`e_intrserial`(內部序號)、`e_liefercharge_id`(供應商批號)、`e_indate` / `e_prddate` / `e_expdate`(入庫、生產、有效日期,格式 `yyyy/mm/dd`)、`e_status`(交易後的批號狀態)、`e_notes`、`e_located`(批號位置)。

視窗專屬事件:

| 事件 | 說明 |
|---|---|
| `item-changed` | 品項變更之後(變數 `dw_1` = 此交易的所有欄位) |
| `item-reset` | 重設品項(交易後,清空所有欄位以開始新交易) |
| `buchen-start` | 交易開始前。把 `e_ok` 設為 `"N"` 可**取消**流程;`dw_1` 為交易資料,`<dw_1.getrow>` = 目前新增的行 |
| `buchen-ende` | 文件建立後,變數 `e_docentry` = 建立的文件 |

> 編註:本節範例 4.8 / 4.9 使用的 `dw_1_item_menge_itemchanged` 屬於 `dw_1` 的欄位事件(見附錄的視窗事件表),與上表的視窗專屬事件是不同層級。

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

(此範例與 [10.3 佔位符與函式呼叫](#103-bsl佔位符與函式呼叫--place-holder-and-function-call) 內容相同,該節有較完整的逐點說明。)

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

> 欄位名稱查證:
>
> - 官方 BSL 頁 `QCOrderMeasurement` 的 Get(`bslqcordermeasurementget.htm`)列出了資料表 **`BEAS_QSFTPOS`** 的對應欄位:`QS_ID`、`MESSWERTC`、`SPERRGRUNDID`、`MINIMAL`。所以 `qs_id`、`messwertc`、`sperrgrundid`、`minimal` 確實是量測樣本資料表的欄位名稱。`maximal` 我沒有在該頁找到對應列,未能確認。
> - 但本範例用的是 `dw_1.item.…` / `dw_2.item.…` 這類**視窗上的欄位**,官方視窗客製化頁(`qs_qsftpos_edit`)只有視窗名稱與「依量測值自動設定封鎖原因」這個延伸範例,沒有列出視窗欄位清單;QC orders、QC Samples 兩頁的原文是空的。因此 `dw_1` 與 `dw_2` 各對應哪一組資料,官方文件無法確認。
> - 要確認視窗欄位名,請在視窗中按 **CTRL 並將滑鼠移到欄位上**,從系統資訊讀取(見第 0 節)。

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

依官方的參數順序,範例 `print||true` 的意思是:第 3 個參數 `print` = 直接列印(省略則為預覽)、第 4 個參數印表機名稱留空(使用目前印表機)、第 5 個參數 `true` = 設定「已列印」旗標。B1UP 的官方 BeasFunction 頁對這幾個參數有進一步說明(官方說明):

- `LineNumber` **不填代表全部位置**。
- `print` 留空 = **檢視(預覽)**;填 `print` = 送到印表機。
- `printername` 為印表機名稱,留空則使用目前的印表機。
- `setprintFlag` 為 `true` 時,會把**工單與工單位置都設為已列印**。

寫法:BeasScript 為 `beasFunction=PrintWorkorder=600344`;B1UP 為 `beasFunction(PrintWorkorder|600344|10|print|HS-30421|true);`。在 B1UP 中執行 Beas 函式要用 `ExecuteBeasScript(...)`,且只能寫成**一行**;多行可用 `<cr_lf>` 佔位符。

> ⚠ 官方頁面對 `beasFunction` 的大小寫不一致(`PrintWorkorder` 頁寫 `beasFunction`,`CalculateAPS`、`CalculateMRP` 頁寫 `beasfunction`);`CalculateAPS` 的參數名拼成 `ScneraioId`(String),`CalculateMRP` 的參數是 `ScenarioId`(Number)。

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

> WEB APP 呼叫 Beas Service Layer 時(官方 WEB 函式庫頁):`ux.dgrid` 使用 `/odata4/Item?$ProgramId=…&$AppId=…`,而 `ux.saveAll`、`extendedSearch` 使用 `/odata4/v1/…`,**前綴不同,官方沒有解釋差別**。官方註解提醒:使用 BSL 時務必定義 `ProgramId` 與 `AppId`。這批 WEB 函式庫頁面中**沒有出現 `beasFunction` / `application.beasFunction`**,所以上面第三種寫法的 WEB 用法,只能依本節頁面本身的說明。詳見 `docs/ref/WEB函式庫.md`。

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

> 備註:官方 `CreateWorkOrderPositionEnd` / `CreateWorkOrderEnd` 事件頁另有一個較短的寫法(只列印目前位置),見 [11.7](#117-工單建立相關事件官方說明)。`CreateWorkOrderPositionEnd` 發生在時間計算之前,本節選用 `Complete` 事件是因為要等時間計算完成。
>
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

### 9.0 DI-API 基本規則(官方說明)

- **盡量使用 Beas Service Layer**:它是建立文件的介面,並支援所有 Beas 規則。DI-API 沒有 Beas 的檢查。
- `declare=<物件名>=ue_api_sbo` 之後,可直接透過物件名稱存取 SAP DI-API;幾乎任何屬性都能存取,`ue_api_sbo` **不做任何檢查**。
- **同一時間只允許一個 DI-API 物件**。不要同時 `declare=b1=ue_api_sbo` 與 `declare=b2=ue_api_sbo`,否則像 `<getlasterror>` 這類特殊變數會有問題。
- 用完一定要 `destroy`,否則可能造成後續錯誤。(例如 9.3 在建立第二份文件前,先 `destroy=di_doc`,即是這個原因。)

**錯誤處理**

| 項目 | 寫法 |
|---|---|
| 回傳碼 | `<物件名.ret_code>`:`-1` = 錯誤、`1` = 成功 |
| 回傳文字 | `<物件名.ret_text>`:最後的錯誤 |
| 詳細錯誤 | `<物件名.getlasterror>` |
| 安靜模式 | `d.silent=true`:不顯示任何錯誤訊息 |

`add`、`getbusinessobject`、`update`、`getbykey` 等函式都會回傳 `value`(`ret_code = 1` 表示成功)。

```
vdoc.add
if <vdoc.ret_code> = -1 then
  message=fehler$error create Order: <vdoc.getlasterror>
end if
```

> 另外,`add`、`update` 的官方方法頁寫的是「回傳變數 `value`:0 = 失敗、1 = 成功」(`update` 在安靜模式下,失敗時 `errortext` 帶有錯誤內容);而總覽頁寫的是 `ret_code`:-1 = 錯誤。兩種表達不一致,本手冊的範例都用 `ret_code = -1` 判斷。

**常用方法**

| 方法 | 說明 |
|---|---|
| `getbusinessobject=<物件>` | 指定要取得、載入或刪除的 SAP 物件(名稱或編號皆可)。指定後該物件本身就成為此商業物件,不會另外回傳物件 |
| `getbykey=<值>`、`getbykey=<值1>=<值2>` | 讀取已指定物件的資料;銷售訂單只需文件號(DocEntry)。數值鍵用 `getbynum=<數字>` |
| `add` | 新增一筆;成功後用 `getnewobjectkey` 取得新文件號 |
| `update` | 更新(儲存)變更 |
| `close` | 結案文件(之後仍需 `update`) |
| `getnewobjectkey` | 取得 `add` 剛建立的文件號 |

**交易(Transaction)(官方說明)**

- 開始:`di_item=starttransaction`;結束:`endtransaction=0` 提交(commit)、`-1` 回滾(rollback)。
- 交易**只對 SAP SDK 連線有效**。Beas 使用自己的資料庫連線,所以在提交之前,用 Beas 的查詢**讀不到**交易中尚未提交的變更(官方範例中該查詢回傳 100,表示 not found)。
- 官方的 `endTransaction` 頁寫 `endTransacton=endType`,`0`=commit、`1`=Rollback;`starttransaction` 頁的範例用 `-1` 回滾。兩頁的回滾值不一致(⚠),使用前請實測。
- 只有在資料庫連線為作用中時才能使用 `endTransaction`;若呼叫時發生例外,變更不會提交,需要依序重新 `StartTransaction`、重新提交變更、再 `EndTransaction`。

**連線其他公司資料庫**:`myapi=connect_extern=db=…<tab>username=…<tab>password=…`;`server`、`dbuser`、`dbpassword`、`servertype` 不填時使用目前的值,`servertype`:`1`=automatic、`4`=sql2005、`6`=sql2008。(`db`、`username`、`password` 在官方頁面標為 "Primary",官方沒有解釋其意義。)

**範例中用到的物件類型(官方「DI-API: Objects」)**

| 編號 | 名稱 | 說明 | 用於 |
|---|---|---|---|
| 4 | `oItems`、`oitm` | 品項 | 9.9 |
| 17 | `oOrders`、`ordr` | 銷售訂單 | 9.8 |
| 20 | `oPurchaseDeliveryNotes` | 採購收貨 PO | 9.5 |
| 22 | `oPurchaseOrders`、`opor` | 採購訂單 | 9.4、9.5(`basetype`) |
| 30 | `oJournalEntries` | 日記帳分錄 | 9.7 |
| 59 | `oInventoryGenEntry` | 一般收貨(Goods Receipt) | 9.1、9.3 |
| 60 | `oInventoryGenExit` | 一般發料(Goods Issue) | — |
| 67 | `oStockTransfer` | 庫存移轉 | 9.3 |
| 73 | `oAlternateCatNum` | 替代目錄編號(業務夥伴目錄編號) | 9.6 |

官方說明指出 `ue_api_sbo` 支援**所有 SAP 物件類型**,完整的編號與名稱對照見 `fetched/missing_b/documenttyp.htm`(資料來源為 SAP Business One SDK)。

**明細行(Line)物件**:階層為 `document → Line → batchnumber / serialnumber`。方法:`add`(新增行)、`setcurrentline=<x>`(切換到第 x 行)、`delete=<x>`(刪除第 x 行)。批號物件的寫法為 `vdoc=lines=batchnumbers=<屬性>=<值>`,屬性有 `BatchNumber`(必填,最多 32 字元,與品項、倉庫組合需唯一)、`Quantity`、`ExpiryDate`、`ManufacturingDate` 等。官方並提醒:批號請盡量改用 Beas Service Layer 的 BatchNumber。

> ⚠ 9.3 範例使用的屬性名稱是 `batchnum`(`lines=batchnumber=batchnum=...`)。官方 DI-API 物件的 21 頁中**沒有出現 `batchnum`**,批號物件頁(`batchnumbers`)只列了 `BatchNumber`。`batchnum` 只出現在 Beas 其他功能的說明裡(例如 `incomminggoods2` 頁的計畫單據寫法 `batchnum=A1234<char 9>bincode=1000`,以及 `wotime` 頁的 `oldbatchnum`、`newbatchnum`),那是 Beas 自己的欄位,不是 DI-API 批號物件的屬性。因此無法判斷 DI-API 這裡的 `batchnum` 是不是 `BatchNumber` 的別名,請實際測試。
>
> ⚠ 官方的寫法也不一致:`batchnumbers` 頁的語法是 `vdoc=lines=batchnumbers=<屬性>=<值>`(**複數**),但 `document`、`line` 頁的行屬性名是 `batchnumber`(**單數**),兩者的關係官方沒有說明。

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
>
> ⚠ 使用者自訂欄位的寫法:本範例用 `di_doc.lines=userfield=U_test2=N`。但官方 DI-API 物件頁(`document`、`line`、`item2`、`whsinfo`)的寫法都是**直接把 UDF 欄位名當屬性**,例如 `vdoc=u_ref=...`、`vdoc=lines=u_color=red`、`myitem=u_fieldname=myvalue`;官方 `item2` 頁甚至寫了「Property Userfields does not exist」。官方文件中**找不到 `userfield=` 這種寫法**,所以本範例的寫法無法由文件證實,請實測;若不行,改用直接屬性寫法。

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

> 官方文件查證:`fetched/missing_c/` 中的 JSON、XML 兩頁只是一般性說明(JSON 的資料型別表等),**沒有任何 Beas 專屬的 JSON 指令、組字串方法或範例**;PSR-File 頁說明 DataStore 的 alias 是 resultSet,處理 resultSet 的範例函式是 `sqlStatement.select`、`bsl.get`,但**沒有 `instance datastore` 指令的語法**。因此本節與前面章節中以 `addvar` 組 JSON 字串、`instance datastore` 取資料的寫法,無法由這幾頁證實,請以各 BSL 指令頁與實測為準。

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

> 各事件的完整變數表見 `docs/ref/事件_工單.md`(workordermanagement,58 頁)與 `docs/ref/事件_其他.md`(其餘群組,58 頁);下面 11.7–11.9 摘錄與本手冊範例有關的事件。

### 11.7 工單建立相關事件(官方說明)

| 事件 | 執行時機 | 備註 |
|---|---|---|
| `CreateWorkOrderPositionEnd` | 建立新工單位置之後、**時間保留之前**。位置、BOM、途程已儲存 | 只對**主工單位置**呼叫,不含子位置。**此時尚未計算時間**;需要時間請改用 `CreateWorkOrderPositionComplete` |
| `CreateWorkOrderPositionComplete` | 建立新工單位置**並完成時間計算之後** | 只對主層級呼叫,不含子位置 |
| `CreateWorkOrderEnd` | **整張工單建立完成之後** | **使用者手動建立工單時不會觸發** |

三個事件共用的變數(`CreateWorkOrderEnd` 只有第一個),皆為唯讀:

| 變數 | 說明 |
|---|---|
| `e_WoDocEntry` | 建立的工單號 |
| `e_WoLineNumber` | 行號 |
| `e_Project` | 專案編號 |
| `e_ItemCode` | 此工單位置的品項代碼 |
| `e_Refresh` | `true` = 更新(refresh)模式,此時建立的是暫時、未存進資料庫的工單;`false` = 建立模式 |

官方 `CreateWorkOrderEnd` 頁的範例 —— 建立後,若任一使用的資源目前停機,就鎖定工單:

```
function CreateWorkOrderEnd

// lock workorder if resource with stillstand - message included

select count(*) into ll_count from "BEAS_FTAPL" where "BEAS_FTAPL"."BELNR_ID" = <e_WoDocEntry> &

  and "APLATZ_ID" in (select "APLATZ_ID" from "BEAS_APLATZ_STILLSTAND" where  &

  CAST(TO_VARCHAR("BEAS_APLATZ_STILLSTAND"."DATUM_BIS",'YYYYMMDD') AS CHAR(8))='20500101')

if <ll_count> n> 0 then

   "SQL"=update "BEAS_FTHAUPT" set "SPERRUNG"='J' where "BEAS_FTHAUPT"."BELNR_ID" = <e_WoDocEntry>

   message=info$msg1$One or more resources current not available. Work order will be locked.

end if

end function
```

> 說明:這個範例使用 `TO_VARCHAR`,是 HANA 的寫法;在 MSSQL 上需要改寫。

官方同一頁還有一個更短的「建立後直接列印」寫法 —— 只列印目前位置,並設定已列印旗標(`print==true`),註解寫「列印目前與所有子位置」,但程式碼本身只呼叫一次,是否涵蓋子位置官方沒有進一步說明:

```
function CreateWorkOrderPositionEnd

// We want to print current and all sub positions

beasfunction=PrintWorkorder=<e_WoDocEntry>=<e_WoLineNumber>=print==true

end function
```

### 11.8 GetBatchNumber / GetSerialNumber(官方說明)

用來定義**下一個批號 / 序號**,適用於手動收貨、採購收貨 PO 等。把新號碼放進變數 `e_DistNumber`;**沒有定義時**,系統使用設定精靈中的設定(標準計數)。

| 變數 | 讀 | 寫 | 說明 |
|---|---|---|---|
| `e_ItemCode` | ✔ | | 品項代碼(官方頁面此列說明為「Calculation methode ID」,疑似複製錯誤) |
| `e_DistNumber` | | ✔ | 新的批號(或序號) |
| `e_DocEntry` | ✔ | | 工單號 |
| `e_LineNumber` | ✔ | | 工單位置 |
| `e_LineNumber2` | ✔ | | BOM 位置(例如負數量產生的收貨) |

**只有 `e_DistNumber` 是可寫的**,這與 11.5 的沙箱規則一致。因此 4.1 範例 4 中對 `e_DocEntry` 的 `setvar` 只是在事件內當暫存變數使用,不會影響事件外的值。

### 11.9 收貨流程的事件順序(官方說明)

收貨流程可以是倒沖(backflushing)的一部分。**若在使用者事件中取消收貨流程,系統無法回復整個倒沖流程**。

1. `workordermanagement.ReceiptStart()`
2. 對送進此流程的**每一行**:
   - `workordermanagement.PostCalculation()`
   - `workordermanagement.GetPrice()` / `GetPrice_Negativeposition()`
   - `workordermanagement.ReceiptLine()`
3. 全部檢查通過後,系統產生文件。若要為多個工單位置收貨,系統會先檢查所有工單位置,再產生文件。
4. 對文件的**每一行、批號/序號與(或)儲位**:
   - `workordermanagement.GetSerialNumber()` / `GetBatchNumber()`(僅在尚未定義時)
   - `stockmanagement.ReceiptLine()`
5. 對**每一份產生的文件**:
   - `workordermanagement.ReceiptEnd()`
   - `stockmanagement.ReceiptEnd()`
6. 發生錯誤時:`stockmanagement.ReceiptRollback()`

> 注意:「手動收貨 - 單筆流程」與「生產時間回報 - 倒沖流程」的事件順序不同。

**`workordermanagement.ReceiptLine`**:對送進此函式的每一行執行,可用 `return failure` 取消該工單位置的處理。

- 若品項受批號或序號管理且尚未定義 `DistNumber`,這個事件會在系統**自動產生 `DistNumber` 之前**被呼叫。
- 序號的特例:若數量 > 3,`workordermanagement.ReceiptLine` 只執行一次,但 `stockmanagement.ReceiptLine` 會對每個序號各執行一次(共 3 次)。
- 可以在這個事件中變更成本中心(Costing Code)。

| 變數 | 讀 | 寫 | 說明 |
|---|---|---|---|
| `e_WoDocEntry` | ✔ | | 工單號 |
| `e_WoLineNumber` | ✔ | | 工單位置 |
| `e_WoLineNumber2` | ✔ | | BOM 位置 |
| `e_ItemCode` | ✔ | | 品項代碼 |
| `e_Version` | ✔ | | 品項版本 |
| `e_Quantity` | ✔ | | 庫存單位數量 |
| `e_ProjectCode` | ✔ | | 專案代碼 |
| `e_WhsCode` | ✔ | | 倉庫代碼。**變更倉庫後系統不會再檢查**:沒有庫存控制、沒有保留控制;若倉庫不可用或帳戶是依倉庫定義,可能造成收貨失敗或會計錯誤 |
| `e_BinCode` | ✔ | ✔ | 儲位。時間回報倒沖與桌面倒沖,僅在只收一個批號/序號/一般品項時支援儲位 |
| `e_RFID` | ✔ | ✔ | RFID / LUID。限制同 `e_BinCode` |
| `e_CostingCode`、`e_CostingCode2`–`e_CostingCode5` | | ✔ | 成本中心 1–5(僅第一筆可用) |
| `e_CloseId` | ✔ | | `1` = 收貨後結案該位置 |
| `e_AccountId` | ✔ | ✔ | 帳戶 ID,可變更此次收貨使用的帳戶。發料:WIP 帳戶;收貨:倉庫帳戶。發料時帳戶必須是 WIP 帳戶且已登錄於 Beas;必須設定 `OACT` 的 `AcctCode`(格式化帳號存在 `OACT.FormatCode`) |

官方範例(依專案改用不同 WIP 帳戶):

```
// Define WIP for Issue

function IssueLine

// Projects starting with "AZ" using Account 501-01

if <e_ProjectId,left 2> = "AZ" then

  select "AcctCode" into e_AccountId from "OACT" where "FormatCode"='501-01'

  return 1

end if

// Projects starting with "AB" using Account 501-01

if <e_ProjectId,left 2> = "AB" then

  select "AcctCode" into e_AccountId from "OACT" where "FormatCode"='500-03'

  return 1

end if

// For all other :

select "AcctCode" into e_AccountId from "OACT" where "FormatCode"='500-01'

end function
```

> ⚠ 官方範例的疑似問題:註解寫「`AB` 開頭使用 501-01」,程式碼用的是 `500-03`;範例使用 `e_ProjectId`,但變數表列的是 `e_ProjectCode`,且範例函式是 `IssueLine`、變數表卻是 `ReceiptLine` 的。程式碼保持原文。

> ⚠ `stockmanagement` 的 `ReceiptLine` 與 `workordermanagement.ReceiptLine` 是不同的事件。官方總覽頁說 `ReceiptLine` 可取消,但 `stockmanagement` 的 Allocation / Transfer / Issue / Receipt 四個 `*Line` 頁都說:用 `e_aktiv` 取消是**舊功能,只為相容模式保留,新物件不支援**。詳見 `docs/ref/事件_其他.md`。

### 11.10 取消流程的寫法(官方各頁彙整)

官方各事件頁用的取消方式**並不統一**:

| 寫法 | 出現在 |
|---|---|
| `return failure` | `workordermanagement`:ReceiptStart、ReceiptLine、IssueStart、IssueLine 等(`TimeRecording` 官方頁只寫「可以取消流程」,沒有寫取消的方法;本手冊 4.7 的範例用 `return failure`);`aps`:StartCalculation、StartWorkorderPosition;`stockmanagement`:ReceiptCostingCode;`Configurator`:CheckConfigBeforeNewLoad(官方寫「腳本必須以 `return failure` 結束」) |
| `return -1` | `stockmanagement.GoodsReceiptLine` |
| `setvar=value=-1` | `beasmenu` 的 `menu[id]` |
| 說可以取消,但沒寫怎麼做 | `GoodsReceiptPO`、`GoodsReceiptPOBeforeCreate`、`StartWorkorderPositionStep` |
| 明寫不能停止 | `GoodsReceiptPOEnd` |

另外,取消**收貨 / 發料**時,若它是**倒沖(backflushing)**流程的一部分,系統無法回復倒沖流程中已完成的另一半(見 11.9)。`TimeRecordingEnd` 取消時,時間回報**仍會被建立**,只是不再建立額外的時間回報(錯誤碼 1101)。

### 11.11 參考文件索引

這些文件是把 `fetched/missing_b/` 與 `fetched/missing_c/` 的官方頁面逐頁翻成繁體中文;**程式碼逐字保留原文**,官方頁面自身的矛盾以「⚠」標出,未自行判斷對錯。

| 文件 | 內容 | 頁數 |
|---|---|---|
| `docs/ref/事件_工單.md` | 使用者事件:workordermanagement(計算、建立、複製/更新、刪除、結案/重開、保留、發料、收貨、時間回報、WIP) | 58 |
| `docs/ref/事件_其他.md` | 使用者事件:addon、beasmenu、aps、Configurator、mrp、sales、stockmanagement、qcmanagement | 58 |
| `docs/ref/API_方法與屬性.md` | B1UP、DI-API 方法與屬性(含 admininfo 257 列)、Service Types(127 列) | 21 |
| `docs/ref/API_物件.md` | DI-API 物件:document、line、batchnumbers、item2、whsinfo 等 | 21 |
| `docs/ref/視窗.md` | Beas 視窗 ID、SAP 表單 ID 與選單 ID 對照表、視窗客製化、視窗事件、datawindow | 36 |
| `docs/ref/介面與關鍵字.md` | SAP UI-API(BS Item / Create objects / Right Click)、Addon connector、Beas common service、PSR / JSON / XML / DateTime format | 13 |
| `docs/ref/資料庫.md` | 資料庫檢視 BEASV_*(欄位與資料表對應)、預存程序、函式、資料模型、DB.INI 結構 | 20 |
| `docs/ref/WEB函式庫.md` | WEB APP 2.0:ui.js、ux.js、beas.js 與序號/批號/搜尋/附件類別 | 31 |

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

### 格式代碼查證(上表各格式在官方文件的出處)

上面「格式轉換」一列是我從範例歸納的。逐項到官方頁面搜尋的結果(`fetched/beasscript/`):

| 格式 | 官方出處 |
|---|---|
| `upper`、`left n`、`right n`、`mid n m`、`dbstring`、`tosql`、`hex2int` | `bs_string.htm` 有 |
| `#0` | `bs_number.htm` 有 |
| `yyyymmdd`、`date` | `bs_date.htm` 有 |
| `dbstring`、`dbnum` | `platzhalter.htm` 有 |
| `#.000000`、`hhmm`、`bsl` | 在上述 `bs_number` / `bs_date` / `platzhalter` 頁中**沒有找到**(可能在其他頁面,我沒有逐頁搜尋全部 `beasscript/`) |

另外,`missing_c` 的「DateTime format」頁(`docs/ref/介面與關鍵字.md`)是 DataWindow 風格的**顯示格式**:日期用 `d`、`dd`、`ddd`、`dddd`、`m`~`mmmm`、`yy`、`yyyy`;時間用 `h`、`hh`、`m`/`mm`(分鐘,必須接在 `h` 或 `hh` 之後)、`s`/`ss`(必須接在 `m` 或 `mm` 之後)、`f`~`ffffff`、`AM/PM` 等。該頁的範例**都帶分隔符號**,沒有 `yyyymmdd`、`hhmm` 這種連在一起的寫法,也沒有說它與 Beas 佔位符 `<今天,yyyymmdd>` 的格式規則相同,所以不能直接當作佔位符格式的依據。

### 範例用到的資料表欄位查證

本手冊的範例直接讀寫 Beas 資料表。對照官方資料庫文件(`docs/ref/資料庫.md`)的結果:

| 範例 | 資料表.欄位 | 官方資料庫文件 |
|---|---|---|
| 4.4 批號保留 | `BEAS_FTPOS.CHARGE_ID` | **未說明**(`BEAS_QSFTHAUPT.CHARGE_ID` 是 QC 訂單的欄位,不同表) |
| 4.3 WMS 生產線 | `BEAS_FTPOS.WMS_PRODLINE`、`BEAS_APLATZ.WMS_PRODLINE` | **未說明** |
| 4.3 WMS 生產線 | `order by "SortId"`(來自 `BEAS_FTAPL` 的查詢) | `SortId` 是檢視 `BEASV_WORKORDER_BOM`、`BEASV_WORKORDER_ROUTING` 的**排序欄位**;官方未說明 `BEAS_FTAPL` 本身有沒有這個欄位 |
| 3.1 清空庫存 | `BEAS_RESERVATION_LINE` | 只出現**表名**(BOM 檢視的 `Reserved` 欄位),沒有欄位說明 |
| 3.2 收貨後處理 | `BEAS_FTPOS.UDF2` | **未說明**(官方只有 `BEAS_FTHAUPT.UDF1…4` 的整段對應) |
| 4.1 範例 4 | `BEAS_FTHAUPT.UDF4` | 以「`UDF1…UDF4`」整段對應出現,沒有個別說明 |
| 11.7 CreateWorkOrderEnd | `BEAS_FTHAUPT.SPERRUNG` | 有說明:對應檢視欄位 `Lock` |
| 4.6 結案工單 | `BEAS_FTHAUPT.ABGKZ` | 有說明:對應檢視欄位 `Closed` |
| 多個範例 | `BELNR_ID`、`BELPOS_ID` | 有說明:對應檢視的 `DocEntry`、`LineNumber` |
| 11.7 CreateWorkOrderEnd | `BEAS_APLATZ_STILLSTAND` | **未說明**(官方 `CreateWorkOrderEnd` 頁自己的範例使用) |
| 5.1 QC | `BEAS_QSFTPOS.QS_ID`、`MESSWERTC`、`SPERRGRUNDID`、`MINIMAL` | 有說明,但出處是 BSL 頁 `bslqcordermeasurementget.htm`,不在資料庫章節;`MAXIMAL` 未確認 |
| 3.2、4.x | `IBT1`、`OBTN`、`OITM` 等 | SAP 標準資料表,不屬於 Beas 文件 |

> 官方 `database.htm` 連結到一份完整的資料庫文件 `database.pdf`,不在目前抓到的頁面中。要確認「未說明」的欄位,需要這份 PDF 或實際查詢資料庫結構。

### Beas 視窗事件一覽(官方 `window events (beas)`)

事件以 `global function <事件名>` … `end global` 宣告,**宣告函式即登錄事件**。
**每個事件只能有一個函式**:同一事件宣告第二次,會**覆蓋**第一次。(所以同一個腳本中不要重複宣告 `form_loaded`,多個動作請合併在同一個函式裡。)

*開啟視窗的流程*

| 事件 | 說明 |
|---|---|
| `form_opened` | 視窗框架已開啟(只執行 1 次) |
| `form_preload` | 初始化 `dw_1` 之前;在頁籤之間切換時也會執行 |
| `form_readsql` | 檢查 data window 背後的 SQL 指令 |
| `dw_1_load` | `dw_1` 取回資料之後立即執行 |
| `form_load` | 表單載入資料之後立即執行 |
| `form_loaded` | 表單載入之後 |
| `form_show` | 開啟視窗流程結束(表單已載入並完整顯示) |

*其他表單流程*

| 事件 | 說明 |
|---|---|
| `form_resize` | 視窗大小改變 |
| `form_rowfocuschange` | `dw_1` 的列即將變更(變更前) |
| `form_rowfocuschanged` | `dw_1` 的列已變更 |
| `form_new` | 視窗以新增模式開啟 |
| `form_update` / `form_updated` | 更新開始 / 更新之後 |
| `form_close` / `form_closed` | 開始關閉 / 關閉中 |
| `form_delete` / `form_deleted` | 刪除指令執行中 / 刪除之後 |

*頁籤事件*:視窗文件內部若有頁籤(不是最上方的頁籤),在多數視窗中,頁籤名稱為 `etab`。

| 事件 | 說明 |
|---|---|
| `etab_firstredraw` | 頁籤第一次重繪時。**最適合插入或變更自己的物件** |
| `etab_redraw` | 重繪頁籤物件 |
| `etab_tabchanged` | 頁籤已切換 |

*DataWindow 事件*:`dw_1`–`dw_7` 各有自己的事件系統,事件名稱以 `dw_[編號]_` 開頭。以下以 `dw_1` 為例。官方只替 `dw_1_windowsetting`、`dw_1_rowfocuschanging` 附了說明,**其餘各列的說明是依事件名稱的解讀**:

| 事件 | 說明 |
|---|---|
| `dw_1_load` | |
| `dw_1_windowsetting` | 瀏覽型表單:定義欄位 |
| `dw_1_rowfocuschanging` | 列焦點變更期間 |
| `dw_1_click`、`dw_1_item_[column]_click` | 點擊 |
| `dw_1_itemchanged`、`dw_1_item_[column]_itemchanged` | 欄位變更 |
| `dw_1_update`、`dw_1_updated` | 更新 / 更新之後 |
| `dw_1_rightclick`、`dw_1_item_[column]_rightclick` | 按右鍵 |
| `dw_1_inserted` | 新增列之後 |
| `dw_1_delete`、`dw_1_deleted` | 刪除 / 刪除之後 |

*`dw_master`*:文件的主框架,呈現所有按鈕(上、右、下)與右鍵彈出選單。

| 事件 | 說明 |
|---|---|
| `dw_master_item_button_[object]_click` | 點擊按鈕或右鍵選單 |
| `dw_master_click` | 點擊 `dw_master` 的元素 |
| `dw_master_item_b_tab_[x]_click` | 點擊主頁籤 x |

### 常見使用者事件(Userevent)

| 事件 | 時機 |
|---|---|
| `CreateWorkOrderPositionEnd` | 工單位置建立後 |
| `CreateWorkOrderPositionComplete` | 每個新的主工單位置完成後 |
| `workordermangement.GetBatchNumber` / `GetSerialNumber` | 工單收貨/序號保留需要新批號/序號時 |
| `ReceiptEnd`(`materialmanagement` / `workordermangement`) | 收貨完成後 |
| `TimeRecording` | 建立時間回報時 |

> 使用者事件的建立方式、沙箱規則與相容性說明,見[第 11 節](#11-使用者事件user-events說明)。
