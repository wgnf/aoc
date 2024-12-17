namespace AdventOfCode.Common.Utils;

public static class MaskGenerator
{
    public static List<List<T>> GenerateMasks<T>(int numberOfDigits, IEnumerable<T> valuesToUse)
    {
        var valuesToUseList = valuesToUse.ToList();

        var maskCombinations = GenerateMaskCombinations(numberOfDigits, valuesToUseList.Count - 1);

        var result = new List<List<T>>();
        foreach (var maskCombination in maskCombinations)
        {
            var valueMaskCombination = new List<T>();
            foreach (var maskCombinationChar in maskCombination)
            {
                var index = int.Parse(maskCombinationChar.ToString());
                var valueToUse = valuesToUseList[index];
                valueMaskCombination.Add(valueToUse);
            }

            result.Add(valueMaskCombination);
        }
        return result;
    }

    public static List<string> GenerateMaskCombinations(int numberOfDigits, int maxAllowedNumber)
    {
        var result = new List<string>();
        var allowedNumbers = new string[maxAllowedNumber + 1];

        for (var index = 0; index < allowedNumbers.Length; index++)
        {
            allowedNumbers[index] = index.ToString();
        }

        GenerateCombinations(result, "", numberOfDigits, allowedNumbers);
        return result;
    }

    private static void GenerateCombinations(List<string> result, string current, int remaining, string[] allowedNumbers)
    {
        if (remaining == 0)
        {
            result.Add(current);
            return;
        }

        foreach (var number in allowedNumbers)
        {
            GenerateCombinations(result, current + number, remaining - 1, allowedNumbers);
        }
    }
}
