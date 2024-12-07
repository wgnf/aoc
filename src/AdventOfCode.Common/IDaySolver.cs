namespace AdventOfCode.Common;

public interface IDaySolver
{
    public void Init(IEnumerable<string> inputLines);
    public string SolvePart1();
    public string SolvePart2();
}
