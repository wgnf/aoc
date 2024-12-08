using System.Diagnostics;
using System.Reflection;

namespace AdventOfCode.Common;

public sealed class Puzzle
{
    private readonly IPuzzleSolver _solver;
    private readonly int _year;
    private readonly string _expectedSampleResultPart1;
    private readonly string _expectedSampleResultPart2;

    public Puzzle(int year, int day, string title, string expectedSampleResultPart1, string expectedSampleResultPart2, IPuzzleSolver solver)
    {
        _year = year;
        _expectedSampleResultPart1 = expectedSampleResultPart1;
        _expectedSampleResultPart2 = expectedSampleResultPart2;
        _solver = solver;

        Day = day;
        Title = title;

        SampleResultPart1 = string.Empty;
        SampleResultPart2 = string.Empty;
        InputResultPart1 = string.Empty;
        InputResultPart2 = string.Empty;
    }

    public int Day { get; }
    public string Title { get; }

    public string SampleResultPart1 { get; private set; }
    public long SampleResultPart1Took { get; private set; }

    public string InputResultPart1 { get; private set; }
    public long InputResultPart1Took { get; private set; }

    public string SampleResultPart2 { get; private set; }
    public long SampleResultPart2Took { get; private set; }

    public string InputResultPart2 { get; private set; }
    public long InputResultPart2Took { get; private set; }

    public void Solve()
    {
        SolveExample();
        SolveInput();
    }

    private static string GetCurrentDirectory()
    {
        var currentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
        return currentDirectory;
    }

    private string GetExampleFilename(int part)
    {
        var filename = $"{_year}/Day{Day}/example{part}.txt";
        var path = Path.Combine(GetCurrentDirectory(), filename);

        if (!File.Exists(path))
        {
            filename = $"{_year}/Day{Day}/example.txt";
            path = Path.Combine(GetCurrentDirectory(), filename);
        }

        if (!File.Exists(path))
        {
            throw new FileNotFoundException(path);
        }

        return path;
    }

    private string GetInputFilename()
    {
        var filename = $"{_year}/Day{Day}/input.txt";
        var path = Path.Combine(GetCurrentDirectory(), filename);

        if (!File.Exists(path))
        {
            throw new FileNotFoundException(path);
        }
        return path;
    }

    private static string[] GetInputFromFile(string fileName)
    {
        var lines = File.ReadAllLines(fileName);
        return lines;
    }

    private void SolveExample()
    {
        // part 1...
        var filenamePart1 = GetExampleFilename(1);
        var inputPart1 = GetInputFromFile(filenamePart1);

        var stopwatchPart1 = Stopwatch.StartNew();
        _solver.Init(inputPart1);
        var resultPart1 = _solver.SolvePart1();
        stopwatchPart1.Stop();

        SampleResultPart1 = resultPart1 == _expectedSampleResultPart1
            ? $"{resultPart1} ✅"
            : $"{resultPart1} ❌ ({_expectedSampleResultPart1})";
        SampleResultPart1Took = stopwatchPart1.ElapsedMilliseconds;

        // part 2...
        var filenamePart2 = GetExampleFilename(2);
        var inputPart2 = GetInputFromFile(filenamePart2);

        var stopwatchPart2 = Stopwatch.StartNew();
        _solver.Init(inputPart2);
        var resultPart2 = _solver.SolvePart2();
        stopwatchPart2.Stop();

        SampleResultPart2 = resultPart2 == _expectedSampleResultPart2
            ? $"{resultPart2} ✅"
            : $"{resultPart2} ❌ ({_expectedSampleResultPart2})";
        SampleResultPart2Took = stopwatchPart2.ElapsedMilliseconds;
    }

    private void SolveInput()
    {
        var filename = GetInputFilename();
        var input = GetInputFromFile(filename);

        // part 1...
        var stopwatchPart1 = Stopwatch.StartNew();
        _solver.Init(input);
        var resultPart1 = _solver.SolvePart1();
        stopwatchPart1.Stop();

        InputResultPart1 = resultPart1;
        InputResultPart1Took = stopwatchPart1.ElapsedMilliseconds;

        // part 2...
        var stopwatchPart2 = Stopwatch.StartNew();
        _solver.Init(input);
        var resultPart2 = _solver.SolvePart2();
        stopwatchPart2.Stop();

        InputResultPart2 = resultPart2;
        InputResultPart2Took = stopwatchPart2.ElapsedMilliseconds;
    }
}
