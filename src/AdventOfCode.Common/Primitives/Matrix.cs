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

    public TElement[][] AsRaw()
    {
        return _matrix!;
    }

    public Matrix<TElement> Copy(Func<TElement?, TElement> copyElementValueFunc)
    {
        var copiedMatrix = new Matrix<TElement>(_rows, _columns);

        for (var row = 0; row < _rows; row++)
        {
            for (var column = 0; column < _columns; column++)
            {
                var position = new Position(row, column);
                var value = _matrix[row][column];
                var copiedValue = copyElementValueFunc(value);

                var matrixElement = new MatrixElement<TElement>(position, copiedValue);
                copiedMatrix.Insert(matrixElement);
            }
        }

        return copiedMatrix;
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

    public Position GetPositionOf(TElement element)
    {
        var foundMatrixElement = AsEnumerable().First(matrixElement => matrixElement.Value!.Equals(element));
        return foundMatrixElement.Position;
    }

    public TElement GetElementAt(Position position)
    {
        return _matrix[position.Row][position.Column]!;
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
        Func<int, MatrixElement<TElement>?, bool>? predicate = null)
    {
        predicate ??= (_, _) => true;

        var currentPosition = startingPosition;
        var collected = 0;
        MatrixElement<TElement>? currentElement = null;

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
                yield return currentElement.Value;
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
