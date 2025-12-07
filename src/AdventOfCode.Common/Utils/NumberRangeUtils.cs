using AdventOfCode.Common.Primitives;

namespace AdventOfCode.Common.Utils;

public static class NumberRangeUtils
{
    public static IEnumerable<NumberRange> CreateNonOverlapping(IEnumerable<NumberRange> ranges)
    {
        var rangesToCheck = ranges.ToList();

        var result = new List<NumberRange>(rangesToCheck);
        
        while (rangesToCheck.Count > 0)
        {
            var rangeToCheck = rangesToCheck[0];
            rangesToCheck.Remove(rangeToCheck);
            
            var overlapping = result
                .Where(r => r.IsInRange(rangeToCheck))
                // no need to concat, because it overlaps with itself
                .ToList();

            foreach (var overlappingToRemove in overlapping)
            {
                result.Remove(overlappingToRemove);
            }
            
            var start = overlapping.MinBy(r => r.Start)!.Start;
            var end = overlapping.MaxBy(r => r.End)!.End;
            result.Add(new NumberRange(start, end));
        }

        return result;
    }
}
