public class StepsProcessorBase
{
    public static int[] Parse(string stepData)
    {
        return stepData
            .Remove(0, 5)
            .Replace(" from ", ",")
            .Replace(" to ", ",")
            .Split(',')
            .Select(int.Parse)
            .ToArray();
    }
}