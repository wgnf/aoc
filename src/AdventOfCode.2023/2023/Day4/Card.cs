namespace AdventOfCode._2023._2023.Day4;

internal sealed class Card
{
    public int Number { get; set; }

    public List<int> WinningNumbers { get; private init; } = [];

    public List<int> ActualNumbers { get; private init; } = [];
}
