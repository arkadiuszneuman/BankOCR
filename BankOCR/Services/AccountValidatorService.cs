using BankOCR.Domain.ValueObjects;

namespace BankOCR.Services
{
    public class AccountValidatorService
    {
        public ValidatedAccount ValidateParsedAccount(ParsedAccount parsedAccount) =>
            new(parsedAccount, IsAccountValid(parsedAccount.Number));

        private AccountValidationResult IsAccountValid(string accountNumber)
        {
            if (accountNumber.Length != 9)
                return AccountValidationResult.InvalidAccountLenght;

            var sum = 0;
            for (var i = 0; i < accountNumber.Length; i++)
            {
                var charNumber = accountNumber[i];
                if (!char.IsDigit(charNumber))
                    return AccountValidationResult.IllegalCharacter;

                sum += (int)char.GetNumericValue(charNumber) * (9 - i);
            }

            var checksum = sum % 11;

            return checksum != 0 ? AccountValidationResult.ChecksumError : AccountValidationResult.Success;
        }
    }
}