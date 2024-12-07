namespace AdventOfCode._2023._2023.Day5;

public sealed class RangeMapSet
{
    public List<RangeMap> Items { get; } = [];

    public IEnumerable<Range> GetRanges(IEnumerable<Range> ranges)
    {
        var unMigratedRanges = new List<Range>(ranges.OrderBy(r => r.Start));
        var migratedRanges = new List<Range>();

        foreach (var rangeMap in Items)
        {
            ProcessFor(rangeMap, unMigratedRanges, migratedRanges);
        }

        return unMigratedRanges.Concat(migratedRanges);
    }

    private static void ProcessFor(RangeMap rangeMap, List<Range> unMigratedRanges, List<Range> migratedRanges)
    {
        // make copy so that adding in the loop is possible
        foreach (var unMigratedRange in unMigratedRanges.ToList())
        {
            if (!TryMigrateRangeFor(
                    rangeMap,
                    unMigratedRange,
                    out var newUnMigratedRanges,
                    out var resultingMigratedRanges))
            {
                continue;
            }

            unMigratedRanges.Remove(unMigratedRange);

            unMigratedRanges.AddRange(newUnMigratedRanges);
            migratedRanges.AddRange(resultingMigratedRanges);
        }
    }

    private static bool TryMigrateRangeFor(
        RangeMap rangeMap,
        Range range,
        out List<Range> newUnMigratedRanges,
        out List<Range> migratedRanges)
    {
        newUnMigratedRanges = [];
        migratedRanges = [];

        var sourceRange = rangeMap.SourceRange;
        var destinationRange = rangeMap.DestinationRange;

        // when the given range does not fit anywhere into the source-range, we cannot migrate it
        // this checks if the given range is partially part of the source-range
        var isInRangeMap = (sourceRange.Start >= range.Start && sourceRange.Start <= range.End) ||
                           (sourceRange.End >= range.Start && sourceRange.End <= range.End) ||
                           (range.Start >= sourceRange.Start && range.Start <= sourceRange.End) ||
                           (range.End >= sourceRange.Start && range.End <= sourceRange.End);
        if (!isInRangeMap)
        {
            return false;
        }

        var givenRangeStartOffset = range.Start - sourceRange.Start;
        var givenRangeEndOffset = range.End - sourceRange.End;

        // lets check if the given range fits completely into the target range
        if (givenRangeStartOffset == 0 && givenRangeEndOffset == 0)
        {
            migratedRanges.Add(rangeMap.DestinationRange);
            return true;
        }

        AddMigratedRangeWhenNoCompleteFit(
            migratedRanges,
            destinationRange,
            givenRangeStartOffset,
            givenRangeEndOffset);

        ConsiderOverhangFront(newUnMigratedRanges, givenRangeStartOffset, range.Start);
        ConsiderOverhangBack(newUnMigratedRanges, givenRangeEndOffset, range.End);

        return true;
    }

    private static void AddMigratedRangeWhenNoCompleteFit(
        List<Range> migratedRanges,
        Range destinationRange,
        long givenRangeStartOffset,
        long givenRangeEndOffset)
    {
        long migratedStart;
        long migratedEnd;

        // when the given range starts in front of or at the source-range start ...
        if (givenRangeStartOffset <= 0)
        {
            // ... the migrated-range has to start at the exact start of the destination-range
            migratedStart = destinationRange.Start;
        }
        else
        {
            // ... else the migrated-range has to start at an offset (from the start) inside the destination-range
            migratedStart = destinationRange.Start + givenRangeStartOffset;
        }

        // when the given range starts behind of or at the source-range end ...
        if (givenRangeEndOffset >= 0)
        {
            // ... the migrated-range has to start at the exact end of the destination-range
            migratedEnd = destinationRange.End;
        }
        else
        {
            // ... else the migrated-range has to end at an offset (from the end) inside the destination-range
            migratedEnd = destinationRange.End + givenRangeEndOffset;
        }

        var migratedRange = Range.FromEnd(migratedStart, migratedEnd);
        migratedRanges.Add(migratedRange);
    }

    private static void ConsiderOverhangFront(
        List<Range> newUnMigratedRanges,
        long givenRangeStartOffset,
        long givenRangeStart)
    {
        // if the given-range starts in the source-range, there is no overhang
        if (givenRangeStartOffset >= 0)
        {
            return;
        }

        var overhangFrontStart = givenRangeStart;
        var overhangFrontEnd = givenRangeStart - givenRangeStartOffset - 1;
        var overhangFrontRange = Range.FromEnd(overhangFrontStart, overhangFrontEnd);

        newUnMigratedRanges.Add(overhangFrontRange);
    }

    private static void ConsiderOverhangBack(
        List<Range> newUnMigratedRanges,
        long givenRangeEndOffset,
        long sourceRangeEnd)
    {
        // if the given-range ends in the source-range, there is no overhang
        if (givenRangeEndOffset <= 0)
        {
            return;
        }

        var overhangBackStart = sourceRangeEnd + 1;
        var overhangBackEnd = sourceRangeEnd + givenRangeEndOffset;
        var overhangBackRange = Range.FromEnd(overhangBackStart, overhangBackEnd);

        newUnMigratedRanges.Add(overhangBackRange);
    }
}
