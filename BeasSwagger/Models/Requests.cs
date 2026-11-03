namespace BeasSwagger.Models;

// ===== BOM =====
/// <summary>BOM 明細(對應原 DLL 的 BomItem,欄位名稱沿用原拼字:Quanity、FaterVersion)。</summary>
public class BomItem
{
    /// <summary>子件料號(BEAS_STL.ART1_ID)</summary>
    public string ItemCode { get; set; } = "";
    /// <summary>用量(quanity → quantity)</summary>
    public decimal Quanity { get; set; }
    /// <summary>顯示的位置號(pos_text)</summary>
    public int PosText { get; set; }
    /// <summary>單位(input_unit);空白則不送出,沿用料號預設單位</summary>
    public string Uom { get; set; } = "";
    /// <summary>長度(length,BEAS_STL.ABM1,原 DLL 當「底數」用)</summary>
    public decimal Length { get; set; }
    /// <summary>每件用量(quantityperpiece,BEAS_STL.MENGE_JE;0 = 固定用量)</summary>
    public decimal QuantityPerPiece { get; set; }
    /// <summary>適用版本(versionarea,BEAS_STL.VersionArea)。空白或 "''" 代表不設。<b>版本是 BOM 明細行上的標記,不是 ItemVersion</b></summary>
    public string FaterVersion { get; set; } = "";
    /// <summary>udf1(原 DLL 稱 workrout);空白時 bom/create 預設為 "01"</summary>
    public string WorkRout { get; set; } = "";
    /// <summary>version(bom=version=);空白或 "''" 代表不設</summary>
    public string Version { get; set; } = "";
    /// <summary>報廢率(stap → scrappercent)</summary>
    public decimal Stap { get; set; }
}
public class BomCreateRequest : BeasRequest
{
    /// <summary>父項目編號</summary><example>FG-001</example>
    public string FatherItem { get; set; } = "";
    public List<BomItem> InputItems { get; set; } = new();
    public string RequestId { get; set; } = "";
    public string CreateTime { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
}
public class BomVersionRequest : BomCreateRequest
{
    /// <summary>父版本</summary>
    public string Version { get; set; } = "";
}
public class BomSearchOtherVersionRequest : BeasRequest
{
    public string ItemCode { get; set; } = "";
    public string Version { get; set; } = "";
    public List<BomItem> InputItems { get; set; } = new();
    public string RequestId { get; set; } = "";
}
public class BomDeleteRequest : BeasRequest
{
    public string FatherItem { get; set; } = "";
    public string RequestId { get; set; } = "";
    public string CreateTime { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
}
public class ItemRequest : BeasRequest
{
    public string ItemCode { get; set; } = "";
}
public class ItemVersionRequest : ItemRequest
{
    public string Version { get; set; } = "";
}
public class SqlResultBackRequest : BeasRequest
{
    /// <summary>MES 回傳訊息</summary>
    public string Mes { get; set; } = "";
    public string CreateTime { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    /// <summary>(選填)直接指定要查的 REQUESTID;留空則用 Mes + CreateTime(推測規則)。</summary>
    public string? RequestId { get; set; }
}

// ===== 物料單位 =====
public class UnitCreateRequest : BeasRequest
{
    public string Meid { get; set; } = "";
    public string TEXTSINGULAR { get; set; } = "";
    public string TEXTPLURAL { get; set; } = "";
    /// <summary>小數位數</summary>
    public int ANZDEZ { get; set; }
    /// <summary>MRP取整方式:-1不取整 0rounding at 5 1取整 2四捨五入</summary>
    public int MRPRUNDUNGTYP { get; set; }
    public int MRPRUNDUNGDEC { get; set; }
    /// <summary>物料清單取整方式:-1不取整 0rounding at 5 1取整 2四捨五入 3Multiple of</summary>
    public int WORKORDERROUNDTYPEDEFAULT { get; set; }
    public int WORKORDERROUNDDECDEFAULT { get; set; }
    public DateTime ErfTStamp { get; set; } = DateTime.Now;
    public string ErfUser { get; set; } = "";
    public DateTime AndTStamp { get; set; } = DateTime.Now;
    public string AndUser { get; set; } = "";
}
public class UnitUpdateRequest : BeasRequest
{
    public string Meid { get; set; } = "";
    public int ANZDEZ { get; set; }
    public int MRPRUNDUNGTYP { get; set; }
    public int MRPRUNDUNGDEC { get; set; }
    public int WORKORDERROUNDTYPEDEFAULT { get; set; }
    public int WORKORDERROUNDDECDEFAULT { get; set; }
    public DateTime AndTStamp { get; set; } = DateTime.Now;
    public string AndUser { get; set; } = "";
}

// ===== 完工回報 / 入庫 =====
public class FinishedRequest : BeasRequest
{
    /// <summary>員工代碼</summary>
    public string EmpCode { get; set; } = "";
    /// <summary>工單號</summary>
    public int BelnrId { get; set; }
    /// <summary>工單位置</summary>
    public int BelPos { get; set; }
    /// <summary>資源位置</summary>
    public int PosId { get; set; }
    /// <summary>資源名稱</summary>
    public string Machine { get; set; } = "";
    /// <summary>耗用時間單位 minute/hour</summary>
    public string TimeType { get; set; } = "minute";
    /// <summary>耗用時間</summary>
    public decimal DoTime { get; set; }
    public DateTime FDate { get; set; } = DateTime.Today;
    public DateTime ToDate { get; set; } = DateTime.Today;
    /// <summary>開始時間 hh:mm</summary><example>08:00</example>
    public string DoFTime { get; set; } = "08:00";
    /// <summary>結束時間 hh:mm</summary><example>09:00</example>
    public string DoToTime { get; set; } = "09:00";
    /// <summary>完工數量</summary>
    public int Quanity { get; set; }
    public string MesId { get; set; } = "";
    public string CreateDate { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    public DateTime DocDate { get; set; } = DateTime.Today;
    public string ItemCode { get; set; } = "";
    public string ItmsGrpCod { get; set; } = "";
    public bool IsRelay { get; set; }
}
public class ReturnGoodRequest : BeasRequest
{
    public DateTime DocDate { get; set; } = DateTime.Today;
    public int BaseDocEntry { get; set; }
    public int BaseLineNum { get; set; }
    public int BaseLineNum2 { get; set; }
    public string ItemCode { get; set; } = "";
    public string WhsCode { get; set; } = "";
    public string BinCode { get; set; } = "";
    public decimal Quanity { get; set; }
    public string BatchNum { get; set; } = "";
    public string MesId { get; set; } = "";
}
public class FinishedReceiptRequest : BeasRequest
{
    public DateTime DocDate { get; set; } = DateTime.Today;
    public int BaseDocEntry { get; set; }
    public int BaseLineNum { get; set; }
    public string ItemCode { get; set; } = "";
    public string WhsCode { get; set; } = "";
    public string BinCode { get; set; } = "";
    public decimal Quanity { get; set; }
    public string BatchNum { get; set; } = "";
    public string MesId { get; set; } = "";
    public string Version { get; set; } = "";
}

// ===== Routing =====
public class RoutingRequest : BeasRequest
{
    public string Item { get; set; } = "";
    public string Operation { get; set; } = "";
    public string Description { get; set; } = "";
    public int SetupForCalculator { get; set; }
    public int MaschineTime { get; set; }
    public int LaborTime { get; set; }
    public int MaschineLaborTime { get; set; }
    public string WorkCenter { get; set; } = "";
    public int SetupForSched { get; set; }
    public string Mes { get; set; } = "";
    public string CreateDate { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
}

// ===== 廢品明細表 =====
public class ScrapDetailCreateRequest : BeasRequest
{
    /// <summary>Scrap Id</summary>
    public string VERSCHN { get; set; } = "";
    /// <summary>從</summary>
    public string VON { get; set; } = "";
    /// <summary>到</summary>
    public decimal BIS { get; set; }
    public decimal MENGE { get; set; }
    public string PROZENT { get; set; } = "";
    public DateTime ErfTStamp { get; set; } = DateTime.Now;
    public string ErfUser { get; set; } = "";
    public DateTime AndTStamp { get; set; } = DateTime.Now;
    public string AndUser { get; set; } = "";
}
public class ScrapDetailDeleteRequest : BeasRequest
{
    public string VERSCHN { get; set; } = "";
    public string VON { get; set; } = "";
}
public class ScrapDetailUpdateRequest : BeasRequest
{
    public string VERSCHN { get; set; } = "";
    public string VON { get; set; } = "";
    public string NewVON { get; set; } = "";
    public decimal BIS { get; set; }
    public decimal MENGE { get; set; }
    public string PROZENT { get; set; } = "";
    public DateTime AndTStamp { get; set; } = DateTime.Now;
    public string AndUser { get; set; } = "";
}
public class ScrapCreateRequest : BeasRequest
{
    /// <summary>報廢量</summary>
    public string Scrap { get; set; } = "";
    public string Descrip { get; set; } = "";
    public string Creater { get; set; } = "";
    public DateTime CreateDate { get; set; } = DateTime.Now;
}

// ===== 版本 =====
public class VersionCreateRequest : BeasRequest
{
    public string Version { get; set; } = "";
    public string ItemCode { get; set; } = "";
    public string Description { get; set; } = "";
}
public class VersionUpdateRequest : BeasRequest
{
    /// <summary>品號(條件)</summary>
    public string ItemCode { get; set; } = "";
    /// <summary>版本號(條件)</summary>
    public string VersionId { get; set; } = "";
    public string Description { get; set; } = "";
    /// <summary>圖號</summary>
    public string Znr { get; set; } = "";
    public string Remark { get; set; } = "";
}

// ===== 工單 =====
/// <summary>工單匯入項目。欄位為推測,請依 DLL 的 inputitem 調整。</summary>
public class WorkOrderItem
{
    public string ItemCode { get; set; } = "";
    public double Qty { get; set; }
}
/// <summary>修改工單 BOM 項目(對應 DLL 的 Changebomitem):把工單 BOM 某一行的料號換成新料號。</summary>
public class ChangeBomItem
{
    /// <summary>工單號(BEAS_FTHAUPT.BELNR_ID)</summary>
    public int Woid { get; set; }
    /// <summary>工單位置(BEAS_FTPOS.BELPOS_ID)</summary>
    public int Woposition { get; set; }
    /// <summary>BOM 行號(BEAS_FTSTL.POS_ID)</summary>
    public int Woitemposition { get; set; }
    /// <summary>新料號</summary>
    public string NewItem { get; set; } = "";
}
public class WorkOrderSingleRequest : BeasRequest
{
    public string ItemCode { get; set; } = "";
    public float Qty { get; set; }
}
public class WorkOrderMoreRequest : BeasRequest
{
    public List<WorkOrderItem> InputItems { get; set; } = new();
    public string Mes { get; set; } = "";
    public string CreateTime { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
}
public class WorkOrderWithMachineRequest : WorkOrderMoreRequest
{
    public string Stalk { get; set; } = "";
    public string Machine { get; set; } = "";
    public string Hour { get; set; } = "";
    public string Assem { get; set; } = "";
}
public class ChangeWorkOrderBomRequest : BeasRequest
{
    public List<ChangeBomItem> InputItems { get; set; } = new();
    public string Mes { get; set; } = "";
    public string CreateTime { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
}
public class DelResultBackRequest : BeasRequest
{
    public string CreateTime { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    /// <summary>(選填)直接指定要查的 REQUESTID;留空則用 "wobominsert" + CreateTime(原 DLL 寫死的規則)。</summary>
    public string? RequestId { get; set; }
}

// ===== 發料 (issue) =====
/// <summary>發料明細(對應 DLL 的 WmsOigeM)</summary>
public class WmsOigeM
{
    public DateTime DocDate { get; set; } = DateTime.Today;
    /// <summary>MES 單號(REQUESTID 會是 "issue"+Oid)</summary>
    public string Oid { get; set; } = "";
    /// <summary>SAP 工單 DocEntry</summary>
    public int BaseEntrySAP { get; set; }
    /// <summary>工單行號</summary>
    public int BaseLineSAP { get; set; }
    /// <summary>發料行(base_linenum2)</summary>
    public int BaseIssueLine { get; set; }
    public string ItemCode { get; set; } = "";
    public string WhsCode { get; set; } = "";
    public string BatchNo { get; set; } = "";
    public decimal Qty { get; set; }
}
public class IssueRequest : BeasRequest
{
    public List<WmsOigeM> OigeList { get; set; } = new();
}
