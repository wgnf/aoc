using AdventOfCode.Common;
using AdventOfCode.Common.Primitives;
using AdventOfCode.Common.Utils;

namespace AdventOfCode._2024._2024.Day4;

[Puzzle(Day = 4, Title = "Ceres Search", ExpectedSampleResultPart1 = "18", ExpectedSampleResultPart2 = "9")]
// ReSharper disable once UnusedType.Global
internal sealed class Day4 : IPuzzleSolver
{
    private Matrix<char> _letterMatrix = new Matrix<char>(1, 1);

    public void Init(IEnumerable<string> inputLines)
    {
        var inputLinesList = inputLines.ToList();

        var rows = inputLinesList.Count;
        var columns = inputLinesList.First().Length;

        _letterMatrix = new Matrix<char>(rows, columns);

        for (var row = 0; row < rows; row++)
        {
            var line = inputLinesList.ElementAt(row);

            for (var column = 0; column < columns; column++)
            {
                var letter = line[column];
                var letterElement = new MatrixElement<char>(row, column, letter);

                _letterMatrix.Insert(letterElement);
            }
        }
    }

    public string SolvePart1()
    {
        var totalOccurrences = 0;
        _letterMatrix.AsEnumerable().ForEach(element =>
        {
            // X marks the start of our search, because it's the start of "XMAS"
            if (element.Value == 'X')
            {
                var occurrences = SearchForOccurrencesStartingFrom(element.Row, element.Column);
                totalOccurrences += occurrences;
            }
        });

        return totalOccurrences.ToString();
    }

    public string SolvePart2()
    {
        var totalOccurrences = 0;
        _letterMatrix.AsEnumerable().ForEach(element =>
        {
            // A marks the start of our search, because it needs to be in the middle of each "MAS"
            if (element.Value == 'A' && HasMasInXFormStartingFrom(element.Row, element.Column))
            {
                totalOccurrences++;
            }
        });

        return totalOccurrences.ToString();
    }

    private int SearchForOccurrencesStartingFrom(int row, int column)
    {
        var occurrences = 0;
        const string wordToCollect = "XMAS";

        foreach (var direction in Enum.GetValues<Direction>())
        {
            var collected = _letterMatrix.CollectInDirection(row, column, direction, wordToCollect.Length);
            var collectedChars = collected.Select(element => element.Value).ToArray();
            var collectedWord = new string(collectedChars);

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

        var collectedLeft = _letterMatrix.CollectInDirection(startLetterTopLeftRow, startLetterTopLeftColumn, Direction.SouthEast, wordToCollect.Length);
        var collectedCharsLeft = collectedLeft.Select(element => element.Value).ToArray();
        var collectedWordLeft = new string(collectedCharsLeft);
        var reversedCollectedWordLeft = new string(collectedWordLeft.Reverse().ToArray());

        var collectedRight = _letterMatrix.CollectInDirection(startLetterTopRightRow, startLetterTopRightColumn, Direction.SouthWest, wordToCollect.Length);
        var collectedCharsRight = collectedRight.Select(element => element.Value).ToArray();
        var collectedWordRight = new string(collectedCharsRight);
        var reversedCollectedWordRight = new string(collectedWordRight.Reverse().ToArray());

        if ((collectedWordLeft == wordToCollect || reversedCollectedWordLeft == wordToCollect) &&
            (collectedWordRight == wordToCollect || reversedCollectedWordRight == wordToCollect))
        {
            return true;
        }

        return false;
    }
}
