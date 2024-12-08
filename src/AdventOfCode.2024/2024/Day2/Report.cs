using AdventOfCode.Common.Utils;

namespace AdventOfCode._2024._2024.Day2;

internal class Report
{
    public IReadOnlyList<Level> Levels { get; private set; }

    public int DiffsIncreasing => GetRawDiffs().Count(diff => diff > 0);
    public int DiffsDecreasing => GetRawDiffs().Count(diff => diff < 0);
    public int NoDiffs => GetRawDiffs().Count(diff => diff == 0);

    public Report(List<int> rawLevels)
    {
        Levels = [];
        RecalculateLevels(rawLevels);
    }

    public bool IsSafe()
    {
        var diffs = GetRawDiffs().ToList();
        var increasingAndValid = diffs.All(diff => diff > 0 && (diff.Abs() <= 3 && diff.Abs() >= 1));
        var decreasingAndValid = diffs.All(diff => diff < 0 && (diff.Abs() <= 3 && diff.Abs() >= 1));

        return increasingAndValid || decreasingAndValid;
    }

    public void TryToMakeSafe() // ... by removing one outlier
    {
        if (IsSafe())
        {
            return;
        }

        if (DiffsIncreasing > 1 && DiffsDecreasing > 1)
        {
            // too many outliers, cannot be made safe
        }

        // one level pair is the same
        else if (NoDiffs == 1)
        {
            var outlierLevel = Levels.Single(level => level.DiffToNextLevel == 0);
            RemoveLevelAndRecalculate(outlierLevel);
        }

        // one diff is increasing, rest is decreasing
        else if (DiffsIncreasing == 1 && DiffsDecreasing > 1)
        {
            var outlierLevel = Levels.Single(level => level.DiffToNextLevel > 0);
            RemoveLevelAndRecalculate(outlierLevel);
        }

        // one diff is decreasing, rest is increasing
        else if (DiffsDecreasing == 1 && DiffsIncreasing > 1)
        {
            var outlierLevel = Levels.Single(level => level.DiffToNextLevel < 0);
            RemoveLevelAndRecalculate(outlierLevel);
        }

        // check for outlier at the beginning
        else if (Levels[0].DiffToNextLevel!.Value.Abs() > 3)
        {
            var outlierLevel = Levels[0];
            RemoveLevelAndRecalculate(outlierLevel);
        }

        // check for outlier at the end (we need to check the second to last element though, because the last has no diff to next - because it has no next)
        else if (Levels[^2].DiffToNextLevel!.Value.Abs() > 3)
        {
            // but remove the last!!!
            var outlierLevel = Levels[^1];
            RemoveLevelAndRecalculate(outlierLevel);
        }
    }

    private void RemoveLevelAndRecalculate(Level levelToRemove)
    {
        var currentLevels = Levels.ToList();
        var indexOfLevelToRemove = currentLevels.IndexOf(levelToRemove);
        var indexBefore = indexOfLevelToRemove - 1;
        var indexAfter = indexOfLevelToRemove + 1;

        // when levelToRemove is not first or last element
        if (indexBefore >= 0 && indexAfter < currentLevels.Count)
        {
            var levelBefore = currentLevels[indexBefore];
            var levelAfter = currentLevels[indexAfter];

            if (levelBefore.Value == levelAfter.Value)
            {
                levelToRemove = currentLevels.Last();
            }
        }

        currentLevels.Remove(levelToRemove);
        Console.WriteLine($"Removed level {levelToRemove.Value}");

        RecalculateLevels(currentLevels.Select(l => l.Value).ToList());
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

    private IEnumerable<int> GetRawDiffs()
    {
        var rawDiffs = Levels
            .Select(level => level.DiffToNextLevel)
            .Where(diff => diff.HasValue)
            .Cast<int>();

        return rawDiffs;
    }
}
