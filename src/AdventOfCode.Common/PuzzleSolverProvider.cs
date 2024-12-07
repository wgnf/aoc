using System.Reflection;
using Microsoft.Extensions.Logging;

namespace AdventOfCode.Common;

internal sealed class PuzzleSolverProvider : IPuzzleSolverProvider
{
    private readonly ILogger<PuzzleSolverProvider> _logger;
    private readonly Lazy<IEnumerable<AdventOfCodeYear>> _yearsProvider;

    public PuzzleSolverProvider(ILogger<PuzzleSolverProvider> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        _yearsProvider = new Lazy<IEnumerable<AdventOfCodeYear>>(RetrieveAllYears);
    }

    public IEnumerable<AdventOfCodeYear> GetAllYears()
    {
        return _yearsProvider.Value;
    }

    private List<AdventOfCodeYear> RetrieveAllYears()
    {
        var executingAssembly = Assembly.GetExecutingAssembly();
        var executingAssemblyDirectory = Path.GetDirectoryName(executingAssembly.Location);
        var currentDirectory = new DirectoryInfo(executingAssemblyDirectory!);

        var yearAssemblies = currentDirectory
            .EnumerateFiles("AdventOfCode.*.dll")
            .Where(file => !file.Name.Contains("Common"))
            .Where(file => !file.Name.Contains("Application"));

        var years = new List<AdventOfCodeYear>();
        foreach (var yearAssembly in yearAssemblies)
        {
            var year = GetYear(Path.GetFileNameWithoutExtension(yearAssembly.FullName));
            if (year != null)
            {
                years.Add(year);
            }
        }
        return years;
    }

    private AdventOfCodeYear? GetYear(string assembly)
    {
        try
        {
            var yearAssembly = Assembly.Load(assembly);
            var extractedYear = int.Parse(assembly.Split('.')[1]);

            var puzzleDays = yearAssembly
                .GetTypes()
                .Where(type => type.IsAssignableTo(typeof(IPuzzleSolver)))
                .Select(type =>
                {
                    if (Activator.CreateInstance(type) is not IPuzzleSolver daySolver)
                    {
                        return null;
                    }

                    var puzzleDayAttribute = type.GetCustomAttribute<PuzzleAttribute>();
                    if (puzzleDayAttribute == null)
                    {
                        return null;
                    }

                    var puzzleDay = new Puzzle(
                        extractedYear,
                        puzzleDayAttribute.Day,
                        puzzleDayAttribute.Title,
                        puzzleDayAttribute.ExpectedSampleResultPart1,
                        puzzleDayAttribute.ExpectedSampleResultPart2,
                        daySolver);
                    return puzzleDay;
                })
                .Where(puzzleDay => puzzleDay != null)
                .Cast<Puzzle>()
                .OrderBy(puzzleDay => puzzleDay.Day);

            var year = new AdventOfCodeYear(extractedYear, puzzleDays.ToList());
            return year;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Unable to create year from assembly '{Assembly}'", assembly);
            return null;
        }
    }
}
