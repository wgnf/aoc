using AdventOfCode.Common;
// ReSharper disable UnusedType.Global

namespace AdventOfCode._2023._2023.Day4;

[Puzzle(Day = 4, Title = "Scratchcards", ExpectedSampleResultPart1 = "13", ExpectedSampleResultPart2 = "30")]
internal sealed class Day4 : IPuzzleSolver
{
    private List<string> _inputLines = [];

    public void Init(IEnumerable<string> inputLines)
    {
        _inputLines = inputLines.ToList();
    }

    public string SolvePart1()
    {
        var cards = GetCardsFromFileContent(_inputLines);
        var winningPointsAll = new List<int>();

        foreach (var card in cards)
        {
            var actualWinningNumbers = card.WinningNumbers.Intersect(card.ActualNumbers).ToList();
            if (actualWinningNumbers.Count == 0)
            {
                continue;
            }

            var winningPoints = 1;
            var timesToDouble = actualWinningNumbers.Count - 1;
            for (var i = 0; i < timesToDouble; i++)
            {
                winningPoints *= 2;
            }

            Console.WriteLine($"Card number [{card.Number}] has {winningPoints} points, because it matched {actualWinningNumbers.Count} times");

            winningPointsAll.Add(winningPoints);
        }

        return winningPointsAll.Sum().ToString();
    }

    private Dictionary<int, int>? _countOfEachCard;

    public string SolvePart2()
    {
        var originalCards = GetCardsFromFileContent(_inputLines);

        // just add each initial card
        _countOfEachCard = Enumerable.Range(1, originalCards.Count).ToDictionary(key => key, _ => 1);

        foreach (var card in originalCards)
        {
            var currentCountOfCard = _countOfEachCard.GetValueOrDefault(card.Number);
            var winCount = card.WinningNumbers.Intersect(card.ActualNumbers).Count();

            for (var cardNumber = card.Number + 1; cardNumber <= card.Number + winCount; cardNumber++)
            {
                // default should never happen, because we populated the dictionary before already
                var currentCount = _countOfEachCard.GetValueOrDefault(cardNumber);

                currentCount += currentCountOfCard;
                _countOfEachCard[cardNumber] = currentCount;
            }
        }

        var countOfAll = _countOfEachCard.Sum(count => count.Value);
        return countOfAll.ToString();
    }

    private static List<Card> GetCardsFromFileContent(IEnumerable<string> fileContents)
    {
        var cards = new List<Card>();
        var cardNumber = 0;

        foreach (var line in fileContents)
        {
            var indexOfColon = line.IndexOf(':');
            var lineWithoutCardPart = line[(indexOfColon + 1)..];

            var lineSplit = lineWithoutCardPart.Split('|', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            if (lineSplit.Length != 2)
            {
                throw new InvalidOperationException($"Line '{line}' has not the correct format");
            }

            var winningNumbers = lineSplit[0]
                .Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse);

            var actualNumbers = lineSplit[1]
                .Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse);

            var card = new Card
            {
                Number = cardNumber + 1,
            };
            card.WinningNumbers.AddRange(winningNumbers);
            card.ActualNumbers.AddRange(actualNumbers);

            Console.WriteLine($"Card number [{card.Number}] has winning numbers: [{string.Join(", ", card.WinningNumbers)}] and actual numbers: [{string.Join(", ", card.ActualNumbers)}]");

            cards.Add(card);
            cardNumber++;
        }

        return cards;
    }
}
