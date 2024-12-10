using AdventOfCode.Common.Utils;

namespace AdventOfCode._2024._2024.Day5;

internal sealed class PrintQueue
{
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

    public bool IsInRightOrder(Update update)
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

    public void FixOrderOfUpdate(Update update)
    {
        var orderingRulesThatApply = OrderingRules
            .Where(orderingRule => IsOrderingRuleApplicable(update, orderingRule))
            .ToList();

        var allOrderingRulePages = orderingRulesThatApply.SelectMany(orderingRule => new[] { orderingRule.PageBefore, orderingRule.PageAfter });
        var numbersNotInAnyRule = update.PagesToProduce.Where(page => !allOrderingRulePages.Contains(page));

        var pagesToProduce = new List<int>();
        foreach (var orderingRule in orderingRulesThatApply)
        {
            if (!pagesToProduce.Contains(orderingRule.PageBefore) && !pagesToProduce.Contains(orderingRule.PageAfter))
            {
                pagesToProduce.Add(orderingRule.PageBefore);
                pagesToProduce.Add(orderingRule.PageAfter);
            }

            else if (!pagesToProduce.Contains(orderingRule.PageBefore) && pagesToProduce.Contains(orderingRule.PageAfter))
            {
                var indexOfPageAfter = pagesToProduce.IndexOf(orderingRule.PageAfter);
                pagesToProduce.Insert(indexOfPageAfter, orderingRule.PageBefore);
            }

            else if (pagesToProduce.Contains(orderingRule.PageBefore) && !pagesToProduce.Contains(orderingRule.PageAfter))
            {
                var indexOfPageBefore = pagesToProduce.IndexOf(orderingRule.PageBefore);
                pagesToProduce.Insert(indexOfPageBefore + 1, orderingRule.PageAfter);
            }
            else
            {
                // we get here if both PageBefore and PageAfter are already in the list
                // we then have to check if their order is correct

                var indexOfPageBefore = pagesToProduce.IndexOf(orderingRule.PageBefore);
                var indexOfPageAfter = pagesToProduce.IndexOf(orderingRule.PageAfter);

                if (indexOfPageBefore > indexOfPageAfter)
                {
                    // order is not correct -> remove after and place behind before
                    pagesToProduce.Remove(orderingRule.PageAfter);

                    var newIndexOfPageBefore = pagesToProduce.IndexOf(orderingRule.PageBefore);
                    pagesToProduce.Insert(newIndexOfPageBefore + 1, orderingRule.PageAfter);
                }
            }
        }

        var actualPagesToProduce = pagesToProduce.Concat(numbersNotInAnyRule).ToList();
        update.PagesToProduce = actualPagesToProduce;
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
    }

    private static bool IsOrderingRuleApplicable(Update update, PageOrderingRule orderingRule)
    {
        return update.PagesToProduce.Contains(orderingRule.PageBefore) && update.PagesToProduce.Contains(orderingRule.PageAfter);
    }
}
