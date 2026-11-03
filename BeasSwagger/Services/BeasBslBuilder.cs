using System.Text;
using System.Text.Json;
using BeasSwagger.Models;

namespace BeasSwagger.Services;

/// <summary>
/// Beas Service Layer 版 script 產生器:在 BeasScript 內以 <c>instance bsl b</c> + <c>b.post=Entity=&lt;json&gt;</c> 呼叫。
/// 依 fetched/bsl 說明撰寫(IssueWo / ReceiptWo / TimeReceipt / BillOfMaterial / Routing / Workorder),
/// 尚未在實際 Beas 驗證。JSON 一律由 JsonSerializer 產生(預設編碼會跳脫 &lt; &gt; &amp;,避免被 script 當成佔位符)。
/// </summary>
public static class BeasBslBuilder
{
    const string NL = "\r\n";
    static string V(string? s, string f) => BeasScriptBuilder.V(s, f);
    static string D(DateTime d) => d.ToString("yyyy/MM/dd");
    static string J(object o) => JsonSerializer.Serialize(o);

    static void Add(Dictionary<string, object?> d, string key, string? value) { if (!string.IsNullOrEmpty(value)) d[key] = value; }

    static StringBuilder Begin() => new StringBuilder("instance bsl b");

    /// <summary>送出一筆 POST 並檢查 ret_code(0 = ok)。</summary>
    static void Post(StringBuilder sb, string varName, string entity, string json, string errPrefix)
    {
        sb.Append(NL).Append("#define ").Append(varName);
        sb.Append(NL).Append(json);
        sb.Append(NL).Append("#end");
        sb.Append(NL).Append("b.post=").Append(entity).Append("=<").Append(varName).Append('>');
        sb.Append(NL).Append("if <b.ret_code> n<> 0 then");
        sb.Append(NL).Append(" message=error$").Append(errPrefix).Append(": <b.ret_text>");
        sb.Append(NL).Append(" return failure");
        sb.Append(NL).Append("end if");
    }

    static (string, string) Finish(StringBuilder sb, string requestId)
    {
        sb.Append(NL).Append("setvar=is_answertext=<b.ret_value>");
        return (requestId, sb.ToString());
    }

    /// <summary>發料:IssueWo。Base_LineNumber2 = BOM 位置;批號用 DistNumber。</summary>
    public static (string requestId, string script) Issue(IReadOnlyList<WmsOigeM> list)
    {
        if (list.Count == 0) throw new ArgumentException("OigeList 不可為空");
        var mes = V(list[0].Oid, "Oid");
        var lines = list.Select(o =>
        {
            var l = new Dictionary<string, object?>
            {
                ["Base_DocEntry"] = o.BaseEntrySAP, ["Base_LineNumber"] = o.BaseLineSAP, ["Base_LineNumber2"] = o.BaseIssueLine,
                ["ItemCode"] = V(o.ItemCode, "ItemCode"), ["Quantity"] = o.Qty,
            };
            Add(l, "WhsCode", V(o.WhsCode, "WhsCode"));
            Add(l, "DistNumber", V(o.BatchNo, "BatchNo"));
            return l;
        }).ToList();
        var doc = new Dictionary<string, object?>
        {
            ["Transaction"] = "issuewo", ["Date"] = D(list[0].DocDate), ["Remarks"] = "MES:" + mes, ["DocumentLines"] = lines,
        };
        var sb = Begin();
        Post(sb, "ls_json", "IssueWo", J(doc), "issue");
        return Finish(sb, "issue" + mes);
    }

    static (string requestId, string script) ReceiptWo(string prefix, DateTime docDate, int baseDocEntry, int baseLine, int baseLine2,
        string itemCode, string whsCode, string binCode, decimal qty, string batchNum, string mesId, string? version)
    {
        var mes = V(mesId, "MesId");
        var line = new Dictionary<string, object?>
        {
            ["Base_DocEntry"] = baseDocEntry, ["Base_LineNumber"] = baseLine, ["Base_LineNumber2"] = baseLine2,
            ["ItemCode"] = V(itemCode, "ItemCode"), ["Quantity"] = qty,
        };
        Add(line, "IVersionId", V(version, "Version"));
        Add(line, "WhsCode", V(whsCode, "WhsCode"));
        Add(line, "BinCode", V(binCode, "BinCode"));
        Add(line, "DistNumber", V(batchNum, "BatchNum"));
        var doc = new Dictionary<string, object?>
        {
            ["Transaction"] = "receiptwo", ["Date"] = D(docDate), ["Remarks"] = "MES:" + mes, ["DocumentLines"] = new[] { line },
        };
        var sb = Begin();
        Post(sb, "ls_json", "ReceiptWo", J(doc), prefix);
        return Finish(sb, prefix + mes);
    }

