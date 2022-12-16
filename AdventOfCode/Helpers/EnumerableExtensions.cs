using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Diagnostics.Runtime.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Helpers
{
    public static class EnumerableExtensions
    {
        public static T Multiply<T>(this IEnumerable<T> values) where T : INumber<T>
        {
            T result = values.First();

            foreach (var value in values.Skip(1))
            {
                result *= value;
            }

            return result;
        }
    }
}
