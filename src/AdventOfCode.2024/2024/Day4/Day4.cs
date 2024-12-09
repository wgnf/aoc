using AdventOfCode.Common;
using AdventOfCode.Common.Utils;

namespace AdventOfCode._2024._2024.Day4;

[Puzzle(Day = 4, Title = "Ceres Search", ExpectedSampleResultPart1 = "18", ExpectedSampleResultPart2 = "9")]
// ReSharper disable once UnusedType.Global
internal sealed class Day4 : IPuzzleSolver
{
    private List<List<char>> _letterMatrix = [];

    public void Init(IEnumerable<string> inputLines)
    {
        _letterMatrix = [];
        foreach (var line in inputLines)
        {
            var letterMatrixLine = new List<char>();
            letterMatrixLine.AddRange(line.ToCharArray());

            _letterMatrix.Add(letterMatrixLine);
        }
    }

    public string SolvePart1()
    {
        var totalOccurrences = 0;
        for (var row = 0; row < _letterMatrix.Count; row++)
        {
            for (var col = 0; col < _letterMatrix.Count; col++)
            {
                var currentLetter = _letterMatrix[row][col];

                // X marks the start of our search, because its the start of "XMAS"
                if (currentLetter == 'X')
                {
                    var occurrences = SearchForOccurrencesStartingFrom(row, col);
                    totalOccurrences += occurrences;
                }
            }
        }

        return totalOccurrences.ToString();
    }

    public string SolvePart2()
    {
        var totalOccurrences = 0;
        for (var row = 0; row < _letterMatrix.Count; row++)
        {
            for (var column = 0; column < _letterMatrix.Count; column++)
            {
                var currentLetter = _letterMatrix[row][column];

                // A marks the start of our search, because it needs to be in the middle of each "MAS"
                if (currentLetter == 'A' && HasMasInXFormStartingFrom(row, column))
                {
                    totalOccurrences++;
                }
            }
        }

        return totalOccurrences.ToString();
    }

    private int SearchForOccurrencesStartingFrom(int row, int column)
    {
        var occurrences = 0;
        const string wordToCollect = "XMAS";

        foreach (var direction in Enum.GetValues<Direction>())
        {
            var collected = MatrixUtils.CollectInDirection(_letterMatrix, row, column, direction, wordToCollect.Length);
            var collectedWord = new string(collected.ToArray());

            if (collectedWord == wordToCollect)
            {
                occurrences++;
            }
        }

        return occurrences;
    }

    private bool HasMasInXFormStartingFrom(int row, int column)
    {
        const string wordToCollect = "MAS";

        var startLetterTopLeftRow = row - 1;
        var startLetterTopLeftColumn = column - 1;

        var startLetterTopRightRow = row - 1;
        var startLetterTopRightColumn = column + 1;

        var collectedLeft = MatrixUtils.CollectInDirection(_letterMatrix, startLetterTopLeftRow, startLetterTopLeftColumn, Direction.SouthEast, wordToCollect.Length);
        var collectedWordLeft = new string(collectedLeft.ToArray());
        var reversedCollectedWordLeft = new string(collectedWordLeft.Reverse().ToArray());

        var collectedRight = MatrixUtils.CollectInDirection(_letterMatrix, startLetterTopRightRow, startLetterTopRightColumn, Direction.SouthWest, wordToCollect.Length);
        var collectedWordRight = new string(collectedRight.ToArray());
        var reversedCollectedWordRight = new string(collectedWordRight.Reverse().ToArray());

        if ((collectedWordLeft == wordToCollect || reversedCollectedWordLeft == wordToCollect) &&
            (collectedWordRight == wordToCollect || reversedCollectedWordRight == wordToCollect))
        {
            return true;
        }

        return false;
    }
}
