using System.Text;
using AdventOfCode.Common;
using AdventOfCode.Common.Utils;
using JetBrains.Annotations;

namespace AdventOfCode._2025._2025.Day3;

[UsedImplicitly]
[Puzzle(Day = 3, Title = "Lobby", ExpectedSampleResultPart1 = "357", ExpectedSampleResultPart2 = "3121910778619")]
public sealed class Day3 : IPuzzleSolver
{
    private readonly List<IEnumerable<ushort>> _batteryBanks = [];
    
    public void Init(IEnumerable<string> inputLines)
    {
        _batteryBanks.Clear();

        foreach (var inputLine in inputLines)
        {
            var batteryBank = inputLine.Select(battery => ushort.Parse(battery.ToString()));
            _batteryBanks.Add(batteryBank);
        }
    }

    public string SolvePart1()
    {
        var joltageSum = GetJoltageSum(2);
        return joltageSum.ToString();
    }

    public string SolvePart2()
    {
        var joltageSum = GetJoltageSum(12);
        return joltageSum.ToString();
    }

    private ulong GetJoltageSum(int numberOfBatteries)
    {
        return _batteryBanks
            .Select(batteryBank => GetMaxJoltage(batteryBank, numberOfBatteries))
            .SumX();
    }

    private static ulong GetMaxJoltage(IEnumerable<ushort> batteryBank, int numberOfBatteries)
    {
        var batteryJoltageBuilder = new StringBuilder();
        var batteryBankList = batteryBank.ToList();
        var elementsPerLayer = batteryBankList.Count - (numberOfBatteries - 1);
        var layerLookupStartIndex = 0;

        for (var layerStartIndex = 0; layerStartIndex < numberOfBatteries; layerStartIndex++)
        {
            var layer = batteryBankList.Slice(layerStartIndex, elementsPerLayer);
            var lookupLayer = layer.Skip(layerLookupStartIndex).ToList();
            
            var maxInLayer = lookupLayer.Max();
            layerLookupStartIndex = lookupLayer.IndexOf(maxInLayer) + layerLookupStartIndex;

            batteryJoltageBuilder.Append(maxInLayer);
        }
        
        var result = ulong.Parse(batteryJoltageBuilder.ToString());
        return result;
    }
}
