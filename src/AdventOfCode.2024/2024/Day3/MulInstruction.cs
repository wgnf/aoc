namespace AdventOfCode._2024._2024.Day3;

internal sealed class MulInstruction : Instruction
{
    private readonly int _value1;
    private readonly int _value2;

    public MulInstruction(int value1, int value2)
    {
        _value1 = value1;
        _value2 = value2;
    }

    public int Calculate()
    {
        return _value1 * _value2;
    }
}
