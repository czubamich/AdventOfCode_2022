using AdventOfCode.Helpers;

public class Day_1 : BaseDay, IDay
{
    record Elf(IEnumerable<int> CarriedCallories)
    {
        public int Total => CarriedCallories.Sum();
    }

    private IEnumerable<Elf> GetElves()
    {
        return File.ReadAllText(InputPath)
                .Split("\r\n\r\n")
                .Select(f => new Elf(f.Split("\r\n").Select(int.Parse)));
    }

    public object PerformPartOne()
    {
        return GetElves().Max(x => x.Total);
    }

    public object PerformPartTwo()
    {
        return GetElves()
            .OrderByDescending(x => x.Total)
            .Take(3)
            .Sum(x => x.Total);
    }
}