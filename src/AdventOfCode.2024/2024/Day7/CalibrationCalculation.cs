using System.Text;

namespace AdventOfCode._2024._2024.Day7;

internal sealed class CalibrationCalculation
{
    private readonly int _row;
    private readonly List<long> _numbers;
    private readonly List<CalibrationOperation> _operations;

    public CalibrationCalculation(int row, List<long> numbers, List<CalibrationOperation> operations)
    {
        _row = row;
        _numbers = numbers;
        _operations = operations;
    }

    public long Calculate()
    {
        var localNumbers = _numbers.ToList();
        var localOperations = _operations.ToList();

        while (localNumbers.Count > 1)
        {
            var operation = localOperations[0];

            var localResult = operation switch
            {
                CalibrationOperation.Addition => localNumbers[0] + localNumbers[1],
                CalibrationOperation.Multiplication => localNumbers[0] * localNumbers[1],
                _ => throw new InvalidOperationException("Invalid operation"),
            };

            // remove first two values, because we calculated them already
            localNumbers.RemoveAt(0);
            localNumbers.RemoveAt(0);

            // add result as first value (as an input for the next iteration)
            localNumbers.Insert(0, localResult);

            // remove taken operation
            localOperations.RemoveAt(0);
        }

        return localNumbers[0];
    }

    public override string ToString()
    {
        var stringBuilder = new StringBuilder();

        stringBuilder.Append($"[{_row}] ");

        var localNumbers = _numbers.ToList();
        var localOperations = _operations.ToList();
        var result = Calculate();

        while (localNumbers.Count > 1)
        {
            var operation = localOperations[0];
            var operationChar = operation switch
            {
                CalibrationOperation.Addition => "+",
                CalibrationOperation.Multiplication => "*",
                _ => throw new InvalidOperationException("Invalid operation"),
            };

            stringBuilder.Append($"{localNumbers[0]}{operationChar}{localNumbers[1]}");

            localNumbers.RemoveAt(0);
            localNumbers.RemoveAt(0);
            localOperations.RemoveAt(0);
        }

        stringBuilder.Append($" = {result}");

        return stringBuilder.ToString();
    }
}