    /// <summary>成品入庫:ReceiptWo,Base_LineNumber2=0(父項);Version → IVersionId。</summary>
    public static (string requestId, string script) FinishedReceipt(FinishedReceiptRequest r) =>
        ReceiptWo("finishedrecipt", r.DocDate, r.BaseDocEntry, r.BaseLineNum, 0, r.ItemCode, r.WhsCode, r.BinCode, r.Quanity, r.BatchNum, r.MesId, r.Version);

    /// <summary>入庫 / 半成品退回:ReceiptWo,Base_LineNumber2 = BOM 行號。</summary>
    public static (string requestId, string script) ReturnGood(ReturnGoodRequest r) =>
        ReceiptWo("returngood", r.DocDate, r.BaseDocEntry, r.BaseLineNum, r.BaseLineNum2, r.ItemCode, r.WhsCode, r.BinCode, r.Quanity, r.BatchNum, r.MesId, null);

    /// <summary>
    /// 完工回報:TimeReceipt(陣列)。DocEntry=工單、LineNumber=工單位置、LineNumber2=途程位置(PosId)。
    /// DoTime 有值時以 Duration(分鐘)送出並關閉自動計算;否則由起訖時間計算。
    /// </summary>
    public static (string requestId, string script) Finished(FinishedRequest r)
    {
        var mes = V(r.MesId, "MesId");
        var t = new Dictionary<string, object?>
        {
            ["DocEntry"] = r.BelnrId, ["LineNumber"] = r.BelPos, ["LineNumber2"] = r.PosId,
            ["PersonnelId"] = V(r.EmpCode, "EmpCode"), ["QuantityGood"] = r.Quanity,
            ["DocDate"] = D(r.DocDate), ["StartDate"] = D(r.FDate), ["EndDate"] = D(r.ToDate),
            ["StartTime"] = V(r.DoFTime, "DoFTime"), ["EndTime"] = V(r.DoToTime, "DoToTime"),
            ["Remarks"] = "MES:" + mes,
        };
        Add(t, "ResourceId", V(r.Machine, "Machine"));
        if (r.DoTime > 0)
        {
            t["Duration"] = string.Equals(r.TimeType, "hour", StringComparison.OrdinalIgnoreCase) ? r.DoTime * 60 : r.DoTime;
            t["CalculateDuration"] = false;
        }
        var sb = Begin();
        Post(sb, "ls_json", "TimeReceipt", J(new[] { t }), "finished");
        return Finish(sb, "finished" + mes);
    }

    static Dictionary<string, object?> BomLine(BomItem it, bool withLineNumber)
    {
        var l = new Dictionary<string, object?> { ["ItemCode"] = V(it.ItemCode, "ItemCode"), ["Input_Quantity"] = it.Quantity };
        if (withLineNumber && it.Position is int p) l["LineNumber"] = p;
        Add(l, "Input_Unit", V(it.Unit, "Unit"));
        return l;
    }

    /// <summary>建立 BOM:BillOfMaterial POST(含 line[])。注意:BSL 預設 replace=true,BOM 已存在時會取代明細(Script 版則會失敗)。</summary>
    public static (string requestId, string script) BomCreate(BomCreateRequest r)
    {
        var father = V(r.FatherItem, "FatherItem");
        var doc = new Dictionary<string, object?>
        {
            ["BillOfMaterialId"] = father,
            ["line"] = r.InputItems.Select(i => BomLine(i, withLineNumber: true)).ToList(),
        };
        var sb = Begin();
        Post(sb, "ls_json", "BillOfMaterial", J(doc), "bom create");
        return Finish(sb, string.IsNullOrWhiteSpace(r.RequestId) ? "bomcreate" + father : V(r.RequestId, "RequestId"));
    }

