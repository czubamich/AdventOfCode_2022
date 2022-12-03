using AdventOfCode.Helpers;

public static class AdventOfCodeRunner
{
    public static void Run<TDay>() where TDay : IDay
    {
        Console.WriteLine($"Running: {typeof(TDay).Name}\r\n");

        var result = DayRunner.PerformPartOne<TDay>();
        Console.WriteLine($"Part  I: {result}");

        var result2 = DayRunner.PerformPartTwo<TDay>();
        Console.WriteLine($"Part II: {result2}");

        Console.ReadKey();

        Console.WriteLine($"Running benchmark:");
        var summary = DayRunner.Benchmark<TDay>();
    }
}