using AdventOfCode.Common;

namespace AdventOfCode._2024._2024.Day7;

[Puzzle(Day = 7, Title = "Bridge Repair", ExpectedSampleResultPart1 = "3749", ExpectedSampleResultPart2 = "?")]
// ReSharper disable once UnusedType.Global
internal sealed class Day7 : IPuzzleSolver
{
    private List<CalibrationEquation> _calibrationEquations = [];

    public void Init(IEnumerable<string> inputLines)
    {
        _calibrationEquations = [];

        foreach (var inputLine in inputLines)
        {
            _calibrationEquations.Add(new CalibrationEquation(inputLine));
        }
    }

    public string SolvePart1()
    {
        var totalCalibrationResult = _calibrationEquations
            .Where(equation => equation.CanBeSolved())
            .Select(equation => equation.TestValue)
            .Sum();

        return totalCalibrationResult.ToString();
    }

    public string SolvePart2()
    {
        return "UNSOLVED";
    }
}
