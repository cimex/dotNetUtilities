using System;
using System.Collections.Generic;
using System.Linq;

namespace CimexUtility.Linq
{
    public static class LinqUtility
    {
        public static IEnumerable<T> OrderRandomly<T>(this IEnumerable<T> sequence)
        {
            var random = new Random();
            var copy = sequence.ToList();

            while (copy.Count > 0)
            {
                var index = random.Next(copy.Count);
                yield return copy[index];
                copy.RemoveAt(index);
            }
        }

        public static IEnumerable<IEnumerable<T>> Partition<T>(this IEnumerable<T> items, int partitionSize)
        {
            return items
                .Select((x, i) => new {Index = i, Value = x})
                .GroupBy(x => x.Index / partitionSize)
                .Select(x => x.Select(v => v.Value));
        }
    }
}
