using MoreLinq;

public class ShipDataParser
{
    const int CRATE_COUNT = 9;
    static int Column(int i) => 1 + i * 4;

    public static Ship Parse(IEnumerable<string> shipData)
    {
        var dataArray = shipData.ToArray();

        var crates = new Stack<char>[CRATE_COUNT];

        for(int i = 0; i < CRATE_COUNT; i++)
        {
            crates[i] = new Stack<char>();
            for (int j=dataArray.Length-3; j>=0; j--)
            {
                var x = dataArray[j][Column(i)];
                if (x == ' ')
                    continue;
                crates[i].Push(x);
            }
        }

        return new Ship(crates);
    }
}
