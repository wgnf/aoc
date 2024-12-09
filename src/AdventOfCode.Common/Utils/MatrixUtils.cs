namespace AdventOfCode.Common.Utils;

public static class MatrixUtils
{
    // TODO: Matrix class
    public static List<char> CollectInDirection(
        List<List<char>> matrix,
        int startingRow,
        int startingColumn,
        Direction collectDirection,
        int stopAfterMax = int.MaxValue)
    {
        Func<int, int, (int nextRow, int nextCol)> indexChanger = collectDirection switch
        {
            Direction.West => (row, col) => (row, col - 1),
            Direction.NorthWest => (row, col) => (row - 1, col - 1),
            Direction.North => (row, col) => (row - 1, col),
            Direction.NorthEast => (row, col) => (row - 1, col + 1),
            Direction.East => (row, col) => (row, col + 1),
            Direction.SouthEast => (row, col) => (row + 1, col + 1),
            Direction.South => (row, col) => (row + 1, col),
            Direction.SouthWest => (row, col) => (row + 1, col - 1),
            _ => throw new ArgumentOutOfRangeException(nameof(collectDirection), collectDirection, null),
        };

        var collection = new List<char>();
        var currentRow = startingRow;
        var currentColumn = startingColumn;

        while (collection.Count < stopAfterMax)
        {
            if (currentRow < 0 || currentRow >= matrix.Count)
            {
                break;
            }

            if (currentColumn < 0 || currentColumn >= matrix[currentRow].Count)
            {
                break;
            }

            var currentValue = matrix[currentRow][currentColumn];
            collection.Add(currentValue);

            (currentRow, currentColumn) = indexChanger(currentRow, currentColumn);
        }

        return collection;
    }
}
