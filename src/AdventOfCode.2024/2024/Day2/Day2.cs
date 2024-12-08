﻿using AdventOfCode.Common;
using AdventOfCode.Common.Utils;

namespace AdventOfCode._2024._2024.Day2;

[Puzzle(Day = 2, Title = "Red-Nosed Reports", ExpectedSampleResultPart1 = "2", ExpectedSampleResultPart2 = "4")]
// ReSharper disable once UnusedType.Global
internal sealed class Day2 : IPuzzleSolver
{
    private List<Report> _reports = [];

    public void Init(IEnumerable<string> inputLines)
    {
        _reports = [];

        foreach (var line in inputLines)
        {
            var levels = line
                .SplitTrimRemoveEmpty(" ")
                .Select(int.Parse)
                .ToList();

            var report = new Report(levels);
            _reports.Add(report);
        }
    }

    public string SolvePart1()
    {
        var safeReports = _reports.Count(report => report.IsSafe());
        return safeReports.ToString();
    }

    public string SolvePart2()
    {
        Console.WriteLine("============================");

        var safeReports = 0;

        foreach (var report in _reports)
        {
            Console.WriteLine($"\n\nREPORT: {string.Join(" ", report.LevelContainer.Levels.Select(l => l.Value))}");

            if (report.IsSafe())
            {
                Console.WriteLine("ALREADY SAFE");
                safeReports++;
                continue;
            }

            report.TryToMakeSafe();

            if (report.IsSafe())
            {
                Console.WriteLine("MADE SAFE");
                safeReports++;
            }
            else
            {
                Console.WriteLine("STILL NOT SAFE");
            }
        }
        return safeReports.ToString();
    }
}
