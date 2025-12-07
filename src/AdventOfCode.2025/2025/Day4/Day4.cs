using AdventOfCode.Common;
using AdventOfCode.Common.Primitives;
using JetBrains.Annotations;

namespace AdventOfCode._2025._2025.Day4;

[UsedImplicitly]
[Puzzle(Day = 4, Title = "Printing Department", ExpectedSampleResultPart1 = "13", ExpectedSampleResultPart2 = "43")]
public sealed class Day4 : IPuzzleSolver
{
    private Matrix<PaperRollPlanElement> _paperRollPlan = new(0, 0);
    
    public void Init(IEnumerable<string> inputLines)
    {
        _paperRollPlan = Matrix<PaperRollPlanElement>.FromInputLines(inputLines, GetPaperRollPlanElement);
    }

    public string SolvePart1()
    {
        var collectedPapers = GetCollectedPapers(false);
        return collectedPapers.ToString();
    }

    public string SolvePart2()
    {
        var collectedPapers = GetCollectedPapers(true);
        return collectedPapers.ToString();
    }

    private int GetCollectedPapers(bool canRemove)
    { 
        var collectedPapers = 0;
        int recentlyCollectedPapers;
        
        do
        {
            var collectiblePapers = _paperRollPlan
                .AsEnumerable()
                .Where(x => x.Value.HasPaper)
                .Select(x => new
                {
                    Element = x, 
                    Neighbors = _paperRollPlan.GetNeighboringElements(x.Position),
                })
                .Where(x => x.Neighbors.Count(n => n.Value.HasPaper) < 4)
                .ToList();

            recentlyCollectedPapers = collectiblePapers.Count;
            collectedPapers += recentlyCollectedPapers;
            
            if (canRemove)
            {
                collectiblePapers.ForEach(x => x.Element.Value.HasPaper = false);
            }

        } while (recentlyCollectedPapers != 0 && canRemove);
        
        return collectedPapers;
    }

    private static PaperRollPlanElement GetPaperRollPlanElement(char c)
    {
        return c switch
        {
            '.' => new PaperRollPlanElement { HasPaper = false },
            '@' => new PaperRollPlanElement { HasPaper = true },
            _ => throw new InvalidOperationException($"'{c}' unknown"),
        };
    }

    private class PaperRollPlanElement
    {
        public bool HasPaper { get; set; }
    };
}
