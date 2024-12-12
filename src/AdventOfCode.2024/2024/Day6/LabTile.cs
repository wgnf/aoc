namespace AdventOfCode._2024._2024.Day6;

internal sealed class LabTile(bool visited, bool isObstacle)
{
    public bool Visited { get; set; } = visited;
    public bool IsObstacle { get; } = isObstacle;
}
