using AdventOfCode.Common.Utils;

namespace AdventOfCode._2024._2024.Day5;

internal sealed class PrintQueue
{
    private readonly Dictionary<int, List<int>> _orderingRules = new();

    public PrintQueue(List<string> inputLines)
    {
        OrderingRules = [];
        Updates = [];

        var sawNewLine = false;
        foreach (var line in inputLines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                sawNewLine = true;
                continue;
            }

            if (sawNewLine)
            {
                ParseUpdate(line);
            }
            else
            {
                ParseOrderingRule(line);
            }
        }
    }

    public List<PageOrderingRule> OrderingRules { get; }

    public List<Update> Updates { get; }

    public List<Update> GetUpdatesThatAreInRightOrder()
    {
        return Updates
            .Where(IsInRightOrder)
            .ToList();
    }

    public List<Update> GetUpdatesThatAreInWrongOrder()
    {
        return Updates
            .Where(update => !IsInRightOrder(update))
            .ToList();
    }

    public void FixOrderOfUpdate(Update update)
    {
        var newUpdates = new int[update.PagesToProduce.Count];

        foreach (var pageToProduce in update.PagesToProduce)
        {
            var newIndex = 0;
            foreach (var innerPageToProduce in update.PagesToProduce)
            {
                if (_orderingRules.TryGetValue(innerPageToProduce, out var orderingRules) && orderingRules.Contains(pageToProduce))
                {
                    newIndex++;
                }
            }

            newUpdates[newIndex] = pageToProduce;
        }

        update.PagesToProduce = newUpdates.ToList();
    }

    private void ParseUpdate(string line)
    {
        var pages = line
            .SplitTrimRemoveEmpty(",")
            .Select(int.Parse)
            .ToList();

        var update = new Update(pages);
        Updates.Add(update);
    }

    private void ParseOrderingRule(string line)
    {
        var parts = line
            .SplitTrimRemoveEmpty("|")
            .Select(int.Parse)
            .ToList();

        var before = parts[0];
        var after = parts[1];

        var orderingRule = new PageOrderingRule(before, after);
        OrderingRules.Add(orderingRule);

        if (!_orderingRules.TryGetValue(before, out var rule))
        {
            _orderingRules.Add(before, [after]);
        }
        else
        {
            rule.Add(after);
        }
    }

    private bool IsInRightOrder(Update update)
    {
        var isInRightOrder = true;

        foreach (var orderingRule in OrderingRules)
        {
            if (!IsOrderingRuleApplicable(update, orderingRule))
            {
                continue;
            }

            var indexOfBefore = update.PagesToProduce.IndexOf(orderingRule.PageBefore);
            var indexOfAfter = update.PagesToProduce.IndexOf(orderingRule.PageAfter);

            isInRightOrder &= indexOfBefore < indexOfAfter;
        }

        return isInRightOrder;
    }

    private static bool IsOrderingRuleApplicable(Update update, PageOrderingRule orderingRule)
    {
        return update.PagesToProduce.Contains(orderingRule.PageBefore) && update.PagesToProduce.Contains(orderingRule.PageAfter);
    }
}
