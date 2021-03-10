using System.Linq;
using BankOCR.Exceptions;

namespace BankOCR.Domain.ValueObjects
{
    public class ParsedAccount : ValueObject
    {
        public DigitalNumber DigitalNumber { get; }
        public string Number { get; }

        private ParsedAccount(DigitalNumber digitalNumber, string accountNumber)
        {
            DigitalNumber = digitalNumber;
            Number = accountNumber;
        }

        public static ParsedAccount Create(DigitalNumber digitalNumber, string accountNumber)
        {
            if (accountNumber.Any(x => !char.IsDigit(x) || x != '?'))
                throw new ParsedAccountNumberShouldHaveOnlyDigitsOrQuestionMarkException(accountNumber);

            return new ParsedAccount(digitalNumber, accountNumber);
        }
    }
}