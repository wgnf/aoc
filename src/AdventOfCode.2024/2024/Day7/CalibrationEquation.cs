using AdventOfCode.Common.Utils;

namespace AdventOfCode._2024._2024.Day7;

internal sealed class CalibrationEquation
{
    private readonly List<CalibrationCalculation> _calculations = [];

    public CalibrationEquation(string inputLine)
    {
        var testValueSplit = inputLine.SplitTrimRemoveEmpty(":");
        TestValue = long.Parse(testValueSplit[0]);

        var numbers = testValueSplit[1]
            .SplitTrimRemoveEmpty(" ")
            .Select(long.Parse)
            .ToList();

        var amountOfPossibleResults = numbers.Count - 1 == 1
            ? 2
            : (int)Math.Pow(numbers.Count - 1, 2);
        var totalColumns = numbers.Count - 1;

        for (var row = 0; row < amountOfPossibleResults; row++)
        {
            var operations = new List<Operation>(totalColumns);
            for (var column = 0; column < totalColumns; column++)
            {
                var operation = GetOperation(row, column, totalColumns);
                operations.Add(operation);
            }

            _calculations.Add(new CalibrationCalculation(row, numbers, operations));
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

        // var amountOfPossibleResults = _numbers.Count - 1 == 1
        //     ? 2
        //     : (int)Math.Pow(_numbers.Count - 1, 2);
        // var possibleResults = new List<long>(amountOfPossibleResults);
        //
        // for (var iteration = 0; iteration < amountOfPossibleResults; iteration++)
        // {
        //     var localNumbers = _numbers.ToList();
        //     var possibleResult = Solve(iteration, localNumbers, localNumbers.Count - 1);
        //     possibleResults.Add(possibleResult);
        // }
        //
        // return possibleResults;
    }

    // private static long Solve(int globalIteration, List<long> numbersToCalculateOn, int totalColumns)
    // {
    //     var localIteration = 0;
    //     while (numbersToCalculateOn.Count > 1)
    //     {
    //         var operation = GetOperation(globalIteration, localIteration, totalColumns);
    //
    //         var localResult = operation switch
    //         {
    //             Operation.Addition => numbersToCalculateOn[0] + numbersToCalculateOn[1],
    //             Operation.Multiplication => numbersToCalculateOn[0] * numbersToCalculateOn[1],
    //             _ => throw new InvalidOperationException("Invalid operation"),
    //         };
    //
    //         // remove first two values, because we calculated them already
    //         numbersToCalculateOn.RemoveAt(0);
    //         numbersToCalculateOn.RemoveAt(0);
    //
    //         // add result as first value (as an input for the next iteration)
    //         numbersToCalculateOn.Insert(0, localResult);
    //
    //         localIteration++;
    //     }
    //
    //     return numbersToCalculateOn[0];
    // }

    private static Operation GetOperation(int row, int column, int totalColumns)
    {
        // Determine the character for the specified row and column (I will understand this one day...)
        return (row & (1 << (totalColumns - column - 1))) != 0
            ? Operation.Multiplication
            : Operation.Addition;
    }
}
