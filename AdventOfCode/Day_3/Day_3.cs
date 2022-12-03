using AdventOfCode.Helpers;
using System.Linq;

public class Day_3 : BaseDay, IDay
{
    record Sack(char[] All)
    {
        int Middle => All.Length/2;
        char[] CompartmentOne => All[0 ..Middle];
        char[] CompartmentTwo => All[Middle..];

        public static char GetCommonItem(Sack sack)
        {
            return sack.CompartmentOne
                .Intersect(sack.CompartmentTwo)
                .Single();
        }
    }

    static class Item
    {
        public static int GetPriority(char item) => char.IsLower(item) ? (int)item-96 : (int)item-64+26;
    }

    static class ElfGroup
    {
        public static char GetCommonItem(Sack[] group)
        {
            IEnumerable<char> items = group[0].All;
            foreach(var elf in group.Skip(1))
            {
                items = items.Intersect(elf.All);
            }
            return items.Single();
        }
    }

    public object PerformPartOne()
    {
        return InputAsLines
            .Select(l => new Sack(l.ToArray()))
            .Select(Sack.GetCommonItem)
            .Select(Item.GetPriority)
            .Sum();
    }

    public object PerformPartTwo()
    {
        return InputAsLines
            .Select(l => new Sack(l.ToArray()))
            .Chunk(3)
            .Select(ElfGroup.GetCommonItem)
            .Select(Item.GetPriority)
            .Sum();

    }
}