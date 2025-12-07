using AdventOfCode.Common;
using AdventOfCode.Common.Primitives;
using AdventOfCode.Common.Utils;
using JetBrains.Annotations;

namespace AdventOfCode._2025._2025.Day1;

[UsedImplicitly]
[Puzzle(Day = 1, Title = "Secret Entrance", ExpectedSampleResultPart1 = "3", ExpectedSampleResultPart2 = "6")]
public sealed class Day1 : IPuzzleSolver
{
    private const int MaxNumber = 100;
    private readonly List<int> _directions = [];
    
    public void Init(IEnumerable<string> inputLines)
    {
        _directions.Clear();
        
        foreach (var line in inputLines)
        {
            var firstElement = line.First();
            var otherElements = line[1..];

            var direction = int.Parse(otherElements);
            if (firstElement == 'L')
            {
                direction *= -1;
            }
            
            _directions.Add(direction);
        }
    }

    public string SolvePart1()
    {
        var currentNumber = new OverflowingNumber(50, MaxNumber);
        var zeroCount = 0;

        foreach (var direction in _directions)
        {
            currentNumber.IncreaseValueBy(direction);
            
            if (currentNumber.GetCurrentValue() == 0)
            {
                zeroCount++;
            }
        }
        
        return zeroCount.ToString();
    }

    public string SolvePart2()
    {
        // TODO: bruteforce, this can be done better!
        var currentNumber = new OverflowingNumber(50, MaxNumber);
        var zeroCount = 0;

        foreach (var direction in _directions)
        {
            for (var turn = 0; turn < direction.Abs(); turn++)
            {
                if (direction < 0)
                {
                    currentNumber.DecreaseValueBy(1);
                }
                else
                {
                    currentNumber.IncreaseValueBy(1);
                }

                if (currentNumber.GetCurrentValue() == 0)
                {
                    zeroCount++;
                }
            }
        }
        
        return zeroCount.ToString();
    }
}
