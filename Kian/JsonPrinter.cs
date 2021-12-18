namespace Kian;

internal static class JsonPrinter
{
    public static void Print(string json)
    {
        Console.WriteLine($"{json}\r\n");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Length: {json.Length}\r\n");
        Console.ForegroundColor = ConsoleColor.White;
    }
}