using System.Linq;

namespace BankOCR.Extensions
{
    public static class StringExtensions
    {
        public static string[] SplitByNewLine(this string stringToSplit) =>
            stringToSplit
                .Split("\n")
                .Select(x => x.Trim('\r'))
                .ToArray();
    }
}