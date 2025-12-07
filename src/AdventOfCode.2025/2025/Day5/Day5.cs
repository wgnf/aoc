using AdventOfCode.Common;
using AdventOfCode.Common.Primitives;
using AdventOfCode.Common.Utils;
using JetBrains.Annotations;

namespace AdventOfCode._2025._2025.Day5;

[UsedImplicitly]
[Puzzle(Day = 5, Title = "Cafeteria", ExpectedSampleResultPart1 = "3", ExpectedSampleResultPart2 = "14")]
public sealed class Day5 : IPuzzleSolver
{
    private readonly List<NumberRange> _freshIds = [];
    private readonly List<long> _ingredients = [];
    
    public void Init(IEnumerable<string> inputLines)
    {
        _freshIds.Clear();
        _ingredients.Clear();

        var seenBlank = false;

        foreach (var inputLine in inputLines)
        {
            if (inputLine == string.Empty)
            {
                seenBlank = true;
                continue;
            }
            
            // ingredient ids
            if (seenBlank)
            {
                var id = long.Parse(inputLine);
                _ingredients.Add(id);
            }
            // ingredient ids for fresh ingredients
            else
            {
                var freshIdRange = NumberRange.FromRangeString(inputLine);
                _freshIds.Add(freshIdRange);
            }
        }
    }

    public string SolvePart1()
    {
        var numberOfFresh = _ingredients
            .Select(ingredient => _freshIds
                .Any(x => x.IsInRange(ingredient)))
            .Select(isFresh => isFresh ? 1 : 0)
            .Sum();

        return numberOfFresh.ToString();
    }

    public string SolvePart2()
    {
        var freshIngredientsCount = NumberRangeUtils
            .CreateNonOverlapping(_freshIds)
            .Select(x => x.AmountOfNumbersInRangeInclusive())
            .Sum();
        return freshIngredientsCount.ToString();
    }
}
