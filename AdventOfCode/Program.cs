using AdventOfCode.Infrastructure;

string dayNumberText = GetDay();

while (!dayNumberText.StartsWith("q"))
{
    if(int.TryParse(dayNumberText, out int dayNumber))
        AdventOfCodeRunner.Run(dayNumber);
    else if (dayNumberText.StartsWith("b"))
        AdventOfCodeRunner.RunBenchmarks();

    Console.WriteLine("---------------------");
    dayNumberText = GetDay();
}

static string GetDay()
{
    Console.Write("Input day: ");

    return Console.ReadLine();
}