namespace BankOCR.Domain.ValueObjects
{
    public enum ParsedAccountStatus
    {
        None,
        ChecksumError,
        IllegalCharacter
    }

    public class ValidatedAccount : ValueObject
    {
        public string AccountNumber { get; }
        public ParsedAccountStatus AccountValidationResult { get; }

        public ValidatedAccount(string accountNumber, ParsedAccountStatus accountValidationResult = ParsedAccountStatus.None)
        {
            AccountNumber = accountNumber;
            AccountValidationResult = accountValidationResult;
        }

        public override string ToString() => $"{AccountNumber}{GetStatus(AccountValidationResult)}";

        private string GetStatus(ParsedAccountStatus accountValidationResult) =>
            accountValidationResult switch
            {
                ParsedAccountStatus.ChecksumError => " ERR",
                ParsedAccountStatus.IllegalCharacter => " ILL",
                _ => string.Empty
            };
    }
}