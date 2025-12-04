using AwesomeAssertions;
using Xunit;

namespace AdventOfCode._2024._2024.Day7;

public class Day7Tests
{
    [Theory]
    [InlineData("21037: 9 7 18 13", 14_742)]
    [InlineData("5341448448: 1 88 5 8 7 4 9 3 7 6 4 392", 175_250_718_720)]
    public void HasExpectedHighestResult(string input, long expectedHighestResult)
    {
        var calibrationEquation = new CalibrationEquation(input, [CalibrationOperation.Addition, CalibrationOperation.Multiplication]);

        var results = calibrationEquation.GetPossibleResults();

        results
            .Should()
            .Contain(expectedHighestResult);
    }
}
