using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INT1448.Shared.Extensions
{
    public static class IEnumerableExtension
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T item in source)
                action(item);
        }

        public static IEnumerable<TOutput> ForEach<TInput, TOutput>(this IEnumerable<TInput> source, Func<TInput, TOutput> action)
        {
            foreach (TInput item in source)
                yield return action(item);
        }
    }
}
