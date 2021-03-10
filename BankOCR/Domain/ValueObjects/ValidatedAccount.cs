using BankOCR.Exceptions;

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
        public ParsedAccount ParsedAccount { get; }
        public ParsedAccountStatus AccountValidationResult { get; }
        
        public ValidatedAccount(ParsedAccount parsedAccount, ParsedAccountStatus accountValidationResult = ParsedAccountStatus.None)
        {
            ParsedAccount = parsedAccount;
            AccountValidationResult = accountValidationResult;
        }

        public override string ToString() => $"{ParsedAccount.Number}{GetStatus(AccountValidationResult)}";

        private string GetStatus(ParsedAccountStatus accountValidationResult) =>
            accountValidationResult switch
            {
                ParsedAccountStatus.ChecksumError => " ERR",
                ParsedAccountStatus.IllegalCharacter => " ILL",
                _ => string.Empty
            };
    }
}