using AdventOfCode.Infrastructure;
using Microsoft.Diagnostics.Tracing.Parsers.FrameworkEventSource;
using System.Reflection;

public static class AdventOfCodeRunner
{
    public static void Run(int dayNumber)
    {
        Type dayType = Type.GetType($"Day_{dayNumber}");
        if (dayType is null)
        {
            Console.WriteLine($"Invalid day: {dayNumber}");
            return;
        }

        Console.WriteLine($"Running: {dayType.Name}\r\n");

        var day = DayRunner.CreateInstance(dayType);

        var result = day.PerformPartOne();
        DisplayResult("Part  I", result);

        var result2 = day.PerformPartTwo();
        DisplayResult("Part II", result2);

        Console.Write($"Run benchmark? [y/N]: ");
        var runBenchmark = Console.ReadKey().KeyChar;
        Console.WriteLine();
        if (runBenchmark != 'y')
            return;

        var summary = DayRunner.Benchmark(dayType);
    }

    public static void RunBenchmarks()
    {
        var dayTypes = typeof(Day_)
            .Assembly
            .GetTypes()
            .Where(t => t.IsAssignableTo(typeof(IDay)) && !t.IsAbstract && !t.IsInterface && t != typeof(Day_))
            .OrderBy(t => int.Parse(t.Name.Remove(0,4)))
            .ToArray();

        var summary = DayRunner.Benchmark(dayTypes);
    }

    private static void DisplayResult(string part, object result)
    {
        var resultText = result switch
        {
            IEnumerable<string> enumerableStr => string.Join("", enumerableStr),
            IEnumerable<char> enumerableChar => string.Join("", enumerableChar),
            _ => result.ToString()
        };

        Console.WriteLine($"{part}: {resultText}");
    }
}

