using System.Linq;

namespace AdventOfCode.Helpers
{
    public static class StackExtensions
    {
        public static List<T> PopMany<T>(this Stack<T> stack, int count)
        {
            var list = new List<T>();
            while(count-- > 0)
            {
                list.Add(stack.Pop());
            }
            return list;
        }

        public static void PushMany<T>(this Stack<T> stack, List<T> items)
        {
            for (int i = 0; i < items.Count; i++)
                stack.Push(items[i]);
        }

        public static void PushManyInReverse<T>(this Stack<T> stack, List<T> items)
        {
            for (int i = items.Count-1; i >= 0; i--)
                stack.Push(items[i]);
        }

        public static void PopTo<T>(this Stack<T> stack, Stack<T> other)
        {
            other.Push(stack.Pop());
        }
    }
}
