namespace AdventOfCode._2023._2023.Day5;

public sealed class Range
{
    public static Range FromLength(long start, long length)
    {
        return new Range(start, length);
    }

    public static Range FromEnd(long start, long end)
    {
        var length = end - start + 1;
        return new Range(start, length);
    }

    private Range(long start, long length)
    {
        Start = start;
        Length = length;
    }

    public long Start { get; }

    public long Length { get; }

    public long End => Start + Length - 1;
}
