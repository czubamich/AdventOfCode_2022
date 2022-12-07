using AdventOfCode.Infrastructure;
using AdventOfCode;

public class Day_7 : BaseDay, IDay
{
    const int TotalDiscSpace = 70000000;
    const int UpdateDiscSpace = 30000000;
    FileSystem fileSystem;

    public Day_7()
    {
        fileSystem = new FileSystem();
        var currentDirectory = fileSystem.Root;

        foreach (var cmd in CommandLineProcessor.Process(InputAsLines))
            cmd.Execute(ref currentDirectory);
    }

    public object PerformPartOne()
    {
        return fileSystem
            .FindDirectories(f => f.DiscSpace <= 100000)
            .Sum(dir => dir.DiscSpace);
    }

    public object PerformPartTwo()
    {
        var totalEmpty = TotalDiscSpace - fileSystem.Root.DiscSpace;
        var discSpaceNeeded = UpdateDiscSpace - totalEmpty;

        return fileSystem
            .FindDirectories(d => d.DiscSpace >= discSpaceNeeded)
            .Min(d => d.DiscSpace);
    }
}
