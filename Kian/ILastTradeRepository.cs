namespace Kian;

interface ILastTradeRepository
{
    Task<IEnumerable<LastTrade>> GetAsync(DateTime? startDate, CancellationToken ct);
    Task<string> SaveToFileAsync(IEnumerable<LastTrade> lastTrades, string path, CancellationToken ct);
}
