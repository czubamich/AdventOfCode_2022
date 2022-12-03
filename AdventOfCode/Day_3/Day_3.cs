using AdventOfCode.Helpers;
using System.Linq;

public class Day_3 : BaseDay, IDay
{
    record Sack(char[] All)
    {
        int middle = 0;
        int Middle => middle > 0 ? middle : middle = All.Length/2;
        char[] CompartmentOne => All[0 ..Middle];
        char[] CompartmentTwo => All[Middle..];

        public static char GetCommonItem(Sack sack)
        {
            return sack.CompartmentOne.Distinct()
                .Intersect(sack.CompartmentTwo.Distinct())
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
            IEnumerable<char> items = null;
            foreach(var elf in group)
            {
                if (items == null)
                {
                    items = elf.All;
                    continue;
                }
                
                items = items.Intersect(elf.All.AsEnumerable());
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