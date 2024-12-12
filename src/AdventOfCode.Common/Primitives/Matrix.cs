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

    public bool IsValid(MatrixElement<TElement> element)
    {
        var isRowValid = IsRowValid(element.Row);
        var isColumnValid = IsColumnValid(element.Column);
        var isValid = isRowValid && isColumnValid;
        return isValid;
    }

    public void Insert(MatrixElement<TElement> element)
    {
        if (!IsValid(element))
        {
            throw new ArgumentOutOfRangeException(nameof(element), element, $"Provided value is not valid: {element}");
        }

        _matrix[element.Row][element.Column] = element.Value;
    }

    public IEnumerable<MatrixElement<TElement>> AsEnumerable()
    {
        for (var row = 0; row < _rows; row++)
        {
            for (var column = 0; column < _columns; column++)
            {
                var currentValue = _matrix[row][column];
                if (currentValue != null)
                {
                    var element = new MatrixElement<TElement>(row, column, currentValue);
                    yield return element;
                }
            }
        }
    }

    public IEnumerable<MatrixElement<TElement>> CollectInDirection(
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
                var element = new MatrixElement<TElement>(currentRow, currentColumn, currentValue);
                yield return element;
            }

            (currentRow, currentColumn) = direction.Adjust2DIndexBasedOnDirection(currentRow, currentColumn);
        }
    }

    public IEnumerable<MatrixElement<TElement>> CollectInDirectionUntilValueCollected(
        int startingRow,
        int startingColumn,
        Direction direction,
        TElement valueToCollect)
    {
        var currentRow = startingRow;
        var currentColumn = startingColumn;

        while (true)
        {
            if (!IsRowValid(currentRow) || !IsColumnValid(currentColumn))
            {
                break;
            }

            var currentValue = _matrix[currentRow][currentColumn];
            if (currentValue != null)
            {
                var element = new MatrixElement<TElement>(currentRow, currentColumn, currentValue);
                yield return element;

                if (currentValue.Equals(valueToCollect))
                {
                    break;
                }
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
