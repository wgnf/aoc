namespace AdventOfCode.Common.Primitives;

public readonly record struct MatrixElement<T>(Position Position, T Value)
{
    public static MatrixElement<T> Empty = new();
}
