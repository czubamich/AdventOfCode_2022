using BenchmarkDotNet.Attributes;

[MemoryDiagnoser]
public class DayOne
{
    record Elf(IEnumerable<int> CarriedCallories)
    {
        public int Total => CarriedCallories.Sum();
    }

    [Benchmark]
    public void GetElves()
    {
        var elves = GetElvesFromFile("DayOne.txt");

        var result1 = elves.Max(x => x.Total);

        var result2 = elves
            .OrderByDescending(x => x.Total)
            .Take(3)
            .Sum(x => x.Total);

        static IEnumerable<Elf> GetElvesFromFile(string fileName)
            => File.ReadAllText(fileName)
                .Split("\r\n\r\n")
                .Select(f => new Elf(f.Split("\r\n").Select(int.Parse)));
    }

    [Benchmark]
    public void GetElvesOneLiner()
    {
        var partialQuery = File.ReadAllText("input.txt")
                .Split("\r\n\r\n")
                .Select(group => group.Split("\n")
                    .Where(x => !string.IsNullOrEmpty(x)).Select(int.Parse))
                .Select(x => x.Sum());

        var result1 = partialQuery.Max();

        var result2 = partialQuery
                .OrderByDescending(x => x)
                .Take(3)
                .Sum();
    }
}