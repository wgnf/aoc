namespace AdventOfCode.Common.Primitives;

public sealed class MathOperation
{
    private MathOperation(MathOperationType type)
    {
        Type = type;
    }

    public static MathOperation FromString(string operation)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(operation);

        var type = operation switch
        {
            "+" => MathOperationType.Addition,
            "-" => MathOperationType.Subtraction,
            "*" => MathOperationType.Multiplication,
            "/" => MathOperationType.Division,
            _ => throw new ArgumentException($"Unknown {nameof(MathOperationType)}: {operation}", nameof(operation)),
        };
        return new MathOperation(type);
    }

    public MathOperationType Type { get; }

    public long Solve(params IEnumerable<int> numbers)
    {
        long? result = null;
        foreach (var number in numbers)
        {
            if (!result.HasValue)
            {
                result = number;
                continue;
            }

            switch (Type)
            {
                case MathOperationType.Addition:
                    result += number;
                    break;
                case MathOperationType.Subtraction:
                    result -= number;
                    break;
                case MathOperationType.Multiplication:
                    result *= number;
                    break;
                case MathOperationType.Division:
                    result /= number;
                    break;
                default:
                    throw new InvalidOperationException($"Unknown {nameof(MathOperationType)}: {Type}");
            }
        }

        return result ?? throw new InvalidOperationException("RESULT HAS NO VALUE?!");
    }
}
