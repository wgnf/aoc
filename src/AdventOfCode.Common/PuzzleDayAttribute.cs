namespace AdventOfCode.Common;

[AttributeUsage(AttributeTargets.Class)]
public sealed class PuzzleDayAttribute : Attribute
{
    public int Day { get; set; }

    public string Title { get; set; } = string.Empty;

    public string ExpectedSampleResultPart1 { get; set; } = string.Empty;

    public string ExpectedSampleResultPart2 { get; set; } = string.Empty;
}
