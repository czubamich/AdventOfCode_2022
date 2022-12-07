namespace AdventOfCode;

public class FileSystem
{
    public Directory Root = new("/", null);

    public class Directory : IFile
    {
        public string Name { get; set; }
        public Directory Parent { get; set; }
        public List<IFile> Content { get; set; } = new();

        public int DiscSpace => Content.Sum(f => f.DiscSpace);

        public Directory(string name, Directory parent)
        {
            Name = name;
            Parent = parent;
        }

        public static Directory Parse(Directory parent, string text)
        {
            var fileData = text.Split(' ');
            return new Directory(fileData[1], parent);
        }
    }

    public record File(string Name, int DiscSpace) : IFile 
    {
        public static File Parse(string text)
        {
            var fileData = text.Split(' ');
            return new File(fileData[1], int.Parse(fileData[0]));
        }
    }

    public interface IFile
    {
        string Name { get; }
        int DiscSpace { get; }
    }

    public IEnumerable<Directory> FindDirectories(Func<Directory, bool> searchFunc)
    {
        return FindDirectories(Root, searchFunc);
    }

    public static IEnumerable<Directory> FindDirectories(Directory dir, Func<Directory, bool> searchFunc)
    {
        var childResult = dir.Content.OfType<Directory>().Any() ?
            dir.Content
            .OfType<Directory>()
            .SelectMany(dir => FindDirectories(dir, searchFunc))
            : Enumerable.Empty<Directory>();

        return dir.Content
            .OfType<Directory>()
            .Where(searchFunc)
            .Union(childResult)
            .ToList();
    }
}