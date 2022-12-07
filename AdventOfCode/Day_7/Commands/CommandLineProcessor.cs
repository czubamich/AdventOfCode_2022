using System.Diagnostics;

namespace AdventOfCode;

public class CommandLineProcessor
{
    public static IEnumerable<ICommand> Process(IEnumerable<string> commandLine)
    {
        var enumerator = commandLine.GetEnumerator();
        enumerator.MoveNext();

        while (true)
        {
            var line = enumerator.Current;
            if (line.StartsWith("$ cd"))
            {
                yield return new ChangeDirectoryCommand { Args = line.Substring(5) };
                if(!enumerator.MoveNext())
                    yield break;
            }
            else if (line.StartsWith("$ ls"))
            {
                List<string> output = new();
                enumerator.MoveNext();
                line = enumerator.Current;

                while (!line.StartsWith('$'))
                {
                    output.Add(line);

                    if (!enumerator.MoveNext())
                    {
                        yield return new ListCommand() { Output = output };
                        yield break;
                    }

                    line = enumerator.Current;
                }

                yield return new ListCommand() { Output = output };
            }
            else
                throw new UnreachableException();
        }
    }
}
