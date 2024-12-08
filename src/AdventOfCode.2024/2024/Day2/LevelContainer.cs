using AdventOfCode.Common.Utils;

namespace AdventOfCode._2024._2024.Day2;

internal sealed class LevelContainer
{
    public IReadOnlyList<Level> Levels { get; private set; }

    public LevelContainer(List<int> rawLevels)
    {
        Levels = [];
        RecalculateLevels(rawLevels);
    }

    public LevelContainer(List<Level> levels) : this(levels.Select(l => l.Value).ToList())
    {
    }

    public bool IsSafe()
    {
        var diffs = GetRawDiffs().ToList();
        var increasingAndValid = diffs.All(diff => diff > 0 && (diff.Abs() <= 3 && diff.Abs() >= 1));
        var decreasingAndValid = diffs.All(diff => diff < 0 && (diff.Abs() <= 3 && diff.Abs() >= 1));

        return increasingAndValid || decreasingAndValid;
    }

    private IEnumerable<int> GetRawDiffs()
    {
        var rawDiffs = Levels
            .Select(level => level.DiffToNextLevel)
            .Where(diff => diff.HasValue)
            .Cast<int>();

        return rawDiffs;
    }

    private void RecalculateLevels(List<int> rawLevels)
    {
        var levels = new List<Level>();

        for (var index = 0; index < rawLevels.Count - 1; index++)
        {
            var current = rawLevels[index];
            var next = rawLevels[index + 1];

            var diffToNext = current - next;
            var level = new Level(current, diffToNext);
            levels.Add(level);
        }

        // add last level that has no next
        var lastRawLevel = rawLevels.Last();
        var lastLevel = new Level(lastRawLevel, null);

        levels.Add(lastLevel);
        Levels = levels;
    }
}
