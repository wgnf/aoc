namespace AdventOfCode.Common.Utils;

public static class IntegerExtensions
{
    extension(int value)
    {
        public int Abs()
        {
            return Math.Abs(value);
        }

        public int Mod(int mod)
        {
            var result = value % mod;
        
            /*
         * modulo has a weird behavior for negative values. So we have to do this.
         * example:
         * mod = 10
         * value = -5
         *
         * -> result = -5
         * ,but it should be: +5
         * so we have to add mod (10) -> 5
         */
            return result < 0 ? result + mod : result;
        }
    }
}
