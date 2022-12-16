using AdventOfCode.Helpers;
using MoreLinq;

public class CrateMover9000StepsProcessor : StepsProcessorBase
{
    public static IEnumerable<Step> Process(IEnumerable<string> stepData)
    {
        var enumerator = stepData.GetEnumerator();

        while (enumerator.MoveNext())
        {
            var stepRawData = Parse(enumerator.Current);

            yield return new CrateMover9000Step(
                stepRawData[1] - 1,
                stepRawData[2] - 1,
                stepRawData[0]
                );
        }
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