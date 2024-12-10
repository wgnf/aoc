using AdventOfCode.Common.Primitives;

namespace AdventOfCode.Common.Utils;

public static class DirectionExtensions
{
    public static (int row, int column) Adjust2DIndexBasedOnDirection(
        this Direction direction,
        int row,
        int column)
    {
        return direction switch
        {
            Direction.West => (row, column - 1),
            Direction.NorthWest => (row - 1, column - 1),
            Direction.North => (row - 1, column),
            Direction.NorthEast => (row - 1, column + 1),
            Direction.East => (row, column + 1),
            Direction.SouthEast => (row + 1, column + 1),
            Direction.South => (row + 1, column),
            Direction.SouthWest => (row + 1, column - 1),
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null),
        };
    }
}
