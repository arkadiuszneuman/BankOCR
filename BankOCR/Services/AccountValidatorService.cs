using BankOCR.Domain.ValueObjects;
using BankOCR.Exceptions;

namespace BankOCR.Services
{
    public interface IAccountValidatorService
    {
        ValidatedAccount ValidateParsedAccount(string accountNumber);
    }

    public class AccountValidatorService : IAccountValidatorService
    {
        public ValidatedAccount ValidateParsedAccount(string accountNumber) =>
            new(accountNumber, IsAccountValid(accountNumber));

        private ParsedAccountStatus IsAccountValid(string accountNumber)
        {
            if (accountNumber.Length != 9)
                throw new InvalidLengthOfAccountException(accountNumber);

            var sum = 0;
            for (var i = 0; i < accountNumber.Length; i++)
            {
                var charNumber = accountNumber[i];
                if (!char.IsDigit(charNumber))
                    return ParsedAccountStatus.IllegalCharacter;

                sum += (int)char.GetNumericValue(charNumber) * (9 - i);
            }

            var checksum = sum % 11;

            return checksum != 0 ? ParsedAccountStatus.ChecksumError : ParsedAccountStatus.None;
        }
    }
}