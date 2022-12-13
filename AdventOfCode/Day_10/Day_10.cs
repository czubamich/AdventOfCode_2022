using AdventOfCode.Infrastructure;
using MoreLinq;

public class Day_10 : BaseDay, IDay
{
    public object PerformPartOne()
    {
        List<int> cycles = ExecuteProgram();

        var cyclesToGet = new int[] { 20, 60, 100, 140, 180, 220 };

        int signalSum = cyclesToGet
            .Select(c => GetSignalStrength(c, cycles))
            .Sum();

        return signalSum;
    }

    public object PerformPartTwo()
    {
        List<int> cycles = ExecuteProgram();

        var image = cycles
            .Batch(40) //Split into lines
            .Select((l) => l.Select((c, i) => GetPixelBrightness(c, i)))
            .Select(l => l.ToDelimitedString(""))
            .ToDelimitedString(Environment.NewLine);

        return Environment.NewLine+image;
    }
    
    private char GetPixelBrightness(int c, int i)
    {
        return (Math.Abs((i - c)) < 2) ? '#' : '.';
    }

    private List<int> ExecuteProgram()
    {
        var X = 1;
        List<int> cycles = new() { X };

        foreach (var line in InputAsLines)
        {
            var data = line.Split(' ');

            if (data[0] == "noop")
            {
                cycles.Add(X);
            }
            else if (data[0] == "addx")
            {
                cycles.Add(X);
                X = X + int.Parse(data[1]);
                cycles.Add(X);
            }
        }

        return cycles;
    }

    private int GetSignalStrength(int c, List<int> cycles)
    {
        return cycles[c-1] * c;
    }
}