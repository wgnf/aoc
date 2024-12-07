namespace AdventOfCode._2023._2023.Day6;

internal sealed class Race
{
    public long MaxTime { get; set; }

    public long RecordDistance { get; set; }

    public int GetNumberOfWaysToWin()
    {
        var waysToWin = 0;
        for (var buttonHoldTime = 0; buttonHoldTime < MaxTime; buttonHoldTime++)
        {
            var speed = buttonHoldTime;
            var remainingTime = MaxTime - buttonHoldTime;
            var distance = remainingTime * speed;

            if (distance > RecordDistance)
            {
                waysToWin++;
            }
            // when we already found ways to win, but did not find a new way to win, we can be sure that we won't find any more ways to win
            else if (waysToWin != 0)
            {
                break;
            }
        }

        return waysToWin;
    }
}
