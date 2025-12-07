namespace AdventOfCode.Common.Utils;

public static class EnumerableUtils
{
    public static IEnumerable<long> LongRange(long start, long length)
    {
        for (var value = start; value < start + length; value++)
        {
            yield return value;
        }
    }

    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        foreach (var item in source)
        {
            action(item);
        }
    }

    public static uint SumX(this IEnumerable<uint> source)
    {
        return source.Aggregate<uint, uint>(0, (current, sum) => current + sum);
    }
    
    public static ulong SumX(this IEnumerable<ulong> source)
    {
        return source.Aggregate<ulong, ulong>(0, (current, sum) => current + sum);
    }
}
