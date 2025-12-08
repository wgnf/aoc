namespace AdventOfCode.Common.Utils;

public static class CharUtils
{
    public static string CreateString(this IEnumerable<char> chars)
    {
        var charArray = chars.ToArray();
        var result = new string(charArray);
        return result;
    }
}
