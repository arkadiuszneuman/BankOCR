using BankOCR.Exceptions;
using BankOCR.Extensions;

namespace BankOCR.Domain.ValueObjects
{
    public class DigitalNumber : ValueObject
    {
        public string Number { get; }

        private DigitalNumber(string accountNumber)
        {
            Number = accountNumber;
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

            return new DigitalNumber(digitalAccountNumber);
        }
    }
}