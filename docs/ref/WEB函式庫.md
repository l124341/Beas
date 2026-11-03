# WEB 函式庫（WEB APP 2.0：ui.js / ux.js / beas.js 等）參考文件

> 來源：Beas 官方說明文件（Beas Script 2026.09）「WEB and Rest Service」章節。本文件依官方目錄順序逐頁翻譯整理；函式名、參數名、屬性名、HTML/CSS 類別名一律保留原文英文；範例程式碼逐字保留原文（含原文錯字）。原文中的截圖以「〔圖：檔名〕」標示，未收錄圖片本身。

## 目錄

- **WEB and Rest Service**
  - [WEB and Rest Service](#web-and-rest-service)（`web_app_version_2.htm`）
- **WEB and Rest Service / Libraries WEB APP 2.0**
  - [Libraries WEB APP 2.0](#libraries-web-app-20)（`libraries.htm`）
  - [ui.js](#uijs)（`ui_js.htm`）
    - [ui radiobox](#ui-radiobox)（`webui_radiobox.htm`）
    - [ui input](#ui-input)（`webui_input.htm`）
    - [ui dgrid](#ui-dgrid)（`ui-dgrid.htm`）
    - [ui fa](#ui-fa)（`webui_fa.htm`）
  - [ux.js](#uxjs)（`ux_js.htm`）
    - [ux absl](#ux-absl)（`ux-absl.htm`）
    - [ux aget](#ux-aget)（`ux-aget.htm`）
    - [ux ajax](#ux-ajax)（`ux-ajax.htm`）
    - [ux apost](#ux-apost)（`ux-apost.htm`）
    - [ux autocomplete](#ux-autocomplete)（`ux-autocomplete.htm`）
    - [ux appTitleMenu](#ux-apptitlemenu)（`ux-apptitlemenu.htm`）
    - [ux click](#ux-click)（`ux-click.htm`）
    - [ux dialog](#ux-dialog)（`ux-dialog.htm`）
    - [ux dgrid](#ux-dgrid)（`ux-dgrid.htm`）
    - [ux focus](#ux-focus)（`ux-focus.htm`）
    - [ux getElement](#ux-getelement)（`ux-getelement.htm`）
    - [ux loading](#ux-loading)（`ux-loading.htm`）
    - [ux listen](#ux-listen)（`webux_listen.htm`）
    - [ux set](#ux-set)（`ux-set.htm`）
    - [ux saveAll](#ux-saveall)（`ux-saveall.htm`）
    - [ux tabs](#ux-tabs)（`ux-tabs.htm`）
    - [ux getToday](#ux-gettoday)（`ux-gettoday.htm`）
  - [beas.js](#beasjs)（`beas_js.htm`）
  - [gs1_128.js](#gs1_128js)（`gs1_128_js.htm`）
  - [class_serials.js](#class_serialsjs)（`class_serials_jx.htm`）
  - [class_batches.js](#class_batchesjs)（`class_batches_js.htm`）
  - [class_extendedSearch.js](#class_extendedsearchjs)（`class_extentedsearch_js.htm`）
  - [class_attachment.js](#class_attachmentjs)（`class_attachment.htm`）

（共 31 頁：「WEB and Rest Service」1 頁；「Libraries WEB APP 2.0」30 頁。）

---

## WEB and Rest Service

### WEB and Rest Service
（`web_app_version_2.htm`）

完整說明以下內容：

- WEB / REST 領域中的所有服務
- 其運作方式
- 如何修改或建立自己的服務
- 所有使用之函式庫（JavaScript、BeasScript）的參考

〔圖：serverconcept.png〕

子頁面：Libraries WEB APP 2.0（`libraries.htm`）

---

## WEB and Rest Service / Libraries WEB APP 2.0

### Libraries WEB APP 2.0
（`libraries.htm`）

**以下函式庫一律內含**

系統使用下列標準函式庫。若執行函式 `_header()`，系統會初始化下列函式庫：

| Library | 說明 |
|---|---|
| ui.js | 用來建立使用者介面（User Interface） |
| ux.js | 用來為元件加上「活性」（"live"） |
| beas.js | 框架導覽列（nav bar）的共用函式 |

**可用的附加函式庫**

範例：

```
<script src="assets21/js/gs1_128.js"></script>
```

可加入下列標準函式庫：

| Library | 說明 |
|---|---|
| assets21/js/gs1_128.js | GS1-128 解碼支援 |
| assets21/js/class_serials.js | batch extended editor（批次延伸編輯器） |
| assets21/js/class_batches.js | serial extended editor（序號延伸編輯器） |
| assets21/js/class_extendedSearch.js | 延伸搜尋功能 |

⚠ 原文疑似不一致：此表中 `class_serials.js` 標為「batch extended editor」、`class_batches.js` 標為「serial extended editor」，與檔名（serials／batches）看似對調；照原文翻譯。

**Current App（目前的 App）**

可以將目前的 app 存成獨立檔案：

```
[program path]\httpserver\beasweb\assets2\apps\[currentapp]\app.js
```

並用以下方式引入：

```
<script src="assets21/apps/[currentapp]/app.js"></script>
```

或者，也可以存放在專案資料夾：

```
[projectfolder]\httpserver\\apps\[currentapp]\app.js
```

並在程式定義（program definition）中以下列方式定義路徑：

```
<script src="assets21/apps/[currentapp]/app.js"></script>
```

⚠ 原文疑似不一致：存檔路徑寫 `assets2\apps\...`，而引入用的 `src` 寫 `assets21/apps/...`；照原文保留。

Note：

每個 app 都以「t20_」開頭。資料夾名稱為去掉前綴後的 app 名稱。

**Multi language（多語言）**

多語言支援方式：

```
<script=src="assets21/apps/goodsIssue/i18n/@currentlanguage@.js"></script>
```

---

### ui.js
（`ui_js.htm`）

〔圖：ui_tab.png〕

本頁以「Topic／Method」表列出 ui.js 的方法；其中 `fa`、`dgrid`、`input`、`radiobox` 另有獨立頁面（見下方各節），其餘方法說明如下。

| Topic | Method |
|---|---|
| columns | `columns(columns)` |
| section | `section(content, options)` |
| title | `title(content, options)` |
| fa | `fa(iconName, size)` return a bitmap（見 `webui_fa.htm`） |
| box | `box(content, options)` |
| card | `card(content, options)` |
| tabs | `tabs(id, tabs)` |
| dgrid | `dgrid(id)` return a container for a table（見 `ui-dgrid.htm`） |
| message | `message(errorCode, errorText)` |
| form | `form(id, action, content)` |
| encodeHTML | `encodeHTML(s)` |
| button | `button(id, label, options)` |
| input | `input(label, name, value, options)`（見 `webui_input.htm`） |
| textarea | `textarea(label, name, value, options)` |
| select | `select(label, name, value, kv, options)` |
| radiobox | `radiobox(name, value, kv, options)`（見 `webui_radiobox.htm`） |
| checkbox | `checkbox(label, name, checked, options)` |
| more | `more(title, content)` |

#### columns — `columns(columns)`

**說明**：繪製一組（響應式）欄位（columns）。

**參數**

| 型別 | 說明 |
|---|---|
| array | 由 JSON 物件組成、描述各欄的陣列：`[ column1, column2, ...m columnN ]`。columnX 定義見下表 |

columnX 定義：

| 屬性 | 說明 |
|---|---|
| ID | （選用）欄位 ID |
| size | 此欄寬度。可用的大小：3/4, 2/3, 1/2, 1/3, 1/4, 4/5, 3/5, 2/5, 1/5 |
| content | 欄位內容 |

**傳回值**：string — 建立這些欄位的 HTML。

**範例**

```
> ui.columns( [{size:"1/5",content:"This columns should be 1/5 of the size. (Or all width in a mobile)"},
{size:"3/5",content:"This columns should be 3/5 of the size. (Or all width in a mobile)"},
{size:"1/5", content:"This columns should be 1/5 of the size. (Or all width in a mobile)"}] );
```

Preview：在範例的預覽中，可以看到上面的字串如何修改欄位的寬度。

#### section — `section(content, options)`

**說明**：一個簡單的容器，用來把頁面分成數個區段（section）。

**參數**

| 型別 | 說明 |
|---|---|
| string | HTML 內容 |
| object | 選項（json object）：見下表 |

| 選項 | 說明 |
|---|---|
| ID | （選用）控制項 ID |
| hero | 此區段為 hero。（true\|false） |

**傳回值**：string — 建立此 section 的 HTML。

**範例**

```
> ui.section( "Put here the content" );
```

Preview：在範例的預覽中，可以看到此字串的結果：
"Insert content here."

⚠ 原文疑似不一致：範例傳入的是 "Put here the content"，但預覽說明的結果文字是 "Insert content here."。

#### title — `title(content, options)`

**說明**：建立標題／副標題（title/subtitle）。

**參數**

| 型別 | 說明 |
|---|---|
| string | HTML 內容 |
| object | 選項（json object）：見下表 |

| 選項 | 說明 |
|---|---|
| type | 標題類型：(title\|subtitle) |
| size | 大小：(1,2,3,4,5,6,7,8) |

**傳回值**：string — 繪製此標題的 HTML。

**範例**

```
> ui.title( "This is a title" );
```

Preview：在範例的預覽中，可以看到此字串的結果：
"This is a title."

#### box — `box(content, options)`

**說明**：一個簡單的方框（box）。

**參數**

| 型別 | 說明 |
|---|---|
| string | HTML 內容 |
| object | 選項（JSON object）：見下表 |

| 選項 | 說明 |
|---|---|
| ID | （選用）此 tab 的 ID |

**傳回值**：string — 繪製此控制項的 HTML。

**範例**

```
> ui.box( "This is the box's content" );
```

Preview：在範例的預覽中，可以看到此字串的結果（一個方框，內容為）：
This is the content of the box.

⚠ 原文疑似不一致：box 的 ID 選項說明寫「The ID for this tab」（此 tab 的 ID）；另範例傳入 "This is the box's content"，預覽卻顯示 "This is the content of the box."。照原文翻譯。

#### card — `card(content, options)`

**說明**：card 元件由數個元素組成：

- header：title、subtitle、icon、image
- content（目前是：在 header 命令之後、直到執行 footer 命令之前的所有輸出）
- footer

**參數**

| 型別 | 說明 |
|---|---|
| object | 選項（JSON object）：見下表 |

| 選項 | 說明 |
|---|---|
| title | The tab title（tab 標題） |
| subtitle | The tab subtitle（tab 副標題） |
| img1 | 標題旁的圖片 |
| img2 | 內容旁的圖片 |
| content | The tab content（tab 內容） |
| buttons | 由 JSON 物件組成的按鈕定義陣列：見下表 |

buttons 中每個物件：

| 屬性 | 說明 |
|---|---|
| title | 按鈕標題 |
| link | 按鈕的連結 |
| onclick | onclick 時執行的 JS |

**傳回值**：string — 繪製此控制項的 HTML。

**範例**

```
> ui.card( { title:"MyCard",subtitle:"underline title",img1:"assets21/img/app_t20_demo.png", content:"
This is the content.
You can put anything here.
Button 3 will call a JS function.
", buttons:[ {title:"Button 1",link:"#"},{title:"Button 2",link:"#"},{title:"Button 3",link:"", onclick:"alert('JDG::JS-test')"}]  });
```

Preview：在範例的預覽中，可以看到此字串的結果：〔圖：ui_my_card.png〕

⚠ 原文疑似不一致：方法簽章為 `card(content, options)`（兩個參數），但參數表只列一個 object，且 content 是放在 options 物件內；選項說明用的是「tab」字樣（The tab title 等）。照原文翻譯。

#### tabs — `tabs(id, tabs)`

**說明**：建立一個 tab 控制項。

**參數**

| 型別 | 說明 |
|---|---|
| string | tab ID |
| array | JSON 物件陣列，包含繪製各 tab 的所有選項：`[ tab1, tab2, ..., tabN ]`。tabX 結構見下表 |

tabX 結構：

| 屬性 | 說明 |
|---|---|
| ID | （選用）此 tab 的 ID |
| title | tab 標題 |
| icon | tab 圖示（可用 `ui.fa()` 繪製圖示） |
| content | tab 內容 |

**傳回值**：string — 建立此 tab 的 HTML。

**範例**

```
> ui.tabs( "tabExample01", [ {title:"Tab1", icon:ui.fa("fire"), content:"Content for tab 1"},
 {title:"Tab2", icon:ui.fa("water")}, {title:"Tab3", icon:ui.fa("wind")}] );
```

Preview：在範例的預覽中，可以看到此字串的結果：〔圖：ui_tabs.png〕

#### message — `message(errorCode, errorText)`

**說明**：建立一個訊息框（message box）。

**參數**

| 型別 | 說明 |
|---|---|
| number | 錯誤代碼。若小於 0 則視為錯誤 |
| string | 訊息的 HTML |

**傳回值**：string — 建立此控制項的 HTML。

**範例**

```
> ui.message( -1, "Ups. This is an error box!" );
```

#### form — `form(id, action, content)`

**說明**：繪製表示一個表單（form）的 html。

**參數**

| 型別 | 說明 |
|---|---|
| string | 表單 ID |
| string | 送出（submit）表單時要執行的 action |
| string | 內容 |

**傳回值**：string — 建立此表單的 HTML。

**範例**

```
> ui.form( "formExample", "?page=myApp", html_with_the_content );
<
  html_with_the_content
```

#### encodeHTML — `encodeHTML(s)`

**說明**：將字串編碼，以便作為 HTML 標籤中的值使用。

**參數**

| 型別 | 說明 |
|---|---|
| string | 要編碼的字串 |

**傳回值**：void

**範例**

```
> ui.encodeHTML( "This & this " );
&lt; This &amp; this &lt;is a test&gt;
```

⚠ 原文疑似不一致：說明為「將字串編碼」，但傳回值寫 void；範例輸入為 `"This & this "`，下方所示結果卻含輸入中沒有的 `<is a test>` 部分。照原文保留。

#### button — `button(id, label, options)`

**說明**：繪製表示一個按鈕的 HTML。

**參數**

| 型別 | 說明 |
|---|---|
| string | 控制項 ID |
| string | 控制項標題 |
| options | 繪製控制項的所有選項（json object）：見下表 |

| 選項 | 說明 |
|---|---|
| link | 定義此按鈕的目標 URL |
| type | 定義套用到按鈕的類型（type） |
| licon | 定義按鈕內顯示的左側圖示（可使用 fontaweson 名稱） |
| readonly | 將控制項定義為唯讀 |
| disabled | 將控制項定義為停用 |

**傳回值**：string — 建立此控制項的 HTML。

**範例**

```
> ui.button( "buttonExample01", "Open google.com", {type:"is-link", link:"https://www.google.com", licon:"spider"} );
```

Preview：在範例的預覽中，可以看到此字串的結果：〔圖：ui_button_spider.png〕

#### textarea — `textarea(label, name, value, options)`

**說明**：繪製表示文字區域方塊（text area box）的 HTML。

**參數**

| 型別 | 說明 |
|---|---|
| string | 控制項的標籤（label） |
| string | 控制項的名稱（name） |
| string | 控制項的預設值 |
| options | 繪製控制項的所有選項（json object）：見下表 |

| 選項 | 說明 |
|---|---|
| placeholder | 定義輸入框的 placeholder，預設使用控制項的標題 |
| readonly | 將控制項定義為唯讀 |
| disabled | 將控制項定義為停用 |
| error | 使用者未選取任何項目時的錯誤 |

**傳回值**：string — 建立此控制項的 HTML。

**範例**

```
> ui.textarea( "Remarks", "notes", "", {placeholder:"Type your notes"} );
```

Preview：在範例的預覽中，可以看到此字串的結果：〔圖：ui_textarea_remarks.png〕

#### select — `select(label, name, value, kv, options)`

**說明**：繪製表示選擇框（下拉選單，dropdown）的 HTML。

**參數**

| 型別 | 說明 |
|---|---|
| string | 控制項的標籤 |
| string | 控制項的名稱 |
| string | 控制項的預設值 |
| array | `[ [key1,value1], ..., [keyN,valueN] ]` |
| options | 繪製控制項的所有選項（json object）：見下表 |

| 選項 | 說明 |
|---|---|
| licon | 定義按鈕內顯示的左側圖示（可使用加上前綴 "fa-" 的 fontawesome 名稱） |
| readonly | 將控制項定義為唯讀 |
| disabled | 將控制項定義為停用 |
| error | 使用者未選取任何項目時的錯誤 |

**傳回值**：string — 建立此控制項的 HTML。

**範例**

```
> ui.select( "Choose an option", "select-example", "", [[1,"Option 1"],[2,"Option 2"],[3,"Option 3"]], {licon:"fa-bolt"} );
```

Preview：在範例的預覽中，可以看到此字串的結果：〔圖：ui_select_option.png〕

#### checkbox — `checkbox(label, name, checked, options)`

**說明**：繪製表示一組選項（核取方塊，check box）的 HTML。

**參數**

| 型別 | 說明 |
|---|---|
| string | 控制項的名稱 |
| string | 控制項的預設值 |
| array | `[ [key1,value1], ..., [keyN,valueN] ]` |
| options | 繪製控制項的所有選項（json object）：見下表 |

| 選項 | 說明 |
|---|---|
| readonly | 將控制項定義為唯讀 |
| disabled | 將控制項定義為停用 |

**傳回值**：string — 建立此控制項的 html。

**範例**

```
> ui.checkbox( "Do you like our new framework?", "checkbox-example", true );
```

Preview：在範例的預覽中，可以看到此字串的結果：〔圖：ui_checkbox.png〕

⚠ 原文疑似不一致：簽章為 `checkbox(label, name, checked, options)`，範例也傳入 (label, name, true)，但參數表列的是 name、預設值、key/value 陣列、options。照原文翻譯。

#### more — `more(title, content)`

**說明**：繪製一行收合起來的「更多資訊」。

**參數**

| 型別 | 說明 |
|---|---|
| string | 控制項標題 |
| string | 控制項內容 |

**傳回值**：string — 建立此控制項的 html。

**範例**

```
> ui.more( "Click here to expand the information", "This is the information
inside of this control
Hello!!" );
```

Preview：在範例的預覽中，可以看到此字串的結果——會出現一個連結：
Click here to expand the information
- 在 app 中點選該連結，可以測試資訊如何展開。可以點選方框右上角的「Close」按鈕關閉顯示的訊息框。

---

### ui radiobox
（`webui_radiobox.htm`）

**說明**：繪製表示一組互斥選項（radio-box）的 HTML。

**參數**

| 型別 | 說明 |
|---|---|
| string | 控制項的名稱 |
| string | 控制項的預設值 |
| array | `[ [key1,value1], ..., [keyN,valueN] ]` |
| options | 繪製控制項的所有選項（json object）：見下表 |

| 選項 | 說明 |
|---|---|
| readonly | 將控制項定義為唯讀 |
| disabled | 將控制項定義為停用 |
| legend | 顯示在 radio 按鈕左側的標題。範例見下 |

legend 範例：

```
ui.radiobox("filterBy","B", [
                ['B',_t('Batches')],
                ['S',_t('Serials')]
                ],
                { legend:_t('Search ins')+': '}
                )
```

**傳回值**：string — 建立此控制項的 HTML。

**範例：**

```
let html=ui.radiobox("filterBy","B", [
                ['B',_t('Batches')],
                ['S',_t('Serials')]
                ],
                { legend:_t('Search ins')+': '}
                )
document.getElementById("app-container").innerHTML = html;

```

(原文此處為圖片:`ui_radiobox_option.png`)

〔圖：ui_radiobox_option.png〕

---

### ui input
（`webui_input.htm`）

**說明**：繪製表示輸入框（input box）的 HTML。

**參數**

| 型別 | 說明 |
|---|---|
| string | 控制項的標籤 |
| string | 控制項的名稱 |
| string | 控制項的預設值 |
| options | 繪製控制項的所有選項（json object）：見下表 |

| 選項 | 說明 |
|---|---|
| type | 定義輸入框的類型，預設為 text。(hidden\|text\|number\|date) |
| placeholder | 定義輸入框的 placeholder，預設使用控制項的標題 |
| licon | 定義按鈕內顯示的左側圖示（可使用 fontawesome 名稱） |
| ricon | 定義按鈕內顯示的右側圖示（可使用 fontawesome 名稱） |
| lbutton | 欄位左側有一個動作按鈕，例如選項圖示 |
| rbutton | 欄位右側有一個動作按鈕，例如搜尋圖示 |
| readonly | 將控制項定義為唯讀 |
| disabled | 將控制項定義為停用 |
| error | 使用者未選取任何項目時的錯誤 |

**傳回值**：string — 建立此控制項的 HTML。

**範例：**

```
ui.input( "User Name", "name", "", {
 placeholder:"Type your name",
 licon:"fa-user",
 rbutton:"fa-search"
 } );
```

〔圖：ui_input_username.png〕

---

### ui dgrid
（`ui-dgrid.htm`）

**說明**：建立用來容納 DataGrid 的容器。

**參數**

| 型別 | 說明 |
|---|---|
| string | 控制項 ID |

**傳回值**：string — 用來容納由 UX 函式庫建立之控制項的 HTML。

**範例**

```
> ui.dgrid( "gridExample01" );
```

Preview：在範例的預覽中，可以看到此字串的結果。

---

### ui fa
（`webui_fa.htm`）

**說明**：繪製表示 font-aweson 圖示的 HTML。

**參數**

| 型別 | 說明 |
|---|---|
| string | 圖示名稱（名稱請查閱：https://fontawesome.com/icons?d=gallery ） |
| string | （選用，預設："small"）圖示大小：small, medium, large, 2x, 3x |

**傳回值**：string — 建立此圖示的 HTML。

**範例**

```
> ui.fa( "smile-wink" );
&lt; <span class="icon"><i class="fas fa-smile-wink"><></span>
```

Preview：在範例的預覽中，可以看到此字串的結果，一個眨眼圖示：〔圖：wink_icon.png〕

⚠ 原文疑似不一致：ui.js 總表中 `fa(iconName, size)` 的說明為「return a bitmap」，本頁則說傳回建立圖示的 HTML（string）。

---

### ux.js
（`ux_js.htm`）

**Function Library（函式庫）**

| Topic | Method |
|---|---|
| absl | `absl(bsl-object,callback,options[])` a bsl wrapper（bsl 包裝函式；見 `ux-absl.htm`） |
| aget | `aget(url, callback, options={})` ux.ajax（GET）的包裝函式（見 `ux-aget.htm`） |
| ajax | `ajax(url, callback, options={})` ux.ajax POST 的包裝函式（見 `ux-ajax.htm`） |
| apost | `apost(url, data, callback, options={})` ux.ajax（POST）的包裝函式（見 `ux-apost.htm`） |
| appTitleMenu | `appTitleMenu(action,id,title,callback)` 操作 app 選單下拉項目（見 `ux-apptitlemenu.htm`） |
| autocomplete | `autocomplete(id, url, options)` 為任何輸入框加上自動完成功能（見 `ux-autocomplete.htm`） |
| click | `click(ID, func)` listen 函式的包裝函式（見 `ux-click.htm`） |
| dgrid | 從資料或 BSL 呼叫建立表格（見 `ux-dgrid.htm`） |
| dialog | `dialog(title, content, options, onclose)` 模態對話框（見 `ux-dialog.htm`） |
| focus | `focus(fs, f)` 依規則設定焦點（見 `ux-focus.htm`） |
| formatNumber | `formatNumber(Number,CountOfDecimals,UseGrouping)` 將數字轉成具有指定小數位數的本地字串格式（原文：Convet Number to local string fomrat with defined decimal places） |
| getElement | `getElement(ID)` 依 Id 或 Name 搜尋元素（見 `ux-getelement.htm`） |
| listen | `listen(eventName, ID, func)`（見 `webux_listen.htm`） |
| loading | `loading(display)` 顯示載入遮罩，避免使用者點選任何控制項（見 `ux-loading.htm`） |
| saveAll | `saveAll(url, data, callback, options={})` 將資料送到後端，期間鎖住 app。同時會把「Save」按鈕切換成「Saving」（見 `ux-saveall.htm`） |
| set | `set(idName, status, options)` 對輸入控制項提供視覺回饋（見 `ux-set.htm`） |
| tabs | `tabs(id, action, params=null)` 為 ui.tabs 加上功能（見 `ux-tabs.htm`） |
| toJson | `toJson(string,boolean quoted)` 將字串轉換成 JSON 字串（convert ' to ''） |

**Date/Time Functions（日期／時間函式）**

| Topic | Method |
|---|---|
| `getDate()` | 以 Date 物件傳回公司日期／時間（company Date/Time） |
| `getToday(Separator,Date)` | 以本地格式字串傳回日期（見 `ux-gettoday.htm`） |
| `getTime(Separator,Time)` | GetTime(json date astring)：以本地字串傳回時間 |
| jsonDate2local | `jsonDate2local(d)` 將 JSON Date 轉成本地日期字串 |
| jsonDateTime2local | `jsonDateTime2local(d)` 將 Json DateTime 字串轉成本地日期時間字串 |
| min2time | `min2time(Minutes)` 傳回格式為 "x Days hh:mm" 的字串 |
| sec2time | `sec2tome(Seconds)` 傳回格式為 "x Days hh:mm:ss" 的字串 |

（原文 Date/Time 表最後另有一列空白列。）

⚠ 原文疑似不一致：Topic 為 `sec2time`，方法欄寫 `sec2tome(Seconds)`；`getTime(Separator,Time)` 的說明寫「GetTime(json date astring)」。照原文保留。

---

### ux absl
（`ux-absl.htm`）

**說明**：以簡單的方式執行 Beas BSL 命令。

**參數**

| 型別／名稱 | 說明 |
|---|---|
| bsl | bsl 物件名稱（bsl object name） |
| object | bsl object：見下表 |
| function | 命令收到回覆後要執行的 Callback 函式 |
| object | 選項（JSON object）：見下表 |

bsl object：

| 屬性 | 說明 |
|---|---|
| select | `$select` 部分，字串 |
| filter | `$filter` 部分，字串 |
| orderby | `$orderby` 部分，字串 |
| transaction | `$transaction` 選項 |
| format | `$format`。Standard = json string |

選項：

| 選項 | 說明 |
|---|---|
| contentType | json\|multipart\|standard |
| timeout | 等待回覆的最長時間 |
| loading | 傳送／接收資料期間鎖住 app。預設：true |

**傳回值**：void

**範例**

```
// Example: get itemName from item
let itemCode='RM';
ux.bsl("Item",
    {
      select:"ItemName",
      filter:"ItemCode eq "+ux.toJson(itemCode)
    },
  function(err, result.value)
    {
     ux.dialog(_t("Info"),_t("Item Name: " +result.value[0], { buttons:[{id:'ok', title:_t("Ok"),type:"is-danger"}] } );
    }
  );
```

⚠ 原文疑似不一致：本頁標題為 `absl`，但範例呼叫的是 `ux.bsl(...)`；ux.js 總表的簽章為 `absl(bsl-object,callback,options[])`（3 個參數），本頁參數表則列 4 個（bsl 物件名稱、bsl object、function、options）。另範例 callback 寫成 `function(err, result.value)`。照原文保留。

---

### ux aget
（`ux-aget.htm`）

**說明**：ux.ajax（GET）的包裝函式。

**參數**

| 型別 | 說明 |
|---|---|
| string | 要載入的 URL |
| function | 命令收到回覆後要執行的 Callback 函式 |
| object | 選項（JSON object）：見下表 |

| 選項 | 說明 |
|---|---|
| contentType | json\|multipart\|standard |
| timeout | 等待回覆的最長時間 |
| loading | 傳送／接收資料期間鎖住 app。預設：true |

**傳回值**：void

**範例**

```
        ux.aget( 'http://www.google.es'
                , function(err, result) {
                    if ( !err /* && result hasn't errors */ ) {
                            ux.dialog(_t("Received!"),result, { buttons:[{id:'ok', title:_t("Ok"),type:"is-success"}] } );
                    } else {
                        ux.dialog(_t("Error"),_t("An unknown error happened. Please wait some seconds and try again."), { buttons:[{id:'ok', title:_t("Ok"),type:"is-danger"}] } );
                        ux.loading(false);
                    }
                }
            );
```

無可用預覽（No Preview available）。
「Push to execute」——會出現一個連結以執行命令。

---

### ux ajax
（`ux-ajax.htm`）

**說明**：ux.ajax（POST）的包裝函式。

**參數**

| 型別 | 說明 |
|---|---|
| string | 要載入的 URL |
| function | 命令收到回覆後要執行的 Callback 函式 |
| object | 選項（JSON object）：見下表 |

| 選項 | 說明 |
|---|---|
| data | 包含要傳送之參數的 JSON 物件 |
| contentType | json\|multipart\|standard |
| timeout | 等待回覆的最長時間 |
| loading | 傳送／接收資料期間鎖住 app。預設：true |

**傳回值**：void

**範例**

```
      ux.ajax( 'http://www.url-to-send-get.com/path'
              , function(err, result) {
                  if ( !err /* && result hasn't errors */ ) {
                          ux.dialog(_t("Sent and Received!"),result, { buttons:[{id:'ok', title:_t("Ok"),type:"is-success"}] } );
                  } else {
                      ux.dialog(_t("Error"),_t("An unknown error happened. Please wait some seconds and try again."),
                       { buttons:[{id:'ok', title:_t("Ok"),type:"is-danger"}] } );
                        ux.loading(false);
                    }
                }
            );
```

無可用預覽（No Preview available）。

⚠ 原文疑似不一致：本頁（`ux.ajax`）說明自身是「A wrapper for ux.ajax (POST)」；範例 URL 名為 `url-to-send-get`。照原文翻譯。

---

### ux apost
（`ux-apost.htm`）

**說明**：ux.ajax（POST）的包裝函式。

**參數**

| 型別 | 說明 |
|---|---|
| string | 要載入的 URL |
| object | 包含要傳送之參數的 JSON 物件 |
| function | 命令收到回覆後要執行的 Callback 函式 |
| object | 選項（JSON object）：見下表 |

| 選項 | 說明 |
|---|---|
| contentType | json\|multipart\|standard |
| timeout | 等待回覆的最長時間 |
| loading | 傳送／接收資料期間鎖住 app。預設：true |

**傳回值**：void

**範例**

```
      ux.apost( 'http://www.url-to-send-post.com/path'
              , {data1:'hello',moreData:'World!'}
              , function(err, result) {
                  if ( !err /* && result hasn't errors */ ) {
                          ux.dialog(_t("Sent and Received!"),result, { buttons:[{id:'ok', title:_t("Ok"),type:"is-success"}] } );
                  } else {
                      ux.dialog(_t("Error"),_t("An unknown error happened. Please wait some seconds and try again."), { buttons:[{id:'ok', title:_t("Ok"),type:"is-danger"}] } );
                      ux.loading(false);
                  }
              }
          );
```

無可用預覽（No Preview available）。

---

### ux autocomplete
（`ux-autocomplete.htm`）

**說明**：（原文本頁「Description」欄為空白。ux.js 總表的說明為：為任何輸入框加上自動完成功能。）

**參數**

| 型別 | 說明 |
|---|---|
| string | 控制項的 ID |
| string | 擷取資料的 URL（filter 的值必須是 `*q*`，本函式之後會將它替換為目前的值） |
| object | 建立自動完成元件的選項（JSON object）：見下表 |

| 選項 | 說明 |
|---|---|
| ondraw | 一個函式，傳入目前欄位的值，必須傳回繪製此儲存格的 HTML |

**傳回值**：void

**範例**

```
// Create the container
document.getElementById("ux-autocomplete-example01").innerHTML =
  ui.input( "ItemCode", "ux-autocomplete-cexample01", "", {
    placeholder:"Type an ItemCode or Name",
    licon:"fa-search" } );
ux.autocomplete('ux-autocomplete-cexample01',
   '?program_id='+appInfo.gid+'&page=t20_demo&get=dgrid02&q=*q*');
```

Preview：（請試著輸入資料庫中存在的「ItemCode」或「ItemName」）
在範例的預覽中，可以看到此字串的結果：
開始輸入現有資料庫中的 ItemCode 或 ItemName，就會跳出一份含有資料的清單，如下：〔圖：ux_autocomplete.png〕

---

### ux appTitleMenu
（`ux-apptitlemenu.htm`）

**說明**：操作 app 選單的下拉項目。

**參數**

| 型別 | 說明 |
|---|---|
| string | 動作（原文：Action add/delete/modify)） |
| string | 該項目的 ID |
| string | 新項目的標題（或新的標題） |
| callback | 選取該選項時的 callback 函式 |

**傳回值**：無傳回值。

**範例**：原文範例欄為空白。

---

### ux click
（`ux-click.htm`）

**說明**：listen 函式的包裝函式。

**參數**

| 型別 | 說明 |
|---|---|
| string | 要加上 onclick 事件的元素 ID |
| function | 事件發生時要觸發的函式 |

**傳回值**：無傳回值。

**範例**

```
<span id="click-example">Try to click me</span>
<script>ux.click("click-example",function(){alert("Not so strong! XD");});</script>
```

Preview：在範例的預覽中，可以看到此字串的結果：
會顯示一個可點選的文字：「Try to click me」。點選後會在另一個方框中出現訊息，可以按「OK」關閉。

---

### ux dialog
（`ux-dialog.htm`）

**說明**：模態對話框（modal dialog box）。

**參數**

| 型別 | 說明 |
|---|---|
| string | 標題 |
| string | HTML 內容 |
| object | 繪製控制項的所有選項（json object）：見下表 |
| function | 對話框關閉或按下按鈕時要呼叫的函式。按下的按鈕 id 會作為參數傳入（若未按任何按鈕就關閉則為 null） |

| 選項 | 說明 |
|---|---|
| icon | 訊息中要顯示的圖示 |
| buttons | 按鈕定義物件的陣列（ID, title, type [is-success/is-info/is-warning/is-danger]]） |

**傳回值**：無傳回值。

**範例**

```
<a href="#" onclick="dialog_example()">Click here</a> to display a modal dialog
 <script>
    function dialog_example() {
      ux.dialog("UX Tutorial","This is a dialog.<br> Do you like it?", {buttons:[ {id:'Y', title:'Yes'},{id:'SY',title:'SUper Yes'}]}, function(b) { alert('You pressed the button with id: '+b); } );
    }
</script>
```

Preview：在範例的預覽中，可以看到此字串的結果：
"Click here to display a modal dialog box."
- 點選預覽中的連結，會開啟一個對話框供測試：〔圖：ux_tutorial_dialogbox.png〕

---

### ux dgrid
（`ux-dgrid.htm`）

**說明**：建立一個可運作的資料表格（data grid），並以有效 oData 來源的資料填入。

1. 用 ui.dgrid 定義容器
2. 定義表格。事先定義資料（Data），或定義 BSL URL 字串來載入資料

Note：僅支援 JSON Array。

**參數**

| 型別 | 說明 |
|---|---|
| string | 控制項的 ID |
| object | 建立表格的選項（JSON object）：見下表 |

**選項（object）**

| 選項 | 說明 |
|---|---|
| fields | 欄位（field）陣列：`[ field1, field2, ..., fieldN ]`。Field 結構見下表 |
| rows | （會透過 url 填入。）由列資料陣列組成的陣列：`[ [row1_col1,row1_col2,...,row1_colN], ..., [rowN_col1,...,rowN_colN]]` |
| bsl | 定義 bsl 物件，系統會自動建立 URL。詳見下方 |
| URL | 取得資料之 url 的字串（oData 或 Beas 產生的 dgrid 資料）。詳見下方 |
| sort_key | 用來排序結果的欄位名稱字串 |
| sort_dir | 設定排序為 (asc)ending 或 (desc)ending。預設：asc |
| onclick_row | 使用者選取一列時要執行的函式，該列的資料會作為引數傳入 |
| displayFilter | 必須傳回 true/false，表示 "dr" 這一列是否要納入。詳見下方 |
| onPageLoad | 在新頁面繪製完成後呼叫。dg 包含目前的 DataGrid |
| ondraw_row | function：若有定義，系統會呼叫此函式來繪製目前這一列。詳見下方 |
| reload | 重新載入表格的 callback 函式（例如：下一頁）。`reload: function(url,sort_key,sort_dir)` |
| page | （原文說明欄空白） |
| currentPage | 定義目前頁碼。預設 1 |
| limit | 定義每頁最多列數，預設 25 |
| offset | 預設 0 |
| totalRowCount | 定義套用篩選後的總列數 |

**Field 結構（fields 的每個元素）**

| 屬性 | 說明 |
|---|---|
| title | 欄位標題 |
| id | 定義 Beas Service Layer 的欄位名稱。若使用 beas Service Layer，可定義 BSL 欄位名稱（即 `$select` 命令後面的屬性）。更多請見屬性 BSL。若未手動定義 sort_key，這會自動設定 sort_key（原文：autoamtically） |
| sort_key | 若可依此欄位排序，填入欄位名稱 |
| align | left\|center\|right |
| hidden | true\|false 或 "M"（視窗寬度 < 300 Pixel 時隱藏）。Note：系統只在 app 重新啟動時才檢查此設定 |
| type | 定義目前欄位的格式／類型：見下表 |
| width | 設定此欄位寬度 |
| ondraw | 允許自行繪製此欄。詳見下方 |
| colorId | 定義與另一個欄位相關的顏色。詳見下方 |
| r | 依螢幕寬度讓欄位顯示／隱藏。詳見下方 |

type 可用值：

| Type | 說明 |
|---|---|
| text | 標準文字 |
| number | 數字 |
| date | 本地日期格式 |
| datetime | 本地日期＋時間格式 |
| time | 本地時間格式 |
| boolean | 核取方塊（check box） |
| min2time | 將分鐘轉換為時間 |
| bmp | 以圖片顯示 |

**ondraw**

`ondraw: function (v, col, dr) { Your Script }`

Parameters：

| 參數 | 說明 |
|---|---|
| v | （原文說明欄空白） |
| col | 欄的編號（第一欄為 0） |
| dr | 此列所有結果元素的陣列 |

```
title: _t('Available Resources'),
ondraw: function (v, col, dr) { return app.displayResourceLine(dr); }

displayResourceLine(dr) {
return 'the new html for this column';
}
```

**colorId**

所指定的（數值）欄位以下列格式提供顏色資訊：
red + 256*green + 65536*blue（BSL 會傳回這種類型的顏色）
在下例中，顏色定義於欄位 4（第一個欄位為 0，名稱 "ResourceColor"）；
設定 colorId:4 後，系統會對此欄位（Resource 與 Description）使用該欄位定義的顏色。

```
ux.dgrid("dgResources",{limit:10,fields:[
                {title:"Bitmap",id:"ResourceBitmap",type:"bmp"},
                {title:"Resource",id:"ResourceId",colorId:4},
                {title:"Description",id:"ResourceDescription",colorId:4},
                {tile:"Active",id:"Active"},
                {id:"ResourceColor",hidden:true}
                ],
                bsl:"Resource",
                orderby:"ResourceId"
                }
           );
```

**r**

範例：responsive:1 // 只在大小 600 以下顯示

| ID | Size |
|---|---|
| 1 | 只到 600ox，之後即不顯示（原文：unvisible）。若只想在行動裝置上顯示，請使用此值 |
| 2 | 從 600 - 835 |
| 3 | 從 835 - 1045 |
| 4 | 從 1045 - 1225 |
| 5 | 從 1255 以上 |

⚠ 原文疑似不一致：此屬性在表中名稱為 `r`，但範例寫的是 `responsive:1`；另 4 的範圍到 1225、5 從 1255 起，中間有落差；1 的單位寫作「600ox」。照原文保留。

**bsl**

定義要取得資料的 bsl 物件。
欄位定義於屬性 field[].Id。
其他屬性：filter、orderby
範例：

```
ux.dgrid("dgResources",{limit:10,fields:[
                {title:"Bitmap",id:"ResourceBitmap",type:"bmp"},
                {title:"Resource",id:"ResourceId",colorId:4},
                {title:"Description",id:"ResourceDescription"},
                {tile:"Active",id:"Active"},
                {title:"Color",id:"ResourceColor",hidden:true}
                ],
                bsl:"Resource",
                orderby:"ResourceId"
                }
           );
```

**URL**

Note：若使用 Beas Service Layer，請使用 bsl。
範例：透過 Beas Service Layer 載入品項（Items），欄位為品號與品名，可排序

```
ux.dgrid('ux-dgrid', {
    ,fields:[ {title:_t("Item No"), sort_key:'ItemCode'},{title:_t("Item Name"), sort_key:'ItemName'} ]
    ,url:'/odata4/Item?'+
      '$ProgramId='+appInfo.gid+ // define always the ProgramId and AppId if you work with BSL
      '&$AppId='+appInfo.appID+
      '&$format=jsonarray'+ // Format is always JSON Array
      '&$inlinecount=allpages'+
      '&$orderby=ItemCode'+ // Default Order
      '&$select=ItemCode,ItemName'
    ,onclick_row:function(d,rid) { console.log('Data in Row selected: ',d); }
    }
);
```

**displayFilter**

```
displayFilter: function (dr) { return true; }
```

**ondraw_row**

`ondraw_row:function(tbody,row,valueArray)`
範例：顯示定義的背景、Position、item number、item Name、Required（含 UoM）與 Issue

```
ondraw_row: function (tbody, rid, dr) {

            let row, cell, i, k, j, qtyString;

            // Material header
            row = tbody.insertRow(-1);
            row.setAttribute('data-rid', rid);
            if (dr[9] < dr[4] && dr[4] > 0 && dr[8] == 'M' && dr[7] == app.lineNumber2 && app.lineNumber3 == 0)
               row.className += 'has-background-danger';
            if ((dr[9] >= dr[4] || dr[8] != 'M' )  && dr[7]==app.lineNumber2 && app.lineNumber3 == 0)
               row.className += 'has-background-success';

            k = 0;
            cell = row.insertCell(k++);   // Pos.
            cell.innerHTML = dr[1];

            cell = row.insertCell(k++);    // Item No.
            cell.innerHTML = dr[2];

            cell = row.insertCell(k++); // Item Name
            cell.innerHTML = dr[3];

            cell = row.insertCell(k++);      // Required
            cell.style = 'text-align:right;padding-right: 4px;';
            cell.innerHTML = ux.formatNumber(dr[4], dr[6]) + (ux.useMinimalistUI ? '' : ' '+dr[5]);

            cell = row.insertCell(k++);    // Issue
            cell.style = 'text-align:right;padding-right: 4px;';
            cell.innerHTML = ux.formatNumber(dr[9], dr[6]) + (ux.useMinimalistUI ? '' : ' '+dr[5]);
                      }
```

⚠ 原文疑似不一致：ondraw_row 的簽章寫 `function(tbody,row,valueArray)`，範例則為 `function (tbody, rid, dr)`。

**傳回值**：void

**Example 01（using Beas Service Layer，使用 Beas Service Layer）**

```
// Create the container
document.getElementById("ux-dgrid-example").innerHTML = ui.dgrid( "ux-dgrid" );

// Initialize the datagrid
ux.dgrid('ux-dgrid', {   limit:10
    ,fields:[ {title:_t("Item No"), sort_key:'ItemCode'},{title:_t("Item Name"), sort_key:'ItemName'} ]
    ,url:'/odata4/Item?'+
      '$ProgramId='+appInfo.gid+
      '&$AppId='+appInfo.appID+
      '&$format=jsonarray'+
      '&$inlinecount=allpages'+
      '&$orderby=ItemCode'+
       &$select=ItemCode,ItemName'
    ,onclick_row:function(d,rid) { console.log('Data in Row selected: ',d); }
    }
);

// Later, we can set a new url with a filter
// ux.dgrid('ux-dgrid').url = **we can reset the url with a filter if we want**
// And reload the page(x)
ux.dgrid('ux-dgrid').page(1);
```

Preview：在範例的預覽中，可以看到此字串的結果：〔圖：ux_dgrid_ex_01.png〕

⚠ 原文疑似不一致：Example 01 標示「using Beas Service Layer」，但用的是 `url:'/odata4/Item?...'` 而非 `bsl` 屬性；其中 `&$select=ItemCode,ItemName'` 一行前缺少開頭引號與 `+`（程式碼照原文保留）。另 URL 範例第一行 `ux.dgrid('ux-dgrid', {` 後接 `,fields:`（物件開頭即有逗號）。

---

### ux focus
（`ux-focus.htm`）

**說明**：可以依規則把焦點設到選定的欄位。
提供一個控制項陣列與一個目標控制項的名稱：
若目標控制項有錯誤，就把焦點設到該控制項；否則應把焦點設到清單中下一個未停用（non-disabled）的欄位。

**參數**

| 型別 | 說明 |
|---|---|
| array | 依焦點順序排列的控制項陣列 |
| string | 目前取得焦點的控制項 |

**傳回值**：void

**範例**

```
// Create three fields
document.getElementById("ux-focus-example01").innerHTML =
    ui.input( "Field 1", "ux-focus01", "" )
        +ui.button("ux-focus01b1","Set error",{link:"javascript:ux.set('ux-focus01','error')"})
        +ui.button("ux-focus01b2","Set clear",{link:"javascript:ux.set('ux-focus01','clear')"})
    +'<br>'
    +ui.input( "Field 2", "ux-focus02", "" )
        +ui.button("ux-focus02b1","Set disabled",{link:"javascript:ux.set('ux-focus02','disabled')"})
        +ui.button("ux-focus02b2","Set clear",{link:"javascript:ux.set('ux-focus02','clear')"})
    +'<br>'
    +ui.input( "Field 3", "ux-focus03", "" )
    +'<br>'
    +'<br>'
    +ui.button("ux-focus01b1","ux.focus(next after =<br> 'Field 1')",{link:"javascript:ux.focus(['ux-focus01','ux-focus02','ux-focus03'],'ux-focus01')"});
```

Preview：（請先設定 error/disable，再點選 Set Focus）
在範例的 Preview 區段中，可以試用「Set error」與「Set clear」按鈕控制項。〔圖：ux_focus.png〕

---

### ux getElement
（`ux-getelement.htm`）

**說明**：依 ID 或 Name 搜尋元素。

**參數**

| 型別 | 說明 |
|---|---|
| string | 元素的 ID 或 name |

**傳回值**：找到時傳回該元素，否則為 null。

**範例**

```
<input id="getElement-exampleID" name="getElement-exampleName">
<input type="button" value="Example 1" onclick="ux.getElement('getElement-exampleID').value = 'By ID';">
<input type="button" value="Example 2" onclick="ux.getElement('getElement-exampleName').value = 'By Name';">
```

Preview：在範例的預覽中，可以看到此字串的結果：〔圖：ux_getelement_preview.png〕

---

### ux loading
（`ux-loading.htm`）

**說明**：顯示載入遮罩，避免使用者點選任何控制項。

**參數**

| 型別 | 說明 |
|---|---|
| boolean | 啟用或停用載入遮罩 |

**傳回值**：無傳回值。

**範例**

```
<a href="#" onclick="loading_example()">Click here</a> to display the loading mask for 5 seconds
 <script>
function loading_example() {
ux.loading(true);
setTimeout(function(){ ux.loading(false); }, 5000); }
</script>
```

Preview：在範例的預覽中，可以看到此字串的結果：
點選「Click here to display the loading mask for 5 seconds」訊息中的連結後，app 會顯示載入遮罩 5 秒。

---

### ux listen
（`webux_listen.htm`）

**說明**：在指定元素 ID 上監聽指定名稱的事件，然後觸發所給的函式。

**參數**

| 型別 | 說明 |
|---|---|
| string | 要監聽的事件。Type：mouseover、mouseout、changing、change、click |
| string | 要監看的元素 ID |
| function | 事件發生時要觸發的函式 |

**傳回值**：無傳回值。

**範例**

```
<span id="listen-example">Try to pass over me</span>
<script>
 ux.listen("mouseover","listen-example",function(){this.className='has-background-danger';});
 ux.listen("mouseout","listen-example",function(){this.className='';});
</script>
```

Preview：在範例的預覽中，可以看到此字串的結果：
會出現一個觸發事件的文字：「Try to pass over me」。滑鼠移到該行上時，只要滑鼠停留，該行就會以紅色醒目顯示。

---

### ux set
（`ux-set.htm`）

**說明**：對輸入控制項提供視覺回饋。

**參數**

| 型別 | 說明 |
|---|---|
| string | 控制項的 id 或 name |
| string | 新狀態：show\|hide\|readonly\|disabled\|error\|title\|subtitle\|clear |
| object | 所有選項（json object）：見下表 |

| 選項 | 說明 |
|---|---|
| buttons | 將此狀態套用到附加的按鈕（true\|false） |
| bX | 將此狀態套用到第 X 個附加按鈕（true\|false） |

**傳回值**：object — 該元素。

**範例**

```
<div id="set-example" class="preview"></div>
<script>
document.getElementById("set-example").innerHTML = ui.input( "User Name", "name", "", {placeholder:"Type your name", licon:"fa-user", rbutton:"fa-search" } )
 + ui.button( "set01", "set Error", {type:"is-link", link:"javascript:set01()", licon:"error"} )
 + ui.button( "set02", "set Disabled", {type:"is-link", link:"javascript:set02()", licon:"eye"} )
 + ui.button( "set03", "set Clear", {type:"is-link", link:"javascript:set03()", licon:"clean"} ) ;
 function set01() { ux.set("set-example","error"); }
 function set02() { ux.set("set-example","disabled"); }
 function set03() { ux.set("set-example","clear"); }
</script>
```

Preview：〔圖：ux_set_username.png〕

---

### ux saveAll
（`ux-saveall.htm`）

**說明**：將資料送到後端，期間鎖住 app。同時會把「Save」按鈕切換成「Saving」。

**參數**

| 型別 | 說明 |
|---|---|
| string | URL |
| object | 包含要傳送之參數的 JSON 物件 |
| function | 命令收到回覆後要執行的 Callback 函式 |
| object | 選項（JSON object）：見下表 |

| 選項 | 說明 |
|---|---|
| contentType | json\|multipart\|standard |
| timeout | 等待回覆的最長時間 |
| loading | 傳送／接收資料期間鎖住 app。預設：true |

**傳回值**：void

**範例**

```
ux.saveAll( '/odata4/v1/issue?$ProgramId='+appInfo.gid+'&$AppId='+appInfo.appID
        , json_with_all_data_to_send
        , function(err, result) {
            if ( !err /* && result hasn't errors */ ) {
                    ux.dialog(_t("Data saved!"),msg, { buttons:[{id:'ok', title:_t("Ok"),type:"is-success"}] }
                                , function(){
                                        // Reset here the app
                                }.bind(this)
                            );
            } else {
                ux.dialog(_t("Error"),_t("An unknown error happened. Please wait some seconds and try again."), { buttons:[{id:'ok', title:_t("Ok"),type:"is-danger"}] } );
                ux.loading(false);
            }
          }.bind(this)
        , {contentType:'json',timeout:180000}
    );
```

無可用預覽（No Preview available）。

---

### ux tabs
（`ux-tabs.htm`）

**說明**：為 ui.tabs 加上功能。

**參數**

| 型別 | 說明 |
|---|---|
| string | 控制項的 ID 或 name |
| string | 命令：init\|active |
| object | 參數依命令而定：見下表 |

| 命令 | 說明 |
|---|---|
| action=init | 將 params 設為 null 以初始化所有 tab，或設定一個 id 只初始化指定的那一個 |
| action=active | 設定要啟用（activate）的 tab 名稱 |
| action=enable | （原文說明欄空白） |
| action=disable | （原文說明欄空白） |

**傳回值**：void

**範例**

```
document.getElementById("your-container").innerHTML = ui.tabs( "tabExample01ux",
[ {title:"Tab1", icon:ui.fa("fire"), content:"Content for tab 1"},
{title:"Tab2", icon:ui.fa("water")}, {title:"Tab3", icon:ui.fa("wind")}] );
 ux.tabs("tabExample01ux","init");
```

Preview：在範例的預覽中，可以看到此字串的結果：〔圖：ux_tabs.png〕

⚠ 原文疑似不一致：命令參數說明只列 `init|active`，但參數表另列出 `action=enable`、`action=disable`（無說明）。

---

### ux getToday
（`ux-gettoday.htm`）

以本地格式字串傳回日期。

`string=getToday(string separator,date)`

- separator = 年、月、日之間的分隔符號。預設 = "/"
- date = 系統要轉換的日期

| 呼叫 | 說明 |
|---|---|
| `getToday()` | 以字串傳回公司日期，例如 2022/05/03 |
| `getToday("-")` | 以 "-" 為分隔符號、以字串傳回公司日期 2022-05-03 |
| `getToday(NULL,anyDate)` | 以 "/" 為分隔符號傳回所指定的日期 |

---

### beas.js
（`beas_js.htm`）

此類別由我們的框架在內部使用，用來處理使用者介面中的一些共通動作。
當你使用標準 UI 框架（在 beasScript 程式碼的開頭／結尾呼叫 `_header()` / `_footer()`）時，此函式庫會自動被引入。

```
<script src="assets21/js/beas.js"></script>
```

**Class members（類別成員）**

| Method Summary | 說明 |
|---|---|
| constructor | `constructor( )` — 初始化類別 |
| ping | `ping( ms )` — 每 Xms 向伺服器 ping 一次 |
| about | `about()` — 顯示「關於」（about）視窗 |
| aboutApp | `aboutApp()` — 顯示目前應用程式的「關於」視窗 |
| print | `print()` — 允許變更位置，並（原文：Allow to change the location and）顯示列出所有已列印文件的視窗，以便再次列印其中任一份 |

**Method Detail（方法細節）**

**constructor** — `constructor( mode, batches )`
初始化類別。
此類別會在頁面載入後自動實例化；不需要另外宣告新的。
Parameters：{void}
Returns：{void}

⚠ 原文疑似不一致：Method Summary 中為 `constructor( )`，Method Detail 中卻寫 `constructor( mode, batches )`，且 Parameters 寫 {void}。

**ping** — `ping( ms )`
每 Xms 向伺服器 ping 一次。
設定一個每 Xms 向伺服器送出請求的計時器；只有在伺服器沒有回應時才應停止計時器，並應顯示類似下圖的錯誤訊息：〔圖：beas_js_ping_error.jpg〕
Parameters：{void}
Returns：{void}

⚠ 原文疑似不一致：ping 的簽章有參數 `ms`，但 Parameters 寫 {void}。

**about** — `about()`
顯示「關於」視窗。〔圖：beas_js_about.jpg〕
Parameters：{void}
Returns：{void}

**aboutApp** — `aboutApp()`
顯示目前應用程式的「關於」視窗。〔圖：beas_js_aboutapp.jpg〕
Parameters：{void}
Returns：{void}

**print** — `print()`
允許變更位置，並（原文：Allow to change the location and）
顯示列出所有已列印文件的視窗，以便再次列印其中任一份。〔圖：beas_js_print.jpg〕
Parameters：{void}
Returns：{void}

（原文頁尾有一個單獨的字元「x」。）

---

### gs1_128.js
（`gs1_128_js.htm`）

此物件會解碼 GS1-128 條碼，並以關聯陣列（associative array）傳回條碼中找到的每個 AI。
要在 app 中引入此函式庫，只需把下列程式碼加到 Beas 中 app 原始碼的「_header()」與「_footer()」之間：

```
<script src="assets21/js/gs1_128.js"></script>
```

**Object members（物件成員）**

| Method Summary | 說明 |
|---|---|
| decode | `decode( bc )` — 解碼 GS1-128 條碼（原文：ba rcode） |
| getdate | `getdate( yymmdd )` — 將條碼日期格式化為 YYYY-MM-DD 格式的日期 |

**Method Detail（方法細節）**

**decode** — `decode( bc )`

解碼 GS1-128 條碼，並傳回一個關聯陣列，內含偵測到的所有 AI 及其值。

Parameters：

```
{string} bc
```

要解碼的條碼
例：Ê97rm01bÊ10LOT01234Ê98BWÊ99C-1001Ê3102012345
Ê0012345678901234567897rm01bÊ10LOT01234Ê98BWÊ99C-1001Ê3102012345

Returns：

```
{associative array}
```

Example

```
var res = gs1_128.decode( "Ê97rm01bÊ10LOT01234Ê98BWÊ99C-1001Ê3102012345" );
console.log(res);

{10: "LOT01234", 97: "rm01b", 98: "BW", 99: "C-1001", 310: "2012345"}
```

**getdate** — `getdate( yymmdd )`

將條碼日期格式化為 YYYY-MM-DD 格式的日期。

Parameters：

```
{string} yymmdd
```

格式為 YYMMDD 的條碼日期

Returns：

```
{string}
The date with the format YYYY-MM-DD
```

Example

```
var res = gs1_128.getdate( "770817" );
console.log( res );

2077-08-17
```

**Additional Info（補充資訊）**

此物件支援的 AI 清單如下：

| AI | 說明（原文） | 說明（中譯） | Type | Length |
|---|---|---|---|---|
| 00 | Serial Shipping Container Code (SSCC) | 系列貨櫃運送碼（SSCC） | string | 18 |
| 01 | Global Trade Item Number (GTIN) ( Item Code ) | 全球貿易品項代碼（GTIN）（品號） | number | 14 |
| 02 | GTIN of Contained Trade Items | 所含貿易品項的 GTIN | number | 14 |
| 10 | Batch/Lot Number (DistNumber) | 批次／批號（DistNumber） | string | 1-20 |
| 11 | Production Date | 生產日期 | date | 6 |
| 12 | Due Date | 到期日（Due Date） | date | 6 |
| 13 | Packaging Date | 包裝日期 | date | 6 |
| 15 | Best Before Date (YYMMDD) | 最佳食用期限（YYMMDD） | date | 6 |
| 17 | Expiration Date | 有效日期（Expiration Date） | date | 6 |
| 20 | Product Variant | 產品變體 | numeric | 2 |
| 21 | Serial Number | 序號 | string | 1-20 |
| 22 | Secondary Data Fields | 次要資料欄位 | string | 1-20 |
| 240 | Additional Product Identification | 附加產品識別 | string | 1-30 |
| 241 | Customer Part Number | 客戶料號 | string | 1-30 |
| 250 | Secondary Serial Number | 次要序號 | string | 1-30 |
| 30 | Count of items | 品項數量 | number | 1-8 |
| 310n-369n | Other information about the item | 品項的其他資訊 | string | 1-6 |
| 37 | Number of Units Contained | 所含單位數 | string | 1-8 |
| 390n | Amount payable (local currency) | 應付金額（本地幣別） | string | 1-15 |
| 391n | Amount payable (with ISO currency code) | 應付金額（含 ISO 幣別代碼） | string | 3-8 |
| 392n | Amount payable per single item (local currency) | 每單一品項應付金額（本地幣別） | string | 1-15 |
| 393n | Amount payable per single item (with ISO currency code) | 每單一品項應付金額（含 ISO 幣別代碼） | string | 3-18 |
| 400 | Customer Purchase Order Number | 客戶採購單號 | string | 1-30 |
| 401 | Consignment Number | 託運編號 | string | 1-30 |
| 402 | Bill of Lading | 提單（Bill of Lading） | number | 17 |
| 403 | Routing code | 路由代碼 | string | 1-30 |
| 410 | Ship To/Deliver To Location Code (Global Location Number) | 出貨／交貨地點代碼（Global Location Number） | number | 13 |
| 411 | Bill To/Invoice Location Code (Global Location Number) | 帳單／發票地點代碼（Global Location Number） | number | 13 |
| 412 | Purchase From Location Code (Global Location Number) | 採購來源地點代碼（Global Location Number） | number | 13 |
| 413 | Ship for, Deliver for, or Forward to Location Code (Global Location Number) | 代運、代交或轉運目的地點代碼（Global Location Number） | number | 13 |
| 414 | Identification of a physical location (Global Location Number) | 實體地點識別（Global Location Number） | number | 13 |
| 420 | Ship To/Deliver To Postal Code (Single Postal Authority) | 出貨／交貨郵遞區號（單一郵政機構） | string | 1-20 |
| 421 | Ship To/Deliver To Postal Code (with ISO country code) | 出貨／交貨郵遞區號（含 ISO 國家代碼） | string | 3-15 |
| 422 | Country of Origin (ISO country code) | 原產國（ISO 國家代碼） | string | 3 |
| 423 | Country or countries of initial processing | 初始加工國家 | string | 3-15 |
| 424 | Country of processing | 加工國家 | string | 3 |
| 425 | Country of disassembly | 拆解國家 | string | 3 |
| 426 | Country of full process chain | 完整加工鏈國家 | string | 3 |
| 7001 | NATO Stock Number (NSN) | 北約庫存編號（NSN） | string | 13 |
| 7002 | UN/ECE Meat Carcasses and cuts classification | UN/ECE 肉類屠體與分切分類 | string | 1-30 |
| 7003 | expiration date and time | 有效日期與時間 | string | 10 |
| 703x | Processor approval (with ISO country code); n indicates sequence number of several processors | 加工者核准（含 ISO 國家代碼）；n 表示多個加工者的順序號 | string | 3-30 |
| 8001 | Roll Products: Width/Length/Core Diameter/Direction/Splices | 捲狀產品：寬度／長度／捲芯直徑／方向／接頭 | string | 14 |
| 8002 | Mobile phone identifier | 行動電話識別碼 | string | 1-20 |
| 8003 | Global Returnable Asset Identifier | 全球可回收資產識別碼 | string | 14-30 |
| 8004 | Global Individual Asset Identifier | 全球個別資產識別碼 | string | 1-30 |
| 8005 | Price per Unit of Measure | 每計量單位價格 | string | 6 |
| 8006 | identification of the components of an item | 品項組成部分的識別 | string | 18 |
| 8007 | International Bank Account Number | 國際銀行帳號 | string | 1-30 |
| 8008 | Date/time of production | 生產日期／時間 | string | 8-12 |
| 8018 | Global Service Relationship Number | 全球服務關係編號 | string | 18 |
| 8020 | Payment slip reference | 付款單參考號 | string | 1-25 |
| 8100 | Coupon Extended Code: Number System and Offer | 折價券延伸碼：編號系統與優惠 | string | 6 |
| 8101 | Coupon Extended Code: Number System, Offer, End of Offer | 折價券延伸碼：編號系統、優惠、優惠結束 | string | 10 |
| 8102 | Coupon Extended Code: Number System preceded by 0 | 折價券延伸碼：前置 0 的編號系統 | string | 2 |
| 90 | Mutually Agreed Between Trading Partners | 交易夥伴相互約定 | string | 1-30 |
| 91-99 | Internal Company Codes | 公司內部代碼 | string | （原文空白） |

（原文頁尾有一個單獨的字元「x」。）

---

### class_serials.js
（`class_serials_jx.htm`）

此類別在我們所有應用程式中作為處理序號（serials）的共通介面，其模態視窗名為「Serials: Extended Information」。
可讓你處理入庫、出庫流程中的一批序號，或僅以唯讀模式顯示資訊，並在各情況套用必要的規則。

〔圖：webapps20_serialsextendedinformation.jpg〕

要在 app 中引入此函式庫，只需把下列程式碼加到 Beas 中 app 原始碼的「_header()」與「_footer()」之間：

```
<script src="assets21/js/class_serials.js"></script>
```

**Class members（類別成員）**

| Method Summary | 說明 |
|---|---|
| constructor | `constructor( mode, batches )` — 初始化類別 |
| set | `set( serials, options={} )` — 在顯示此視窗前設定序號與選項 |
| get | `get()` — 取得序號清單，含使用者對序號所做的所有修改 |
| display | `display(onclose=null)` — 開啟模態視窗 |

**Method Detail（方法細節）**

**constructor** — `constructor( mode, batches )`

初始化類別。

Parameters：
- `{string} mode` — 處理項目的模式（見「set」）
- `{array of json objects} serials` — 填入此視窗的批次（batches）（見「set」）

Returns：`{class_serials object}`

Example

```
var serialsAdvancedEditor = new itemSerialInfo( 'out', [] );
```

**set** — `set( batches, options={} )`

在顯示此視窗前設定批次（batches）與選項。

Parameters：

`{array of json objects} serials` — 填入模態視窗的批次（batches），為一個陣列，每個元素是含有下列欄位的物件：`[ {...}, ...., {...} ]`

| Property | Data Type | 說明 |
|---|---|---|
| serial | {string} | 序號 |
| serial_exists | {boolean} | 若序號存在則為 true，否則為 false |
| version | {string} | 序號版本（依模式套用規則） |
| version_description | {string} | 版本說明 |
| quantity | `{number}` | 此序號選取的數量 = 1；若序號為空且選項允許，可設定 > 1 |
| attr1 | {string} | 屬性 1（Attribute 1） |
| attr2 | {string} | 屬性 2（Attribute 2） |
| dateOfPermssion | {date} | 許可日期（Date of Permission） |
| manufacturingDate | {date} | 製造日期 |
| shelfLife | {date} | 保存期限（Shelf Life） |
| expirationDate | {date} | 有效日期（Expiration Date） |
| guaranteeStart | {date} | 保固開始日 |
| guaranteeEnd | {date} | 保固結束日 |
| details | {string} | 詳細資料 |
| status | {number} | 狀態，有效值為：<br>• 2 = Locked（鎖定）<br>• 1 = Not Accessible（不可存取）<br>• 0 = Released（已放行） |
| has_error | {boolean} | 若任何欄位不符合規則，此記錄會被標記為有錯誤（true） |

`{json object} options` — 開啟模態視窗時要套用的所有選項：

| Property | Data Type | 說明 |
|---|---|---|
| mode | {string} | 此編輯器的運作模式，各模式對於項目是否有效、倉庫與數量有各自的規則：<br>• in<br>• out<br>• in-wo<br>• info |
| multiLine | {boolean} | 控制延伸編輯器是否可同時容納多於一個序號，以及「Lines」分頁是否啟用。default: true |
| automaticNumber | {boolean} | 若為 true，序號可以為空。default: false |
| itemCode | {string} | 用來查詢序號是否有效／存在／可用倉庫的品號 |
| whsCode | `{string}` | 此組批次（batches）的 whsCode |
| whsLocated | `{string}` | 表示倉庫是否為儲位管理（bin located）的旗標。default: false |
| whsLocation | {string} | 儲位管理倉庫的儲位（Bin Location）代碼 |
| version | {string} | 序號版本號；若品項未做版本控管則為 null。default: null（無版本） |
| uom | {string} | 計量單位（Unit of Measure） |
| uom_dec | {number} | 所選 uom 的小數位數。default: 6 |

Returns：`{void}`

Example

```
serialsAdvancedEditor.set(
[{serial:'S1'}, {serial:'S2'}],
{mode:'in',itemCode:'RM_S', version:null, whsCode:'01', uom:'Kgs',uom_dec:3 }
);
```

**get** — `get()`

取得序號清單，含使用者對批次（batches）所做的所有修改。

Parameters：none

Returns：`{array of json objects} serials`（見「set」）

**display** — `display(onclose=null)`

開啟模態視窗。

Parameters：
- function callback — 使用者關閉模態視窗時會呼叫此函式

Returns：`{void}`

Example

```
serialsAdvancedEditor.display(function() { batches = serialsAdvancedEditor.get(); /* do something here */ } );
```

⚠ 原文疑似不一致：本頁為序號（serials）類別，但 constructor 簽章為 `constructor( mode, batches )`、參數表卻寫 `serials`；Method Summary 為 `set( serials, options={} )`，Method Detail 為 `set( batches, options={} )`；說明多處寫「batches」；範例使用 `new itemSerialInfo(...)` 而 Returns 寫 `{class_serials object}`；選項 `whsLocated` 型別寫 `{string}` 但預設值為 false。照原文翻譯。

（原文頁尾有一個單獨的字元「x」。）

---

### class_batches.js
（`class_batches_js.htm`）

此類別在我們所有應用程式中作為處理批次（batches）的共通介面，其模態視窗名為「Batches: Extended Information」。
可讓你處理入庫、出庫流程中的一批批次，或僅以唯讀模式顯示資訊，並在各情況套用必要的規則。

〔圖：webapps20_batchesextendedinformation.jpg〕

要在 app 中引入此函式庫，只需把下列程式碼加到 Beas 中 app 原始碼的「_header()」與「_footer()」之間：

```
<script src="assets21/js/class_serials.js"></script>
```

⚠ 原文疑似不一致：本頁為 class_batches.js，但引入程式碼寫的是 `class_serials.js`；以下成員說明、參數與範例也幾乎與 class_serials.js 頁相同（寫的是 serial／serials、`itemSerialInfo`、`{class_serials object}`）。照原文翻譯。

**Class members（類別成員）**

| Method Summary | 說明 |
|---|---|
| constructor | `constructor( mode, batches )` — 初始化類別 |
| set | `set( serials, options={} )` — 在顯示此視窗前設定序號與選項 |
| get | `get()` — 取得序號清單，含使用者對序號所做的所有修改 |
| display | `display(onclose=null)` — 開啟模態視窗 |

**Method Detail（方法細節）**

**constructor** — `constructor( mode, batches )`

初始化類別。

Parameters：
- `{string} mode` — 處理項目的模式（見「set」）
- `{array of json objects} serials` — 填入此視窗的批次（batches）（見「set」）

Returns：`{class_serials object}`

Example

```
var serialsAdvancedEditor = new itemSerialInfo( 'out', [] );
```

**set** — `set( batches, options={} )`

在顯示此視窗前設定批次與選項。

Parameters：

`{array of json objects} serials` — 填入模態視窗的批次，為一個陣列，每個元素是含有下列欄位的物件：`[ {...}, ...., {...} ]`

| Property | Data Type | 說明 |
|---|---|---|
| serial | {string} | 序號 |
| serial_exists | {boolean} | 若序號存在則為 true，否則為 false |
| version | {string} | 序號版本（依模式套用規則） |
| version_description | {string} | 版本說明 |
| quantity | `{number}` | 此序號選取的數量 = 1；若序號為空且選項允許，可設定 > 1 |
| attr1 | {string} | 屬性 1（Attribute 1） |
| attr2 | {string} | 屬性 2（Attribute 2） |
| dateOfPermssion | {date} | 許可日期（Date of Permission） |
| manufacturingDate | {date} | 製造日期 |
| shelfLife | {date} | 保存期限（Shelf Life） |
| expirationDate | {date} | 有效日期（Expiration Date） |
| guaranteeStart | {date} | 保固開始日 |
| guaranteeEnd | {date} | 保固結束日 |
| details | {string} | 詳細資料 |
| status | {number} | 狀態，有效值為：<br>• 2 = Locked（鎖定）<br>• 1 = Not Accessible（不可存取）<br>• 0 = Released（已放行） |
| has_error | {boolean} | 若任何欄位不符合規則，此記錄會被標記為有錯誤（true） |

`{json object} options` — 開啟模態視窗時要套用的所有選項：

| Property | Data Type | 說明 |
|---|---|---|
| mode | {string} | 此編輯器的運作模式，各模式對於項目是否有效、倉庫與數量有各自的規則：<br>• in<br>• out<br>• in-wo<br>• info |
| multiLine | {boolean} | 控制延伸編輯器是否可同時容納多於一個序號，以及「Lines」分頁是否啟用。default: true |
| automaticNumber | {boolean} | 若為 true，序號可以為空。default: false |
| itemCode | {string} | 用來查詢序號是否有效／存在／可用倉庫的品號 |
| whsCode | `{string}` | 此組批次（batches）的 whsCode |
| whsLocated | `{string}` | 表示倉庫是否為儲位管理（bin located）的旗標。default: false |
| whsLocation | {string} | 儲位管理倉庫的儲位（Bin Location）代碼 |
| version | {string} | 序號版本號；若品項未做版本控管則為 null。default: null（無版本） |
| uom | {string} | 計量單位（Unit of Measure） |
| uom_dec | {number} | 所選 uom 的小數位數。default: 6 |

Returns：`{void}`

Example

```
serialsAdvancedEditor.set(
[{serial:'S1'}, {serial:'S2'}],
{mode:'in',itemCode:'RM_S', version:null, whsCode:'01', uom:'Kgs',uom_dec:3 }
);
```

**get** — `get()`

取得序號清單，含使用者對批次所做的所有修改。

Parameters：none

Returns：`{array of json objects} serials`（see "set"）

**display** — `display(onclose=null)`

開啟模態視窗。

Parameters：
- function callback — 使用者關閉模態視窗時會呼叫此函式

Returns：`{void}`

Example

```
serialsAdvancedEditor.display(function() { batches = serialsAdvancedEditor.get(); /* do something here */ } );
```

**newBatch()**

傳回一筆 Batch 項目定義用的物件（原文：Return the object for defintion from one Batch entry）。內容：

```
batch_exists:false,
                         batch:'',
                         rfid:'',sscc:'',
                         version_readonly:true,
                         version:'',
                         version_description:'',
                         quantity:0,
                         reserved:0,
                         attr1:'',
                         attr2:'',
                         dateOfPermssion:'',
                         manufacturingDate:this.options.manufacturingDate,
                         shelfLife:this.options.shelfLife,
                         expirationDate:this.options.expirationDate,
                         details:'',
                         status:1,
                         whsLocation:'',
                         whsLocation_description:null,
                         whsLocated:this.options.whsLocated,
                         whsLocation_qty:0,
                         has_error:false
```

（原文頁尾有一個單獨的字元「x」。）

---

### class_extendedSearch.js
（`class_extentedsearch_js.htm`）

使用此類別，可以輕鬆建立延伸搜尋的模態視窗。每個「放大鏡圖示」（magnify icon）都使用此類別。

〔圖：webapps20_extendedsearch.jpg〕

要在 app 中引入此函式庫，只需把下列程式碼加到 beas 中 app 原始碼的「_header()」與「_footer()」之間：

```
<script src="assets21/js/class_extendedSearch.js"></script>
```

**Class members（類別成員）**

| Method Summary | 說明 |
|---|---|
| constructor | `constructor( params )` — 初始化類別 |
| search | `search(onclose=null)` — 開啟模態視窗 |

**Method Detail（方法細節）**

**constructor** — `constructor( mode, batches )`

初始化類別。

Parameters：`{json object} params` — 初始化搜尋對話框的參數：

| Property | Data Type | 說明 |
|---|---|---|
| id | {string} | 對話框 ID |
| title | {string} | 對話框視窗要顯示的標題 |
| url | {string} | 資料來源，可以是：<br>• Array of Arrays（陣列的陣列）資料——此時篩選會在內部以所有欄位進行（字串比對），分頁也會自動處理：`[ ['R1-F1','R1-F2','R1-F3'], ...,['Rn-F1','Rn-F2','Rn-F3'] ]`<br>• BSL data source（BSL 資料來源）——若包含權杖（token）`*q*`，它會自動被替換為 filter 欄位中的文字，並在使用者按下搜尋按鈕後呼叫該 url。下例中，顯示 {ItemCode} 或 {ItemName} 含有「filter」欄位文字的所有倉庫（範例見表下） |
| fields | {array of json objects} | 建立資料表格用的欄位，是具有下列結構的 json 物件陣列：`[ {...}, ..., {...} ]`（見下表） |
| filter | {string} | 模態視窗中「filter」欄位的初始值 |

url 的 BSL data source 範例：

```
/odata4/v1/WareHouse?$select=WhsCode,WhsName&$orderby=WhsCode&$filter=(WhsCode like "*q*" or WhsName like "*q*")
```

fields 結構：

| Property | Data Type | 說明 |
|---|---|---|
| title | {string} | 欄標題 |
| sort_key | {string} | 用來排序的欄位名稱 |
| sort_dir | {string} | 排序方向（ASC 或 DESC） |
| align | {string} | 欄位對齊（left, right, center） |
| hidden | {boolean} | 隱藏欄位 true/false/"M"。若提供含字元 "M" 的字串，則只在小型裝置上隱藏該欄位（ux.useMinimalistUI） |
| type | {string} | 資料類型，以便更好地繪製欄位（string, number, date, boolean） |
| width | {string} | 欄位寬度 |
| ondraw | {function} | 設定此函式即可覆寫預設的儲存格繪製函式（見 ux.dgrid，原文連結指向 `ux_js.htm`） |

Returns：`{class_extendedSearch object}`

Example

```
let s = new extendedSearch(
{id:'warehouseSearch',
title:_t('Warehouse Search'),
url:'/odata4/v1/WareHouse?$select=WhsCode,WhsName&$orderby=WhsCode&$filter=(WhsCode like "*q*" or WhsName like "*q*")',
fields:[
{title:_t("WhsCode"), sort_key:'WhsCode'},
{title:_t("WhsName"), sort_key:'WhsName'}
]
}
);
```

**search** — `display(onclose=null)`

開啟模態視窗。

Parameters：
- function callback onclose — 使用者關閉模態視窗時會呼叫此函式。選取的列可從變數 "selected_row" 讀取（原文：`"selected_row""`）

Returns：`{void}`

Example

```
searchWindow.search(
function() {
if (searchWindow.selected_row != null )
do_something(searchWindow.selected_row);
}
);
```

⚠ 原文疑似不一致：Method Summary 中為 `constructor( params )`，Method Detail 中寫 `constructor( mode, batches )`；Method Summary 中為 `search(onclose=null)`，Method Detail 中寫 `display(onclose=null)`；建構範例的變數為 `s`，search 範例卻用 `searchWindow`；倉庫（WareHouse）範例說明提到 {ItemCode} 或 {ItemName}，而範例 URL 篩選的是 WhsCode/WhsName。照原文保留。

（原文頁尾有一個單獨的字元「x」。）

---

### class_attachment.js
（`class_attachment.htm`）

管理附件（attachments）。

**Class members（類別成員）**

| Method Summary | 說明 |
|---|---|
| constructor | `constructor( id,openViewer_Click,upload_click )` — 初始化類別 |
| add | `add( fid,descr,fn )` — 將附件加入清單。參數：fid＝Internal Id（內部 Id）；Descr＝Description（說明）；fn＝FileName（檔名） |
| del | `del(fid)` — 從清單刪除附件（原文：delete attachment form list） |
| upload | `upload(url,cb)` — 上傳檔案 |
| viewList | `viewList()` — 顯示檢視清單（Display View List） |
| attachmentPathForbidden | 顯示允許的資料夾（原文：Display folders, which allowed） |
| getButtons | `getButtosn()` — 傳回用於顯示按鈕的 HTML Script〔圖：classattachment_button.png〕 |
| run | `run()` — 啟用 VlewList 所需的所有事件 |

（以下為原文頁面上的程式碼片段，原文未加說明：）

```
this.attachmentsViewer = new attachments('iHAttachments',this.attachments_view,null);
this.attachmentsViewer.add('Id','Info','FileName');
this.appData.attachements = [];
html_itemInfo += this.attachmentsViewer.getButtons();
this.attachmentsViewer.run();
```

⚠ 原文疑似不一致：方法名 `getButtons` 的簽章寫成 `getButtosn()`；`attachmentPathForbidden`（字面為「禁止的附件路徑」）的說明卻是「Display folders, which allowed」（顯示允許的資料夾）；頁面標題為 class_attachment.js，程式碼片段使用 `new attachments(...)`。照原文保留。
