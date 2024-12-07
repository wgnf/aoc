using AdventOfCode.Common;

namespace AdventOfCode._2023._2023.Day2;

// ReSharper disable UnusedType.Global

[PuzzleDay(Day = 2, Title = "Cube Conundrum", ExpectedSampleResultPart1 = "8", ExpectedSampleResultPart2 = "2286")]
internal sealed class Day2 : IDaySolver
{
    private List<string> _inputLines = [];
    private readonly List<Cube> _cubeConfigurations = new()
    {
        new Cube { Amount = 12, Color = "red" },
        new Cube { Amount = 13, Color = "green" },
        new Cube { Amount = 14, Color = "blue" },
    };

    public void Init(IEnumerable<string> inputLines)
    {
        _inputLines = inputLines.ToList();
    }

    public string SolvePart1()
    {
        var games = ParseGames(_inputLines);

        var possibleGameIds = GetIdsOfPossibleGames(games);
        return possibleGameIds.Sum().ToString();
    }

    public string SolvePart2()
    {
        var games = ParseGames(_inputLines);

        var powersPerGame = new List<int>();

        foreach (var game in games)
        {
            var cubes = game
                .CubeRounds
                .SelectMany(round => round.RevealedCubes);

            var groupedCubes = cubes.GroupBy(cube => cube.Color);
            var maxAmounts = groupedCubes
                .Select(group => group.MaxBy(g => g.Amount))
                .Select(g => g!.Amount);

            var power = maxAmounts.Aggregate(1, (current, maxAmount) => current * maxAmount);

            powersPerGame.Add(power);
        }

        return powersPerGame.Sum().ToString();
    }

    private static List<Game> ParseGames(IEnumerable<string> fileContents)
    {
        var games = new List<Game>();
        foreach (var line in fileContents)
        {
            var game = ParseLine(line);
            games.Add(game);
        }

        return games;
    }

    private static Game ParseLine(string line)
    {
        var lineSplit = line.Split(":", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        if (lineSplit.Length != 2)
        {
            throw new InvalidOperationException("Error parsing line");
        }

        var game = new Game();

        var gameIdPart = lineSplit[0];
        var cubeDefinitionPart = lineSplit[1];

        var gameId = GetGameId(gameIdPart);
        game.Id = gameId;

        ParseCubeDefinitionPart(cubeDefinitionPart, game);

        Console.WriteLine($"Parsed line '{line}' to ==> {game}");
        return game;
    }

    private static int GetGameId(string gameIdPart)
    {
        var split = gameIdPart.Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        if (split.Length != 2)
        {
            throw new InvalidOperationException("Error parsing game-id");
        }

        var gameIdString = split[1];
        if (!int.TryParse(gameIdString, out var gameId))
        {
            throw new InvalidOperationException($"Could not parse game-id '{gameId}' to int for gameIdPart '{gameIdPart}'");
        }

        return gameId;
    }

    private static void ParseCubeDefinitionPart(string cubeDefinitionPart, Game game)
    {
        var cubeRoundsSplit = cubeDefinitionPart.Split(";", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        foreach (var cubeRoundString in cubeRoundsSplit)
        {
            var cubeRound = new CubeRound();
            ParseCubeRound(cubeRoundString, cubeRound);

            game.CubeRounds.Add(cubeRound);
        }
    }

    private static void ParseCubeRound(string cubeRoundString, CubeRound cubeRound)
    {
        var cubesSplit = cubeRoundString.Split(",", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        foreach (var cubeString in cubesSplit)
        {
            var cubeDefinitionsSplit = cubeString.Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            var amountString = cubeDefinitionsSplit[0];
            if (!int.TryParse(amountString, out var amount))
            {
                throw new InvalidOperationException($"Could not parse '{amountString}' to int for cube-round '{cubeRoundString}'");
            }

            var color = cubeDefinitionsSplit[1];

            cubeRound.RevealedCubes.Add(new Cube { Amount = amount, Color = color });
        }
    }

    private IEnumerable<int> GetIdsOfPossibleGames(IEnumerable<Game> games)
    {
        var possibleGameIds = games
            .Where(IsGamePossible)
            .Select(game => game.Id);
        return possibleGameIds;
    }

    private bool IsGamePossible(Game game)
    {
        foreach (var cubeRound in game.CubeRounds)
        {
            foreach (var cube in cubeRound.RevealedCubes)
            {
                foreach (var cubeConfiguration in _cubeConfigurations)
                {
                    if (cubeConfiguration.Color == cube.Color &&
                        cubeConfiguration.Amount < cube.Amount)
                    {
                        Console.WriteLine($"Impossible: {game}");
                        return false;
                    }
                }
            }
        }

        Console.WriteLine($"Possible: {game}");
        return true;
    }
}
