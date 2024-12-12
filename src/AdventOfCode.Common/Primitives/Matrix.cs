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

    public bool IsPositionValid(Position position)
    {
        var isRowValid = IsRowValid(position.Row);
        var isColumnValid = IsColumnValid(position.Column);
        var isValid = isRowValid && isColumnValid;
        return isValid;
    }

    public void Insert(MatrixElement<TElement> element)
    {
        if (!IsPositionValid(element.Position))
        {
            throw new ArgumentOutOfRangeException(nameof(element), element, $"Position of provided element is not valid: {element.Position}");
        }

        _matrix[element.Position.Row][element.Position.Column] = element.Value;
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
                    var position = new Position(row, column);
                    var element = new MatrixElement<TElement>(position, currentValue);
                    yield return element;
                }
            }
        }
    }

    public IEnumerable<MatrixElement<TElement>> CollectInDirection(
        Position startingPosition,
        Direction direction,
        Func<int, MatrixElement<TElement>, bool>? predicate = null)
    {
        predicate ??= (_, _) => true;

        var currentPosition = startingPosition;
        var collected = 0;
        var currentElement = new MatrixElement<TElement>();

        while (predicate.Invoke(collected, currentElement))
        {
            if (!IsPositionValid(currentPosition))
            {
                break;
            }

            collected++;
            var currentValue = _matrix[currentPosition.Row][currentPosition.Column];
            if (currentValue != null)
            {
                currentElement = new MatrixElement<TElement>(currentPosition, currentValue);
                yield return currentElement;
            }

            currentPosition = currentPosition.AdjustBasedOnDirection(direction);
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
