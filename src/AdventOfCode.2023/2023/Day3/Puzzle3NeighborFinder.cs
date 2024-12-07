namespace AdventOfCode._2023._2023.Day3;

internal sealed class Puzzle3NeighborFinder
{
    public static Dictionary<Symbol, List<PartNumber>> GetPartNumbersNextToSymbols(
        IEnumerable<PartNumber> partNumbers,
        IEnumerable<Symbol> symbols)
    {
        var symbolToPartNumberMap = new Dictionary<Symbol, List<PartNumber>>();
        var symbolsList = symbols.ToList();

        foreach (var partNumber in partNumbers)
        {
            var symbolNextTo = symbolsList.Find(symbol => symbol.LineNumber >= partNumber.LineNumber - 1 && symbol.LineNumber <= partNumber.LineNumber + 1 &&
                                                          symbol.Index >= partNumber.StartIndex - 1 && symbol.Index <= partNumber.EndIndex + 1);

            if (symbolNextTo != null)
            {
                Console.WriteLine($"Part-number '{partNumber}' is next to '{symbolNextTo}'");

                if (symbolToPartNumberMap.TryGetValue(symbolNextTo, out var partNumbersInMap))
                {
                    partNumbersInMap.Add(partNumber);
                }
                else
                {
                    partNumbersInMap = new List<PartNumber> { partNumber };
                    symbolToPartNumberMap.Add(symbolNextTo, partNumbersInMap);
                }
            }
        }

        return symbolToPartNumberMap;
    }
}
