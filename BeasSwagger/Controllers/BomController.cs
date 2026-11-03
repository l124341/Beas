using BeasSwagger.Models;
using BeasSwagger.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeasSwagger.Controllers;

[ApiController]
[Route("api/bom")]
public class BomController(BeasScriptRunner runner, IBeasQueryClient query, IBeasCommandClient command, BeasResultReader reader) : ControllerBase
{
    static bool IsMock(List<Dictionary<string, object?>> rows) => rows.Count > 0 && rows[0].ContainsKey("MOCK_SQL");

    async Task<long> CountAsync(BeasSqlStatement s, BeasConnection? conn, CancellationToken ct)
    {
        var rows = await query.QueryAsync(s.Sql, s.Args, conn, ct);
        if (rows.Count == 0 || IsMock(rows)) return 0;   // Mock 模式沒有資料庫,視為 0
        var v = rows[0].Values.FirstOrDefault();
        return v is null ? 0 : Convert.ToInt64(v);
    }

    static string Str(Dictionary<string, object?> r, string col) => r.GetValueOrDefault(col)?.ToString() ?? "";
    static decimal Dec(Dictionary<string, object?> r, string col) => r.GetValueOrDefault(col) is { } v ? Convert.ToDecimal(v) : 0m;

    /// <summary>讀出「該版本以外」的既有明細(原 Searchotherversionbom 的 SELECT),轉成 BomItem。</summary>
    async Task<List<BomItem>> LoadOtherLinesAsync(string itemCode, string version, BeasConnection? conn, CancellationToken ct)
    {
        var q = BeasScriptBuilder.BomOtherVersionLinesQuery(itemCode, version);
        var rows = await query.QueryAsync(q.Sql, q.Args, conn, ct);
        if (IsMock(rows)) return new();
        return rows.Select(r =>
        {
            var pos = Str(r, "POS_TEXT");
            if (!int.TryParse(pos, out var posText)) throw new ArgumentException($"既有明細的 POS_TEXT '{pos}' 不是整數,無法保留");
            return new BomItem
            {
                ItemCode = Str(r, "ART1_ID"), Quanity = Dec(r, "INPUT_QTY"), PosText = posText, QuantityPerPiece = Dec(r, "MENGE_JE"),
                Length = Dec(r, "ABM1"), FaterVersion = Str(r, "VersionArea"), Stap = Dec(r, "ScrapPercent"), WorkRout = Str(r, "UDF1"),
            };
        }).ToList();
    }

    /// <summary>
    /// 取代某版本的明細(原 Searchotherversionbom):讀出其他版本的既有明細 → 在同一交易刪除 BOM 表頭與該版本的行並寫入 INPUT →
    /// script 以「新明細 + 其他版本既有明細」重建整張 BOM。只有 Script 版,需 AllowDirectSqlWrite。
    /// </summary>
    async Task<BeasResult> ReplaceVersionAsync(BeasRequest req, string itemCode, string version, IReadOnlyList<BomItem> items, string createTime, CancellationToken ct)
    {
        command.EnsureWriteAllowed();   // 先檢查開關,再碰資料庫
        var all = items.Concat(await LoadOtherLinesAsync(itemCode, version, req.Connection, ct)).ToList();
        return await runner.RunAsync(req, () => BeasScriptBuilder.BomReplaceVersion(itemCode, all, createTime), null, ct,
            BeasScriptBuilder.BomReplaceVersionStatements(itemCode, version));
    }

    /// <summary>建立BOM多明細</summary>
    /// <remarks>DLL 函數:<c>CreateM</c>。Script 版:BOM 已存在時失敗;Bsl 版:加入明細。REQUESTID = "Bom"+CreateTime。</remarks>
    [HttpPost("create")]
    public async Task<ActionResult<BeasResult>> Create(BomCreateRequest req, CancellationToken ct) =>
        Ok(await runner.RunAsync(req, () => BeasScriptBuilder.BomCreate(req), () => BeasBslBuilder.BomCreate(req), ct));

    /// <summary>尋找其他版本的BOM(取代指定版本的明細,保留其他版本)</summary>
    /// <remarks>DLL 函數:<c>Searchotherversionbom</c>。名稱容易誤解:它會刪除該版本的明細與 BOM 表頭,再用「新明細 + 其他版本既有明細」重建。
    /// 直接刪除 Beas 資料表,需 <c>Beas:Script:AllowDirectSqlWrite=true</c>;只有 Script 版。</remarks>
    [HttpPost("search-other-version")]
    public async Task<ActionResult<BeasResult>> SearchOtherVersion(BomSearchOtherVersionRequest req, CancellationToken ct) =>
        Ok(await ReplaceVersionAsync(req, req.ItemCode, req.Version, req.InputItems, DateTime.Now.ToString("yyyyMMddHHmmssfff"), ct));

