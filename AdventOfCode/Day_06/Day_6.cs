using AdventOfCode.Infrastructure;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class Day_6 : BaseDay, IDay
{
    public object PerformPartOne()
    {
        return GetFirstGroupDistinctIndex(InputAsText, 4);
    }

    public object PerformPartTwo()
    {
        return GetFirstGroupDistinctIndex(InputAsText, 14);
    }

    private static int GetFirstGroupDistinctIndex(string input, int requiredDistinct)
    {
        var dataAsArray = input.ToCharArray();
        int i = requiredDistinct;
        while (dataAsArray[(i - requiredDistinct)..i++].ToHashSet().Count != requiredDistinct);
        return --i;
    }
}