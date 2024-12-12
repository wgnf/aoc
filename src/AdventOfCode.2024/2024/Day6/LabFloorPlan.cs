using AdventOfCode.Common.Primitives;
using AdventOfCode.Common.Utils;

namespace AdventOfCode._2024._2024.Day6;

internal sealed class LabFloorPlan
{
    private readonly Matrix<LabTile> _floorPlan;

    private Position _currentPlayerPosition = new(0, 0);
    private Direction _currentPlayerDirection = Direction.North;

    public LabFloorPlan(IEnumerable<string> inputLines)
    {
        var inputLinesList = inputLines.ToList();

        _floorPlan = new Matrix<LabTile>(inputLinesList.Count, inputLinesList.FirstOrDefault()?.Length ?? 1);

        for (var row = 0; row < inputLinesList.Count; row++)
        {
            var inputLine = inputLinesList[row];

            for (var column = 0; column < inputLine.Length; column++)
            {
                var labTileChar = inputLine[column];
                var isPlayer = labTileChar == '^';
                var isObstacle = labTileChar == '#';

                if (isPlayer)
                {
                    _currentPlayerPosition = new Position(row, column);
                }

                var labTile = new LabTile(isPlayer, isObstacle);
                var position = new Position(row, column);
                var element = new MatrixElement<LabTile>(position, labTile);
                _floorPlan.Insert(element);
            }
        }
    }

    public void AdvanceUntilPlayerIsOffMap()
    {
        while (_floorPlan.IsPositionValid(_currentPlayerPosition))
        {
            var collected = _floorPlan
                .CollectInDirection(_currentPlayerPosition, _currentPlayerDirection, (_, labTileElement) => !labTileElement?.Value.IsObstacle ?? true)
                .ToList();

            var collectedWithoutObstacle = collected.Where(element => !element.Value.IsObstacle).ToList();

            // mark all as visited
            collectedWithoutObstacle.ForEach(element => element.Value.Visited = true);

            // we're finished if we never hit an obstacle
            if (!collected.Any(element => element.Value.IsObstacle))
            {
                break;
            }

            // new player position is the last collected position
            _currentPlayerPosition = collectedWithoutObstacle.Last().Position;
            // new player direction is 90° to the right
            _currentPlayerDirection = _currentPlayerDirection.Turn90DegreesRight();
        }
    }

    public int GetCountOfVisitedTiles()
    {
        var visitedTiles = _floorPlan.AsEnumerable().Count(element => element.Value.Visited);
        return visitedTiles;
    }

    public void Print()
    {
        var rawMatrix = _floorPlan.AsRaw();

        foreach (var row in rawMatrix)
        {
            foreach (var tile in row)
            {
                if (tile.IsObstacle)
                {
                    Console.Write('#');
                }
                else if (tile.Visited)
                {
                    Console.Write('X');
                }
                else
                {
                    Console.Write('.');
                }
            }

            Console.WriteLine();
        }
    }
}
