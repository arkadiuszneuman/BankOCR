using System.Collections.Generic;
using System.Linq;
using System.Text;
using BankOCR.Exceptions;
using BankOCR.Extensions;

namespace BankOCR.Domain.ValueObjects
{
    public class DigitalNumber : ValueObject
    {
        public string Number { get; }
        public string[] Digits { get; }

        private DigitalNumber(string accountNumber, string[] digits)
        {
            Number = accountNumber;
            Digits = digits;
        }

        public static DigitalNumber Create(string digitalAccountNumber)
        {
            var splittedNumber = digitalAccountNumber.SplitByNewLine();

            if (splittedNumber.Length != 3)
                throw new DigitalNumberDoesNotHave3LinesException(digitalAccountNumber);

            if (splittedNumber[0].Length != splittedNumber[1].Length ||
                splittedNumber[0].Length != splittedNumber[2].Length)
                throw new DigitalNumberDoesNotHaveSameLenghtForEveryLineException(digitalAccountNumber);

            if (splittedNumber[0].Length % 3 != 0)
                throw new DigitalNumberLenghtIsNotDivisibleBy3Exception(digitalAccountNumber);

            return new DigitalNumber(digitalAccountNumber, GroupByDigits(splittedNumber).ToArray());
        }
        
        private static IEnumerable<string> GroupByDigits(string[] lines)
        {
            var firstLine = lines.First();

            for (var i = 0; i < firstLine.Length; i += 3)
            {
                var sb = new StringBuilder();
                for (var y = 0; y < 3; y++)
                    sb.Append(string.Concat(lines[y].Skip(i).Take(3)));

                yield return sb.ToString();
            }
        }
    }
}