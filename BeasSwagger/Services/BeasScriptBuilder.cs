using System.Text;
using BeasSwagger.Models;

namespace BeasSwagger.Services;

public static class BeasScriptBuilder
{
    const string NL = "\r\n";

    /// <summary>script 欄位值不得含換行/Tab/?,否則會被當成 script 指令注入。</summary>
    internal static string V(string? s, string field)
    {
        s ??= "";
        if (s.IndexOfAny(new[] { '\r', '\n', '\t', '?' }) >= 0)
            throw new ArgumentException($"欄位 {field} 含不合法字元(換行/Tab/?)");
        return s;
    }

    /// <summary>發料(對應原 issue 函數)。多筆明細共用第一筆的 DocDate 與 Oid(MES 單號)。</summary>
    public static (string requestId, string script) Issue(IReadOnlyList<WmsOigeM> list)
    {
        if (list.Count == 0) throw new ArgumentException("OigeList 不可為空");
        var mes = V(list[0].Oid, "Oid");
        var docDate = list[0].DocDate.ToString("yyyy/MM/dd");

        var sb = new StringBuilder();
        sb.Append("declare=res=ue_reservation").Append(NL);
        sb.Append("res=planned=new=issuewo");
        foreach (var o in list)
        {
            sb.Append(NL).Append("res=planned=docdate=").Append(docDate);
            sb.Append(NL).Append("res=planned=addline=base_doctype=wo")
              .Append("\tbase_docentry=").Append(o.BaseEntrySAP)
              .Append("\tbase_linenum=").Append(o.BaseLineSAP)
              .Append("\tbase_linenum2=").Append(o.BaseIssueLine)
              .Append("\titemcode=").Append(V(o.ItemCode, "ItemCode"))
              .Append("\twhscode=").Append(V(o.WhsCode, "WhsCode"))
              .Append("\tbatchnum=").Append(V(o.BatchNo, "BatchNo"))
              .Append("\tquantity=").Append(o.Qty.ToString(System.Globalization.CultureInfo.InvariantCulture));
        }
        sb.Append("\tsave"); // 與原程式相同:附加在最後一筆明細尾端
        sb.Append(NL).Append("res=issuewo=comments=MES:").Append(mes);
        sb.Append(NL).Append("setvar=is_answertext=<e_docentry>");
        sb.Append(NL).Append("if <value> = -1 then");
        sb.Append(NL).Append("end if");
        return ("issue" + mes, sb.ToString());
    }

    /// <summary>
    /// 工單入庫(ue_reservation=receiptwo):先建 planned document(單據行),再產生收貨單。
    /// base_linenum2=0 代表父項成品入庫;非 0 為 BOM 行號(半成品)。批號、儲位寫在 planned 行(文件要求)。
    /// 依 fetched/beasscript 的 reservation_issue_2、planned 說明撰寫,未在實際 Beas 驗證。
    /// </summary>
    static (string requestId, string script) ReceiptWo(string prefix, DateTime docDate, int baseDocEntry, int baseLine, int baseLine2,
        string itemCode, string whsCode, string binCode, decimal qty, string batchNum, string mesId)
    {
        var mes = V(mesId, "MesId");
        var sb = new StringBuilder();
        sb.Append("declare=res=ue_reservation");
        sb.Append(NL).Append("res=planned=docdate=").Append(docDate.ToString("yyyy/MM/dd")); // 文件:先設 docdate 再 new
        sb.Append(NL).Append("res=planned=new=receiptwo");
        sb.Append(NL).Append("res=planned=addline=base_doctype=wo")
          .Append("\tbase_docentry=").Append(baseDocEntry)
          .Append("\tbase_linenum=").Append(baseLine)
          .Append("\tbase_linenum2=").Append(baseLine2)
          .Append("\titemcode=").Append(V(itemCode, "ItemCode"))
          .Append("\twhscode=").Append(V(whsCode, "WhsCode"))
          .Append("\tquantity=").Append(Num(qty));
        var batch = V(batchNum, "BatchNum");
        var bin = V(binCode, "BinCode");
        if (batch != "") sb.Append("\tbatchnum=").Append(batch);
        if (bin != "") sb.Append("\tbincode=").Append(bin);
        sb.Append("\tsave");
        sb.Append(NL).Append("res=receiptwo=comments=MES:").Append(mes);
        sb.Append(NL).Append("res=receiptwo");
        sb.Append(NL).Append("if <value> = -1 then");
        sb.Append(NL).Append(" destroy=res");
        sb.Append(NL).Append(" return failure");
        sb.Append(NL).Append("end if");
        sb.Append(NL).Append("setvar=is_answertext=<e_docentry>");
        sb.Append(NL).Append("destroy=res");
        return (prefix + mes, sb.ToString());
    }

