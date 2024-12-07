using AdventOfCode.Common;

namespace AdventOfCode._2023._2023.Day3;

// ReSharper disable UnusedType.Global

[Puzzle(Day = 3, Title = "Gear Ratios", ExpectedSampleResultPart1 = "4361", ExpectedSampleResultPart2 = "467835")]
internal sealed class Day3 : IPuzzleSolver
{
    private List<string> _inputLines = [];

    public void Init(IEnumerable<string> inputLines)
    {
        _inputLines = inputLines.ToList();
    }

    public string SolvePart1()
    {
        var (symbols, partNumbers) = Puzzle3Parser.GetSymbolsAndPartNumbers(_inputLines);

        var partNumbersNextToSymbols = Puzzle3NeighborFinder.GetPartNumbersNextToSymbols(partNumbers, symbols);

        return partNumbersNextToSymbols
            .Values
            .SelectMany(v => v)
            .Select(partNumber => partNumber.Value)
            .Sum()
            .ToString();
    }

    public string SolvePart2()
    {
        var (symbols, partNumbers) = Puzzle3Parser.GetSymbolsAndPartNumbers(_inputLines);

        var gearSymbols = symbols.Where(symbol => symbol.Value == '*');
        var partNumbersNextToSymbols = Puzzle3NeighborFinder.GetPartNumbersNextToSymbols(partNumbers, gearSymbols);

        var gearRatios = new List<int>();

        foreach (var partNumberNextToSymbol in partNumbersNextToSymbols)
        {
            if (partNumberNextToSymbol.Value.Count != 2)
            {
                continue;
            }

            var gearRatio = partNumberNextToSymbol.Value.Aggregate(1, (ratio, partNumber) => ratio * partNumber.Value);
            gearRatios.Add(gearRatio);
        }

        return gearRatios.Sum().ToString();
    }
}
