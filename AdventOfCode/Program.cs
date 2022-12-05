using AdventOfCode.Infrastructure;

string dayNumberText = GetDay();

while (int.TryParse(dayNumberText, out int dayNumber))
{
    AdventOfCodeRunner.Run(dayNumber);

    Console.WriteLine("---------------------");
    dayNumberText = GetDay();
}

static string GetDay()
{
    Console.Write("Input day: ");

    return Console.ReadLine();
}