    /// <summary>成品入庫(原 finishedrecipt):父項入庫,base_linenum2=0。Version 無對應語法,未送出。</summary>
    public static (string requestId, string script) FinishedReceipt(FinishedReceiptRequest r) =>
        ReceiptWo("finishedrecipt", r.DocDate, r.BaseDocEntry, r.BaseLineNum, 0,
            r.ItemCode, r.WhsCode, r.BinCode, r.Quanity, r.BatchNum, r.MesId);

    /// <summary>入庫 / 半成品退回(原 returngood):base_linenum2 帶 BOM 行號。</summary>
    public static (string requestId, string script) ReturnGood(ReturnGoodRequest r) =>
        ReceiptWo("returngood", r.DocDate, r.BaseDocEntry, r.BaseLineNum, r.BaseLineNum2,
            r.ItemCode, r.WhsCode, r.BinCode, r.Quanity, r.BatchNum, r.MesId);

    // ======================================================================
    // 以下依 fetched/beasscript 說明(ue_api_bom / ue_api_routing / ue_api_time)撰寫,
    // 尚未在實際 Beas 環境驗證。
    // ======================================================================

    static readonly System.Globalization.CultureInfo Inv = System.Globalization.CultureInfo.InvariantCulture;
    static string Num(decimal d) => d.ToString(Inv);
    static string Rid(string? given, string prefix, string key) =>
        string.IsNullOrWhiteSpace(given) ? prefix + key : V(given, "RequestId");

    static bool NoValue(string? s) => string.IsNullOrEmpty(s) || s == "''";
    static string CleanQuotes(string? s) => (s ?? "").Replace("'", "");

