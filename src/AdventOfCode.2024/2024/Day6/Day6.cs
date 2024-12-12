using AdventOfCode.Common;

namespace AdventOfCode._2024._2024.Day6;

[Puzzle(Day = 6, Title = "Guard Gallivant", ExpectedSampleResultPart1 = "41", ExpectedSampleResultPart2 = "6")]
// ReSharper disable once UnusedType.Global
internal sealed class Day6 : IPuzzleSolver
{
    private LabFloorPlan _labFloorPlan = new([]);

    public void Init(IEnumerable<string> inputLines)
    {
        _labFloorPlan = new LabFloorPlan(inputLines);
    }

    public string SolvePart1()
    {
        _labFloorPlan.AdvanceUntilPlayerIsOffMap();

        var visitedTiles = _labFloorPlan.GetCountOfVisitedTiles();

        return visitedTiles.ToString();
    }

    public string SolvePart2()
    {
        return "UNSOLVED";
    }
}
