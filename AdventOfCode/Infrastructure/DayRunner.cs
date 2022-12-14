using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;

namespace AdventOfCode.Infrastructure;

public static class DayRunner
{
    public static IDay CreateInstance(Type dayType)
        => Activator.CreateInstance(dayType) as IDay ?? throw new ArgumentException($"Invalid type {dayType.Name}");

    public static Summary Benchmark(Type dayType)
    {
        var benchmarkType = GetBenchmarkWrapperType(dayType);

        return BenchmarkRunner.Run(benchmarkType,
            ManualConfig.Create(DefaultConfig.Instance)
                .HideColumns("Type", "Method")
                .AddColumn(new CustomColumn("Day", "Day of AdventOfCode", ExtractDayNumber))
                .AddColumn(new CustomColumn("Part", "Part of the day", ExtractDayPart))
                .AddJob(Job.MediumRun)
                .WithOrderer(new DefaultOrderer(SummaryOrderPolicy.Declared, MethodOrderPolicy.Alphabetical)));
    }

    public static Summary[] Benchmark(Type[] dayType)
    {
        var benchmarkTypes = dayType
            .Select(GetBenchmarkWrapperType)
            .ToArray();

        return BenchmarkRunner.Run(benchmarkTypes,
            ManualConfig.Create(DefaultConfig.Instance)
                .HideColumns("Type", "Method")
                .AddColumn(new CustomColumn("Day", "Day of AdventOfCode", ExtractDayNumber))
                .AddColumn(new CustomColumn("Part", "Part of the day", ExtractDayPart))
                .AddJob(Job.ShortRun)
                .WithOrderer(new DefaultOrderer(SummaryOrderPolicy.Declared, MethodOrderPolicy.Alphabetical))
                .WithOptions(ConfigOptions.JoinSummary));
    }

    private static string ExtractDayNumber(BenchmarkCase benchmarkCase)
        => benchmarkCase.Descriptor.Type.GenericTypeArguments.First().Name.Replace(nameof(Day_), "");

    private static string ExtractDayPart(BenchmarkCase benchmarkCase)
        => benchmarkCase.Descriptor.WorkloadMethod.Name switch 
            { 
                nameof(DayBenchmarkWrapper<IDay>.PartOne) => "One", 
                nameof(DayBenchmarkWrapper<IDay>.PartTwo) => "Two", 
                _ => "???" 
            };

    public static Type GetBenchmarkWrapperType(Type type)
    {
        var typeString = $"AdventOfCode.Infrastructure.DayBenchmarkWrapper`1[[{type.Name}, AdventOfCode, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]";
        return Type.GetType(typeString) ?? throw new InvalidOperationException($"Unable to resolve benchmark of type {typeString}");
    }
    public class CustomColumn : IColumn
    {
        Func<BenchmarkCase, string> TextSelector { get; init; }
        public string Id { get; }
        public string ColumnName { get; }

        public CustomColumn(string columnName, string legend, Func<BenchmarkCase, string> textSelector)
        {
            ColumnName = columnName;
            Id = nameof(CustomColumn) + "." + ColumnName;
            TextSelector =  textSelector;
            Legend = legend;
        }

        public bool IsDefault(Summary summary, BenchmarkCase benchmarkCase) => false;
        public string GetValue(Summary summary, BenchmarkCase benchmarkCase) => TextSelector(benchmarkCase);

        public bool IsAvailable(Summary summary) => true;
        public bool AlwaysShow => true;
        public ColumnCategory Category => ColumnCategory.Job;
        public int PriorityInCategory => 0;
        public bool IsNumeric => false;
        public UnitType UnitType => UnitType.Dimensionless;
        public string Legend { get; }
        public string GetValue(Summary summary, BenchmarkCase benchmarkCase, SummaryStyle style) => GetValue(summary, benchmarkCase);
        public override string ToString() => ColumnName;
    }
}

