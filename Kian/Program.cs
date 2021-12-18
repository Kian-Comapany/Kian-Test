Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine("NOTE: Before starting, you have to run Kian.sql file.");
Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine("Starting process...");

var ct = new CancellationTokenSource().Token;
var lastTradeRepository = new LastTradeRepository();
var lastTrades = await lastTradeRepository.GetAsync(null, ct);
var path = Path.Combine(Environment.CurrentDirectory, "last-trades.json");
var lastTradesJson = await lastTradeRepository.SaveToFileAsync(lastTrades, path, ct);

JsonPrinter.Print(lastTradesJson);

Console.ForegroundColor = ConsoleColor.Blue;
Console.WriteLine($"JSON file saved in path: {path}");
Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine("Press any key to exit.");
Console.ReadKey();