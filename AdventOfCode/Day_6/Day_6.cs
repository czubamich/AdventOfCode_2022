using AdventOfCode.Infrastructure;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class Day_6 : BaseDay, IDay
{
    public object PerformPartOne()
    {
        var dataAsArray = InputAsText.ToCharArray();
        int i = 4;
        while (dataAsArray[(i - 4)..i++].ToHashSet().Count != 4) ;
        return --i;
    }

    public object PerformPartTwo()
    {
        var dataAsArray = InputAsText.ToCharArray();
        int i = 14;
        while (dataAsArray[(i - 14)..i++].ToHashSet().Count != 14) ;
        return --i;
    }
}