namespace AdventOfCode.Common;

public interface IPuzzleSolverProvider
{
    public IEnumerable<YearDto> GetAllYears();
}
