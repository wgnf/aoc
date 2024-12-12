using AdventOfCode.Common;

namespace AdventOfCode._2024._2024.Day5;

[Puzzle(Day = 5, Title = "Print Queue", ExpectedSampleResultPart1 = "143", ExpectedSampleResultPart2 = "123")]
// ReSharper disable once UnusedType.Global
internal sealed class Day5 : IPuzzleSolver
{
    private PrintQueue _queue = new([]);

    public void Init(IEnumerable<string> inputLines)
    {
        _queue = new PrintQueue(inputLines.ToList());
    }

    public string SolvePart1()
    {
        var result = _queue
            .GetUpdatesThatAreInRightOrder()
            .Select(update => update.GetMiddlePageNumber())
            .Sum();

        return result.ToString();
    }

    public string SolvePart2()
    {
        var updatesInWrongOrder = _queue.GetUpdatesThatAreInWrongOrder();
        updatesInWrongOrder.ForEach(update => _queue.FixOrderOfUpdate(update));

        var result = updatesInWrongOrder
            .Select(update => update.GetMiddlePageNumber())
            .Sum();

        return result.ToString();
    }
}
