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
}
