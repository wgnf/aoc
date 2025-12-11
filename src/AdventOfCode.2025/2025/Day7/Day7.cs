using AdventOfCode.Common;
using AdventOfCode.Common.Primitives;
using JetBrains.Annotations;

namespace AdventOfCode._2025._2025.Day7;

[UsedImplicitly]
[Puzzle(Day = 7, Title = "Laboratories", ExpectedSampleResultPart1 = "21", ExpectedSampleResultPart2 = "40")]
public sealed class Day7 : IPuzzleSolver
{
    // ReSharper disable once IdentifierTypo
    private Matrix<char> _taychonManifold = new(0, 0);
    
    public void Init(IEnumerable<string> inputLines)
    {
        _taychonManifold = Matrix<char>.FromInputLines(inputLines, c => c);
    }

    public string SolvePart1()
    {
        var splits = 0;
        var start = _taychonManifold.AsEnumerable().FirstOrDefault(x => x.Value == 'S');
        var seenSplitPositions = new HashSet<Position>();
        var beamStartPositions = new Queue<Position>();
        beamStartPositions.Enqueue(start.Position);

        while (beamStartPositions.Count > 0)
        {
            var beamStartPosition = beamStartPositions.Dequeue();
            
            var collected = _taychonManifold.CollectInDirection(beamStartPosition, Direction.South, (_, element) => element.Value != '^').ToList();
            var lastElement = collected.Last();
            if (lastElement.Value != '^')
            {
                continue;
            }

            if (!seenSplitPositions.Add(lastElement.Position))
            {
                continue;
            }

            splits++;
                
            var newPositionLeft = lastElement.Position.AdjustBasedOnDirection(Direction.West);
            var newPositionRight = lastElement.Position.AdjustBasedOnDirection(Direction.East);
                
            beamStartPositions.Enqueue(newPositionLeft);
            beamStartPositions.Enqueue(newPositionRight);
        }

        return splits.ToString();
    }

    public string SolvePart2()
    {
        var splits = 0;
        var start = _taychonManifold.AsEnumerable().FirstOrDefault(x => x.Value == 'S');
        var seenSplitPositions = new HashSet<Position>();
        var beamStartPositions = new Queue<Position>();
        beamStartPositions.Enqueue(start.Position);

        while (beamStartPositions.Count > 0)
        {
            var beamStartPosition = beamStartPositions.Dequeue();
            
            var collected = _taychonManifold.CollectInDirection(beamStartPosition, Direction.South, (_, element) => element.Value != '^').ToList();
            var lastElement = collected.Last();
            if (lastElement.Value != '^')
            {
                continue;
            }

            if (!seenSplitPositions.Add(lastElement.Position))
            {
                continue;
            }

            splits++;
                
            var newPositionLeft = lastElement.Position.AdjustBasedOnDirection(Direction.West);
            var newPositionRight = lastElement.Position.AdjustBasedOnDirection(Direction.East);
                
            beamStartPositions.Enqueue(newPositionLeft);
            beamStartPositions.Enqueue(newPositionRight);
        }

        var timelines = (splits * 2) - 2;

        return timelines.ToString();
    }
}
