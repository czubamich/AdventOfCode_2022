using AdventOfCode.Helpers;
using Microsoft.Diagnostics.Tracing.Parsers.FrameworkEventSource;

public static class AdventOfCodeRunner
{
    public static void Run(int dayNumber)
    {
        Type dayType = Type.GetType($"Day_{dayNumber}");
        if (dayType is null )
        {
            Console.WriteLine($"Invalid day: {dayNumber}");
            return;
        }

        Console.WriteLine($"Running: {dayType.Name}\r\n");

        var day = DayRunner.CreateInstance(dayType);

        var result = day.PerformPartOne();
        Console.WriteLine($"Part  I: {result}");

        var result2 = day.PerformPartTwo();
        Console.WriteLine($"Part II: {result2}");

        Console.Write($"Run benchmark? [y/N]: ");
        var runBenchmark = Console.ReadKey().KeyChar;
        Console.WriteLine();
        if(runBenchmark != 'y')
            return;

        var summary = DayRunner.Benchmark(dayType);
    }
}