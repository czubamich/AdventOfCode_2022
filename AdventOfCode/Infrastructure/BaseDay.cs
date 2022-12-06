using FastSerialization;

namespace AdventOfCode.Infrastructure;

public abstract class BaseDay
{
    private static IEnumerable<string>? _inputAsLines = null;
    private static string? _inputAsText = null;
    private static Type? _last = null;

    protected string InputPath => $"Data/{GetType().Name}.txt";
    protected IEnumerable<string> InputAsLines
    {
        get
        {
            if(GetType() != _last)
            {
                _last = GetType(); _inputAsText = null; _inputAsLines = null;
            }

            return _inputAsLines ??= File.ReadLines(InputPath).ToArray();
        }
    }
    
    protected string InputAsText 
    {
        get
        {
            if (GetType() != _last)
            {
                _last = GetType(); _inputAsText = null; _inputAsLines = null;
            }

            return _inputAsText ??= File.ReadAllText(InputPath);
        } 
    }
}