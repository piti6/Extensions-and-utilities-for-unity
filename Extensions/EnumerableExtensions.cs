using System;
using System.Collections.Generic;
using System.Linq;

#if UNIRX
using UniRx;
#endif

public static class EnumerableExtensions
{
    public static IEnumerable<T> Yield<T>(this T source)
    {
        yield return source;
    }

    public static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
    {
        foreach (var element in source)
        {
            action.Invoke(element);
        }
    }

    public static IEnumerable<(T Value, int Index)> WithIndex<T>(this IEnumerable<T> source)
    {
        return source.Select((x, i) => (x, i));
    }

    public static IEnumerable<TSource> DistinctBy<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
    {
        return source.GroupBy(selector).Select(x => x.First());
    }

    public static TSource MinBy<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
    {
        return source.OrderBy(selector).First();
    }

    public static TSource MaxBy<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
    {
        return source.OrderByDescending(selector).First();
    }

#if UNIRX
    public static IObservable<TResult> SelectMany<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, IObservable<TResult>> resultSelector)
    {
        return source.Select(resultSelector).Merge();
    }
#endif
}