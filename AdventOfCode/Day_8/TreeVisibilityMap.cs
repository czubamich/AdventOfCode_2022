public class TreeVisibilityMap : TreeMapBase
{
    public const int VisibleFromAll = 0b1111;
    public const int VisibleFromNone = 0b0000;

    private const int VisibleFromRight = 0b1000;
    private const int VisibleFromUp = 0b0100;
    private const int VisibleFromLeft = 0b0010;
    private const int VisibleFromDown = 0b0001;

    private TreeVisibilityMap(int[,] trees)
    {
        Trees = trees;
    }

    public int CountVisible()
    {
        (int mX, int mY) = this.Dimensions;

        int count = 0;
        for(int x = 0; x < mX; x++)
            for (int y = 0; y < mY; y++)
                if (Trees[x,y] != VisibleFromNone)
                    count++;

        return count;
    }

    internal static TreeVisibilityMap FromHeightMap(TreeHeightMap heightMap)
    {
        (var mX, var mY) = heightMap.Dimensions;
        
        var result = new int[mX,mY];

        for (int x = 0; x < mX; x++)
            for (int y = 0; y < mY; y++)
                result[x, y] = GetVisibility(x, y, heightMap);

        return new TreeVisibilityMap(result);
    }

    private static int GetVisibility(int x, int y, TreeHeightMap heightMap)
    {
        (var mX, var mY) = heightMap.Dimensions;
        var result = VisibleFromAll;
        var testedTree = heightMap[x,y];

        for(int cX = x+1; cX < mX; cX++)
        {
            if(testedTree <= heightMap[cX, y])
            {
                result &= ~VisibleFromRight;
                break;
            }
        }
        for (int cX = x-1; cX >= 0; cX--)
        {
            if (testedTree <= heightMap[cX, y])
            {
                result &= ~VisibleFromLeft;
                break;
            }
        }
        for (int cY = y+1; cY < mY; cY++)
        {
            if (testedTree <= heightMap[x, cY])
            {
                result &= ~VisibleFromDown;
                break;
            }
        }
        for (int cY = y-1; cY >= 0; cY--)
        {
            if (testedTree <= heightMap[x, cY])
            {
                result &= ~VisibleFromUp;
                break;
            }
        }

        return result;
    }
}