using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;

namespace AdventOfCode.Helpers;

public static class DayRunner
{
    public static object PerformPartOne<TDay>() where TDay: IDay 
        => Activator.CreateInstance<TDay>().PerformPartOne();

    public static object PerformPartTwo<TDay>() where TDay : IDay
        => Activator.CreateInstance<TDay>().PerformPartTwo();

    public static Summary Benchmark<TDay>() where TDay : IDay
        => BenchmarkRunner.Run<DayBenchmarkWrapper<TDay>>();
}