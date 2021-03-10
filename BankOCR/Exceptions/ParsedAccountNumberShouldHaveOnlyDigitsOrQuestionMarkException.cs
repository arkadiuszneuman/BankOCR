namespace BankOCR.Exceptions
{
    public class ParsedAccountNumberShouldHaveOnlyDigitsOrQuestionMarkException : BaseBankOcrException
    {
        public string AccountNumber { get; }

        public ParsedAccountNumberShouldHaveOnlyDigitsOrQuestionMarkException(string accountNumber)
            : base("Parsed account number should have only digits or question mark")
        {
            AccountNumber = accountNumber;
        }
    }
}