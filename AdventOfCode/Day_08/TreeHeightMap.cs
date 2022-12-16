using MoreLinq.Extensions;

public class TreeHeightMap : TreeMapBase
{
    public static TreeHeightMap FromData(IEnumerable<string> inputAsLines)
    {
        var inputX = inputAsLines.First().Length;
        var inputY = inputAsLines.Count();

        var trees = new int[inputX, inputY];
        for (var y = 0; y < inputY; y++)
        {
            var line = inputAsLines.ElementAt(y).ToCharArray();
            for (var x = 0; x < line.Length; x++)
            {
                trees[x, y] = line[x] - '0';
            }
        }
        return new TreeHeightMap(trees);
    }

    private TreeHeightMap(int[,] trees)
    {
        Trees = trees;
    }
}
