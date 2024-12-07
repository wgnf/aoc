namespace AdventOfCode.Common.Utils;

public static class StringExtensions
{
    public static string[] SplitTrimRemoveEmpty(this string input, char splitChar)
    {
        return input.Split(splitChar, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
    }
}
