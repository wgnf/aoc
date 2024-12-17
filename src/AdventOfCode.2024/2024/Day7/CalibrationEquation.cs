using AdventOfCode.Common.Utils;

namespace AdventOfCode._2024._2024.Day7;

internal sealed class CalibrationEquation
{
    private readonly List<CalibrationCalculation> _calculations = [];

    public CalibrationEquation(string inputLine, IEnumerable<CalibrationOperation> operations)
    {
        var testValueSplit = inputLine.SplitTrimRemoveEmpty(":");
        TestValue = long.Parse(testValueSplit[0]);

        var numbers = testValueSplit[1]
            .SplitTrimRemoveEmpty(" ")
            .Select(long.Parse)
            .ToList();

        var numberOfDigits = numbers.Count - 1;
        var operationMasks = MaskGenerator.GenerateMasks(numberOfDigits, operations);

        var row = 0;
        foreach (var operationMask in operationMasks)
        {
            var calculation = new CalibrationCalculation(row, numbers, operationMask);
            _calculations.Add(calculation);

            row++;
        }
    }

    public long TestValue { get; }

    public bool CanBeSolved()
    {
        var possibleResults = GetPossibleResults();
        var containsTestValue = possibleResults.Contains(TestValue);
        return containsTestValue;
    }

    public List<long> GetPossibleResults()
    {
        var possibleResults = _calculations
            .Select(calculation => calculation.Calculate())
            .ToList();

        return possibleResults;
    }
}
