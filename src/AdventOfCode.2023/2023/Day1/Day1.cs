using System.Text;
using AdventOfCode.Common;

// ReSharper disable UnusedType.Global

namespace AdventOfCode._2023._2023.Day1;

[Puzzle(Day = 1, Title = "Trebuchet?!", ExpectedSampleResultPart1 = "142", ExpectedSampleResultPart2 = "281")]
public sealed class Day1 : IPuzzleSolver
{
    private List<string> _inputLines = [];

    public void Init(IEnumerable<string> inputLines)
    {
        _inputLines = inputLines.ToList();
    }

    public string SolvePart1()
    {
        var numbers = new List<int>();

        foreach (var line in _inputLines)
        {
            var numberBuilder = new StringBuilder();

            // forward search for first digit
            foreach (var @char in line)
            {
                if (char.IsDigit(@char))
                {
                    numberBuilder.Append(@char);
                    break;
                }
            }

            // backward search for last digit (line reversed)
            foreach (var @char in line.Reverse())
            {
                if (char.IsDigit(@char))
                {
                    numberBuilder.Append(@char);
                    break;
                }
            }

            var number = numberBuilder.ToString();
            if (number.Length != 2)
            {
                throw new InvalidOperationException($"Number '{number}' is wrong length?!");
            }

            if (!int.TryParse(number, out var intNumber))
            {
                throw new InvalidOperationException($"Number '{number}' cannot be converted into int");
            }

            numbers.Add(intNumber);
        }

        return numbers.Sum().ToString();
    }

    public string SolvePart2()
    {
        var values = new[]
        {
            new
            {
                Word = "1",
                Value = 1,
            },
            new
            {
                Word = "2",
                Value = 2,
            },
            new
            {
                Word = "3",
                Value = 3,
            },
            new
            {
                Word = "4",
                Value = 4,
            },
            new
            {
                Word = "5",
                Value = 5,
            },
            new
            {
                Word = "6",
                Value = 6,
            },
            new
            {
                Word = "7",
                Value = 7,
            },
            new
            {
                Word = "8",
                Value = 8,
            },
            new
            {
                Word = "9",
                Value = 9,
            },
            new
            {
                Word = "one",
                Value = 1,
            },
            new
            {
                Word = "two",
                Value = 2,
            },
            new
            {
                Word = "three",
                Value = 3,
            },
            new
            {
                Word = "four",
                Value = 4,
            },
            new
            {
                Word = "five",
                Value = 5,
            },
            new
            {
                Word = "six",
                Value = 6,
            },
            new
            {
                Word = "seven",
                Value = 7,
            },
            new
            {
                Word = "eight",
                Value = 8,
            },
            new
            {
                Word = "nine",
                Value = 9,
            },
        };

        var numbers = new List<int>();

        foreach (var line in _inputLines)
        {
            var indexForwardSearch = line.Length;
            var valueForwardSearch = 0;

            var indexBackwardSearch = -1;
            var valueBackwardSearch = 0;

            foreach (var value in values)
            {
                var indexFirst = line.IndexOf(value.Word, StringComparison.InvariantCulture);
                var indexLast = line.LastIndexOf(value.Word, StringComparison.InvariantCulture);

                if (indexFirst < indexForwardSearch && indexFirst >= 0)
                {
                    indexForwardSearch = indexFirst;
                    valueForwardSearch = value.Value;
                }

                if (indexLast > indexBackwardSearch && indexFirst >= 0)
                {
                    indexBackwardSearch = indexLast;
                    valueBackwardSearch = value.Value;
                }
            }

            var numberValue = valueForwardSearch * 10 + valueBackwardSearch;
            numbers.Add(numberValue);
        }

        return numbers.Sum().ToString();
    }
}
