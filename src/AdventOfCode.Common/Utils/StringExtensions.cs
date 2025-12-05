namespace AdventOfCode.Common.Utils;

public static class StringExtensions
{
    extension(string input)
    {
        public string[] SplitTrimRemoveEmpty(char splitChar)
        {
            return input.Split(splitChar, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        }

        public string[] SplitTrimRemoveEmpty(string splitChar)
        {
            return input.Split(splitChar, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        }

        public IEnumerable<string> SplitChunk(int chunkSize)
        {
            if (input.Length % chunkSize != 0)
            {
                throw new ArgumentException("The length of the input string must be divisible by the chunk size.");
            }
        
            return Enumerable
                .Range(0, input.Length / chunkSize)
                .Select(i => input
                    .Substring(i * chunkSize, chunkSize));
        }
    }
}
