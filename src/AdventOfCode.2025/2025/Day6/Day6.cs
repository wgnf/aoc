using AdventOfCode.Common;
using AdventOfCode.Common.Primitives;
using AdventOfCode.Common.Utils;
using JetBrains.Annotations;

namespace AdventOfCode._2025._2025.Day6;

[UsedImplicitly]
[Puzzle(Day = 6, Title = "Trash Compactor", ExpectedSampleResultPart1 = "4277556", ExpectedSampleResultPart2 = "3263827")]
public sealed class Day6 : IPuzzleSolver
{
    private Matrix<string> _mathProblems = new(0, 0);
    
    public void Init(IEnumerable<string> inputLines)
    {
        var inputLinesList = inputLines.ToList();
        var columns = inputLinesList[0].SplitTrimRemoveEmpty(" ").Length;
        
        _mathProblems = new Matrix<string>(inputLinesList.Count, columns);
        for (var row = 0; row < inputLinesList.Count; row++)
        {
            var split = inputLinesList[row].SplitTrimRemoveEmpty(" ");
            for (var column = 0; column < split.Length; column++)
            {
                var position = new Position(row, column);
                var element = new MatrixElement<string>(position, split[column]);
                _mathProblems.Insert(element);
            }
        }
    }

    public string SolvePart1()
    {
        var results = new List<ulong>();
        for (var column = 0; column < _mathProblems.Columns; column++)
        {
            var position = new Position(0, column);
            var mathProblem = _mathProblems.CollectInDirection(position, Direction.South).ToList();

            var inputs = mathProblem.Take(mathProblem.Count - 1).Select(x => x.Value);
            var operation = mathProblem.Last().Value;

            ulong? result = null;
            foreach (var input in inputs)
            {
                var inputNumber = ulong.Parse(input);
                if (!result.HasValue)
                {
                    result = inputNumber;
                    continue;
                }
                
                switch (operation)
                {
                    case "*":
                        result *= inputNumber;
                        break;
                    case "+":
                        result += inputNumber;
                        break;
                    default:
                        throw new InvalidOperationException($"Unknown operation '{operation}'");
                }
            }

            if (!result.HasValue)
            {
                throw new InvalidOperationException("RESULT HAS NO VALUE?!");
            }
            
            results.Add(result.Value);
        }

        var resultsSum = results.SumX();
        return resultsSum.ToString();
    }

    public string SolvePart2()
    {
        return string.Empty;
    }
}
