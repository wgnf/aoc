using AdventOfCode.Common;
using AdventOfCode.Common.Primitives;
using JetBrains.Annotations;

namespace AdventOfCode._2025._2025.Day7;

[UsedImplicitly]
[Puzzle(Day = 7, Title = "Laboratories", ExpectedSampleResultPart1 = "21", ExpectedSampleResultPart2 = "40")]
public sealed class Day7 : IPuzzleSolver
{
    // ReSharper disable once IdentifierTypo
    private TaychonManifold _taychonManifold = new([]);
    
    public void Init(IEnumerable<string> inputLines)
    {
        _taychonManifold = new TaychonManifold(inputLines);
    }

    public string SolvePart1()
    {
        _taychonManifold.Solve();

        return _taychonManifold.Splits.Count.ToString();
    }

    public string SolvePart2()
    {
        _taychonManifold.Solve();

        var allEndingBeams = _taychonManifold.Beams.Where(x => x.EndSplit == null);

        var realities = allEndingBeams.Sum(x => GetPossibilities(x, 0));
        return realities.ToString();
    }

    private long GetPossibilities(Beam beam, long localPossibilities)
    {
        var origins = beam.OriginSplits;
        if (origins.Any(x => x.IsStart))
        {
            return localPossibilities + 1;
        }

        foreach (var origin in origins)
        {
            var beamsOfOrigin = _taychonManifold.Beams.Where(x => x.EndSplit == origin);
            localPossibilities += beamsOfOrigin.Count();
            localPossibilities += GetPossibilities(beam, localPossibilities);
        }

        return 0;
    }

    private class TaychonManifold
    {
        private readonly Matrix<char> _taychonManifold;

        public TaychonManifold(IEnumerable<string> inputLines)
        {
            _taychonManifold = Matrix<char>.FromInputLines(inputLines, c => c);
        }

        public List<Beam> Beams { get; } = [];

        public List<Split> Splits { get; } = [];

        public void Solve()
        {
            var start = _taychonManifold.AsEnumerable().FirstOrDefault(x => x.Value == 'S');

            var thingsToProcess = new Queue<(Split Origin, List<Position> BeamStartPositions)>();
            var startSplit = new Split { IsStart = true, Position = start.Position, };
            thingsToProcess.Enqueue((startSplit, [start.Position]));
        
            while (thingsToProcess.Count > 0)
            {
                var thingToProcess = thingsToProcess.Dequeue();

                foreach (var beamStartPosition in thingToProcess.BeamStartPositions)
                {
                    var foundBeam = Beams.FirstOrDefault(x => x.StartPosition == beamStartPosition);
                    if (foundBeam == null)
                    {
                        foundBeam = new Beam { StartPosition = beamStartPosition };
                        foundBeam.OriginSplits.Add(thingToProcess.Origin);
                        Beams.Add(foundBeam);   
                    }
                    
                    var collected = _taychonManifold.CollectInDirection(foundBeam.StartPosition, Direction.South, (_, element) => element.Value != '^').ToList();
                    var lastElement = collected.Last();
                    if (lastElement.Value != '^')
                    {
                        continue;
                    }
            
                    var seenSplit = Splits.FirstOrDefault(x => x.Position == lastElement.Position);
                    if (seenSplit != null)
                    {
                        foundBeam.EndSplit = seenSplit;
                        continue;
                    }

                    var split = new Split { Position = lastElement.Position };
                    Splits.Add(split);
                    foundBeam.EndSplit = split;

                    var newPositionLeft = lastElement.Position.AdjustBasedOnDirection(Direction.West);
                    var newPositionRight = lastElement.Position.AdjustBasedOnDirection(Direction.East);
                    
                    thingsToProcess.Enqueue((split, [newPositionLeft, newPositionRight]));
                }
            }
        }
    }

    private class Beam
    {
        public Position StartPosition { get; init; }
        
        public HashSet<Split> OriginSplits { get; } = [];
        
        public Split? EndSplit { get; set; }
    }

    private class Split
    {
        public bool IsStart { get; set; }
        
        public Position Position { get; init; }
    }
}
