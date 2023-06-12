using System.Diagnostics;
using System.Text;

namespace TouchMySquirly;

public class Main
{
    public static void Run()
    {
        var symbols = new char[]
        {
            '{',
            '}',
            '[',
            ']',
            '+',
            '-',
            '_',
            '=',
            '*',
            '&',
            '%',
            '$',
            '@',
            '!',
            '(',
            ')',
            '^',
            '/',
            '|',
            '\\',
            '<',
            '>',
            '?',
            '\'',
            '"',
            ';',
            ':',
            '#',
        };

        SymbolStats[] symbolStatsArray = new SymbolStats[symbols.Length];
        for (int i = 0; i < symbolStatsArray.Length; i++)
        {
            symbolStatsArray[i] = new SymbolStats();
        }

        var rand = new Random();
        var stopwatch = new Stopwatch();
        Console.OutputEncoding = Encoding.UTF8;

        int correctCount = 0;
        int total = 1;

        Console.WriteLine("Press enter to start");
        Console.ReadKey();

        stopwatch.Start();

        while (true)
        {
            int symbolIndex = rand.Next(symbols.Length);
            char charToType = symbols[symbolIndex];
            Console.Write(charToType);
            char inputKey = Console.ReadKey(true).KeyChar;
            if (inputKey == 'q')
            {
                ShowStats(symbols, symbolStatsArray);
                return;
            }

            var correct = inputKey == charToType;
            if (correct)
            {
                correctCount++;
            }
            symbolStatsArray[symbolIndex].Occurences++;
            symbolStatsArray[symbolIndex].TotalTime += stopwatch.ElapsedMilliseconds;
            Console.WriteLine(" " + (correct ? "✅" : "❌") + $" ({correctCount} / {total}) - {stopwatch.ElapsedMilliseconds}ms");
            total++;
            stopwatch.Restart();
        }
    }

    private static void ShowStats(char[] symbols, SymbolStats[] stats)
    {
        if (symbols.Length != stats.Length)
        {
            throw new Exception("symbols array and stats array have diffent length");
        }

        Console.WriteLine("\n\nHere's the stats:");
        for (int i = 0; i < symbols.Length; i++)
        {
            if (stats[i].Occurences == 0)
            {
                continue;
            }

            long averageTime = stats[i].TotalTime / stats[i].Occurences;
            Console.WriteLine($"{symbols[i]}: Occurences: {stats[i].Occurences} - Time: {averageTime}ms");
        }
    }
}

class SymbolStats
{
    public long TotalTime { get; set; }
    public int Occurences { get; set; }
}
