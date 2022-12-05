using MoreLinq;

public class ShipDataParser
{
    public static Ship Parse(IEnumerable<string> shipData)
    {
        var shipDataEnumerator = shipData
            .Reverse()
            .Skip(1)
            .GetEnumerator();
        shipDataEnumerator.MoveNext();

        //get number of crates
        var cratesCount = 9;

        //cratesListText.
        var crates = Enumerable.Repeat(0, cratesCount)
            .Select(x => new Stack<char>())
            .ToArray();

        while (shipDataEnumerator.MoveNext())
        {
            shipDataEnumerator.Current
                .Chunk(4)
                .Select(s => s[1])
                .ForEach((d, i) =>
                {
                    if(char.IsLetterOrDigit(d))
                        crates[i].Push(d);
                });
        }

        return new Ship(crates);
    }
}
