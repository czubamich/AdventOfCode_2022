using AdventOfCode.Infrastructure;
using MoreLinq;

public class Day_4 : BaseDay, IDay
{
    record Assignment(int start, int end)
    {
        public static Assignment Parse(string range)
        {
            var indexes = range
                .Split('-')
                .Select(int.Parse)
                .ToArray();
            return new Assignment(indexes[0], indexes[1]);
        }

        public int Compare(Assignment other)
        {
            if (other.start >= this.start && other.end <= this.end
                || other.start <= this.start && other.end >= this.end)
                return 0;
            else if (other.start <= this.end && this.start <= other.end)
                return 1;
            return -1;
        }

        public static bool ContainsEachOther(Assignment[] assignments)
            => assignments.Pairwise((a, b) => a.Compare(b)).All(a => a == 0);

        public static bool Overlap(Assignment[] assignments)
            => assignments.Pairwise((a, b) => a.Compare(b)).All(a => a >= 0);

        public static Assignment[] ParseLine(string line)
            => line.Split(',').Select(Parse).ToArray();
    }

    public object PerformPartOne()
    {
        return InputAsLines
            .Select(Assignment.ParseLine)
            .Count(Assignment.ContainsEachOther);
    }

    public object PerformPartTwo()
    {
        return InputAsLines
            .Select(Assignment.ParseLine)
            .Count(Assignment.Overlap);
    }
}