using AdventOfCode.Helpers;

public class Day_2 : BaseDay, IDay
{
    struct Match
    {
        const char Kam = 'A';
        const char Pap = 'B';
        const char Noz = 'C';

        const char Prz = 'X';
        const char Wyg = 'Z';

        char[] XX;

        public char A => XX[0];
        public char B => XX[1];

        public Match(char[] X) { XX = X; }

        public static Match FromOne(char[] X) => new Match(new[] { X[0], Translate(X[1]) });

        public static Match FromTwo(char[] X) => new Match(new[] { X[0], ResolveStrategy(X) });

        public int TotalScore => BaseScore + ResolveMatchScore();

        public int BaseScore => B switch
        {
            Kam => 1,
            Pap => 2,
            Noz => 3,
            _ => throw new ArgumentOutOfRangeException()
        };

        public int ResolveMatchScore() => A == B ? 3 : XX switch 
        {
            [Pap, Noz] => 6,
            [Kam, Pap] => 6,
            [Noz, Kam] => 6,
            _ => 0,
        };

        private static char ResolveStrategy(char[] AM) => AM switch
        {
            [Pap, Prz] => Kam,
            [Kam, Prz] => Noz,
            [Noz, Prz] => Pap,
            [Pap, Wyg] => Noz,
            [Kam, Wyg] => Pap,
            [Noz, Wyg] => Kam,
            _ => AM[0]
        };

        private static char Translate(char X) => X switch
        {
            'X' => Kam,
            'Y' => Pap,
            'Z' => Noz,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public object PerformPartOne()
    {
        return GetData(InputPath)
            .Select(Match.FromOne)
            .Sum(m => m.TotalScore);
    }

    public object PerformPartTwo()
    {
        return GetData(InputPath)
            .Select(Match.FromTwo)
            .Sum(m => m.TotalScore);
    }

    IEnumerable<char[]> GetData(string fileName)
            => InputAsLines.Select(s => s.Remove(1,1).ToArray());
}