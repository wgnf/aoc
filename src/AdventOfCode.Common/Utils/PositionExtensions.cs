using AdventOfCode.Common.Primitives;

namespace AdventOfCode.Common.Utils;

public static class PositionExtensions
{
    public static Position AdjustBasedOnDirection(this Position position, Direction direction)
    {
        return direction switch
        {
            Direction.West => position with { Column = position.Column - 1 },
            Direction.NorthWest => new Position(Row: position.Row - 1, Column: position.Column - 1),
            Direction.North => position with { Row = position.Row - 1 },
            Direction.NorthEast => new Position(Row: position.Row - 1, Column: position.Column + 1),
            Direction.East => position with { Column = position.Column + 1 },
            Direction.SouthEast => new Position(Row: position.Row + 1, Column: position.Column + 1),
            Direction.South => position with { Row = position.Row + 1 },
            Direction.SouthWest => new Position(Row: position.Row + 1, Column: position.Column - 1),
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null),
        };
    }
}
