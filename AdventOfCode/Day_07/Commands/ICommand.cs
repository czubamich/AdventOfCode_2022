namespace AdventOfCode;

public interface ICommand
{
    public void Execute(ref FileSystem.Directory currentDirectory);
}
