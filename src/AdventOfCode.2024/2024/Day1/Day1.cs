using AdventOfCode.Common;
using AdventOfCode.Common.Utils;

namespace AdventOfCode._2024._2024.Day1;

// ReSharper disable UnusedType.Global

[Puzzle(Day = 1, Title = "Historian Hysteria", ExpectedSampleResultPart1 = "11", ExpectedSampleResultPart2 = "31")]
public sealed class Day1 : IPuzzleSolver
{
    private List<int> _leftElements = [];
    private List<int> _rightElements = [];

    public void Init(IEnumerable<string> inputLines)
    {
        _leftElements = [];
        _rightElements = [];

        foreach (var line in inputLines)
        {
            var subParts = line.SplitTrimRemoveEmpty("   ");

            _leftElements.Add(int.Parse(subParts[0]));
            _rightElements.Add(int.Parse(subParts[1]));
        }

        _leftElements.Sort();
        _rightElements.Sort();
    }

    public string SolvePart1()
    {
        var totalDistance = 0;

        for (var index = 0; index < _leftElements.Count; index++)
        {
            var distance = Math.Abs(_leftElements[index] - _rightElements[index]);

            totalDistance += distance;
        }

        return totalDistance.ToString();
    }

    public string SolvePart2()
    {
        var similarityScore = 0;

        foreach (var leftElement in _leftElements)
        {
            var occurences = _rightElements.Count(rightElement => rightElement == leftElement);
            var similarity = occurences * leftElement;

            similarityScore += similarity;
        }

        return similarityScore.ToString();
    }
}