    /// <summary>
    /// 修改 BOM:逐行 BillOfMaterialLine POST(LineNumber 存在則更新,沒給則新增)。
    /// 有 Version 時先 <c>b.get=ItemVersion("item","ver")/BillOfMaterialId/$value</c> 取得該版本的 BOM id。
    /// </summary>
    public static (string requestId, string script) BomChange(BomVersionRequest r)
    {
        var father = V(r.FatherItem, "FatherItem");
        var ver = V(r.Version, "Version");
        const string Mark = "@@BOM@@";
        var sb = Begin();
        string bomId = father;
        if (ver != "")
        {
            sb.Append(NL).Append("b.get=ItemVersion(\"").Append(father).Append("\",\"").Append(ver).Append("\")/BillOfMaterialId/$value");
            sb.Append(NL).Append("if <b.ret_code> n<> 0 then");
            sb.Append(NL).Append(" message=error$bom version: <b.ret_text>");
            sb.Append(NL).Append(" return failure");
            sb.Append(NL).Append("end if");
            sb.Append(NL).Append("setvar=ls_bom_id=<b.ret_value>");
            bomId = Mark;
        }
        int n = 0;
        foreach (var it in r.InputItems)
        {
            var l = BomLine(it, withLineNumber: true);
            var line = new Dictionary<string, object?> { ["BillOfMaterialId"] = bomId };
            foreach (var kv in l) line[kv.Key] = kv.Value;
            var json = J(line).Replace(Mark, "<ls_bom_id>");
            Post(sb, "ls_json" + (++n), "BillOfMaterialLine", json, "bom change");
        }
        return Finish(sb, string.IsNullOrWhiteSpace(r.RequestId) ? "bomchange" + father + ver : V(r.RequestId, "RequestId"));
    }

    /// <summary>新增途程:Routing POST,replace=false(加入明細)。Operation→CatalogId(文件要求放在第一個)、WorkCenter→ResourceId。</summary>
    public static (string requestId, string script) RoutingAdd(RoutingRequest r)
    {
        var item = V(r.Item, "Item");
        var line = new Dictionary<string, object?>();
        Add(line, "CatalogId", V(r.Operation, "Operation"));
        Add(line, "ResourceId", V(r.WorkCenter, "WorkCenter"));
        Add(line, "Description", V(r.Description, "Description"));
        line["SetupForCalculator"] = r.SetupForCalculator;
        line["SetupForSchedule"] = r.SetupForSched;
        line["MachineTime"] = r.MaschineTime;
        line["LaborTime"] = r.LaborTime;
        line["MachineLaborTime"] = r.MaschineLaborTime;
        var doc = new Dictionary<string, object?> { ["RoutingId"] = item, ["replace"] = false, ["line"] = new[] { line } };
        var sb = Begin();
        Post(sb, "ls_json", "Routing", J(doc), "routing add");
        return Finish(sb, "routingadd" + V(r.Mes, "Mes") + item);
    }

    /// <summary>工單匯入:每個品項一次 Workorder POST({"WorkorderPos":[...]}),MES 單號寫入 AdditionalText;回傳所有工單號。</summary>
    public static (string requestId, string script) WorkOrderImport(IReadOnlyList<(string item, decimal qty)> orders, string mes)
    {
        if (orders.Count == 0) throw new ArgumentException("InputItems 不可為空");
        var m = V(mes, "Mes");
        var sb = Begin();
        sb.Append(NL).Append("setvar=ls_ids=");
        int n = 0;
        foreach (var (item, qty) in orders)
        {
            var doc = new Dictionary<string, object?>();
            if (m != "") doc["AdditionalText"] = "MES:" + m;
            doc["WorkorderPos"] = new[] { new Dictionary<string, object?> { ["ItemCode"] = V(item, "ItemCode"), ["Quantity"] = qty } };
            Post(sb, "ls_json" + (++n), "Workorder", J(doc), "workorder");
            sb.Append(NL).Append("addvar=ls_ids=<b.ret_value>,");
        }
        sb.Append(NL).Append("setvar=is_answertext=<ls_ids>");
        return ("workorder" + (m != "" ? m : V(orders[0].item, "ItemCode")), sb.ToString());
    }
}
