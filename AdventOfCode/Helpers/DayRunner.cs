using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;

namespace AdventOfCode.Helpers;

public static class DayRunner
{
    public static IDay CreateInstance(Type dayType)
        => Activator.CreateInstance(dayType) as IDay ?? throw new ArgumentException($"Invalid type {dayType.Name}");

    public static Summary Benchmark(Type dayType)
    {
        var typeString = $"AdventOfCode.Helpers.DayBenchmarkWrapper`1[[{dayType.Name}, AdventOfCode, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]";
        var benchmarkType = Type.GetType(typeString);

        return BenchmarkRunner.Run(benchmarkType);
    }
}