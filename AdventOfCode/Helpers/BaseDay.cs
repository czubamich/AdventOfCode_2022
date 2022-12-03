namespace AdventOfCode.Helpers;

public abstract class BaseDay
{
    protected string InputPath => $"Data/{GetType().Name}.txt";

    protected IEnumerable<string> InputAsLines => File.ReadLines(InputPath);
    protected string InputAsText => File.ReadAllText(InputPath);
}