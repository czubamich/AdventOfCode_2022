using MoreLinq;

namespace AdventOfCode;

public class ChangeDirectoryCommand : ICommand
{
    public required string Args { get; init; }

    public void Execute(ref FileSystem.Directory currentDirectory)
    {
        if (Args == "/")
            while(currentDirectory.Parent != null)
                currentDirectory = currentDirectory.Parent;
        else if (Args == "..")
            currentDirectory = currentDirectory.Parent;
        else
            currentDirectory = currentDirectory.Content.OfType<FileSystem.Directory>().First(c => c.Name == Args);
    }
}
