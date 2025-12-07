using AdventOfCode.Common.Utils;

namespace AdventOfCode.Common.Primitives;

public sealed record NumberRange(long Start, long End)
{
    public static NumberRange FromRangeString(string rangeString, char separator = '-')
    {
        var split = rangeString.SplitTrimRemoveEmpty(separator);
        if (split.Length != 2)
        {
            throw new ArgumentException($"The range string has invalid format: '{rangeString}'");
        }

        var rangeStart = long.Parse(split[0]);
        var rangeEnd = long.Parse(split[1]);
        
        var numberRange = new NumberRange(rangeStart, rangeEnd);
        return numberRange;
    }

    public IEnumerable<long> GetNumbersInclusive()
    {
        return EnumerableUtils.LongRange(Start, End - Start + 1);
    }

    public long AmountOfNumbersInRangeInclusive()
    {
        return End - Start + 1;
    }

    public bool IsInRange(long number)
    {
        return Start <= number && End >= number;
    }

    public bool IsInRange(NumberRange numberRange)
    {
        return IsInRange(numberRange.Start) || IsInRange(numberRange.End);
    }
}