    /// <summary>
    /// BOM script(原 CreateM / CreateMultVer / Searchotherversionbom 共用的組法):
    /// bom=new → 失敗中止 → header(description=my bom)→ 每個品項一組欄位,品項間以 bom=addline 分隔 → update。
    /// forcedVersionArea 有值時,所有行的 versionarea 都用它(CreateMultVer);否則用各行的 FaterVersion(CreateM)。
    /// </summary>
    static string BomScript(string fatherItem, IReadOnlyList<BomItem> items, string? forcedVersionArea, bool defaultWorkRout, bool setVersion)
    {
        if (items.Count == 0) throw new ArgumentException("InputItems 不可為空");
        var sb = new StringBuilder();
        sb.Append("declare=bom=ue_api_bom");
        sb.Append(NL).Append("bom=new=").Append(V(fatherItem, "FatherItem"));
        sb.Append(NL).Append("if <return> = -1 then");
        sb.Append(NL).Append(" destroy=bom");
        sb.Append(NL).Append(" return failure");
        sb.Append(NL).Append("end if");
        sb.Append(NL).Append("bom=header=description=my bom");
        sb.Append(NL).Append("bom=addline");
        for (int i = 0; i < items.Count; i++)
        {
            var it = items[i];
            sb.Append(NL).Append("bom=itemcode=").Append(V(it.ItemCode, "ItemCode"));
            sb.Append(NL).Append("bom=quantity=").Append(Num(it.Quanity));
            sb.Append(NL).Append("bom=pos_text=").Append(it.PosText);
            if (!string.IsNullOrEmpty(it.Uom)) sb.Append(NL).Append("bom=input_unit=").Append(V(it.Uom, "Uom"));
            sb.Append(NL).Append("bom=length=").Append(Num(it.Length));
            sb.Append(NL).Append("bom=quantityperpiece=").Append(Num(it.QuantityPerPiece));
            var area = forcedVersionArea ?? it.FaterVersion;
            if (!NoValue(area)) sb.Append(NL).Append("bom=versionarea=").Append(V(CleanQuotes(area), "FaterVersion"));
            var udf1 = it.WorkRout;
            if (defaultWorkRout && string.IsNullOrEmpty(udf1)) udf1 = "01";
            if (!string.IsNullOrEmpty(udf1)) sb.Append(NL).Append("bom=udf1=").Append(V(udf1, "WorkRout"));
            if (setVersion && !NoValue(it.Version)) sb.Append(NL).Append("bom=version=").Append(V(it.Version, "Version"));
            sb.Append(NL).Append("bom=scrappercent=").Append(Num(it.Stap));
            if (i < items.Count - 1) sb.Append(NL).Append("bom=addline");   // 原 DLL 會移除最後一個 addline
        }
        sb.Append(NL).Append("bom=update");
        sb.Append(NL).Append("destroy=bom");
        sb.Append(NL).Append("setvar=is_parameter1=<e_belnr_id>");
        sb.Append(NL).Append("setvar=is_answertext=OK");   // 原 DLL 回 <e_belnr_id>;空值會被輪詢判成「無結果」,改固定 OK
        return sb.ToString();
    }

    /// <summary>建立 BOM(原 CreateM)。REQUESTID = "Bom" + CreateTime(與原 DLL 的 sqlresultback 查詢規則一致)。BOM 已存在時 new 回傳 -1 而失敗。</summary>
    public static (string requestId, string script) BomCreate(BomCreateRequest r) =>
        ("Bom" + V(r.CreateTime, "CreateTime"), BomScript(r.FatherItem, r.InputItems, null, defaultWorkRout: true, setVersion: true));

    /// <summary>
    /// 新版本 BOM 插入(原 CreateMultVer):與 CreateM 相同,但所有行的 versionarea 都設為 Version,
    /// 不預設 udf1、不送 version。REQUESTID = "Bom" + RequestId + CreateTime(原 DLL 如此)。
    /// 注意:bom=new 在 BOM 已存在時會失敗,所以只能用在尚無此 BOM 的情況。
    /// </summary>
    public static (string requestId, string script) BomCreateVersion(BomVersionRequest r) =>
        ("Bom" + V(r.RequestId, "RequestId") + V(r.CreateTime, "CreateTime"),
         BomScript(r.FatherItem, r.InputItems, r.Version, defaultWorkRout: false, setVersion: false));

    /// <summary>取代某版本的明細(原 Searchotherversionbom 的建立部分):items = 新明細 + 其他版本既有明細。</summary>
    public static (string requestId, string script) BomReplaceVersion(string itemCode, IReadOnlyList<BomItem> items, string createTime) =>
        ("Bom" + V(createTime, "CreateTime"), BomScript(itemCode, items, null, defaultWorkRout: true, setVersion: true));

    /// <summary>某版本以外的既有明細(原 Searchotherversionbom 的 SELECT)。version 空白 = 取所有有 VersionArea 的行。</summary>
    public static BeasSqlStatement BomOtherVersionLinesQuery(string itemCode, string version)
    {
        const string cols = @"select t0.""ART1_ID"", t0.""INPUT_QTY"", t0.""POS_TEXT"", t0.""MENGE_JE"", t0.""ABM1"", t0.""VersionArea"", t0.""ScrapPercent"", t0.""UDF1"" from {db}.""BEAS_STL"" t0 where t0.""ItemCode"" = ?";
        var item = V(itemCode, "ItemCode"); var ver = V(version, "Version");
        return ver == ""
            ? new BeasSqlStatement(cols + @" and t0.""VersionArea"" is not null", new object?[] { item })
            : new BeasSqlStatement(cols + @" and t0.""VersionArea"" not like ?", new object?[] { item, "%" + ver + "%" });
    }

