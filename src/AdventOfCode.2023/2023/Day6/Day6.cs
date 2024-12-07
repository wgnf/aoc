using AdventOfCode.Common;
using AdventOfCode.Common.Utils;

namespace AdventOfCode._2023._2023.Day6;

[Puzzle(Day = 6, Title = "Wait For It", ExpectedSampleResultPart1 = "288", ExpectedSampleResultPart2 = "71503")]
// ReSharper disable once UnusedType.Global
internal sealed class Day6 : IPuzzleSolver
{
    private List<string> _inputLines = [];

    public void Init(IEnumerable<string> inputLines)
    {
        _inputLines = inputLines.ToList();
    }

    public string SolvePart1()
    {
        var raceConfig = ParseInput(_inputLines, false);

        var numberOfWaysToWinPerRace = new List<int>();
        foreach (var race in raceConfig.Races)
        {
            var numberOfWaysToWin = race.GetNumberOfWaysToWin();
            numberOfWaysToWinPerRace.Add(numberOfWaysToWin);
        }

        var marginOfError = numberOfWaysToWinPerRace.Aggregate(1, (current, numberOfWays) => current * numberOfWays);
        return marginOfError.ToString();
    }

    public string SolvePart2()
    {
        var raceConfig = ParseInput(_inputLines, true);

        var waysToWin = raceConfig.Races[0].GetNumberOfWaysToWin();

        return waysToWin.ToString();
    }

    private static RaceConfig ParseInput(IEnumerable<string> fileContents, bool removeSpaces)
    {
        var lines = fileContents.ToList();
        var raceConfig = new RaceConfig();

        for (var lineNumber = 0; lineNumber < lines.Count; lineNumber++)
        {
            var line = lines[lineNumber];

            if (removeSpaces)
            {
                line = line.Replace(" ", "");
            }

            var lineSplit = line.SplitTrimRemoveEmpty(':');
            var texts = lineSplit[1].SplitTrimRemoveEmpty(' ');
            var values = texts.Select(long.Parse).ToList();

            switch (lineNumber)
            {
                // first line are times
                case 0:
                {
                    values.ForEach(time => raceConfig.Races.Add(new Race { MaxTime = time }));
                    break;
                }
                // second line are record distances
                case 1:
                {
                    for (var index = 0; index < values.Count; index++)
                    {
                        raceConfig.Races[index].RecordDistance = values[index];
                    }
                    break;
                }
                default:
                {
                    throw new InvalidOperationException($"Did not expect to receive line number {lineNumber}");
                }
            }
        }

        return raceConfig;
    }
}
