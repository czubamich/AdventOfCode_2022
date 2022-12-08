public abstract class TreeMapBase
{
    protected int[,] Trees {get; init; }

    public int this[int x, int y] => Trees[x, y];

    public (int mX, int mY) Dimensions => (Trees.GetLength(0), Trees.GetLength(1));
}