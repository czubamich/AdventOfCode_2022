using AdventOfCode.Infrastructure;
using MoreLinq;
using System.Security.Cryptography;

public class Day_5 : BaseDay, IDay
{
    public object PerformPartOne()
    {
        Ship ship = ShipDataParser.Parse(ShipData);

        foreach (var step in CrateMover9000StepsProcessor.Process(StepsData))
            step.Execute(ship);
        
        return ship.Crates
            .Select(c => c.Peek());
    }

    public object PerformPartTwo()
    {
        Ship ship = ShipDataParser.Parse(ShipData);

        foreach(var step in CrateMover9001StepsProcessor.Process(StepsData))
            step.Execute(ship);

        return ship.Crates
            .Select(c => c.Peek());
    }

    public IEnumerable<string> StepsData => InputAsLines.SkipUntil(string.IsNullOrWhiteSpace);
    public IEnumerable<string> ShipData => InputAsLines.TakeUntil(string.IsNullOrWhiteSpace);
}
