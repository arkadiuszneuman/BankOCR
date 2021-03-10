using System.Collections.Generic;
using System.Linq;
using System.Text;
using BankOCR.Domain.ValueObjects;
using BankOCR.Extensions;

namespace BankOCR.Services
{
    public class NumberParserService
    {
        private readonly IDigitParserService _digitParserService;

        public NumberParserService(IDigitParserService digitParserService)
        {
            _digitParserService = digitParserService;
        }

        public ParsedAccount ParseNumber(DigitalNumber digitalNumber)
        {
            var lines = digitalNumber.Number.SplitByNewLine();
            var digits = GroupByDigits(lines).ToList();

            var accountNumber = ParseAccountNumber(digits);

            return ParsedAccount.Create(digitalNumber, accountNumber);
        }

        private string ParseAccountNumber(List<string> digits)
        {
            var sb = new StringBuilder();
            foreach (var digit in digits)
            {
                sb.Append(_digitParserService.ParseDigit(digit) ?? '?');
            }

            return sb.ToString();
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