using AdventOfCode.Common;

namespace AdventOfCode._2024._2024.Day7;

[Puzzle(Day = 7, Title = "Bridge Repair", ExpectedSampleResultPart1 = "3749", ExpectedSampleResultPart2 = "11387")]
// ReSharper disable once UnusedType.Global
internal sealed class Day7 : IPuzzleSolver
{
    private List<string> _inputLines = [];

    public void Init(IEnumerable<string> inputLines)
    {
        _inputLines = inputLines.ToList();
    }

    public string SolvePart1()
    {
        var calibrationEquations = _inputLines
            .Select(inputLine => new CalibrationEquation(inputLine, [CalibrationOperation.Addition, CalibrationOperation.Multiplication]));

        var totalCalibrationResult = calibrationEquations
            .Where(equation => equation.CanBeSolved())
            .Select(equation => equation.TestValue)
            .Sum();

        return totalCalibrationResult.ToString();
    }

    public string SolvePart2()
    {
        var calibrationEquations = _inputLines
            .Select(inputLine => new CalibrationEquation(inputLine, [CalibrationOperation.Addition, CalibrationOperation.Multiplication, CalibrationOperation.Concatenation]));

        var totalCalibrationResult = calibrationEquations
            .Where(equation => equation.CanBeSolved())
            .Select(equation => equation.TestValue)
            .Sum();

        return totalCalibrationResult.ToString();
    }
}
