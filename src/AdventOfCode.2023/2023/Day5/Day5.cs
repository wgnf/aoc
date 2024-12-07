using AdventOfCode.Common;
using AdventOfCode.Common.Utils;

namespace AdventOfCode._2023._2023.Day5;

[Puzzle(Day = 5, Title = "If You Give A Seed A Fertilizer", ExpectedSampleResultPart1 = "35", ExpectedSampleResultPart2 = "46")]
// ReSharper disable once UnusedType.Global
internal sealed class Day5 : IPuzzleSolver
{
    private List<string> _inputLines = [];

    public void Init(IEnumerable<string> inputLines)
    {
        _inputLines = inputLines.ToList();
    }

    public string SolvePart1()
    {
        var almanac = ParseAlmanac(_inputLines, false);
        var closestSeedLocation = almanac.GetClosestSeedLocation();

        return closestSeedLocation.ToString();
    }

    public string SolvePart2()
    {
        var almanac = ParseAlmanac(_inputLines, true);
        var closestSeedLocation = almanac.GetClosestSeedLocation();

        return closestSeedLocation.ToString();
    }

    private static Almanac ParseAlmanac(IEnumerable<string> fileContents, bool considerSeedsAsRanged)
    {
        var firstLine = true;
        var almanac = new Almanac();
        List<RangeMap>? currentDestinationList = null;

        foreach (var line in fileContents)
        {
            if (line == string.Empty)
            {
                continue;
            }

            if (firstLine)
            {
                ParsSeeds(line, almanac, considerSeedsAsRanged);
                firstLine = false;
                continue;
            }

            if (line.Contains('-') && line.EndsWith(':'))
            {
                currentDestinationList = DetermineDestinationList(line, almanac);
                continue;
            }

            ParseMaps(line, currentDestinationList!);
        }

        return almanac;
    }

    private static void ParsSeeds(string line, Almanac almanac, bool considerSeedsAsRanges)
    {
        var lineSplit = line.SplitTrimRemoveEmpty(':');
        if (lineSplit.Length != 2)
        {
            throw new InvalidOperationException($"First '{line}' line has not expected format");
        }

        var seedTexts = lineSplit[1].SplitTrimRemoveEmpty(' ');

        if (!considerSeedsAsRanges)
        {
            foreach (var seedText in seedTexts)
            {
                var start = long.Parse(seedText);
                var seedRange = Range.FromLength(start, 1);
                almanac.SeedRanges.Add(seedRange);
            }
        }
        else
        {
            for (var index = 0; index < seedTexts.Length; index += 2)
            {
                var rangeStart = long.Parse(seedTexts[index]);
                var rangeLength = long.Parse(seedTexts[index + 1]);

                var seedRange = Range.FromLength(rangeStart, rangeLength);
                almanac.SeedRanges.Add(seedRange);
            }
        }
    }

    private static List<RangeMap> DetermineDestinationList(string line, Almanac almanac)
    {
        return line switch
        {
            "seed-to-soil map:" => almanac.SeedToSoilMap.Items,
            "soil-to-fertilizer map:" => almanac.SoilToFertilizerMap.Items,
            "fertilizer-to-water map:" => almanac.FertilizerToWaterMap.Items,
            "water-to-light map:" => almanac.WaterToLightMap.Items,
            "light-to-temperature map:" => almanac.LightToTemperatureMap.Items,
            "temperature-to-humidity map:" => almanac.TemperatureToHumidityMap.Items,
            "humidity-to-location map:" => almanac.HumidityToLocationMap.Items,
            _ => throw new ArgumentOutOfRangeException(nameof(line), line, "Oops unrecognized category"),
        };
    }

    // ReSharper disable once SuggestBaseTypeForParameter
    private static void ParseMaps(string line, List<RangeMap> currentDestinationList)
    {

        var lineSplit = line.SplitTrimRemoveEmpty(' ');
        if (lineSplit.Length != 3)
        {
            throw new InvalidOperationException($"Line '{line}' is not of expected format");
        }

        var destinationStart = long.Parse(lineSplit[0]);
        var sourceStart = long.Parse(lineSplit[1]);
        var rangeLength = long.Parse(lineSplit[2]);

        var destinationRange = Range.FromLength(destinationStart, rangeLength);
        var sourceRange = Range.FromLength(sourceStart, rangeLength);

        currentDestinationList.Add(new RangeMap(sourceRange, destinationRange));
    }
}
