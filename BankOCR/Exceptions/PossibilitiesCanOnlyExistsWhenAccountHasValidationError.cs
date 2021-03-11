namespace BankOCR.Exceptions
{
    public class PossibilitiesCanOnlyExistsWhenAccountHasValidationError : BaseBankOcrException
    {
        public PossibilitiesCanOnlyExistsWhenAccountHasValidationError() : base("Possibilities can only exists when account has validation error")
        {
        }
    }
}