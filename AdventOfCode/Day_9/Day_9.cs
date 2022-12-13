using AdventOfCode.Infrastructure;
using MoreLinq;
using Perfolizer.Mathematics.Thresholds;

public class Day_9 : BaseDay, IDay
{
    public object PerformPartOne()
    {
        Rope initial = Rope.OfLength(2);

        SimulateRope(initial, out var ropeSegmentPositions);

        return ropeSegmentPositions
            .Select(x => x.Segments[1])
            .Distinct()
            .Count();
    }

    public object PerformPartTwo()
    {
        Rope initial = Rope.OfLength(10);

        SimulateRope(initial, out var ropeSegmentPositions);

        return ropeSegmentPositions
            .Select(x => x.Segments[9])
            .Distinct()
            .Count();
    }

    private Rope SimulateRope(Rope current, out List<Rope> ropeSegmentPositions)
    {
        ropeSegmentPositions = new() { current };

        foreach (var line in InputAsLines)
        {
            var data = ParseMovement(line);

            for (int i = 0; i < data.amount; i++)
            {
                current = current.Move(data.movement);
                ropeSegmentPositions.Add(current);
            }
        }

        return current;
    }

    private static (Point movement, int amount) ParseMovement(string line)
    {
        var data = line.Split(' ');

        return (data[0] switch
        {
            "U" => Point.Up,
            "D" => Point.Down,
            "R" => Point.Right,
            "L" => Point.Left,
            _ => throw new ArgumentOutOfRangeException()
        }, int.Parse(data[1]));
    }
}

public record Point(int x, int y)
{
    public static Point Zero => new (0, 0);

    public static Point Up => new(0, 1);
    public static Point Down => new(0, -1);
    public static Point Right => new(1, 0);
    public static Point Left => new(-1, 0);

    public static Point operator +(Point a, Point b) => new Point(a.x+b.x, a.y+b.y);
}

public record Rope(Point[] Segments)
{
    public static Rope OfLength(int length)
    {
        var segments = new Point[length];
        for (int i = 0; i < length; i++)
            segments[i] = Point.Zero;
        return new Rope(segments);
    }

    public Rope Move(Point movement)
    {
        var newSegments = new Point[Segments.Length];
        newSegments[0] = Segments[0] + movement;

        for (int i = 1; i < Segments.Length; i++)
        {
            newSegments[i] = SimulateTail(newSegments[i - 1], Segments[i]);
        }

        return new Rope(newSegments);
    }

    public static Point SimulateTail(Point headNew, Point tail)
    {
        var xDiff = headNew.x - tail.x;
        var yDiff = headNew.y - tail.y;

        var touching = Math.Abs(xDiff) <= 1 && Math.Abs(yDiff) <= 1;
        if(touching)
            return tail;
        //horizontal
        if(yDiff == 0)
            return tail + (xDiff > 0 ? Point.Right : Point.Left);
        //vertical
        if (xDiff == 0)
            return tail + (yDiff > 0 ? Point.Up : Point.Down);
        //diagonal
        return tail with
        {
            x = tail.x + (xDiff > 0 ? 1 : -1),
            y = tail.y + (yDiff > 0 ? 1 : -1)
        };
    }
}