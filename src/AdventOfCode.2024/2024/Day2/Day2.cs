using System.Diagnostics;
using AdventOfCode.Common;
using AdventOfCode.Common.Utils;

namespace AdventOfCode._2024._2024.Day2;

[Puzzle(Day = 2, Title = "Red-Nosed Reports", ExpectedSampleResultPart1 = "2", ExpectedSampleResultPart2 = "4")]
// ReSharper disable once UnusedType.Global
internal sealed class Day2 : IPuzzleSolver
{
    private List<List<int>> _diffsPerLine = [];

    public void Init(IEnumerable<string> inputLines)
    {
        _diffsPerLine = [];

        foreach (var line in inputLines)
        {
            var levels = line
                .SplitTrimRemoveEmpty(" ")
                .Select(int.Parse)
                .ToList();

            var diffs = new List<int>();

            for (var index = 0; index < levels.Count - 1; index++)
            {
                var current = levels[index];
                var next = levels[index + 1];

                var diff = current - next;
                diffs.Add(diff);
            }

            _diffsPerLine.Add(diffs);
        }
    }

    public string SolvePart1()
    {
        var safeReports = 0;

        foreach (var reportDiffs in _diffsPerLine)
        {
            if (IsReportSafe(reportDiffs))
            {
                safeReports++;
            }
        }

        return safeReports.ToString();
    }

    public string SolvePart2()
    {
        var safeReports = 0;

        foreach (var reportDiffs in _diffsPerLine)
        {
            if (reportDiffs is [-2, -2, -3, -1, 3, 3])
            {
                Debugger.Break();
            }

            var numberIncreasing = reportDiffs.Count(x => x > 0);
            var numberDecreasing = reportDiffs.Count(x => x < 0);
            var numberNoDiff = reportDiffs.Count(x => x == 0);

            // no outliers
            if ((numberIncreasing == 0 || numberDecreasing == 0) && numberNoDiff == 0)
            {
                if (IsReportSafe(reportDiffs))
                {
                    safeReports++;
                }

                continue;
            }

            // too many outliers
            if (numberIncreasing > 1 && numberDecreasing > 1)
            {
                continue;
            }

            if (numberNoDiff == 1)
            {
                var outlier = reportDiffs.Single(x => x == 0);
                var outlierIndex = reportDiffs.IndexOf(outlier);

                RemoveOutlierAtIndexAndRecalculate(reportDiffs, outlierIndex);

                if (IsReportSafe(reportDiffs))
                {
                    safeReports++;
                }
            }

            // one is increasing rest is decreasing
            if (numberIncreasing == 1 && numberDecreasing > 1)
            {
                int outlier;
                try
                {
                    outlier = reportDiffs.Single(x => x > 0);
                }
                catch (Exception)
                {
                    // TODO: HOW?!?!??!?!?!?!?!??!
                    continue;
                }

                var outlierIndex = reportDiffs.IndexOf(outlier);

                RemoveOutlierAtIndexAndRecalculate(reportDiffs, outlierIndex);

                if (IsReportSafe(reportDiffs))
                {
                    safeReports++;
                }
            }

            // one is decreasing rest is increasing
            if (numberIncreasing > 1 && numberDecreasing == 1)
            {
                int outlier;
                try
                {
                    outlier = reportDiffs.Single(x => x < 0);
                }
                catch (Exception)
                {
                    continue;
                }
                var outlierIndex = reportDiffs.IndexOf(outlier);

                RemoveOutlierAtIndexAndRecalculate(reportDiffs, outlierIndex);

                if (IsReportSafe(reportDiffs))
                {
                    safeReports++;
                }
            }
        }

        return safeReports.ToString();
    }

    private static bool IsReportSafe(List<int> reportDiffs)
    {
        var increasingAndValid = reportDiffs.TrueForAll(diff => diff > 0 && (diff.Abs() <= 3 && diff.Abs() >= 1));
        var decreasingAndValid = reportDiffs.TrueForAll(diff => diff < 0 && (diff.Abs() <= 3 && diff.Abs() >= 1));

        return increasingAndValid || decreasingAndValid;
    }

    private static void RemoveOutlierAtIndexAndRecalculate(List<int> reportDiffs, int outlierIndex)
    {
        // when it's not front or back recalculate diffs
        if (outlierIndex == 0 || outlierIndex == reportDiffs.Count - 1)
        {
            reportDiffs.RemoveAt(outlierIndex);
            return;
        }

        var newDiff = reportDiffs[outlierIndex] + reportDiffs[outlierIndex + 1];
        reportDiffs.RemoveAt(outlierIndex);
        reportDiffs[outlierIndex - 1] = newDiff;
    }
}