    /// <summary>取代版本前要刪的資料(原 Searchotherversionbom):整個 BOM 表頭,加上該版本的行(版本空白 = VersionArea 為 NULL 的行)。</summary>
    public static IReadOnlyList<BeasSqlStatement> BomReplaceVersionStatements(string itemCode, string version)
    {
        var item = V(itemCode, "ItemCode"); var ver = V(version, "Version");
        return new[]
        {
            new BeasSqlStatement(@"delete from {db}.""BEAS_STL_HEADER"" where ""ItemCode"" = ?", new object?[] { item }),
            ver == ""
                ? new BeasSqlStatement(@"delete from {db}.""BEAS_STL"" where ""ItemCode"" = ? and ""VersionArea"" is null", new object?[] { item })
                : new BeasSqlStatement(@"delete from {db}.""BEAS_STL"" where ""ItemCode"" = ? and ""VersionArea"" = ?", new object?[] { item, ver }),
        };
    }

    /// <summary>原 DeleteBom:刪除 BOM 表頭與全部明細。(原 DLL 的 SQL 少了「.」且把兩句塞進同一個命令,在 HANA 上應該跑不起來;這裡拆成兩句。)</summary>
    public static IReadOnlyList<BeasSqlStatement> BomDeleteStatements(string itemCode)
    {
        var item = V(itemCode, "ItemCode");
        return new[]
        {
            new BeasSqlStatement(@"delete from {db}.""BEAS_STL_HEADER"" where ""ItemCode"" = ?", new object?[] { item }),
            new BeasSqlStatement(@"delete from {db}.""BEAS_STL"" where ""ItemCode"" = ?", new object?[] { item }),
        };
    }

    /// <summary>原 checkbom:該 BOM 的明細行數。</summary>
    public static BeasSqlStatement BomCountQuery(string itemCode) =>
        new(@"select count(""ART1_ID"") from {db}.""BEAS_STL"" where ""ItemCode"" = ?", new object?[] { V(itemCode, "ItemCode") });

    /// <summary>原 checkbomversion:該版本的明細行數(版本空白 = VersionArea 為 NULL;否則 VersionArea like %版本%)。</summary>
    public static BeasSqlStatement BomVersionCountQuery(string itemCode, string version)
    {
        var item = V(itemCode, "ItemCode"); var ver = V(version, "Version");
        return ver == ""
            ? new BeasSqlStatement(@"select count(""ART1_ID"") from {db}.""BEAS_STL"" where ""ItemCode"" = ? and ""VersionArea"" is null", new object?[] { item })
            : new BeasSqlStatement(@"select count(""ART1_ID"") from {db}.""BEAS_STL"" where ""ItemCode"" = ? and ""VersionArea"" like ?", new object?[] { item, "%" + ver + "%" });
    }

    /// <summary>
    /// 途程 script(原 routing.add / routing.change 共用):ue_api_routing 新增一行。
    /// REQUESTID 與原 DLL 一致:"Rot:" + Mes + CreateDate;結果回傳新行的 pos_id(&lt;e_pos_id&gt;)。
    /// 原 DLL 另外設了 routing=aplatz_id=&lt;operation&gt;,但隨後 workcenter(同為 aplatz_id)會覆寫,故省略。
    /// </summary>
    static (string requestId, string script) RoutingScript(RoutingRequest r)
    {
        var item = V(r.Item, "Item");
        var sb = new StringBuilder();
        sb.Append("declare=routing=ue_api_routing");
        sb.Append(NL).Append("routing=new=").Append(item);
        sb.Append(NL).Append("routing=addline");
        sb.Append(NL).Append("routing=operation=").Append(V(r.Operation, "Operation"));
        sb.Append(NL).Append("routing=description=").Append(V(r.Description, "Description"));
        sb.Append(NL).Append("routing=setupforcalculator=").Append(r.SetupForCalculator);
        sb.Append(NL).Append("routing=maschinetime=").Append(r.MaschineTime);
        sb.Append(NL).Append("routing=labortime=").Append(r.LaborTime);
        sb.Append(NL).Append("routing=maschinelabortime=").Append(r.MaschineLaborTime);
        sb.Append(NL).Append("routing=workcenter=").Append(V(r.WorkCenter, "WorkCenter"));
        sb.Append(NL).Append("routing=setupforsched=").Append(r.SetupForSched);
        sb.Append(NL).Append("routing=update");
        sb.Append(NL).Append("destroy=routing");
        sb.Append(NL).Append("setvar=is_answertext=<e_pos_id>");
        return (RoutingRequestId(r), sb.ToString());
    }

