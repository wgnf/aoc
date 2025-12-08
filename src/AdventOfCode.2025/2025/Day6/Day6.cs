using AdventOfCode.Common;
using AdventOfCode.Common.Primitives;
using AdventOfCode.Common.Utils;
using JetBrains.Annotations;

namespace AdventOfCode._2025._2025.Day6;

[UsedImplicitly]
[Puzzle(Day = 6, Title = "Trash Compactor", ExpectedSampleResultPart1 = "4277556", ExpectedSampleResultPart2 = "3263827")]
public sealed class Day6 : IPuzzleSolver
{
    private List<string> _inputLines = [];
    
    public void Init(IEnumerable<string> inputLines)
    {
        _inputLines = inputLines.ToList();
    }

    public string SolvePart1()
    {
        var columns = _inputLines[0].SplitTrimRemoveEmpty(" ").Length;
        var rows = _inputLines.Count;
        var mathProblems = new MathProblem[columns];
        for (var column = 0; column < mathProblems.Length; column++)
        {
            mathProblems[column] = new MathProblem();
        }

        for (var row = 0; row < rows; row++)
        {
            var split = _inputLines[row].SplitTrimRemoveEmpty(" ");
            for (var column = 0; column < columns; column++)
            {
                var mathProblem = mathProblems[column];
                var value = split[column];
                if (int.TryParse(value, out var parsedValue))
                {
                    mathProblem.Numbers.Add(parsedValue);
                }
                else
                {
                    mathProblem.Operation = MathOperation.FromString(value);
                }
            }
        }

        var result = SolveMathProblems(mathProblems);
        return result.ToString();
    }

    public string SolvePart2()
    {
        var matrix = Matrix<char>.FromInputLines(_inputLines, c => c);

        var mathProblems = new List<MathProblem>();
        var currentMathProblem = new MathProblem();
        
        for (var column = matrix.Columns - 1; column >= 0; column--)
        {
            var position = new Position(0, column);
            var allElementsInThatColumn = matrix.CollectInDirection(position, Direction.South).ToList();

            var valueString = allElementsInThatColumn
                .Select(c => c.Value)
                .Where(char.IsDigit)
                .CreateString();
            
            // when we have an empty column, we can conclude the current math problem
            if (valueString == string.Empty)
            {
                mathProblems.Add(currentMathProblem);
                currentMathProblem = new MathProblem();
                continue;
            }
            
            var number = int.Parse(valueString);
            currentMathProblem.Numbers.Add(number);
            
            var lastElement = allElementsInThatColumn.Last().Value;
            if (!char.IsDigit(lastElement) && lastElement != ' ')
            {
                currentMathProblem.Operation = MathOperation.FromString(lastElement.ToString());
            }
        }
        
        // when we're finished we need to add the remaining math problem
        mathProblems.Add(currentMathProblem);
        
        var result = SolveMathProblems(mathProblems);
        return result.ToString();
    }

    private static long SolveMathProblems(IEnumerable<MathProblem> problems)
    {
        var result = problems
            .Select(p => p.Solve())
            .Sum();
        return result;
    }
    
    private class MathProblem
    {
        public List<int> Numbers { get; } = [];
        public MathOperation Operation { get; set; } = MathOperation.FromString("+");

        public long Solve()
        {
            var result = Operation.Solve(Numbers);
            return result;
        }
    }
}