    /// <summary>新版本BOM插入</summary>
    /// <remarks>DLL 函數:<c>CreateMultVer</c>。所有明細行的 VersionArea 設為 Version。Script 版的 <c>bom=new</c> 在 BOM 已存在時會失敗。</remarks>
    [HttpPost("create-version")]
    public async Task<ActionResult<BeasResult>> CreateVersion(BomVersionRequest req, CancellationToken ct) =>
        Ok(await runner.RunAsync(req, () => BeasScriptBuilder.BomCreateVersion(req), () => BeasBslBuilder.BomCreateVersion(req), ct));

    /// <summary>修改/更新BOM</summary>
    /// <remarks>DLL 函數:<c>ChangeBom</c>。沒有 BOM(明細數為 0)→ 走建立;已有 BOM → 取代該版本的明細(同 search-other-version,需 AllowDirectSqlWrite)。
    /// Version 為 "-1" 視為空白版本。</remarks>
    [HttpPost("change")]
    public async Task<ActionResult<BeasResult>> Change(BomVersionRequest req, CancellationToken ct)
    {
        var version = req.Version == "-1" ? "" : req.Version;
        if (await CountAsync(BeasScriptBuilder.BomCountQuery(req.FatherItem), req.Connection, ct) == 0)
            return Ok(await runner.RunAsync(req, () => BeasScriptBuilder.BomCreate(req), () => BeasBslBuilder.BomCreate(req), ct));
        return Ok(await ReplaceVersionAsync(req, req.FatherItem, version, req.InputItems, req.CreateTime, ct));
    }

    /// <summary>查看BOM(明細行數)</summary>
    /// <remarks>DLL 函數:<c>checkbom</c>。回傳 BEAS_STL 中該 ItemCode 的明細行數。</remarks>
    [HttpPost("check")]
    public async Task<ActionResult<BeasResult>> Check(ItemRequest req, CancellationToken ct)
    {
        var n = await CountAsync(BeasScriptBuilder.BomCountQuery(req.ItemCode), req.Connection, ct);
        return Ok(new BeasResult(true, $"{n} 筆", n));
    }

    /// <summary>查看BOM版本(該版本的明細行數)</summary>
    /// <remarks>DLL 函數:<c>checkbomversion</c>。版本空白 = VersionArea 為 NULL 的行;否則 VersionArea like %版本%。</remarks>
    [HttpPost("check-version")]
    public async Task<ActionResult<BeasResult>> CheckVersion(ItemVersionRequest req, CancellationToken ct)
    {
        var n = await CountAsync(BeasScriptBuilder.BomVersionCountQuery(req.ItemCode, req.Version), req.Connection, ct);
        return Ok(new BeasResult(true, $"{n} 筆", n));
    }

    /// <summary>刪除BOM</summary>
    /// <remarks>DLL 函數:<c>DeleteBom</c>。直接刪除 BEAS_STL_HEADER 與 BEAS_STL(同一交易),需 <c>Beas:Script:AllowDirectSqlWrite=true</c>。</remarks>
    [HttpPost("delete")]
    public async Task<ActionResult<BeasResult>> Delete(BomDeleteRequest req, CancellationToken ct) =>
        Ok(await command.ExecuteAsync(BeasScriptBuilder.BomDeleteStatements(req.FatherItem), req.Connection, ct));

    /// <summary>回傳成功與否</summary>
    /// <remarks>DLL 函數:<c>sqlresultback</c>。原 DLL 會把 mes 清成空字串,所以 REQUESTID = "Bom" + CreateTime(Mes 不參與)。</remarks>
    [HttpPost("sql-result-back")]
    public async Task<ActionResult<BeasResult>> SqlResultBack(SqlResultBackRequest req, CancellationToken ct) =>
        Ok(await reader.ReadAsync(string.IsNullOrWhiteSpace(req.RequestId) ? "Bom" + req.CreateTime : req.RequestId, req.Connection, ct));

    /// <summary>取得itemcode中的最大階層</summary>
    /// <remarks>DLL 函數:<c>selectmaxpostext</c>。原 DLL 對字串欄位取 MAX(字典序),這裡改取數值最大值。</remarks>
    [HttpPost("max-level")]
    public async Task<ActionResult<BeasResult>> MaxLevel(ItemRequest req, CancellationToken ct)
    {
        var rows = await query.QueryAsync(
            @"select ""pos_text"" from {db}.""BEAS_STL"" where ""ItemCode""=?",
            new object?[] { req.ItemCode }, req.Connection, ct);
        var max = rows.Select(r => r.GetValueOrDefault("pos_text")?.ToString())
                      .Where(t => decimal.TryParse(t, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out _))
                      .Select(t => decimal.Parse(t!, System.Globalization.CultureInfo.InvariantCulture))
                      .DefaultIfEmpty(0).Max();
        return Ok(new BeasResult(true, "完成", max));
    }
}