    internal static string RoutingRequestId(RoutingRequest r) => "Rot:" + V(r.Mes, "Mes") + V(r.CreateDate, "CreateDate");

    /// <summary>新增途程(原 routing.add)。</summary>
    public static (string requestId, string script) RoutingAdd(RoutingRequest r) => RoutingScript(r);

    /// <summary>更新途程(原 routing.change):與 add 相同的 script;呼叫端要先在同一交易刪除舊途程(見 RoutingDeleteStatements)。</summary>
    public static (string requestId, string script) RoutingChange(RoutingRequest r) => RoutingScript(r);

    /// <summary>原 routing.delete:刪除該料號的 BEAS_APL_WKZ 與 BEAS_APL(整條途程)。注意是直接對 Beas 表下 DELETE。</summary>
    public static IReadOnlyList<BeasSqlStatement> RoutingDeleteStatements(string itemCode)
    {
        var item = V(itemCode, "ItemCode");
        return new[]
        {
            new BeasSqlStatement(@"delete from {db}.""BEAS_APL_WKZ"" where ""ItemCode"" = ?", new object?[] { item }),
            new BeasSqlStatement(@"delete from {db}.""BEAS_APL"" where ""ItemCode"" = ?", new object?[] { item }),
        };
    }

    /// <summary>
    /// 完工回報(原 finished),使用 ue_api_time(wotime)。
    /// DoTime/TimeType 目前不送:耗時由 fromdate/fromtime/todate/totime 經 timecalc 計算。
    /// Quanity→quantity(良品數);IsRelay、ItmsGrpCod、ItemCode、DocDate 以外的欄位 PDF/文件無對應,未使用。
    /// </summary>
    public static (string requestId, string script) Finished(FinishedRequest r)
    {
        var mes = V(r.MesId, "MesId");
        var sb = new StringBuilder();
        sb.Append("declare=wotime=ue_api_time");
        sb.Append(NL).Append("wotime=addline");
        sb.Append(NL).Append("wotime=belnr_id=").Append(r.BelnrId);
        sb.Append(NL).Append("wotime=belpos_id=").Append(r.BelPos);
        sb.Append(NL).Append("wotime=pos_id=").Append(r.PosId);
        sb.Append(NL).Append("wotime=user=").Append(V(r.EmpCode, "EmpCode"));
        if (!string.IsNullOrEmpty(r.Machine)) sb.Append(NL).Append("wotime=workcenter=").Append(V(r.Machine, "Machine"));
        sb.Append(NL).Append("wotime=docdate=").Append(r.DocDate.ToString("yyyy/MM/dd"));
        sb.Append(NL).Append("wotime=fromdate=").Append(r.FDate.ToString("yyyy/MM/dd"));
        sb.Append(NL).Append("wotime=fromtime=").Append(V(r.DoFTime, "DoFTime"));
        sb.Append(NL).Append("wotime=todate=").Append(r.ToDate.ToString("yyyy/MM/dd"));
        sb.Append(NL).Append("wotime=totime=").Append(V(r.DoToTime, "DoToTime"));
        sb.Append(NL).Append("wotime=quantity=").Append(r.Quanity);
        sb.Append(NL).Append("wotime=info=MES:").Append(mes);
        sb.Append(NL).Append("wotime=timecalc");
        sb.Append(NL).Append("wotime=save");
        sb.Append(NL).Append("if <return> = -1 then");
        sb.Append(NL).Append(" message=error$finished: <wotime.errormessage>");
        sb.Append(NL).Append(" destroy=wotime");
        sb.Append(NL).Append(" return failure");
        sb.Append(NL).Append("end if");
        sb.Append(NL).Append("destroy=wotime");
        sb.Append(NL).Append("setvar=is_answertext=OK");
        return ("finished" + mes, sb.ToString());
    }

