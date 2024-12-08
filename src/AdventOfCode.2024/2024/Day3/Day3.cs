using System.Text.RegularExpressions;
using AdventOfCode.Common;
using AdventOfCode.Common.Utils;

namespace AdventOfCode._2024._2024.Day3;

[Puzzle(Day = 3, Title = "Mull It Over", ExpectedSampleResultPart1 = "161", ExpectedSampleResultPart2 = "48")]
// ReSharper disable once UnusedType.Global
internal sealed partial class Day3 : IPuzzleSolver
{
    private List<Instruction> _instructions = [];

    public void Init(IEnumerable<string> inputLines)
    {
        var linesCombined = string.Join("", inputLines);
        var matches = InstructionsRegex().Matches(linesCombined);

        var instructions = new List<Instruction>();
        foreach (var match in matches.OfType<Match>())
        {
            var matchedValue = match.Value;

            if (matchedValue.StartsWith("mul"))
            {
                var strippedMatch = matchedValue.Replace("mul(", "").Replace(")", "");
                var instructionValues = strippedMatch
                    .SplitTrimRemoveEmpty(",")
                    .Select(int.Parse)
                    .ToList();

                var instruction = new MulInstruction(instructionValues[0], instructionValues[1]);
                instructions.Add(instruction);
            }
            else if (matchedValue.StartsWith("do("))
            {
                var instruction = new DoInstruction();
                instructions.Add(instruction);
            }
            else if (matchedValue.StartsWith("don't("))
            {
                var instruction = new DontInstruction();
                instructions.Add(instruction);
            }
        }

        _instructions = instructions;
    }

    public string SolvePart1()
    {
        var result = _instructions
            .OfType<MulInstruction>()
            .Sum(instruction => instruction.Calculate());
        return result.ToString();
    }

    public string SolvePart2()
    {
        var result = 0;
        var disabled = false;

        foreach (var instruction in _instructions)
        {
            disabled = instruction switch
            {
                DontInstruction => true,
                DoInstruction => false,
                _ => disabled,
            };

            if (!disabled && instruction is MulInstruction mulInstruction)
            {
                var calculatedValue = mulInstruction.Calculate();
                result += calculatedValue;
            }
        }

        return result.ToString();
    }

    [GeneratedRegex(@"(mul\(\d{1,3},\d{1,3}\))|(don't\(\))|(do\(\))")]
    private static partial Regex InstructionsRegex();
}
