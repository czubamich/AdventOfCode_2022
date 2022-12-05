using AdventOfCode.Helpers;
using MoreLinq;

public class CrateMover9000StepsFactory : StepsFactoryBase
{
    public static Step Create(string stepData)
    {
        var stepRawData = Parse(stepData);

        return new CrateMover9000Step(
            stepRawData[1]-1,
            stepRawData[2]-1,
            stepRawData[0]
            );
    }
}
public record CrateMover9000Step(int from, int to, int amount) : Step(from, to, amount)
{
    public override void Execute(Ship ship)
    {
        var crateFrom = ship.Crates[from];
        var crateTo = ship.Crates[to];

        for(int i = 0; i < amount; i++) 
            crateFrom.PopTo(crateTo);
    }
}