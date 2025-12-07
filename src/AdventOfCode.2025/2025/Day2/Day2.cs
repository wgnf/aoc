using System.Diagnostics;
using AdventOfCode.Common;
using AdventOfCode.Common.Utils;
using JetBrains.Annotations;

namespace AdventOfCode._2025._2025.Day2;

[UsedImplicitly]
[Puzzle(Day = 2, Title = "Gift Shop", ExpectedSampleResultPart1 = "1227775554", ExpectedSampleResultPart2 = "4174379265")]
public sealed class Day2 : IPuzzleSolver
{
    private readonly List<IEnumerable<long>> _idRanges = [];

    public void Init(IEnumerable<string> inputLines)
    {
        _idRanges.Clear();

        var idRanges = inputLines.First().SplitTrimRemoveEmpty(",");
        foreach (var idRange in idRanges)
        {
            var idRangeSplit = idRange.SplitTrimRemoveEmpty("-");
            var startId = long.Parse(idRangeSplit[0]);
            var endId = long.Parse(idRangeSplit[1]);

            var allIdsInRange = EnumerableUtils.LongRange(startId, endId - startId + 1);

            _idRanges.Add(allIdsInRange);
        }
    }

    public string SolvePart1()
    {
        long result = 0;

        foreach (var idRange in _idRanges)
        {
            foreach (var id in idRange)
            {
                var idText = id.ToString();
                if (idText.Length % 2 != 0)
                {
                    continue;
                }

                var firstHalf = idText[..(idText.Length / 2)];
                var secondHalf = idText[(idText.Length / 2)..];

                if (secondHalf.Contains(firstHalf))
                {
                    result += id;
                }
            }
        }

        return result.ToString();
    }

    public string SolvePart2()
    {
        long result = 0;

        foreach (var idRange in _idRanges)
        {
            foreach (var id in idRange)
            {
                var idText = id.ToString();
                var idGrouped = idText.GroupBy(c => c).ToArray();

                // when there is a single digit, not everything is repeated
                if (idGrouped.Any(g => g.Count() == 1))
                {
                    continue;
                }

                if (idGrouped.Length == 1) // all digits are repeated
                {
                    result += id;
                    Debug.WriteLine($"JA: {idText}");
                    continue;
                }

                for (var chunkSize = idText.Length - 1; chunkSize >= 2; chunkSize--)
                {
                    if (idText.Length % chunkSize != 0)
                    {
                        continue;
                    }

                    var idChunks = idText.SplitChunk(chunkSize);
                    var allTheSame = !idChunks.Distinct().Skip(1).Any();
                    if (allTheSame)
                    {
                        result += id;
                        Debug.WriteLine($"JA: {idText}");
                        break;
                    }
                }
            }
        }

        return result.ToString();
    }
}
