using System.Collections.Generic;
using System.Text;
using BankOCR.Domain.ValueObjects;

namespace BankOCR.Services
{
    public interface INumberParserService
    {
        ParsedAccount ParseNumber(DigitalNumber digitalNumber);
    }

    public class NumberParserService : INumberParserService
    {
        private readonly IDigitParserService _digitParserService;

        public NumberParserService(IDigitParserService digitParserService)
        {
            _digitParserService = digitParserService;
        }

        public ParsedAccount ParseNumber(DigitalNumber digitalNumber)
        {
            var accountNumber = ParseAccountNumber(digitalNumber.Digits);

            return ParsedAccount.Create(digitalNumber, accountNumber);
        }

        private string ParseAccountNumber(IEnumerable<string> digits)
        {
            var sb = new StringBuilder();
            foreach (var digit in digits)
            {
                var parsedDigit = _digitParserService.ParseDigit(digit);
                if (parsedDigit == null)
                    sb.Append("?");
                else
                    sb.Append(parsedDigit.Value);
            }

            return sb.ToString();
        }
    }
}