using AdventOfCode.Infrastructure;
using MoreLinq;

public class Day_5 : BaseDay, IDay
{
    public object PerformPartOne()
    {
        Ship ship = ShipDataParser.Parse(ShipData);

        StepsData
            .Select(CrateMover9000StepsFactory.Create)
            .ForEach(step => step.Execute(ship));
        
        return ship.Crates
            .Select(c => c.Peek());
    }

    public object PerformPartTwo()
    {
        Ship ship = ShipDataParser.Parse(ShipData);

        StepsData
            .Select(CrateMover9001StepsFactory.Create)
            .ForEach(step => step.Execute(ship));

        return ship.Crates
            .Select(c => c.Peek());
    }

    public IEnumerable<string> StepsData => InputAsLines.SkipUntil(string.IsNullOrWhiteSpace);
    public IEnumerable<string> ShipData => InputAsLines.TakeUntil(string.IsNullOrWhiteSpace);
}
