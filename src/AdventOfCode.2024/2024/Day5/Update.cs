namespace AdventOfCode._2024._2024.Day5;

internal sealed class Update(List<int> pagesToProduce)
{
    public List<int> PagesToProduce { get; set; } = pagesToProduce;

    public int GetMiddlePageNumber()
    {
        var middleIndex = (PagesToProduce.Count - 1) / 2;
        return PagesToProduce[middleIndex];
    }
}
