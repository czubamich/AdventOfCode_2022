using AdventOfCode.Infrastructure;
using System.Diagnostics;

public class Day_6 : BaseDay, IDay
{
    public object PerformPartOne()
    {
        return SignalProcessor.FindFirstMarker(InputAsText, 4);
    }

    public object PerformPartTwo()
    {
        return SignalProcessor.FindFirstMarker(InputAsText, 14);
    }
}

public class SignalProcessor
{
    public static int FindFirstMarker(string data, int expectedDistinct)
    {
        for (int i = expectedDistinct; i < data.Length; i++)
            if (data[(i - expectedDistinct)..i].Distinct().Count() == expectedDistinct)
                return i;
        throw new UnreachableException();
    }
}