using AdventOfCode.Helpers;
using MoreLinq;

public class CrateMover9001StepsFactory : StepsFactoryBase
{
    public static Step Create(string stepData)
    {
        var stepRawData = Parse(stepData);

        return new CrateMover9001Step(
            stepRawData[1]-1,
            stepRawData[2]-1,
            stepRawData[0]
            );
    }
}
public record CrateMover9001Step(int from, int to, int amount) : Step(from, to, amount)
{
    public override void Execute(Ship ship)
    {
        var crateFrom = ship.Crates[from];
        var crateTo = ship.Crates[to];

        var items = crateFrom.PopMany(amount);
        crateTo.PushManyInReverse(items);
    }
}