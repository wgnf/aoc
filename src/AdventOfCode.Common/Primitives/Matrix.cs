using AdventOfCode.Common.Utils;

namespace AdventOfCode.Common.Primitives;

public sealed class Matrix<TElement>
{
    private readonly TElement?[][] _matrix;
    private readonly int _rows;
    private readonly int _columns;

    public Matrix(int rows, int columns)
    {
        _rows = rows;
        _columns = columns;

        _matrix = new TElement?[rows][];

        for (var row = 0; row < rows; row++)
        {
            var columnArray = new TElement?[columns];

            for (var column = 0; column < columns; column++)
            {
                columnArray[column] = default;
            }

            _matrix[row] = columnArray;
        }
    }

    public void Insert(TElement element, int row, int column)
    {
        if (!IsRowValid(row))
        {
            throw new ArgumentOutOfRangeException(nameof(row), row, $"Row value should be between 0  and {_rows}");
        }

        if (!IsColumnValid(column))
        {
            throw new ArgumentOutOfRangeException(nameof(column), column, $"Column value should be between 0  and {_columns}");
        }

        _matrix[row][column] = element;
    }

    public void ForEach(Action<int, int, TElement> action)
    {
        for (var row = 0; row < _rows; row++)
        {
            for (var column = 0; column < _columns; column++)
            {
                var element = _matrix[row][column];
                if (element != null)
                {
                    action(row, column, element);
                }
            }
        }
    }

    public IEnumerable<TElement> CollectInDirection(
        int startingRow,
        int startingColumn,
        Direction direction,
        int stopAfterMax = int.MaxValue)
    {
        var currentRow = startingRow;
        var currentColumn = startingColumn;
        var collected = 0;

        while (collected < stopAfterMax)
        {
            if (!IsRowValid(currentRow) || !IsColumnValid(currentColumn))
            {
                break;
            }

            collected++;
            var currentValue = _matrix[currentRow][currentColumn];
            if (currentValue != null)
            {
                yield return currentValue;
            }

            (currentRow, currentColumn) = direction.Adjust2DIndexBasedOnDirection(currentRow, currentColumn);
        }
    }

    private bool IsRowValid(int row)
    {
        return row >= 0 && row < _rows;
    }

    private bool IsColumnValid(int column)
    {
        return column >= 0 && column < _columns;
    }
}
