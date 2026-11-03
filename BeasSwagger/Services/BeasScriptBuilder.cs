using System.Text;
using BeasSwagger.Models;

namespace BeasSwagger.Services;

public static class BeasScriptBuilder
{
    const string NL = "\r\n";

    /// <summary>script 欄位值不得含換行/Tab/?,否則會被當成 script 指令注入。</summary>
    static string V(string? s, string field)
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
}
