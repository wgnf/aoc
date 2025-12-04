using AdventOfCode.Common.Utils;

namespace AdventOfCode.Common.Primitives;

public sealed class OverflowingNumber
{
    private int _value;
    private readonly int _maxValue;

    public OverflowingNumber(int startingValue, int maxValue)
    {
        _maxValue = maxValue;
        SetValue(startingValue);
    }

    public int GetCurrentValue()
    {
        return _value;
    }

    public void SetValue(int valueToSet)
    {
        _value = valueToSet.Mod(_maxValue);
    }

    public void IncreaseValueBy(int amount)
    {
        var newValue = GetCurrentValue() + amount;
        SetValue(newValue);
    }

    public void DecreaseValueBy(int amount)
    {
        var newValue = GetCurrentValue() - amount;
        SetValue(newValue);
    }

    public override string ToString()
    {
        return GetCurrentValue().ToString();
    }
}
