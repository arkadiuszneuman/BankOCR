namespace BankOCR.Exceptions
{
    public class InvalidLengthOfAccountException : BaseBankOcrException
    {
        public string AccountNumber { get; }

        public InvalidLengthOfAccountException(string accountNumber) : base($"Invalid lenght of account {accountNumber}")
        {
            AccountNumber = accountNumber;
        }
    }
}