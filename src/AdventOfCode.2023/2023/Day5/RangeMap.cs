namespace AdventOfCode._2023._2023.Day5;

public sealed class RangeMap
{
    public RangeMap(Range sourceRange, Range destinationRange)
    {
        SourceRange = sourceRange;
        DestinationRange = destinationRange;
    }

    public Range SourceRange { get; }

    public Range DestinationRange { get; }
}
