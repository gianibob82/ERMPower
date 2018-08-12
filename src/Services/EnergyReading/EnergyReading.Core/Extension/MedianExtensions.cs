using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class MedianExtensions
{
    public static decimal Median(this IEnumerable<decimal> source)
    {
        if (source == null)
            throw new ArgumentNullException("source");
        var data = source.OrderBy(n => n).ToArray();
        if (data.Length == 0)
            throw new InvalidOperationException();
        if (data.Length % 2 == 0)
            return (data[data.Length / 2 - 1] + data[data.Length / 2]) / 2.0m;
        return data[data.Length / 2];
    }

    public static decimal Median<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> selector)
    {
        return source.Select(selector).Median();
    }
}
