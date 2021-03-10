using System;
using System.Linq;

namespace BankOCR.Extensions
{
    public static class StringExtensions
    {
        public static string[] SplitByNewLine(this string stringToSplit) =>
            stringToSplit
                .Split("\n", StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim('\r'))
                .ToArray();
    }
}