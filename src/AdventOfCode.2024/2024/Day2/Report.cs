namespace AdventOfCode._2024._2024.Day2;

internal class Report
{
    public LevelContainer LevelContainer { get; private set; }

    public Report(List<int> rawLevels)
    {
        LevelContainer = new LevelContainer(rawLevels);
    }

    public bool IsSafe()
    {
        var isSafe = LevelContainer.IsSafe();
        return isSafe;
    }

    public void TryToMakeSafe() // ... by removing one outlier
    {
        if (IsSafe())
        {
            return;
        }

        // check if removing a level makes the levels safe, if so, return and use new level-container
        foreach (var level in LevelContainer.Levels)
        {
            var levelCopy = LevelContainer.Levels.ToList();
            levelCopy.Remove(level);

            var newLevelContainer = new LevelContainer(levelCopy);
            if (newLevelContainer.IsSafe())
            {
                LevelContainer = newLevelContainer;
                return;
            }
        }
    }
}
