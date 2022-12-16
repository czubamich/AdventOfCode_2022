using AdventOfCode.Infrastructure;
using AdventOfCode;

public class Day_7 : BaseDay, IDay
{
    const int TotalDiscSpace = 70000000;
    const int UpdateDiscSpace = 30000000;

    public object PerformPartOne()
    {
        FileSystem fileSystem = InitFileSystem(InputAsLines);

        return fileSystem
            .FindDirectories(f => f.DiscSpace <= 100000)
            .Sum(dir => dir.DiscSpace);
    }

    public object PerformPartTwo()
    {
        FileSystem fileSystem = InitFileSystem(InputAsLines);

        var totalEmpty = TotalDiscSpace - fileSystem.Root.DiscSpace;
        var discSpaceNeeded = UpdateDiscSpace - totalEmpty;

        return fileSystem
            .FindDirectories(d => d.DiscSpace >= discSpaceNeeded)
            .Min(d => d.DiscSpace);
    }

    private static FileSystem InitFileSystem(IEnumerable<string> input)
    {
        var fileSystem = new FileSystem();
        var currentDirectory = fileSystem.Root;

        foreach (var cmd in CommandLineProcessor.Process(input).ToList())
            cmd.Execute(ref currentDirectory);
        return fileSystem;
    }
}
