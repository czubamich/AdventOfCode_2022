namespace AdventOfCode;

public class ListCommand : ICommand
{
    public required List<string> Output { get; init; }

    public void Execute(ref FileSystem.Directory currentDirectory)
    {
        foreach(var item in Output)
        {
            if(item.StartsWith("dir"))
                currentDirectory.Content.Add(FileSystem.Directory.Parse(currentDirectory, item));
            else
                currentDirectory.Content.Add(FileSystem.File.Parse(item));
        }
    }
}
