public class TreeScenicMap : TreeMapBase
{
    private TreeScenicMap(int[,] trees)
    {
        Trees = trees;
    }

    public int MaxScenic()
    {
        (int mX, int mY) = this.Dimensions;

        int max = 0;
        for (int x = 0; x < mX; x++)
            for (int y = 0; y < mY; y++)
                if (Trees[x, y] > max)
                    max = Trees[x, y];

        return max;
    }

    public static TreeScenicMap FromHeightMap(TreeHeightMap heightMap)
    {
        (var mX, var mY) = heightMap.Dimensions;

        var result = new int[mX, mY];

        for (int x = 0; x < mX; x++)
            for (int y = 0; y < mY; y++)
                result[x, y] = GetScenic(x, y, heightMap);

        return new TreeScenicMap(result);
    }

    private static int GetScenic(int x, int y, TreeHeightMap heightMap)
    {
        (var mX, var mY) = heightMap.Dimensions;
        var testedTree = heightMap[x, y];
        var r = new int[4];

        for (int cX = x + 1; cX < mX; cX++)
        {
            r[0]++;
            if (testedTree <= heightMap[cX, y])
                break;
        }
        for (int cX = x - 1; cX >= 0; cX--)
        {
            r[1]++;
            if (testedTree <= heightMap[cX, y])
                break;
        }
        for (int cY = y + 1; cY < mY; cY++)
        {
            r[2]++;
            if (testedTree <= heightMap[x, cY])
                break;
        }
        for (int cY = y - 1; cY >= 0; cY--)
        {
            r[3]++;
            if (testedTree <= heightMap[x, cY])
                break;
        }

        return r[0]*r[1]*r[2]*r[3];
    }
}