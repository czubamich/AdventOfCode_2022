using BenchmarkDotNet.Attributes;

namespace AdventOfCode.Helpers;

[MemoryDiagnoser]
public class DayBenchmarkWrapper<TDay> where TDay : IDay
{
    TDay instance;

    public DayBenchmarkWrapper()
    {
        instance = Activator.CreateInstance<TDay>();
    }

    [Benchmark]
    public void PartOne()
    {
        instance.PerformPartOne();
    }

    [Benchmark]
    public void PartTwo()
    {
        instance.PerformPartTwo();
    }
}
