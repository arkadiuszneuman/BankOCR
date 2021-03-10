namespace BankOCR.Domain.ValueObjects
{
    public enum AccountValidationResult
    {
        InvalidAccountLenght,
        ChecksumError,
        IllegalCharacter,
        Success
    }
    
    public class ValidatedAccount : ValueObject
    {
        public ParsedAccount ParsedAccount { get; }
        public AccountValidationResult AccountValidationResult { get; }
        
        public ValidatedAccount(ParsedAccount parsedAccount, AccountValidationResult accountValidationResult)
        {
            ParsedAccount = parsedAccount;
            AccountValidationResult = accountValidationResult;
        }
    }
}