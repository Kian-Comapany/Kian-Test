using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace Kian;

internal class LastTradeRepository : ILastTradeRepository
{
    public async Task<IEnumerable<LastTrade>> GetAsync(DateTime? startDate, CancellationToken ct)
    {
        var connectinString = "Server=.; Database=TestDb1; Trusted_Connection=Yes;";
        using SqlConnection connection = new(connectinString);
        await connection.OpenAsync(ct);

        var startDateStr = startDate?.ToString("yyyy-dd-MM HH:mm:ss.fff");
        var command = startDateStr is null ?
            "SELECT * FROM LastTrades" :
            $"SELECT * FROM LastTrades WHERE DateTimeEn >= '{startDateStr}'";

        SqlCommand sqlCommand = new(command, connection);

        var reader = await sqlCommand.ExecuteReaderAsync(ct);
        var lastTrades = await MapToLastTrades(reader, ct);

        await connection.CloseAsync();

        return lastTrades;
    }

    public async Task<string> SaveToFileAsync(IEnumerable<LastTrade> lastTrades, string path, CancellationToken ct)
    {
        var lastTradesJson = JsonConvert.SerializeObject(lastTrades, Formatting.Indented);
        await File.WriteAllTextAsync(path, lastTradesJson, ct);
        return lastTradesJson;
    }

    private static async Task<IEnumerable<LastTrade>> MapToLastTrades(
        SqlDataReader reader,
        CancellationToken ct
    )
    {
        var lastTrades = new HashSet<LastTrade>();
        while (await reader.ReadAsync(ct))
        {
            _ = lastTrades.Add(new LastTrade()
            {
                ID = reader.GetValue(nameof(LastTrade.ID)).Convert<int>(),
                ShortName = reader.GetValue(nameof(LastTrade.ShortName)).Convert<string>(),
                InstrumentID = reader.GetValue(nameof(LastTrade.InstrumentID)).Convert<int>(),
                DateTimeEn = reader.GetValue(nameof(LastTrade.DateTimeEn)).Convert<DateTime>(),
                OpenPrice = reader.GetValue(nameof(LastTrade.OpenPrice)).Convert<decimal?>(),
                HighPrice = reader.GetValue(nameof(LastTrade.HighPrice)).Convert<decimal?>(),
                LowPrice = reader.GetValue(nameof(LastTrade.LowPrice)).Convert<decimal?>(),
                ClosePrice = reader.GetValue(nameof(LastTrade.ClosePrice)).Convert<decimal>(),
                RealClosePrice = reader.GetValue(nameof(LastTrade.RealClosePrice)).Convert<decimal?>(),
            });
        }

        return lastTrades;
    }
}
