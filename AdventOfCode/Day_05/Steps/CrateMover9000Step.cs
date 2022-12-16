using AdventOfCode.Helpers;
using MoreLinq;

public class CrateMover9001StepsProcessor : StepsProcessorBase
{
    public static IEnumerable<Step> Process(IEnumerable<string> stepData)
    {
        var enumerator = stepData.GetEnumerator();

        while(enumerator.MoveNext())
        {
            var stepRawData = Parse(enumerator.Current);

            yield return new CrateMover9001Step(
                stepRawData[1] - 1,
                stepRawData[2] - 1,
                stepRawData[0]
                );
        }
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