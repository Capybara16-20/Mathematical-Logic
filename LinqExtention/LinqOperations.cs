using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqExtention
{
    public static class LinqOperations
    {
        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> items, Func<T, TKey> property)
        {
            return items.GroupBy(property).Select(x => x.First());
        }
    }
}
