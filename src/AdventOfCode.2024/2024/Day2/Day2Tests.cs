using AdventOfCode.Common.Utils;
using FluentAssertions;
using Xunit;

namespace AdventOfCode._2024._2024.Day2;

public class Day2Tests
{
    [Theory]
    [InlineData("7 6 4 2 1")]
    [InlineData("1 3 2 4 5")]
    [InlineData("8 6 4 4 1")]
    [InlineData("1 3 6 7 9")]
    [InlineData("44 47 48 49 48")]
    [InlineData("10 13 16 20 17")]
    [InlineData("13 11 10 20 9")]
    [InlineData("64 66 68 69 71 72 72")]
    [InlineData("32 35 36 35 38 40 43 44")]
    public void CanBeMadeSafe(string input)
    {
        var levels = input
            .SplitTrimRemoveEmpty(" ")
            .Select(int.Parse)
            .ToList();

        var report = new Report(levels);

        report.TryToMakeSafe();

        report
            .IsSafe()
            .Should()
            .BeTrue();
    }
}
