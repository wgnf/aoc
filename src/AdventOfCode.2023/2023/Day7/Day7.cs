using AdventOfCode.Common;
using AdventOfCode.Common.Utils;

namespace AdventOfCode._2023._2023.Day7;

// TODO: UNFINISHED

[Puzzle(Day = 7, Title = "Camel Cards", ExpectedSampleResultPart1 = "6440", ExpectedSampleResultPart2 = "?")]
// ReSharper disable once UnusedType.Global
internal sealed class Day7 : IPuzzleSolver
{
    private List<string> _inputLines = [];

    private static readonly Dictionary<char, uint> _cardValues = new()
    {
        { '2', 1 },
        { '3', 2 },
        { '4', 3 },
        { '5', 4 },
        { '6', 5 },
        { '7', 6 },
        { '8', 7 },
        { '9', 8 },
        { 'T', 9 },
        { 'J', 10 },
        { 'Q', 11 },
        { 'K', 12 },
        { 'A', 13 },
    };

    private static readonly Dictionary<HandType, uint> _handValues = new()
    {
        { HandType.HighCard, 1_000 },
        { HandType.OnePair, 2_000 },
        { HandType.TwoPair, 3_000 },
        { HandType.ThreeOfAKind, 4_000 },
        { HandType.FullHouse, 5_000 },
        { HandType.FourOfAKind, 6_000 },
        { HandType.FiveOfAKind, 7_000 },
    };

    public void Init(IEnumerable<string> inputLines)
    {
        _inputLines = inputLines.ToList();
    }

    public string SolvePart1()
    {
        var hands = ParseHands(_inputLines).ToList();
        HandleHandType(hands);
        var rankedHands = RankHands(hands);
        var totalWinnings = CalculateTotalWinnings(rankedHands);

        return totalWinnings.ToString();
    }

    public string SolvePart2()
    {
        return "UNFINISHED";
    }

    private static IEnumerable<Hand> ParseHands(IEnumerable<string> fileContents)
    {
        foreach (var line in fileContents)
        {
            var lineSplit = line.SplitTrimRemoveEmpty(' ');
            if (lineSplit.Length != 2)
            {
                throw new InvalidOperationException($"Line '{line}' has unrecognized format");
            }

            var cards = lineSplit[0];
            var bidAmount = int.Parse(lineSplit[1]);

            yield return new Hand
            {
                Cards = cards,
                BidAmount = bidAmount,
            };
        }
    }

    private static void HandleHandType(List<Hand> hands)
    {
        foreach (var hand in hands)
        {
            HandleHandTypeFor(hand);
        }
    }

    private static void HandleHandTypeFor(Hand hand)
    {
        var groupedCards = hand
            .Cards
            .GroupBy(h => h)
            .Select(g => new
            {
                Card = g.Key,
                Count = g.Count(),
            })
            .ToList();

        HandType handType;
        if (groupedCards.Exists(gc => gc.Count == 5))
        {
            handType = HandType.FiveOfAKind;
        }
        else if (groupedCards.Exists(gc => gc.Count == 4))
        {
            handType = HandType.FourOfAKind;
        }
        else if (groupedCards.Exists(gc => gc.Count == 3) && groupedCards.Exists(gc => gc.Count == 2))
        {
            handType = HandType.FullHouse;
        }
        else if (groupedCards.Exists(gc => gc.Count == 3))
        {
            handType = HandType.ThreeOfAKind;
        }
        else if (groupedCards.Exists(gc => gc.Count == 2))
        {
            var pairs = groupedCards.Where(gc => gc.Count == 2);
            handType = pairs.Count() == 2 ? HandType.TwoPair : HandType.OnePair;
        }
        else
        {
            handType = HandType.HighCard;
        }

        hand.Type = handType;
        hand.Points = _handValues[handType];
    }

    private static List<Hand> RankHands(List<Hand> hands)
    {
        var localHands = hands.OrderBy(hand => hand.Points).ToList();

        for (var index = 0; index < localHands.Count; index++)
        {
            if (index + 1 >= localHands.Count)
            {
                break;
            }

            var currentHand = localHands[index];
            var nextHand = localHands[index + 1];

            if (currentHand.Type != nextHand.Type)
            {
                continue;
            }

            RankSameHands(currentHand, nextHand);
        }

        return localHands.OrderBy(hand => hand.Points).ToList();
    }

    private static void RankSameHands(Hand hand1, Hand hand2)
    {
        for (var index = 0; index < hand1.Cards.Length; index++)
        {
            var card1 = hand1.Cards[index];
            var card2 = hand2.Cards[index];

            var pointsCard1 = _cardValues[card1];
            var pointsCard2 = _cardValues[card2];

            if (pointsCard1 == pointsCard2)
            {
                continue;
            }

            if (pointsCard1 > pointsCard2)
            {
                hand1.Points += 10;
                break;
            }

            if (pointsCard1 < pointsCard2)
            {
                hand2.Points += 10;
                break;
            }
        }
    }

    private static long CalculateTotalWinnings(List<Hand> rankedHands)
    {
        var totalWinnings = 0L;
        for (var index = 0; index < rankedHands.Count; index++)
        {
            var bid = rankedHands[index].BidAmount;

            var rank = index + 1;
            totalWinnings += rank * bid;
        }

        return totalWinnings;
    }
}
