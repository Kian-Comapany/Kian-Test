namespace Kian;

internal record class LastTrade
{
    public int ID { get; init; }
    public string? ShortName { get; init; }
    public int InstrumentID { get; init; }
    public DateTime DateTimeEn { get; init; }
    public decimal? OpenPrice { get; init; }
    public decimal? HighPrice { get; init; }
    public decimal? LowPrice { get; init; }
    public decimal ClosePrice { get; init; }
    public decimal? RealClosePrice { get; init; }
}
