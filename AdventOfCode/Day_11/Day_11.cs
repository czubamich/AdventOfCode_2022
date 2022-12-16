using AdventOfCode.Helpers;
using AdventOfCode.Infrastructure;
using System.Numerics;

public class Day_11 : BaseDay, IDay
{
    public object PerformPartOne()
    {
        var monkeys = MonkeyProcessor.Process(InputAsLines);
        uint worryLevelDivisor = 3;

        for (ulong i = 0; i < 20; i++)
        {
            foreach(var monkey in monkeys)
                monkey.ProcessRound(monkeys, worryLevelDivisor);
        }

        return monkeys
            .Select(m => m.TotalInspections)
            .OrderByDescending(f => f)
            .Take(2)
            .Multiply();
    }

    public object PerformPartTwo()
    {
        var monkeys = MonkeyProcessor.Process(InputAsLines);
        uint worryLevelDivisor = 1;

        var commonDivisor = monkeys
            .Select(m => m.TestDivisibleBy)
            .Aggregate((f1, f2) => f1 * f2);

        for (ulong i = 0; i < 10_000; i++)
        {
            foreach (var monkey in monkeys)
            {
                monkey.ProcessRound(monkeys, worryLevelDivisor, commonDivisor);
            }
        }

        return monkeys
            .Select(m => m.TotalInspections)
            .OrderByDescending(f => f)
            .Take(2)
            .Multiply();
    }
}

public class MonkeyProcessor
{
    public static List<Monkey> Process(IEnumerable<string> input)
    {
        var result = new List<Monkey>();
        var en = input.GetEnumerator();

        while(en.MoveNext())
        {
            //Parse monkeyId
            var line = en.Current.Replace("Monkey ", "").Replace(":", "");
            var id = uint.Parse(line);
            
            var monkey = new Monkey()
            {
                Id = id,
                Items = ExtractValue(en, "  Starting items: ").Split(", ").Select(ulong.Parse).ToList(),
                Operation = ParseOperation(ExtractValue(en, "  Operation: new = ")),
                TestDivisibleBy = uint.Parse(ExtractValue(en, "  Test: divisible by ")),
                MonkeyToThrowOnTrue = uint.Parse(ExtractValue(en, "    If true: throw to monkey ")),
                MonkeyToThrowOnFalse = uint.Parse(ExtractValue(en, "    If false: throw to monkey ")),
            };

            result.Add(monkey);
            en.MoveNext(); //Skip empty line
        };

        return result;
    }

    private static string ExtractValue(IEnumerator<string> enumerator, string text)
    {
        string line;
        enumerator.MoveNext();
        line = enumerator.Current.Replace(text, "");
        return line;
    }

    private static Func<ulong, ulong> ParseOperation(string v)
    {
        if(v == "old * old")
            return (ulong old) => old * old;
        
        var data = v.Split(' ');
        var variable = ulong.Parse(data[2]);

        if (data[1] == "*")
            return (ulong old) => old * variable;
        
        if (data[1] == "+")
            return (ulong old) => old + variable;

        throw new InvalidDataException();
    }
}

public class Monkey
{
    public ulong TotalInspections = 0;
    public uint Id { get; set; }
    public List<ulong> Items { get; set; } = new List<ulong>();

    public Func<ulong, ulong> Operation { get; set; }
    public uint TestDivisibleBy { get; set; }
    

    public uint MonkeyToThrowOnTrue { get; set; }
    public uint MonkeyToThrowOnFalse { get; set; }

    public void ProcessRound(List<Monkey> monkeys, uint worryLevelDivisor, uint commonDivisor = 1)
    {
        for(int i=0; i<Items.Count; i++)
        {
            Items[i] = InspectItem(Items[i], worryLevelDivisor, commonDivisor);

            if (TestItem(Items[i]))
            {
                monkeys.Single(m => m.Id == MonkeyToThrowOnTrue).Items.Add(Items[i]);
            }
            else
                monkeys.Single(m => m.Id == MonkeyToThrowOnFalse).Items.Add(Items[i]);
        }
        Items = new();
    }

    private bool TestItem(ulong item)
    {
        return item % TestDivisibleBy == 0;
    }

    public ulong InspectItem(ulong item, uint worryLevelDivisor, uint commonDivisor) 
        {
        TotalInspections++;
        return worryLevelDivisor > 1 
            ? Operation(item) / worryLevelDivisor 
            : Operation(item) % commonDivisor;
    }
}