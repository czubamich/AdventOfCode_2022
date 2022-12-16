using AdventOfCode.Infrastructure;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Numerics;

public class Day_8 : BaseDay, IDay
{
    public object PerformPartOne()
    {
        var heightMap = TreeHeightMap.FromData(InputAsLines);

        var visibilityMap = TreeVisibilityMap.FromHeightMap(heightMap);

        return visibilityMap.CountVisible();
    }

    public object PerformPartTwo()
    {
        var heightMap = TreeHeightMap.FromData(InputAsLines);

        var scenicMap = TreeScenicMap.FromHeightMap(heightMap);

        return scenicMap.MaxScenic();
    }
}