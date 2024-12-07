using System.Text;

namespace AdventOfCode._2023._2023.Day3;

internal sealed class Puzzle3Parser
{
    public static (IEnumerable<Symbol>, IEnumerable<PartNumber>) GetSymbolsAndPartNumbers(IEnumerable<string> fileContents)
    {
        var partNumbers = new List<PartNumber>();
        var symbols = new List<Symbol>();

        var lineNumber = 0;
        foreach (var line in fileContents)
        {
            ParseLine(line, lineNumber, symbols, partNumbers);
            lineNumber++;
        }

        return (symbols, partNumbers);
    }

    private static void ParseLine(string line, int lineNumber, List<Symbol> symbols, List<PartNumber> partNumbers)
    {
        var partNumberBuilder = new StringBuilder();
        var partNumberStartIndex = -1;
        var partNumberEndIndex = -1;

        for (var index = 0; index < line.Length; index++)
        {
            var character = line[index];

            if (char.IsDigit(character))
            {
                partNumberBuilder.Append(character);

                if (partNumberStartIndex == -1)
                {
                    partNumberStartIndex = index;
                }

                partNumberEndIndex = index;
            }
            else
            {
                if (character != '.')
                {
                    var symbol = new Symbol(character, lineNumber, index);
                    Console.WriteLine($"Found symbol '{symbol}'");
                    symbols.Add(symbol);
                }

                FinishUpPartNumber();
            }
        }

        // Important to finish up a part-number when the line ends as well!
        FinishUpPartNumber();


        void FinishUpPartNumber()
        {
            var partNumberStr = partNumberBuilder.ToString();
            if (partNumberStr == string.Empty)
            {
                return;
            }

            if (!int.TryParse(partNumberStr, out var partNumberValue))
            {
                throw new InvalidOperationException($"Unable to parse part-number '{partNumberStr}' on line {lineNumber}");
            }

            var partNumber = new PartNumber(partNumberValue, lineNumber, partNumberStartIndex, partNumberEndIndex);
            Console.WriteLine($"Found part-number '{partNumber}'");
            partNumbers.Add(partNumber);

            // clean up
            partNumberBuilder.Clear();
            partNumberStartIndex = -1;
            partNumberEndIndex = -1;
        }
    }
}
