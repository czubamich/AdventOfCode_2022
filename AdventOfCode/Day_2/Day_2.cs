using AdventOfCode.Helpers;

public class Day_2 : BaseDay, IDay
{
    struct Match
    {
        const char Kam = 'A';
        const char Pap = 'B';
        const char Noz = 'C';

        const char Prz = 'X';
        const char Rem = 'Y';
        const char Wyg = 'Z';

        char[] XX;

        public char A => XX[0];
        public char B => XX[1];

        public Match(char[] X) { XX = X; }

        public static Match FromOne(char[] X) => new Match(new[] { X[0], Translate(X[1]) });

        public static Match FromTwo(char[] X) => new Match(new[] { X[0], ResolveStrategy(X) });

        public int BaseScore => B switch
        {
            Kam => 1,
            Pap => 2,
            Noz => 3,
            _ => throw new ArgumentOutOfRangeException()
        };

        public int ResolveMatchScore()
        {
            if (A == B)
                return 3;

            if ((A == Pap && B == Noz)
                || (A == Kam && B == Pap)
                || (A == Noz && B == Kam))
                return 6;

            return 0;
        }

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
            'X' => 'A',
            'Y' => 'B',
            'Z' => 'C',
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public object PerformPartOne()
    {
        static IEnumerable<Match> GetMatches(string fileName)
            => File.ReadAllText(fileName)
                .Split("\r\n")
                .Select(f => Match.FromOne(f.Remove(1, 1).ToArray()));

        return GetMatches(InputPath)
            .Sum(m => m.BaseScore + m.ResolveMatchScore());
    }

    public object PerformPartTwo()
    {
        static IEnumerable<Match> GetMatches(string fileName)
            => File.ReadAllText(fileName)
                .Split("\r\n")
                .Select(f => Match.FromTwo(f.Remove(1, 1).ToArray()));

        return GetMatches(InputPath)
            .Sum(m => m.BaseScore + m.ResolveMatchScore());
    }
}