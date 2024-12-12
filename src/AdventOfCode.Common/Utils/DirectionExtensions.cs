using AdventOfCode.Common.Primitives;

namespace AdventOfCode.Common.Utils;

public static class DirectionExtensions
{
    public static Direction Turn90DegreesRight(this Direction direction)
    {
        // TODO: support other directions aswell
        return direction switch
        {
            Direction.North => Direction.East,
            Direction.East => Direction.South,
            Direction.South => Direction.West,
            Direction.West => Direction.North,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, "Cases are not complete yet"),
        };
    }
}
