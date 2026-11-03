# Beas DI-API 物件參考(繁體中文)

> 來源:Beas 官方文件「SAP DI API / DI-API: Objects」章節,共 21 頁。
> 本文件僅轉寫原文頁面內容;專有名詞、方法名、屬性名、指令一律保留原文英文;範例程式碼逐字保留原文(含錯字)。
> 原文多處標註「(^1) Quelle: SAP Business One SDK」或「(^1) Source: SAP Business One SDK」,意為「來源:SAP Business One SDK」。

## 目錄

1. [Addresses](#addresses)(`addresses.htm`)
2. [batchnumbers](#batchnumbers)(`batchnumbers.htm`)
3. [Batchnumbers Details](#batchnumbers-details)(`batchnumbers_details.htm`)
4. [businesspartners](#businesspartners)(`businesspartners.htm`)
5. [ContactEmployees](#contactemployees)(`contactemployees.htm`)
6. [CustomerEquipmentCards](#customerequipmentcards)(`customerequipmentcard.htm`)
7. [document](#document)(`document.htm`)
8. [forecast](#forecast)(`forecast.htm`)
9. [generalservice](#generalservice)(`generalservice.htm`)
10. [item](#item)(`item2.htm`)
11. [pricelist](#pricelist)(`items_prices.htm`)
12. [whsinfo](#whsinfo)(`whsinfo.htm`)
13. [line](#line)(`line.htm`)
14. [messages](#messages)(`messages.htm`)
15. [MultiLanguageTranslations](#multilanguagetranslations)(`multilanguagetranslations.htm`)
16. [picklists](#picklists)(`picklists.htm`)
17. [recipients](#recipients)(`recipients.htm`)
18. [serialnumbers](#serialnumbers)(`serialnumbers.htm`)
19. [servicecall](#servicecall)(`servicecall.htm`)
20. [speciallines](#speciallines)(`speciallines.htm`)
21. [ServiceContracts](#servicecontracts)(`servicecontracts.htm`)

---

### Addresses

(`addresses.htm`)

**物件階層:** Addresses-Object

```
BusinessPartners
   |_ Addresses
```

**方法**(^1)

| 方法 | 說明 |
|---|---|
| add | 新增一行(add Line) |
| setcurrentline=<x> | 設定到第 x 行 |
| delete=<x> | 刪除第 x 行 |

**屬性**(^1)

| 屬性 | 說明 |
|---|---|
| addressname | (原文無說明) |
| addressname2 | (原文無說明) |
| addressname3 | (原文無說明) |
| addresstype | (原文無說明) |
| block | (原文無說明) |
| bpcode | (原文無說明) |
| buildingfloorroom | (原文無說明) |
| country | (原文無說明) |
| county | (原文無說明) |
| federaltaxid | (原文無說明) |
| globallocationnumber | (原文無說明) |
| nationality | (原文無說明) |
| state | (原文無說明) |
| street | (原文無說明) |
| streetno | (原文無說明) |
| taxcode | (原文無說明) |
| typeofaddress | (原文無說明) |
| zipcode | (原文無說明) |
| userfields | (原文無說明) |

(^1) 來源:SAP Business One SDK

---

### batchnumbers

(`batchnumbers.htm`)

**用途:** batchnumbers-object(批號物件)。

> **Note:** 請使用 Beas Service Layer 的 Batchnumber(原文:Use Beas Service Layer Batchnumber)。

使用語法:

```
vdoc=lines=batchnumbers=<property>=<value>
```

**方法**(^1)

| 方法 | 說明 |
|---|---|
| add | (原文無說明) |
| setcurrentline | setcurrentline=1 |

**屬性**(^1)

| 屬性 | 說明 |
|---|---|
| AddmisionDate | 設定或傳回該批次的入庫(admission)日期。 |
| BaseLineNumber | 設定或傳回基礎單據中的行號。 |
| BatchNumber | 設定或傳回批號。batchNumber、ItemCode 與 WarehouseCode 的組合必須唯一。<br>必填屬性(Mandatory property)。<br>長度:32 字元。 |
| Count | 傳回 BatchNumbers 物件中的記錄數。 |
| ExpiryDate | 設定或傳回該批次的到期日。 |
| InternalSerialNumber | 設定或傳回該物料的內部序號。<br>長度:32 字元。 |
| Location | 設定或傳回該批次的位置,例如在倉庫中的位置。<br>長度:100 字元。 |
| ManufacturerSerialNumber | 設定或傳回所選物料的製造商序號。<br>長度:32 字元。 |
| ManufacturingDate | 設定或傳回該批次的製造日期。 |
| Notes | 設定或傳回一個 memo 型別字串,指定該批號的註解。<br>長度:64,000 字元。 |
| Quantity | 設定或傳回用於定義或更新批號的物料數量。 |

(^1) 來源:SAP Business One SDK

> ⚠ 原文疑似不一致:本頁語法使用 `lines=batchnumbers=`(複數),但 document 頁與 line 頁的行屬性名稱寫作 `batchnumber`(單數,line 頁說明為「see batchnumbers」)。原文未說明兩者關係。
> ⚠ 原文疑似不一致:屬性名拼寫為 `AddmisionDate`(原文如此)。

---

### Batchnumbers Details

(`batchnumbers_details.htm`)

**用途:** Batchnumbers Details - service object(服務物件)。

> **Note:** 在 Beas Service Layer 中可用,且功能更多!(原文:Available in Beas Service Layer with more functionality!)

**方法**(^1)

| 方法 | 說明 |
|---|---|
| getdatainterface | 1 - Find |
| get | 取得 batchnumber 物件 |
| update | 透過 service object 儲存資料 |

**參數(Parameters)**(^1)

| 參數 | 說明 |
|---|---|
| DocEntry | 設定 Docentry 值以擷取所選物件。<br>提示(Tip):此值與 OBTN 資料表的 AbsEntry 值相關。 |

**屬性**(^1)

| 屬性 | 說明 |
|---|---|
| AddmisionDate | 設定或傳回該批次的入庫(admission)日期。 |
| Batch | 設定或傳回基礎單據中的行號。 |
| BatchAttribute1 | (原文無說明) |
| BatchAttribute2 | (原文無說明) |
| Details | (原文無說明) |
| ExpirationDate | (原文無說明) |
| ItemCode | (原文無說明) |
| ItemDescription | (原文無說明) |
| ManufacturingDate | (原文無說明) |
| Status | (原文無說明) |
| UserFields | (原文無說明) |

(^1) 來源:SAP Business One SDK

**範例:**

```
declare=di_batch=ue_api_sbo
di_batch=getbusinesservice=batchnumberdetailsservice
di_batch=service=getdatainterface=1
di_batch=param=docentry=1
di_batch=service=get
di_batch=expirationdate=<today,yyyy/mm/dd>
di_batch=manufacturingdate=<today,yyyy/mm/dd>
di_batch=admissiondate=<today,yyyy/mm/dd>
di_batch=batchattribute1=hello
di_batch=batchattribute2=bye
di_batch=details=info for details
di_batch=service=update
destroy=di_batch
```

> ⚠ 原文疑似不一致:屬性表寫 `AddmisionDate`,範例卻使用 `admissiondate`。
> ⚠ 原文疑似不一致:屬性 `Batch` 的說明為「設定或傳回基礎單據中的行號」,與屬性名稱不符(原文如此)。
> ⚠ 原文疑似不一致:範例使用 `getbusinesservice`(原文拼寫),本頁未說明此指令。

---

### businesspartners

(`businesspartners.htm`)

**方法**(^1)

| 方法 | 說明 |
|---|---|
| add | (原文無說明) |
| cancel | (原文無說明) |
| close | (原文無說明) |
| getasxml | (原文無說明) |
| getbykey | (原文無說明) |
| savetofile | (原文無說明) |
| savexml | (原文無說明) |
| update | (原文無說明) |

**屬性**(^1)

| 屬性 | 說明 |
|---|---|
| additionalid | 業務夥伴的附加 ID 號碼 |
| address | Bill To Address(帳單地址)的街道(Street) |
| block | Bill To Address 的 block |
| cardcode | (原文無說明) |
| cardname | (原文無說明) |
| cardtype | c/ccustomer=Customer,s/csupplier=supplier,l/clid=lid |
| cardforeignname,cardfname | 夥伴的外文名稱 |
| cellular | 行動電話號碼 |
| city | Bill To Address 的城市 |
| country | Bill To Address 的國家。連結 OCRY,Char(3) |
| county | Bill To Address 的 county。連結 OCST,Char(100) |
| emailaddress, e_mail | (原文無說明) |
| fax | (原文無說明) |
| freetext,free_text | freetext |
| frozen | y/n |
| frozenremarks | Frozen-Remark(凍結備註) |
| frozenfor | date(日期) |
| frozento | date(日期) |
| iban | International Bank Account Number(國際銀行帳號) |
| languagecode,langcode | (原文無說明) |
| mailaddress | ShipTo Street(送貨地址街道) |
| mailcity | ShipTo City(送貨地址城市) |
| mailcountry | ShipTo Country。連結 OCRY,Char(3) |
| mailcounty | ShipTo County。連結 OCST,Char(100) |
| mailzipcode | ShipTo Zipcode Char(20) |
| pager | Pager |
| password | (原文無說明) |
| phone1 | (原文無說明) |
| phone2 | (原文無說明) |
| projectcode,projectcod | Projectcode,連結 oprj |
| valid | y/n |
| validfrom | date(日期) |
| validremarks | char(30) |
| validto | date(日期) |
| website,intrntsite | Website |
| zipcode | Bill To Adress 的 zipcode,char(20) |

**範例:**

```
declare=ocrd=ue_api_sbo
ocrd=getbusinessobject=obusinesspartners
ocrd=cardcode=1000
ocrd=cardname=myName
ocrd=add
destroy=ocrd
```

(^1) 來源(Source):SAP Business One SDK

---

### ContactEmployees

(`contactemployees.htm`)

**物件階層:** ContactEmployees-Object

```
BusinessPartners
   |_ ContactEmployees
```

**方法**(^1)

| 方法 | 說明 |
|---|---|
| add | 新增一行(add Line) |
| setcurrentline=<x> | 設定到第 x 行 |
| delete=<x> | 刪除第 x 行 |

**屬性**(^1)

| 屬性 | 說明 |
|---|---|
| active | (原文無說明) |
| address | (原文無說明) |
| cityofbirth | (原文無說明) |
| dateofbirth | (原文無說明) |
| e_mail | (原文無說明) |
| emailgroupcode | (原文無說明) |
| fax | (原文無說明) |
| firstname | (原文無說明) |
| gender | (原文無說明) |
| lastname | (原文無說明) |
| middlename | (原文無說明) |
| mobilephone | (原文無說明) |
| name | (原文無說明) |
| pager | (原文無說明) |
| password | (原文無說明) |
| phone1 | (原文無說明) |
| phone2 | (原文無說明) |
| placeofbirth | (原文無說明) |
| position | (原文無說明) |
| profession | (原文無說明) |
| remarks1 | (原文無說明) |
| remarks2 | (原文無說明) |
| title | (原文無說明) |
| userfields | (原文無說明) |

(^1) 來源:SAP Business One SDK

---

### CustomerEquipmentCards

(`customerequipmentcard.htm`)

**方法**(^1)

| 方法 | 說明 |
|---|---|
| add | (原文無說明) |

**屬性**(^1)(原文僅列名稱,無說明)

| 屬性 | 說明 |
|---|---|
| Attachmententry | (原文無說明) |
| Attachments | (原文無說明) |
| Block | (原文無說明) |
| Browse | (原文無說明) |
| BuildingFloorRoom | (原文無說明) |
| City | (原文無說明) |
| ContactEmployeeCode | (原文無說明) |
| ContactPhone | (原文無說明) |
| CountryCode | (原文無說明) |
| County | (原文無說明) |
| CustomerCode | (原文無說明) |
| CustomerName | (原文無說明) |
| DefaultTechnician | (原文無說明) |
| DefaultTerritory | (原文無說明) |
| DeliveryCode | (原文無說明) |
| DeliveryDate | (原文無說明) |
| DeliveryNumber | (原文無說明) |
| DirectNumber | (原文無說明) |
| DirectCustomerCode | (原文無說明) |
| DirectCustomerName | (原文無說明) |
| EquipmentCardNum | (原文無說明) |
| InstallLocation | (原文無說明) |
| InternalSerialNum | (原文無說明) |
| InvoiceCode | (原文無說明) |
| InvoiceNumber | (原文無說明) |
| ItemCode | (原文無說明) |
| ItemDescription | (原文無說明) |
| ManufacturerSerialNum | (原文無說明) |
| ReplacedBySN | (原文無說明) |
| ReplaceSN | (原文無說明) |
| StateCode | (原文無說明) |
| StatusOfSerialNumber | (原文無說明) |
| Street | (原文無說明) |
| ZipCode | (原文無說明) |

**範例:**

```
declare vservice=ue_api_sbo
vservice=getbusinessobject=ocustomerequipmentcard
vservice=customercode=C001
vservice=itemcode=Child1
vservice=internalserialnum=XXX
vservice=add
destroy vservice
```

---

### document

(`document.htm`)

**方法**(^1)

| 方法 | 說明 |
|---|---|
| add | (原文無說明) |
| cancel | (原文無說明) |
| close | (原文無說明) |
| getasxml | (原文無說明) |
| getbykey | (原文無說明) |
| savetofile | (原文無說明) |
| savexml | (原文無說明) |
| update | (原文無說明) |
| setbpl=<warehouse> | 依倉庫設定分支(branch)(僅在 multi-branch 啟用時) |

**屬性**(^1)

| 屬性 | 說明 |
|---|---|
| acivity, action | 設定或傳回 BoActivities 型別的有效值,指定與業務夥伴的活動。<br>Set: `activity=1 (meeting)`<br>Get: `activity=` 傳回值在 `<value>` |
| activitytype, cntcttype | 設定或取得活動。<br>Set: `activitytype=1`<br>Get: `activitytype=` 傳回值在 `<e_activitytype>` |
| address | (原文無說明) |
| adress2 | (原文無說明) |
| agentcode | (原文無說明) |
| bpl_idassignedtoinvoice | 僅在 multi-branch 啟用時<br>設定分支 OBPL 的 bpl_id |
| cardcode | (原文無說明) |
| cardname | (原文無說明) |
| comments | (原文無說明) |
| confirmed | y/n |
| contactcode | 設定或傳回聯絡人的代碼<br>Set: `contactcode=112`<br>Get: `contactcode=` 傳回值在 `<e_contactcode>` |
| contactpersoncode | 設定或傳回指定業務夥伴的聯絡人代碼。<br>Set: `contactpersoncode=23`<br>Get: `contactpersoncode=` 傳回值在 `<e_ContactPersonCode>` |
| contactdate, cntctdate | 設定或傳回聯絡日期。<br>Set: `contactdate=2014.04.23`<br>Get: `contactdate=` 傳回值在 `<e_ContactDate>` |
| details | 設定或傳回詳細內容。<br>Set: `details=Set my details text`<br>Get: `details=` 傳回值在 `<e_detail>` |
| doccurrency | (原文無說明) |
| docdate | (原文無說明) |
| docduedate | 在銷售中為交貨日期,在發票中為起息日(value date),在報價單中為有效至(validate to)…… |
| docentry | 內部編號 - 不可設定! |
| docnum | 設定或傳回單據編號。<br>Set: `docnum=10002341`<br>Get: `docnum=` 傳回值在 `<e_docnum>` |
| docnum_str | 設定或傳回匯入或匯出的單據編號。<br>Set: `docnum_str=A23ZR`<br>Get: `docnum_str=` 傳回值在 `<e_ls_docnum>` |
| docobjectcode | 參照 getbusinessobject。在 drafdocuments(草稿單據)中必須設定。僅為向下相容。 |
| docobjectcodeex | docobjectcode 的新功能。參見 getbusinessobject |
| fromwarehouse | 僅限調撥(transfer):來源倉庫 |
| handwritten | y/n |
| languagecode | (原文無說明) |
| lines | lines-object |
| manualnumber | char(20) |
| numatcard | char(16) 設定或傳回您的供應商/客戶用來識別貴公司的唯一代碼。 |
| project | (原文無說明) |
| reference1 | char(11) |
| reference2 | char(11) |
| shiptocode | 設定或傳回 Ship To 地址名稱。<br>長度:50 字元。 |
| serialnumbers | (原文無說明) |
| transportationcode,trnspcode | (原文無說明) |
| SequenceCode | 產生 SAP 單據。此欄位在 Beas 中已啟用,但在 Beas 流程中不會自動填入。<br>測試用範例見下方。 |

SequenceCode 測試用範例(Example to test):

```
declare=di_doc=ue_api_sbo
di_doc=getbusinessobject=20
di_doc=cardcode=C0001
di_doc=BPL_IDAssignedToInvoice=1
di_doc=SequenceCode=23
di_doc=line.itemcode=ItemXXX
di_doc=line.price=1
di_doc=line.quantity=1
di_doc=add
```

**Lines 方法(Lines-Methods)**

| 方法 | 說明 |
|---|---|
| add | (原文無說明) |
| setcurrentline | setcurrentline=1 |
| setcurrentline_linenum | 依 "lineNum" 欄位選取一行。 |
| speciallines | 特殊行(special lines)的屬性 |

**行屬性(Line-Properties)**(^1)

| 屬性 | 說明 |
|---|---|
| accountcode | (原文無說明) |
| address | (原文無說明) |
| barcode | 設定或傳回此物料的條碼(EAN code)。<br>長度:16 字元。 |
| baseentry | (原文無說明) |
| baseline | (原文無說明) |
| basetype | (原文無說明) |
| batchnumber | batchnumber-object |
| backorder | 設定或傳回 Y/N 型別的有效值,指定是否允許單據中每行物料的部分數量。 |
| commisionpercent | (原文無說明) |
| consumersalesforcast | y/n |
| costingcode, profitcenter | (原文無說明) |
| countryorg | (原文無說明) |
| currency | (原文無說明) |
| discountpercent, discprcnt | (原文無說明) |
| factor1 .... factor4 | (原文無說明) |
| height1 | (原文無說明) |
| height2 | (原文無說明) |
| height1unit | (原文無說明) |
| height2unit | (原文無說明) |
| itemcode | (原文無說明) |
| itemdescription, itemname | (原文無說明) |
| lengh1 | (原文無說明) |
| lengh1unit | (原文無說明) |
| lengh2 | (原文無說明) |
| lengh2unit | (原文無說明) |
| price | (原文無說明) |
| priceaftervat | (原文無說明) |
| projectcode, project | (原文無說明) |
| quantity | (原文無說明) |
| rate | (原文無說明) |
| salespersoncode | (原文無說明) |
| serialnumbers | (原文無說明) |
| shipdate | (原文無說明) |
| shippingmethod | (原文無說明) |
| suppliercatnum | (原文無說明) |
| sww | (原文無說明) |
| taxcode | (原文無說明) |
| vatgroup | (原文無說明) |

**範例:Order(訂單)**

```
declare vdoc=ue_api_sbo
vdoc=getbusinessobject=ordr
vdoc=doctype=0
vdoc=docduedate=<today>
vdoc=numatcard=mycreference
vdoc=cardcode=KundeA
vdoc=lines=itemcode=bau01
vdoc=lines=quantity=10
vdoc=lines=shipdate=2006/08/01
vdoc=lines=price=10
vdoc=add
destroy vdoc
```

**範例:Draft-Purcaseorder(採購訂單草稿)**

```
declare vdoc=ue_api_sbo
vdoc=getbusinessobject=odrafts
vdoc=docobjectcode=opurchaseorders
vdoc=doctype=0
vdoc=docduedate=<today>
vdoc=numatcard=mycreference
vdoc=cardcode=KundeA
vdoc=lines=itemcode=bau01
vdoc=lines=quantity=10
vdoc=lines=shipdate=2006/08/01
vdoc=lines=price=10
vdoc=add
destroy vdoc
```

**範例:含 UDF(Example with UDF)**

```
declare vdoc=ue_api_sbo
vdoc=getbusinessobject=ordr
vdoc=doctype=0
vdoc=docduedate=<today>
vdoc=numatcard=mycreference
vdoc=cardcode=KundeA
vdoc=u_ref=3246535-55
vdoc=lines=itemcode=bau01
vdoc=lines=quantity=10
vdoc=lines=shipdate=2006/08/01
vdoc=lines=price=10
vdoc=lines=u_color=red
vdoc=add
destroy vdoc
```

(^1) 來源:SAP Business One SDK

> ⚠ 原文疑似不一致:屬性名寫作 `acivity, action`,但其 Set/Get 範例使用 `activity=`。
> ⚠ 原文疑似不一致:SequenceCode 範例使用 `di_doc=line.itemcode=` 等(`line.` 加點號),而其他範例使用 `vdoc=lines=itemcode=`(`lines=`)。原文未說明兩種寫法的關係。
> ⚠ 原文疑似不一致:本頁行屬性列有 `price`,範例也使用 `lines=price=`;但 line 頁對 `price` 註明「Do not use price... YOU NEED TO USE "unitprice"」。
> ⚠ 原文疑似不一致:範例使用的 `doctype` 未列在本頁屬性表中。

---

### forecast

(`forecast.htm`)

**方法**(^1)

| 方法 | 說明 |
|---|---|
| add | (原文無說明) |

**屬性**(^1)

| 屬性 | 說明 |
|---|---|
| forecastcode | 設定或傳回銷售預測代碼。<br>Set: `forecastcode=ForNextYear_2015`<br>Get: `forecastcode=` 傳回值在 `<e_ForecastCode>` |
| forecastenddate | 設定或傳回銷售預測的結束日期。<br>Set: `forecastenddate=20140514`<br>Get: `forecastenddate=` 傳回值在 `<e_ForecastEndDate>` |
| forecastname | 設定或傳回銷售預測名稱。<br>Set: `forecastname=ForNextYear_2015`<br>Get: `forecastname=` 傳回值在 `<e_ForecastName>` |
| forecaststartdate | 設定或傳回銷售預測的開始日期。<br>Set: `forecaststartdate=2014.04.24`<br>Get: `forecaststartdate=` 傳回值在 `<e_ForecastStartDate>` |
| notes | 設定或傳回關於業務夥伴的簡短註記。<br>Set: `notes=My Note`<br>Get: `notes=` 傳回值在 `<e_notes>` |
| numerator, absid | 傳回識別鍵(identification key)。<br>Get: `numerator=` 傳回值在 `<e_absid>` |
| view, fromview | 指示各行代表每日、每週或每月的預測。<br>Set: `view=day`  可設定 day、week、month |

**Lines**(^1)

| 屬性/方法 | 說明 |
|---|---|
| count | 傳回資料表中的記錄總數,放在 `<e_count>`<br>Get: `lines.count=`<br>傳回值在 `<e_count>` |
| forecastedday | 設定或傳回指定的預測日期 `<e_ForcastedDay>`<br>Set: `lines.forecastedday=2013.04.23`<br>Get: `lines.forecastedday=` 傳回值在 `<e_ForcastedDay>` |
| itemno | 設定或傳回庫存中的物料代碼 `<e_ItemNo>`<br>Set: `lines.itemno=00121231212`<br>Get: `lines.itemno=` 傳回值 `<e_ItemNo>` |
| quantity | 設定或傳回數量 `<e_line_qty>`<br>Set: `lines.quantity=0,43`<br>Get: `lines.quantityquantity=` 傳回值 `<e_line_qty>`<br>Set: `forecaststartdate=2014.04.24`<br>Get: `forecaststartdate=` 傳回值在 `<e_ForecastStartDate>` |

> ⚠ 原文疑似不一致:Lines 的 `quantity` 說明中,Get 寫作 `lines.quantityquantity=`,且其後重複出現 `forecaststartdate` 的 Set/Get 說明(與 quantity 無關)。
> ⚠ 原文疑似不一致:forecast 物件的 `notes` 說明為「關於業務夥伴(business partner)的簡短註記」。

---

### generalservice

(`generalservice.htm`)

**用途:** GeneralService 物件可用於在指定的使用者自訂物件(UDO)主資料表中新增、擷取與刪除資料列。

(原文本頁無方法表與屬性表。)

**範例:在 UDO 中新增(ADD)/更新(UPDATE)記錄**

```
//Add Data
declare=udo=ue_api_sbo
udo=getgeneralservice=BEAS_MASTERDATA
udo=setproperty=Code=010
udo=addgeneralservice

//Update Data
declare=udo=ue_api_sbo
udo=getgeneralservice=BEAS_MASTERDATA
udo=loadgeneralservice=Code=012
udo=setproperty=Name=hola
udo=updategeneralservice
```

---

### item

(`item2.htm`)

**用途:** 操作 Item 物件(Working with Item Object)— 物料的管理。

> **NOTE:** 若可能,請使用 BeasServiceLayer Item。只有該物件支援完整的 Beas 功能。

**使用 SAP SDK 處理物料的範例:** 載入物料 10003,並變更物料名稱與預設倉庫

```
declare=di_item=ue_api_sbo
di_item=getbusinessobject=oitems
di_item=getbykey=10003
if <value> <> 1 then
  message=error$item not found <di_item.getlasterror>
  return failure
end if
di_item=itemname=newname
// with di-api-name
di_item=defaultaultwarehouse=01
// or width columnname
di_item=dfltwh=01
// Write change
di_item=update
```

> **Note:** 錯誤寫法:`declare=item=ue_api_spo`  user is not "item"。這是一個 sbo-gui-api 方法。(原文:Wrong: declare=item=ue_api_spo  user is not "item". This is a sbo-gui-api-method.)

**物件模型(Objectmodel)**

```
items
  |_ Pricelist
  |_ whsinfo
```

**方法**(^1)

| 方法 | 說明 |
|---|---|
| add | 新增物料 |
| cancel | 從物件資料表取消一筆記錄。 |
| close | 不支援。 |
| getasxml | 從 XML 資料傳回物件,該 XML 資料以字串形式存放在緩衝區中。 |
| getbykey | 取得 itemcode。value=1=ok(範例見下方) |
| savetofile | 將物件以 XML 資料儲存到檔案。 |
| remove | 刪除該物料 |
| savexml | 將物件資料儲存為 XML 格式資料。 |
| update | 更新公司資料庫中的物件資料。 |

getbykey 範例:

```
declare=di_item=ue_api_sbo
di_item=getbusinessobject=oitems
di_item=getbykey=10003
if <value> <> 1 then
  message=error$item not found <di_item.getlasterror>
  return failure
end if
...
```

> **注意(Attention):** 以下各欄位定義的詳細內容,請參閱 SAP 文件。

**屬性(Property)**(^1)

| 屬性 | 說明 |
|---|---|
| AssetItem | 決定此物料是否為固定資產(Fixed Assets)。<br>(field name: AssetItem) |
| AvgStdPrice | 設定物料成本(Item Cost)。<br>(field name: AvgPrice) |
| BarCode | 設定此物料的條碼(EAN code)。<br>(field name: CodeBars)<br>長度:16 字元。 |
| BaseUnitName | 設定庫存單位名稱(inventory Unit Name)。<br>(field name: BaseUnit)<br>長度:20 字元。 |
| Browser | 傳回 DataBrowser 物件。 |
| CommissionGroup | 設定佣金群組(Commission Group)代碼。<br>(field name: CommisGrp) |
| CommissionPercent | 設定指定業務夥伴的佣金百分比。<br>(field name: CommisPcnt) |
| CommissionSum | 設定物料的總佣金(Total Commission)。<br>(field name: CommisSum) |
| CostAccountingMethod | 設定用於評估庫存的評價方法(Valuation Method)。<br>(field name: EvalSystem) |
| CustomsGroupCode | 設定海關群組(Customs Group)代碼。海關群組決定從國外採購物料的關稅。<br>(field name: CstGrpCode) |
| DataExportCode | 設定資料匯出代碼(Data Export Code)。<br>(field name: ExportCode)<br>長度:20 字元。 |
| DefaultWarehouse | 設定預設倉庫(Default Warehouse)。<br>(field name: DfltWH)<br>長度:8 字元。 |
| DefaultCountingUom | (原文無說明) |
| DesiredInventory | 設定以採購單位計的偏好數量(Preferred Quantity)。<br>(field name: ReorderQty) |
| ForceSelectionOfSerialNumber | 設定 "Y" 或 "N" 型別的有效值,指定單據是否強制選取序號。<br>(field name: BlockOut) |
| ForeignExpensesAccount | 設定國外費用科目。<br>長度:15 字元。 |
| ForeignName | 設定物料名稱或外文描述。<br>長度:100 字元。 |
| Frozen | 設定 "Y" 或 "N" 型別的有效值,指定此物料是否暫停(on hold)。 |
| FrozenFrom | 設定物料暫停的開始日期。 |
| FrozenRemarks | 設定關於暫停期間的備註。<br>長度:30 字元。 |
| FrozenTo | 設定物料暫停的結束日期。 |
| GLMethod | 有效值為:<br>0 - Warehouses<br>1 - Item Group<br>2 - Item Level |
| IncomeAccount | DI API 6.2 版(含)以上不再使用。 |
| IncomingServiceCode | 設定進項服務代碼(incoming service code)。<br>(field name: ISvcCode)<br>這是指向服務代碼資料表(OSCD)的外鍵,未透過 DI API 公開。 |
| IndirectTax | 設定 "Y" 或 "N" 型別的有效值,指定是否套用間接稅。 |
| InventoryItem | 設定 "Y" 或 "N" 型別的有效值,指定此物料是否為倉庫物料(而非服務)。 |
| InventoryUOM | 設定物料的計量單位(例如:box、case、piece)。<br>長度:5 字元。 |
| IsPhantom | 設定 "Y" 或 "N" 型別的有效值,指定此物料是否為虛擬物料(phantom item)。 |
| IssueMethod (issuemthd) | 設定有效值,指定從庫存發料的方法:Backflash(自動)或 Manual(手動)。<br>0 或 B = SAP Business One 自動從庫存發出物料。<br>1 或 M = 使用者手動從庫存發出指定物料。 |
| ItemClass | 設定以決定目前物料是服務(Service)還是物料(Material)。<br>(field name: ItemClass) |
| ItemCode | 設定庫存中的物料代碼。物料代碼必須唯一。<br>必填屬性(Mandatory property)。<br>長度:20 字元。<br>ItemCode 是 SAP Business One 中物料記錄的主鍵,用於在系統中區分物料。 |
| ItemCountryOrg | 設定物料的原產國。<br>長度:3 字元。 |
| ItemName | 設定物料名稱/描述。<br>長度:100 字元。 |
| ItemsGroupCode | 設定物料群組代碼。使用者可依群組分類物料。每個物料只能指派到一個群組。物料群組用於報表與評估。 |
| ItemType | 設定此物料類型(Item Type)的有效值。<br>(field name: ItemType) |
| IUomEntry | (原文無說明) |
| LeadTime | 設定訂購此物料的前置時間(天數)。 |
| Mainsupplier | 設定此物料主要供應商的 card code。<br>長度:15 字元。 |
| ManageBatchNumbers | 設定 "Y" 或 "N" 型別的有效值,指定此物料是否以批號管理。 |
| ManageSerialNumbers | 設定 "Y" 或 "N" 型別的有效值,指定此物料是否以序號管理。 |
| ManageSerialNumbersOnReleaseOnly | 設定 "Y" 或 "N" 型別的有效值,決定是否在收貨時自動建立序號。<br>(field name: ManOutOnly) |
| ManageStockByWarehouse | 設定 "Y" 或 "N" 型別的有效值,指定庫存是否依倉庫數量水準管理。 |
| Manufacturer | 設定物料的製造商代碼(Manufacturers 物件的外鍵)。<br>必填屬性(Mandatory property)。 |
| MaterialGroup | 設定物料群組(Material Group)。<br>(field name: MatGrp)<br>這是指向 Material Group OMGP 物件的外鍵,未透過 DI API 公開。 |
| MaterialType | 設定 BoMaterialTypes 的有效值,決定目前物料狀態,從原料(Raw Material)到成品(Finished Goods)。<br>(field name: MatType) |
| MaxInventory | 設定此物料的最大庫存數量。 |
| MinInventory | 設定此物料的最小庫存數量。 |
| MinOrderQuantity | 設定此物料的最小訂購數量限制。(field name: MinOrdrQty) |
| MovingAveragePrice | 傳回依數量與價格計算的物料平均價格。 |
| ManageStockByWareHouse | (原文無說明) |
| NCMCode | 傳回由巴西政府發布、用於決定 IPI 稅率的物料分類。<br>(field name: NCMCode)<br>這是指向 NCM Code 資料表(ONCM)的外鍵,未透過 DI API 公開。 |
| OrderIntervals | 設定盤點/庫存週期,例如每週一,或每月第一天。庫存週期在 SAP Business One 中定義(OCYC 資料表,未透過 DI API 公開)。 |
| OrderMultiple | 設定單一訂單中,在最小數量之外的倍數數量。 |
| OutgoingServiceCode | 設定銷項服務代碼(Outgoing Service Code)。<br>(field name: OSvcCode)<br>這是指向服務代碼資料表(OSCD)的外鍵,未透過 DI API 公開。 |
| Picture | 設定要附加到物料的圖片檔名。<br>(field name: PicturName)<br>長度:200 字元。 |
| PlanningSystem | 設定 BoPlanningSystem 型別的有效值,指定庫存計畫系統:MRP 或 None。 |
| PriceList | 設定 pricelist。參見 pricelist 方法(pricelistmethode)。 |
| ProcurementMethod | 設定 BoProcurementMethod 型別的有效值,指定物料的取得方式:Buy 或 Make。 |
| ProductSource | 設定 BoProductSources 的有效值,決定產品來源。<br>(field name: ProductSrc) |
| Properties | 設定 "Y" 或 "N" 型別的有效值,指定此物料是否屬於一個或多個查詢群組(query groups)。 |
| PurchaseFactor1 | 設定因子 #1,以基礎價格表乘以此因子計算採購價格。 |
| PurchaseFactor2 | 設定因子 #2,以基礎價格表乘以此因子計算採購價格。 |
| PurchaseFactor3 | 設定因子 #3,以基礎價格表乘以此因子計算採購價格。 |
| PurchaseFactor4 | 設定因子 #4,以基礎價格表乘以此因子計算採購價格。 |
| PurchaseHeightUnit | 以計量單位的 ID 定義採購單位(purchase unit)。 |
| PurchaseHeightUnit1 | 以計量單位的 ID 定義採購高度單位。 |
| PurchaseItem | 設定 "Y" 或 "N" 型別的有效值,指定此物料是否用於採購(例如,物料可被定義為資產)。 |
| PurchaseItemsPerUnit | 設定每個採購單位的物料數量。 |
| PurchaseLengthUnit | 以計量單位的 ID 定義採購長度單位。 |
| PurchaseLengthUnit1 | 以計量單位的 ID 定義第二採購長度單位。 |
| PurchasePackagingUnit | 以計量單位的 ID 定義採購包裝單位。 |
| PurchaseQtyPerPackUnit | 以計量單位的 ID 定義採購包裝單位。 |
| PurchaseUnit | 以計量單位的 ID 定義採購單位。 |
| PurchaseUnitHeight | 以計量單位的 ID 定義採購單位高度。 |
| PurchaseUnitHeight1 | 以計量單位的 ID 定義第二採購單位高度。 |
| PurchaseUnitLength | 以計量單位的 ID 定義採購單位長度。 |
| PurchaseUnitLength1 | 以計量單位的 ID 定義第二採購單位長度。 |
| PurchaseUnitVolume | 以計量單位的 ID 定義採購單位體積。 |
| PurchaseUnitWeight | 以計量單位的 ID 定義採購單位重量。 |
| PurchaseUnitWeight1 | 以計量單位的 ID 定義第二採購單位重量。 |
| PurchaseUnitWidth | 以計量單位的 ID 定義採購單位寬度。 |
| PurchaseUnitWidth1 | 以計量單位的 ID 定義第二採購單位寬度。 |
| PurchaseVATGroup | 設定採購物料的 VAT 群組。<br>長度:8 字元。 |
| PurchaseVolumeUnit | 以計量單位的 ID 定義採購體積單位。 |
| PurchaseWeightUnit | 以計量單位的 ID 定義採購重量單位。 |
| PurchaseWeightUnit1 | 以計量單位的 ID 定義第二採購重量單位。 |
| PurchaseWidthUnit | 以計量單位的 ID 定義採購寬度單位。 |
| PurchaseWidthUnit1 | 以計量單位的 ID 定義第二採購寬度單位。 |
| PUomEntry | (原文無說明) |
| QuantityOnStock | 設定倉庫中該物料的總數量。 |
| QuantityOrderedByCustomers | 設定客戶訂購該物料的總數量。 |
| QuantityOrderedFromVendors | 設定向供應商訂購該物料的總數量。 |
| SalesFactor1 | 設定因子 #1,以基礎價格表乘以此因子計算銷售價格。 |
| SalesFactor2 | 設定因子 #2,以基礎價格表乘以此因子計算銷售價格。 |
| SalesFactor3 | 設定因子 #3,以基礎價格表乘以此因子計算銷售價格。 |
| SalesFactor4 | 設定因子 #4,以基礎價格表乘以此因子計算銷售價格。 |
| SalesHeightUnit | 以計量單位的 ID 定義銷售高度單位。 |
| SalesHeightUnit1 | 以計量單位的 ID 定義第二銷售高度單位。 |
| SalesItem | 設定 "Y" 或 "N" 型別的有效值,指定此物料是否為銷售物料。 |
| SalesItemsPerUnit | 設定每個銷售單位的物料數量。 |
| SalesLengthUnit | 以計量單位的 ID 定義銷售長度單位。 |
| SalesLengthUnit1 | 以計量單位的 ID 定義第二銷售長度單位。 |
| SalesPackagingUnit | 以計量單位的 ID 定義銷售包裝單位。 |
| SalesQtyPerPackUnit | 設定每個銷售包裝單位的物料數量。 |
| SalesUnit | 以計量單位的 ID 定義銷售單位。 |
| SalesUnitHeight | 以計量單位的 ID 定義銷售高度單位。 |
| SalesUnitHeight1 | 以計量單位的 ID 定義第二銷售高度單位。 |
| SalesUnitLength | 以計量單位的 ID 定義銷售長度單位。 |
| SalesUnitLength1 | 以計量單位的 ID 定義第二銷售長度單位。 |
| SalesUnitVolume | 以計量單位的 ID 定義銷售單位體積。 |
| SalesUnitWeight | 以計量單位的 ID 定義銷售單位重量。 |
| SalesUnitWeight1 | 以計量單位的 ID 定義第二銷售單位重量。 |
| SalesUnitWidth | 以計量單位的 ID 定義銷售單位寬度。 |
| SalesUnitWidth1 | 以計量單位的 ID 定義第二銷售單位寬度。 |
| SalesVATGroup | 設定銷售物料的 VAT 群組。<br>長度:8 字元。 |
| SalesVolumeUnit | 以計量單位的 ID 定義銷售體積單位。 |
| SalesWeightUnit | 以計量單位的 ID 定義銷售重量單位。 |
| SalesWeightUnit1 | 以計量單位的 ID 定義第二銷售重量單位。 |
| SalesWidthUnit | 以計量單位的 ID 定義銷售寬度單位。 |
| SalesWidthUnit1 | 以計量單位的 ID 定義第二銷售寬度單位。 |
| SerialNum | 設定物料的序號。<br>長度:17 字元 |
| ServiceGroup | 設定服務群組(Service Group)。<br>(field name: ServiceGrp)<br>這是指向 OSGP 物件的外鍵,未透過 DI API 公開)。 |
| ShipType | 設定物料的運送類型(空運、快遞等)。 |
| SRIAndBatchManageMethod | 設定有效值,指定管理序號與批號的方法。 |
| SupplierCatalogNo | 設定由主要供應商提供的物料型錄編號。<br>長度:17 字元。 |
| SWW | 設定物料的附加識別碼。<br>長度:16 字元。 |
| SUomEntry | (原文無說明) |
| TreeType | 傳回 BoItemTreeTypes 型別的有效值,指定物料的產品樹類型(亦稱物料清單 bill of material 類型)。 |
| User_Text | 設定此物料的自由文字字串。<br>長度:10 字元。 |
| UomGroupEntry | (原文無說明) |
| UserFields | Userfields 屬性不存在。<br>設定 userfield 的範例:`myitem=u_fieldname=myvalue` |
| Valid | 設定 "Y" 或 "N" 型別的有效值,指定此物料是否有效。 |
| ValidFrom | 設定物料有效期間的開始日期。 |
| ValidRemarks | 設定關於有效期間的備註。<br>長度:30 字元。 |
| ValidTo | 設定物料有效期間的結束日期。 |
| VatLiable | 設定 "Y" 或 "N" 型別的有效值,指定此物料是否需課 VAT。 |
| WarrantyTemplate | 設定服務保固範本。<br>長度:20 字元。 |
| WhsInfo | 設定 whsinfo。參見 whsinfo 方法(whsinfo methode)。 |
| WTLiable | 設定 "Y" 或 "N" 型別的有效值,指定此物料是否需課預扣稅(Withholding tax)。 |

(^1) 來源:SAP Business One SDK

UserFields 範例(原文):

```
myitem=u_fieldname=myvalue
```

**範例:**

```
declare=di_item=ue_api_sbo
di_item=getbusinessobject=oitems
di_item=itemcode=10003
di_item=itemname=Sofa
di_item=invntryuom=Peace
di_item=u_me_verbr=Peace
di_item=saleunitmsr=Peace
di_item=buyunitmsr=Peace
di_item=add
destroy di_item
```

**與 DI-API 的差異(Differences to DI-API)**

所有屬性都可以用資料表欄位名稱(table field name)來指定。

```
 di_item=defaultaultwarehouse=01
 di_item=dfltwh=01
```

部分存在的差異已被避免(Partly available discrepancies are avoided)。

範例:

```
 di_item=validfrom=
```

(上例)刪除該欄位中的日期。

> ⚠ 原文疑似不一致:範例使用 `defaultaultwarehouse`(標示為「with di-api-name」),但屬性表中的名稱為 `DefaultWarehouse`。
> ⚠ 原文疑似不一致:Note「Wrong: declare=item=ue_api_spo  user is not "item". This is a sbo-gui-api-method.」語意不明(`ue_api_spo` 與其他範例的 `ue_api_sbo` 不同;「user is not "item"」所指不明),照原文翻譯。
> ⚠ 原文疑似不一致:屬性表中 `ManageStockByWarehouse` 與 `ManageStockByWareHouse` 重複出現(僅大小寫不同,後者無說明)。
> ⚠ 原文疑似不一致:`PurchaseHeightUnit` 的說明為「定義採購單位(purchase unit)」,與 `PurchaseUnit` 說明相同,與名稱(Height)不符;`PurchaseQtyPerPackUnit` 的說明與 `PurchasePackagingUnit` 相同(「定義採購包裝單位」),而對應的 `SalesQtyPerPackUnit` 說明為「每個銷售包裝單位的物料數量」。
> ⚠ 原文疑似不一致:`ServiceGroup` 說明結尾多一個右括號「not exposed through the DI API)」。

---

### pricelist

(`items_prices.htm`)

**用途:** 來自 item 物件(itemobject)的 pricelist-object。

使用語法:

```
myitem=pricelist=property=value
```

| 屬性/方法 | 說明 |
|---|---|
| setcurrentline=<value> | (原文無說明) |
| count | 將筆數傳回到變數 value(範例見下方) |
| currency | 設定幣別 |
| price | 設定價格 |
| pricelist | 取得目前 pricelist 的索引,放入變數 value |
| pricelistname | 取得目前 pricelist 的名稱,放入變數 value |

count 範例:

```
myitem=pricelist=count
setvar=e_count=<value>
for=loop=0=<e_count>
..
```

**範例:** 以下範例為物料 "RM" 在第二個價格表中定義新價格,並將新價格設為 25。

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

> ⚠ 原文疑似不一致:頁面語法寫作 `myitem=pricelist=property=value`(以 `=` 分隔),但範例使用 `itemprice.pricelist.price=25`(以 `.` 分隔)。

---

### whsinfo

(`whsinfo.htm`)

**用途:** whsinfo(^1)是 Items 物件的子物件,代表倉庫中的物料。此物件屬於 Inventory and Production(庫存與生產)模組。此物件可讓您將物料新增到倉庫。

來源資料表(Source table):OITW。

**範例:鎖定物料的某個倉庫(Lock an item warehouse)**

```
// Row from OWHS (you must read it before
setvar=my_row=11
// from item
setvar=my_itemcode=FP
​declare=myitem=ue_api_sbo
myitem=getbusinessobject=oitems
myitem=getbykey=<my_itemcode>
myitem=whsinfo=SetCurrentLine=<my_row>
myitem=whsinfo=locked=Y
myitem=update
destroy=myitem
```

**範例:將倉庫新增到物料(Add warehouse to an item)**

```
setvar=my_itemcode=FP
declare=myitem=ue_api_sbo
myitem=getbusinessobject=oitems
myitem=getbykey=<my_itemcode>
myitem=whsinfo=warehousecode=01
myitem=whsinfo=add
myitem=whsinfo=whscode=02
```

**公開方法(Public Methods)**

| 方法 | 說明 |
|---|---|
| Add | 在 SAP Business One 公司資料庫的物件資料表中新增一筆記錄。 |
| SetCurrentLine | 將作用列設定為指定的列號。 |

**公開屬性(Public Properties)**

| 屬性 | 說明 |
|---|---|
| Committed | 傳回已承諾給客戶的物料數量。 |
| CostAccount | 設定或傳回物料的銷貨成本(cost-of-sale)。<br>長度:15 字元。 |
| CostInflationAccount | 設定或傳回此倉庫的 G/L decrease account。(field name: DecresGlAc)<br>長度:15 字元。 |
| CostInflationOffsetAccount | 設定或傳回此倉庫的 cost inflation account。(field name: CostRvlAct)<br>長度:15 字元。 |
| Count | 傳回 ItemWarehouseInfo 物件中的總列數。 |
| Counted | 傳回庫存中目前已盤點物料的數量。 |
| CountedQuantity | 設定或傳回已盤點的庫存數量。 |
| DecreasingAccount | 設定或傳回庫存減少(inventory decreasing)。<br>長度:15 字元。 |
| EUExpensesAccount | 設定或傳回歐盟境內的總費用。<br>長度:15 字元。 |
| EUPurchaseCreditAcc | 設定或傳回與 EU Purchase Credit Account 關聯的 G/L 科目。(field name: APCMEUAct)<br>長度:15 字元。 |
| EURevenuesAccount | 設定或傳回歐盟境內的總收入。<br>長度:15 字元。 |
| ExchangeRateDifferencesAcct | 設定或傳回與 Exchange Rate Differences Account(匯差科目)關聯的 G/L 科目。(field name: ExchangeAc)<br>長度:15 字元。 |
| ExemptedCredits | 設定或傳回與 Tax Exempt Credits account 關聯的 G/L 科目。(field name: ARCMExpAct)<br>長度:15 字元。 |
| ExemptIncomeAcc | 設定或傳回免稅收入(exempt revenue)。<br>長度:15 字元。 |
| ExpenseClearingAct | 設定或傳回與 Expenses clearing Account 關聯的 G/L 科目。<br>(field name: ExpClrAct)<br>長度:15 字元。 |
| ExpenseOffsettingAccount | 設定或傳回與 Expense Offset Account 關聯的 G/L 科目。(field name: ExpOfstAct)<br>長度:15 字元。<br>這是指向 ChartOfAccounts 物件的外鍵。 |
| ExpensesAccount | 設定或傳回費用科目。<br>長度:15 字元。 |
| ForeignExpensAcc | 設定或傳回歐盟境外的總費用。<br>長度:15 字元。 |
| ForeignPurchaseCreditAcc | 設定或傳回與 Foreign Purchase Credit Account 關聯的 G/L 科目。(field name: APCMFrnAct)<br>長度:15 字元。 |
| ForeignRevenueAcc | 設定或傳回歐盟境外的總收入。<br>長度:15 字元。 |
| GLDecreaseAcct | 設定或傳回此倉庫的 G/L decrease account。(field name: DecresGlAc)<br>長度:15 字元。 |
| GLIncreaseAcct | 設定或傳回此倉庫的 PA return account。(field name: PAReturnAc)<br>長度:15 字元。 |
| GoodsClearingAcct | 設定或傳回與 Goods Clearing Account 關聯的 G/L 科目。(field name: BalanceAcc)<br>長度:15 字元。 |
| IncreasingAccount | 設定或傳回庫存增加(inventory increasing)。<br>長度:15 字元。 |
| InStock | 傳回倉庫中的物料數量。 |
| InventoryAccount | 設定或傳回庫存科目。<br>長度:15 字元。 |
| ItemCycleCount | 傳回 ItemCycleCount 物件。 |
| Locked | 設定或傳回 BoYesNoEnum 型別的有效值,指定該物料在此倉庫是否被鎖定進出。 |
| MaximalStock | 設定或傳回該物料在倉庫中允許的最大數量。 |
| MinimalOrder | 設定或傳回訂單允許的最小數量。 |
| MinimalStock | 設定或傳回該物料在倉庫中允許的最小數量。 |
| NegativeInventoryAdjustmentAccount | 設定或傳回與 Negative Inventory Adjustment 關聯的 G/L 科目。(field name: NegStckAct)<br>長度:15 字元。<br>這是指向 ChartOfAccounts 物件的外鍵。 |
| Ordered | 傳回向供應商訂購的物料數量。 |
| PAReturnAcct | 設定或傳回此倉庫的 PA return acct。(field name: PAReturnAc)<br>長度:15 字元。 |
| PriceDifferenceAcc | 設定或傳回發票與出貨單據之間的價差。<br>長度:15 字元。 |
| PurchaseAcct | 設定或傳回與 Purchase Account 關聯的 G/L 科目。(field name: PurchaseAc)<br>長度:15 字元。 |
| PurchaseCreditAcc | 設定或傳回與 Purchase Credit Account 關聯的 G/L 科目。(field name: APCMAct)<br>長度:15 字元。 |
| PurchaseOffsetAcct | 設定或傳回此倉庫的 purchase offset account。(field name: PurchOfsAc)<br>長度:15 字元。 |
| ReturningAccount | 設定或傳回退回給供應商的庫存。<br>長度:15 字元。 |
| RevenuesAccount | 設定或傳回總收入。<br>長度:15 字元。 |
| SalesCreditAcc | 設定或傳回與 Sales Credit Account 關聯的 G/L 科目。(field name: ARCMAct)<br>長度:15 字元。 |
| SalesCreditEUAcc | 設定或傳回與 Sales Credit EU Account 關聯的 G/L 科目。(field name: ARCMEUAct)<br>長度:15 字元。 |
| SalesCreditForeignAcc | 設定或傳回與 Sales Credit Foreign Acct 關聯的 G/L 科目。(field name: ARCMFrnAct)<br>長度:15 字元。 |
| ShippedGoodsAccount | 設定或傳回此倉庫的 Shipped Goods Account。(field name: ShpdGdsAct)<br>長度:15 字元。 |
| StandardAveragePrice | 設定或傳回物料成本(Item Cost)。<br>(field name: AvgPrice) |
| StockInflationAdjustAccount | 設定或傳回此倉庫的 stock inflation adjust account。(field name: StokRvlAct)<br>長度:15 字元。 |
| StockInflationOffsetAccount | 設定或傳回此倉庫的 stock inflation offset account。(field name: StkOffsAct)<br>長度:15 字元。 |
| TransferAccount | 設定或傳回調撥科目編號。<br>長度:15 字元。 |
| UserFields | 傳回 UserFields 物件。<br>以 BeasScript 填寫與 Items 物件的 WhsInfo 相關的 "Userfield" 資訊(範例見下方)。 |
| VarienceAccount | 設定或傳回實際價格與標準價格之間的差異。<br>長度:15 字元。 |
| VATInRevenueAccount | 設定或傳回此倉庫的 VAT in revenue account。(field name: VatRevAct)<br>長度:15 字元。 |
| WarehouseCode | 設定或傳回倉庫代碼。<br>必填屬性(Mandatory property)。<br>長度:8 字元。(field name: whscode) |
| WasCounted | 傳回 BoYesNoEnum 型別的有效值,指定庫存是否已盤點。 |
| WipAccount | 設定或傳回與 WIP Material Account 關聯的 G/L 科目。(field name: WipAcct)<br>長度:15 字元。 |
| WipVarianceAccount | 設定或傳回與 Work In Progress(WIP)差異關聯的 G/L 科目,即用於過帳原料(生產前)價值與完成品(生產後)價值之間差異的科目。<br>(field name: WipVarAcct)<br>長度:15 字元。 |

UserFields 範例:

```
destroy=myitem
declare=myitem=ue_api_sbo
myitem=getbusinessobject=oitems
myitem=getbykey=RM
myitem=whsinfo=SetCurrentLine=0
myitem=whsinfo=U_myfield=Test
myitem=update
```

(^1) 來源:SAP Business One SDK

> ⚠ 原文疑似不一致:`CostInflationAccount` 與 `GLDecreaseAcct` 的說明相同(皆為「G/L decrease account」,field name: DecresGlAc);`GLIncreaseAcct` 與 `PAReturnAcct` 的 field name 皆為 PAReturnAc,且 `GLIncreaseAcct` 說明為「PA return account」,與名稱(Increase)不符。
> ⚠ 原文疑似不一致:「Add warehouse to an item」範例以 `myitem=whsinfo=whscode=02` 結尾,未見 `update` 與 `destroy`;且該行在 `add` 之後設定 `whscode`(前面使用 `warehousecode`),原文未說明用意。
> ⚠ 原文疑似不一致:「Lock an item warehouse」範例的第一行註解「(you must read it before」缺少右括號;`​declare=myitem=ue_api_sbo` 一行在原文中前面含一個不可見字元(U+200B 零寬空格),本文件依原文保留。

---

### line

(`line.htm`)

**物件階層:** Line-Object

```
document
   |_ Line
       |_ batchnumber
         |_ serialnumber
```

**方法**(^1)

| 方法 | 說明 |
|---|---|
| add | 新增一行(add Line) |
| setcurrentline=<x> | 設定到第 x 行 |
| delete=<x> | 刪除第 x 行 |

**屬性**(^1)

| 屬性 | 說明 |
|---|---|
| accountcode | (原文無說明) |
| address | (原文無說明) |
| additionalreference | (原文無說明) |
| basesum | (原文無說明) |
| backorder | (原文無說明) |
| baseentry | (原文無說明) |
| baseline | (原文無說明) |
| batchnumber | 參見 batchnumbers |
| commisionpercent | (原文無說明) |
| consumersalesforcast | (原文無說明) |
| costingcode, profitcenter | (原文無說明) |
| countryorg | (原文無說明) |
| credit | 來自 journalentry 的貸方(credit)。若變數 "journal_storno" 不為 true,且 oadm.negamount 不為 "Y",且 credit 為負值,則 Beas 會把該值寫入 debit(借方)。(Storno 範例見下方) |
| currency | (原文無說明) |
| debit | 來自 journalentry 的借方(debit)。若變數 "journal_storno" 不為 true,且 oadm.negamount 不為 "Y",且 debit 為負值,則 beas 會把該值寫入 credit(貸方)。 |
| discountpercent, discprcnt | (原文無說明) |
| factor1 ,, factor4 | (原文無說明) |
| freetext | (原文無說明) |
| height1 | (原文無說明) |
| height2 | (原文無說明) |
| height1unit | (原文無說明) |
| height2unit | (原文無說明) |
| itemcode | (原文無說明) |
| itemdescription,itemname,description | (原文無說明) |
| lengh1 | (原文無說明) |
| lengh1unit | (原文無說明) |
| lengh2 | (原文無說明) |
| lengh2unit | (原文無說明) |
| price | 不要使用 price……您必須使用 "unitprice"(原文:Do not use price... YOU NEED TO USE "unitprice") |
| priceaftervat | (原文無說明) |
| projectcode,project | (原文無說明) |
| quantity | (原文無說明) |
| rate | (原文無說明) |
| salespersoncode | (原文無說明) |
| serialnumbers | 參見 serialnumbers |
| shipdate | (原文無說明) |
| shippingmethod | (原文無說明) |
| suppliercatnum | (原文無說明) |
| sww | (原文無說明) |
| taxcode | (原文無說明) |
| unitprice | 若此欄位不存在,則使用 price 欄位 |
| vatcode | (原文無說明) |
| vatgroup | (原文無說明) |
| wahrehousecode | (原文無說明) |
| u_xxx | UDF's(使用者自訂欄位) |

(^1) 來源:SAP Business One SDK

**credit 屬性中的 Storno 範例(目前物件 = journal):**

```
// enable storno - option
setvar=journal_storno=true
..
journal.lines.credit=-500
journal.lines.debit1=-300
journal.lines.debit2=-200
..
// disable storno - option !!
setvar=journal_storno=false
```

結果(原文):

- if oadm.negamount=Y then credit=-500, debit=-300 and -200
  (若 oadm.negamount=Y,則 credit=-500,debit=-300 與 -200)
- if oadm.negamount=N then debit=-500, credit=-300 and -200
  (若 oadm.negamount=N,則 debit=-500,credit=-300 與 -200)

> ⚠ 原文疑似不一致:credit/debit 說明指「journal_storno 不為 true」時才會把負值移到另一側;但 Storno 範例在 `journal_storno=true` 的情況下,仍描述 oadm.negamount=N 時 credit/debit 互換。兩者關係原文未說明。
> ⚠ 原文疑似不一致:範例使用 `journal.lines.debit1`、`journal.lines.debit2`,屬性表中只有 `debit`;結果說明則寫「debit=-300 and -200」。
> ⚠ 原文疑似不一致:物件階層寫 `serialnumber`(單數),屬性表寫 `serialnumbers`(複數);`batchnumber` 屬性說明為「see batchnumbers」(複數)。
> ⚠ 原文疑似不一致:`price` 的說明要求改用 `unitprice`,而 `unitprice` 的說明為「若此欄位不存在,則使用 price 欄位」;document 頁的範例則都使用 `lines=price=`。屬性 `wahrehousecode` 拼寫照原文。

---

### messages

(`messages.htm`)

**用途:** Messages 是代表 Administration(管理)模組中訊息的業務物件。此物件可讓您透過 SAP Business One 訊息服務傳送訊息。

您可以使用 MessagesService,它提供比 Messages 物件更多的功能。

來源資料表(Source table):OALR

**方法**(^1)

| 方法 | 說明 |
|---|---|
| add | (原文無說明) |

**屬性**(^1)

| 屬性 | 說明 |
|---|---|
| AttachmentEntry | 設定或傳回附件檔的識別鍵,由 SAP Business One 在將 Attachment Entry 加入警示訊息時指派。<br>(field name: AtcEntry) |
| MessageText | 設定或傳回一個 memo 型別字串,指定訊息內容。<br>(field name: MsgData)<br>長度:64,000 字元。 |
| Priority | 設定或傳回決定訊息優先順序的有效值。<br>0=low<br>1=Normal<br>2=High |
| recipients | recipients-object |
| subject | 設定或傳回訊息主旨,50 字元。 |

**範例:**

```
// Send Email from test to manager
// ===============================
declare vmessage=ue_api_sbo
vmessage=getbusinessobject=81
vmessage=messagetext=this is the contect of message
vmessage=subject=Message from test to manager
vmessage=recipients=add
vmessage=recipients=setcurrentline=0
vmessage=recipients=usercode=manager
vmessage=recipients=nameto=test
vmessage=recipients=sendinternal=1
vmessage=add
destroy vmessage
```

---

### MultiLanguageTranslations

(`multilanguagetranslations.htm`)

**用途:** MultilanguageTransactions 是代表翻譯(Translations)的業務物件。

**方法**(^1)

| 方法 | 說明 |
|---|---|
| add | (原文無說明) |

**屬性**(^1)

| 屬性 | 說明 |
|---|---|
| Tablename | (原文無說明) |
| fieldAlias | (原文無說明) |
| primarykeyofobject | (原文無說明) |
| languagecodeofuserlanguage | (原文無說明) |
| translationcontent | (原文無說明) |

**範例:**

```
// Send Email from test to manager
// ===============================
destroy=myitem
declare=myitem=ue_api_sbo
myitem=getbusinessobject=oMultiLanguageTranslations
myitem=tablename=OITM
myitem=fieldalias=ItemName
myitem=primarykeyofobject=Jesús
myitem=translationsinuserlanguages=languagecodeofuserlanguage=23
myitem=translationsinuserlanguages=translationscontent=test translation
myitem=translationsinuserlanguages=add
myitem=add
if <value> = 0 then
message=Error: <myitem.getlasterror>
end if
destroy=myitem
```

> ⚠ 原文疑似不一致:用途說明寫「MultilanguageTransactions」,但物件名稱為 MultiLanguageTranslations。
> ⚠ 原文疑似不一致:屬性表為 `translationcontent`,範例使用 `translationscontent`;範例中 `languagecodeofuserlanguage` 與 `translationscontent` 是經由 `translationsinuserlanguages=` 子物件設定,此子物件未列在屬性表中。
> ⚠ 原文疑似不一致:範例開頭註解「Send Email from test to manager」與翻譯物件內容無關(與 messages 頁範例相同)。

---

### picklists

(`picklists.htm`)

**方法**(^1)

| 方法 | 說明 |
|---|---|
| add | (原文無說明) |
| close | (原文無說明) |
| getasxml | (原文無說明) |
| getbykey | (原文無說明) |
| savetofile | (原文無說明) |
| savexml | (原文無說明) |
| update | (原文無說明) |

**屬性**(^1)

| 屬性 | 說明 |
|---|---|
| lines | lines-object |
| name | char(30) |
| ownercode | 設定或傳回與此 Pick list 關聯的使用者代碼。連結 OUSR 資料表 |
| pickdate | Pick list 的日期 |
| pickremarks | 備註 |

**Lines 方法(Lines-Methods)**

| 方法 | 說明 |
|---|---|
| add | (原文無說明) |
| setcurrentline | setcurrentline=1 |

**行屬性(Line-Properties)**(^1)

| 屬性 | 說明 |
|---|---|
| baseobjecttype | 17 - orders(訂單)<br>13 - reserve invoices(預留發票) |
| orderentry | 基礎單據的 docentry |
| orderrowid | 基礎單據的 linenum |
| pickedquantity | (原文無說明) |
| releasedquantity | (原文無說明) |
| batchnumber | batchnumber-object |
| serialnumbers | (原文無說明) |

**範例:建立新的 Pick list**

```
declare=di_picklist=ue_api_sbo
di_picklist=getbusinessobject=opicklists
di_picklist=pickownercode=1
di_picklist=name=pepe
di_picklist=pickdate=<today>
di_picklist=remarks=remarks pl
di_picklist=lines=baseobjecttype=17
di_picklist=lines=orderentry=783
di_picklist=lines=orderrowid=0
di_picklist=add
```

(^1) 來源:SAP Business One SDK

> ⚠ 原文疑似不一致:屬性表為 `ownercode`、`pickremarks`,範例卻使用 `pickownercode`、`remarks`。

---

### recipients

(`recipients.htm`)

**用途:** Recipients 是代表訊息或警示的收件者清單的業務物件。此物件可讓您將收件者資訊加入收件者清單以傳送訊息。

您可以改用 RecipientCollection 與 Recipient 物件來取代 Recipients 物件(仍維持向下相容)。

來源資料表(Source table):AOB1。

**方法**

| 方法 | 說明 |
|---|---|
| add | 新增一位收件者到集合中。 |
| setcurrentline | 將作用列設定為指定的列號。 |

**屬性**

| 屬性 | 說明 |
|---|---|
| cellularnumber, portnum | 設定或傳回收件者的行動電話號碼。 |
| emailaddresse,e_mail | (原文無說明) |
| faxnumber, fax | (原文無說明) |
| nameto, objname | 設定或傳回收件者名稱。 |
| sendmail, sendemail | 以 email 傳送 |
| sendfax | 以傳真傳送 |
| sendinternal | 內部傳送 |
| sendsms | 以 SMS(簡訊)傳送 |
| usercode | 收件者的代碼(原文拼為 cecipient)。必填! |
| usertype | 收件者類型。必填!<br>-1 指定 SAP Business One 應用程式的任何使用者。<br>12 指定公司內部使用者。<br>11 指定供應商或客戶使用者。 |

(原文本頁未標「Methods」「Properties」標題,上表依原文表格中的空白列分組;原文無範例。)

---

### serialnumbers

(`serialnumbers.htm`)

**用途:** serial numbers Object(序號物件)

使用語法:

```
vdoc=lines=serialnumbers=<property>=<value>
```

**方法**(^1)

| 方法 | 說明 |
|---|---|
| add | (原文無說明) |
| setcurrentline | setcurrentline=1 |

**屬性**(^1)

| 屬性 | 說明 |
|---|---|
| BaseLineNumber | property BaseLineNumber |
| BatchID | 設定或傳回物料的唯一批號。<br>長度:32 字元。 |
| Count | 傳回 SerialNumbers 物件中的資料列總數。 |
| ExpiryDate | 設定或傳回物料的到期日。 |
| InternalSerialNumber | 設定或傳回物料的內部序號。<br>長度:32 字元。 |
| Location | 設定或傳回物料的位置,例如在倉庫中的位置。<br>長度:100 字元。 |
| ManufactureDate | 設定或傳回該批次的製造日期。 |
| ManufacturerSerialNumber | 設定或傳回所選物料的製造商序號。<br>長度:32 字元。 |
| Notes | 設定或傳回一個 memo 型別字串,指定該物料的註解。<br>長度:64,000 字元。 |
| ReceptionDate | 設定或傳回收貨日期。 |
| SystemSerialNumber | 設定或傳回從 1 開始的連續編號,為每個以序號管理的物料發出。此編號隨同類(同一物料)新單位的建立而遞增。 |
| WarrantyEnd | 設定或傳回物料的保固結束日期。 |
| WarrantyStart | 設定或傳回物料的保固開始日期。 |

(^1) 來源:SAP Business One SDK

---

### servicecall

(`servicecall.htm`)

**方法**(^1)

| 方法 | 說明 |
|---|---|
| add | (原文無說明) |

**屬性**(^1)

| 屬性 | 說明 |
|---|---|
| customercode | 設定或傳回客戶代碼,即業務夥伴主資料中的 card code。<br>必填屬性(Mandatory property)。<br>(field name: customer)<br>長度:15 字元。<br>這是指向 BusinessPartners 物件的外鍵。 |
| servicecallid, callid | 傳回服務呼叫(service call)的識別號碼。 |
| subject | 設定或傳回問題的簡短描述。<br>必填屬性(Mandatory property)。 |

**範例:**

```
declare vservice=ue_api_sbo
vservice=getbusinessobject=191
vservice=customercode=10001
vservice=subject=MyServiceCall
vservice=add
destroy vservice
```

---

### speciallines

(`speciallines.htm`)

**用途:** document-speciallines(單據的特殊行)。

更多資訊請參閱 SAP 文件(For more Information see SAP-Documentation)。

**範例:**

```
setvar=ls_linetext=this is my new line
// Text line= 0 / Sum = 1
vdoc=speciallines=linetype=0
// Afert first line
vdoc=speciallines=afterlinenumber=0
vdoc=speciallines=linetext=<ls_linetext>
vdoc=speciallines=add
```

**可用屬性(Available Properties)**

| 屬性/方法 | 說明 |
|---|---|
| add | (原文無說明) |
| setcurrentline | (原文無說明) |
| linenumber | (原文無說明) |
| count | (原文無說明) |
| delete | (原文無說明) |
| freight1 | 在 `<value>` 中傳回運費 |
| freight1fc | 在 `<value>` 中傳回運費 |
| freight1sc | 在 `<value>` 中傳回運費 |
| freight2 | 在 `<value>` 中傳回運費 |
| freight2fc | 在 `<value>` 中傳回運費 |
| freight2sc | 在 `<value>` 中傳回運費 |
| freight3 | 在 `<value>` 中傳回運費 |
| freight3fc | 在 `<value>` 中傳回運費 |
| freight3sc | 在 `<value>` 中傳回運費 |
| grosstotal | 在 `<value>` 中傳回價格 |
| grosstotalfc | 在 `<value>` 中傳回價格 |
| linenum | (原文無說明) |
| linetext | (原文無說明) |
| linetype | 0 = Text,1=Sum |
| ordernumber | 在 `<value>` 中傳回訂單編號 |
| subtotal | 在 `<value>` 中傳回價格 |
| taxamount | 在 `<value>` 中傳回價格 |
| taxamountfc | 在 `<value>` 中傳回稅額 |
| taxamountsc | 在 `<value>` 中傳回稅額 |
| u_[...] | UDF's(使用者自訂欄位) |

> ⚠ 原文疑似不一致:範例使用 `afterlinenumber`,但「Available Properties」表中只有 `linenumber` 與 `linenum`,未列 `afterlinenumber`。

---

### ServiceContracts

(`servicecontracts.htm`)

**方法**(^1)

| 方法 | 說明 |
|---|---|
| add | (原文無說明) |

**屬性**(^1)(原文僅列名稱,無說明)

| 屬性 | 說明 |
|---|---|
| attachmententry | (原文無說明) |
| attachments | (原文無說明) |
| contactcode | (原文無說明) |
| contractid | (原文無說明) |
| contracttemplate | (原文無說明) |
| contracttype | (原文無說明) |
| customercode | (原文無說明) |
| customername | (原文無說明) |
| description | (原文無說明) |
| durationofcoverage | (原文無說明) |
| enddate | (原文無說明) |
| fridayenabled | (原文無說明) |
| fridayend | (原文無說明) |
| fridaystart | (原文無說明) |
| includeholidays | (原文無說明) |
| includelabor | (原文無說明) |
| includeparts | (原文無說明) |
| includetravel | (原文無說明) |
| Lines | (原文無說明) |
| mondayenabled | (原文無說明) |
| mondayend | (原文無說明) |
| mondaystart | (原文無說明) |
| owner | (原文無說明) |
| remarks | (原文無說明) |
| remindertime | (原文無說明) |
| remindunit | (原文無說明) |
| renewal | (原文無說明) |
| resolutiontime | (原文無說明) |
| resolutionunit | (原文無說明) |
| responsetime | (原文無說明) |
| responseunit | (原文無說明) |
| saturdayenabled | (原文無說明) |
| saturdayend | (原文無說明) |
| saturdaystart | (原文無說明) |
| servicetype | (原文無說明) |
| startdate | (原文無說明) |
| status | (原文無說明) |
| sundayenabled | (原文無說明) |
| sundayend | (原文無說明) |
| sundaystart | (原文無說明) |
| templateremarks | (原文無說明) |
| terminationdate | (原文無說明) |
| thursdayenabled | (原文無說明) |
| thursdayend | (原文無說明) |
| thursdaystart | (原文無說明) |
| userfields | (原文無說明) |
| wednesdayenabled | (原文無說明) |
| wednesdayend | (原文無說明) |
| wednesdaystart | (原文無說明) |

**Lines**(^1)

| 屬性 | 說明 |
|---|---|
| enddate | (原文無說明) |
| internalserialnum | (原文無說明) |
| itemcode | (原文無說明) |
| itemgroup | (原文無說明) |
| itemgroupname | (原文無說明) |
| manufacturerserialnumber | (原文無說明) |
| startdate | (原文無說明) |
| terminationdate | (原文無說明) |
| userfields | (原文無說明) |

(原文本頁無範例。)
