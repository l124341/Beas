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

    /// <summary>ue_api_bom 開頭:new/load 失敗時中止(文件寫法 if &lt;return&gt; = -1)。</summary>
    static void BomOpen(StringBuilder sb, string openCmd)
    {
        sb.Append("declare=bom=ue_api_bom");
        sb.Append(NL).Append(openCmd);
        sb.Append(NL).Append("if <return> = -1 then");
        sb.Append(NL).Append(" destroy=bom");
        sb.Append(NL).Append(" return failure");
        sb.Append(NL).Append("end if");
    }

    static void BomLines(StringBuilder sb, IReadOnlyList<BomItem> items, bool byPosition)
    {
        foreach (var it in items)
        {
            // 有 Position 時 goto_pos_id:該 pos_id 不存在就新增,存在就修改(文件:goto_pos_id)
            if (byPosition && it.Position is int pos)
                sb.Append(NL).Append("bom=goto_pos_id=").Append(pos);
            else
                sb.Append(NL).Append("bom=addline");
            if (!byPosition && it.Position is int p2) sb.Append(NL).Append("bom=pos_id=").Append(p2);
            sb.Append(NL).Append("bom=itemcode=").Append(V(it.ItemCode, "ItemCode"));
            sb.Append(NL).Append("bom=quantity=").Append(Num(it.Quantity));
        }
    }

    static void BomClose(StringBuilder sb)
    {
        sb.Append(NL).Append("bom=update");
        sb.Append(NL).Append("destroy=bom");
        sb.Append(NL).Append("setvar=is_answertext=OK");
    }

    /// <summary>建立 BOM(原 CreateM)。BOM 已存在時 new 會回傳 -1 而失敗。Unit 欄位:文件說明 api_bom 不支援單位換算,未送出。</summary>
    public static (string requestId, string script) BomCreate(BomCreateRequest r)
    {
        var father = V(r.FatherItem, "FatherItem");
        var sb = new StringBuilder();
        BomOpen(sb, "bom=new=" + father);
        BomLines(sb, r.InputItems, byPosition: false);
        BomClose(sb);
        return (Rid(r.RequestId, "bomcreate", father), sb.ToString());
    }

    /// <summary>修改 BOM(原 ChangeBom)。有 Version 時先用 versioncontrol=getbomid 取得該版本的 BOM id。</summary>
    public static (string requestId, string script) BomChange(BomVersionRequest r)
    {
        var father = V(r.FatherItem, "FatherItem");
        var ver = V(r.Version, "Version");
        var sb = new StringBuilder();
        string open = "bom=getbykey=" + father;
        if (ver != "")
        {
            sb.Append("object=ue_api_bom=versioncontrol=getbomid\titemcode=").Append(father).Append("\tversion=").Append(ver);
            sb.Append(NL).Append("setvar=ls_bom_id=<value1>");
            sb.Append(NL);
            open = "bom=getbykey=<ls_bom_id>";
        }
        BomOpen(sb, open);
        BomLines(sb, r.InputItems, byPosition: true);
        BomClose(sb);
        return (Rid(r.RequestId, "bomchange", father + ver), sb.ToString());
    }

    /// <summary>
    /// 新版本 BOM(原 CreateMultVer):versioncontrol=itemversion 由來源版本(Version)複製出新版本,
    /// 取得新版本的 BOM id 後再套用 InputItems(同 Change:有 Position 則修改/新增該行)。
    /// </summary>
    public static (string requestId, string script) BomCreateVersion(BomVersionRequest r)
    {
        var father = V(r.FatherItem, "FatherItem");
        var ver = V(r.Version, "Version");
        var sb = new StringBuilder();
        sb.Append("object=ue_api_bom=versioncontrol=itemversion\titemcode=").Append(father);
        if (ver != "") sb.Append("\tversion=").Append(ver);
        sb.Append(NL).Append("setvar=ls_newver=<value>");
        sb.Append(NL).Append("object=ue_api_bom=versioncontrol=getbomid\titemcode=").Append(father).Append("\tversion=<ls_newver>");
        sb.Append(NL).Append("setvar=ls_bom_id=<value1>");
        sb.Append(NL);
        BomOpen(sb, "bom=getbykey=<ls_bom_id>");
        BomLines(sb, r.InputItems, byPosition: true);
        BomClose(sb);
        return (Rid(r.RequestId, "bomversion", father + ver), sb.ToString());
    }

    /// <summary>新增途程(原 routing.add)。Operation→ag_id、WorkCenter→aplatz_id。</summary>
    public static (string requestId, string script) RoutingAdd(RoutingRequest r)
    {
        var item = V(r.Item, "Item");
        var sb = new StringBuilder();
        sb.Append("declare=routing=ue_api_routing");
        sb.Append(NL).Append("routing=new=").Append(item);
        sb.Append(NL).Append("if <return> = -1 then");
        sb.Append(NL).Append(" destroy=routing");
        sb.Append(NL).Append(" return failure");
        sb.Append(NL).Append("end if");
        sb.Append(NL).Append("routing=addline");
        sb.Append(NL).Append("routing=operation=").Append(V(r.Operation, "Operation"));
        sb.Append(NL).Append("routing=description=").Append(V(r.Description, "Description"));
        sb.Append(NL).Append("routing=workcenter=").Append(V(r.WorkCenter, "WorkCenter"));
        sb.Append(NL).Append("routing=setupforcalculator=").Append(r.SetupForCalculator);
        sb.Append(NL).Append("routing=setupforsched=").Append(r.SetupForSched);
        sb.Append(NL).Append("routing=maschinetime=").Append(r.MaschineTime);
        sb.Append(NL).Append("routing=labortime=").Append(r.LaborTime);
        sb.Append(NL).Append("routing=maschinelabortime=").Append(r.MaschineLaborTime);
        sb.Append(NL).Append("routing=update");
        sb.Append(NL).Append("destroy=routing");
        sb.Append(NL).Append("setvar=is_answertext=OK");
        return (Rid(null, "routingadd", V(r.Mes, "Mes") + item), sb.ToString());
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
}
