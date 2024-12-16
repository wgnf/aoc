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

        var numberOfOperationsToGenerate = numbers.Count - 1;
        // which is basically a number from 0...numberOfOperationsToGenerate - 1
        var bitMasks = CreateBitMasks(numberOfOperationsToGenerate);
        var row = 0;

        foreach (var bitMask in bitMasks)
        {
            var operations = ConvertBitMaskToOperations(bitMask, numberOfOperationsToGenerate);
            var calculation = new CalibrationCalculation(row, numbers, operations);
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

    private List<uint> CreateBitMasks(int numberOfBits)
    {
        var masks = new List<uint>();
        var totalMasks = (uint)(1 << numberOfBits); // this is 2^n

        // Generate all masks from 0 to 2^n - 1
        for (uint mask = 0; mask < totalMasks; mask++)
        {
            masks.Add(mask);
        }

        return masks;
    }

    private List<Operation> ConvertBitMaskToOperations(uint mask, int numberOfBits)
    {
        var operations = new List<Operation>();

        // starting from the most significant bit...
        for (var i = numberOfBits - 1; i >= 0; i--)
        {
            /*
             * checks whether the i-th bit in the mask is set (set to 1)
             *
             * 1. 1 << i -> bitwise left shift on the number 1 -> shifts the bit of 1 to the left by 'i' positions
             *    -> creating a binary number with only the i-th bit set to one
             *    i.e.  1 << 0 == 00000001 ; i << 2 == 00000100
             * 2. bitwise AND operation to isolate the i-th bit of the mask
             * 3. check if the result is non-zero -> which means that the i-th bit in the mask is set to 1
             */
            operations.Add((mask & (1 << i)) != 0
                ? Operation.Multiplication
                : Operation.Addition);
        }

        return operations;
    }
}
