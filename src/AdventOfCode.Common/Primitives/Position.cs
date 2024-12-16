namespace AdventOfCode.Common.Primitives;

public readonly record struct Position(int Row, int Column)
{
    public Position AdjustBasedOnDirection(Direction direction)
    {
        return direction switch
        {
            Direction.West => this with { Column = Column - 1 },
            Direction.NorthWest => new Position(Row: Row - 1, Column: Column - 1),
            Direction.North => this with { Row = Row - 1 },
            Direction.NorthEast => new Position(Row: Row - 1, Column: Column + 1),
            Direction.East => this with { Column = Column + 1 },
            Direction.SouthEast => new Position(Row: Row + 1, Column: Column + 1),
            Direction.South => this with { Row = Row + 1 },
            Direction.SouthWest => new Position(Row: Row + 1, Column: Column - 1),
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null),
        };
    }
}