    /// <summary>
    /// 工單匯入(ue_api_wo):每個品項一張工單;多張時以 reset 清除(文件要求)。
    /// MES 單號寫入 additionaltext。建立後的工單號以 &lt;mywo.docentry&gt; 取得(此屬性名稱為推測,文件只列出 docentry 為 header 屬性)。
    /// </summary>
    public static (string requestId, string script) WorkOrderImport(IReadOnlyList<(string item, decimal qty)> orders, string mes)
    {
        if (orders.Count == 0) throw new ArgumentException("InputItems 不可為空");
        var m = V(mes, "Mes");
        var sb = new StringBuilder();
        sb.Append("declare=mywo=ue_api_wo");
        sb.Append(NL).Append("setvar=ls_ids=");
        foreach (var (item, qty) in orders)
        {
            sb.Append(NL).Append("mywo=new");
            if (m != "") sb.Append(NL).Append("mywo=additionaltext=MES:").Append(m);
            sb.Append(NL).Append("mywo=line=itemcode=").Append(V(item, "ItemCode"));
            sb.Append(NL).Append("mywo=line=quantity=").Append(Num(qty));
            sb.Append(NL).Append("mywo=add");
            sb.Append(NL).Append("if <return> = -1 then");
            sb.Append(NL).Append(" destroy=mywo");
            sb.Append(NL).Append(" return failure");
            sb.Append(NL).Append("end if");
            sb.Append(NL).Append("addvar=ls_ids=<mywo.docentry>,");
            sb.Append(NL).Append("mywo=reset");
        }
        sb.Append(NL).Append("destroy=mywo");
        sb.Append(NL).Append("setvar=is_answertext=<ls_ids>");
        return ("workorder" + (m != "" ? m : V(orders[0].item, "ItemCode")), sb.ToString());
    }

    /// <summary>
    /// 修改工單 BOM(原 changeworkorederbom):ue_api_wo 的 bom_replaceitem,取代工單 BOM 某行的料號。
    /// 文件:不能換成 phantom 或組件。原 DLL 每筆都重新 declare 且不 destroy,這裡只 declare 一次並在結尾 destroy。
    /// </summary>
    public static (string requestId, string script) ChangeWorkOrderBom(IReadOnlyList<ChangeBomItem> items, string mes, string createTime)
    {
        if (items.Count == 0) throw new ArgumentException("InputItems 不可為空");
        var m = V(mes, "Mes");
        var sb = new StringBuilder();
        sb.Append("declare=mywo=ue_api_wo");
        foreach (var it in items)
        {
            sb.Append(NL).Append("setvar=ls_newitemcode=").Append(V(it.NewItem, "NewItem"));
            sb.Append(NL).Append("mywo=bom_replaceitem=").Append(it.Woid).Append('=').Append(it.Woposition)
              .Append('=').Append(it.Woitemposition).Append("=<ls_newitemcode>");
            sb.Append(NL).Append("if <return> = -1 then");
            sb.Append(NL).Append(" destroy=mywo");
            sb.Append(NL).Append(" return failure");
            sb.Append(NL).Append("end if");
        }
        sb.Append(NL).Append("destroy=mywo");
        sb.Append(NL).Append("setvar=is_answertext=OK");
        return ("changebom" + m + V(createTime, "CreateTime"), sb.ToString());
    }
}
