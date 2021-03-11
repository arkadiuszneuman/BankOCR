namespace BankOCR.Exceptions
{
    public class DigitalNumberDoesNotHave3LinesException : BaseBankOcrException
    {
        public string[] DigitalNumber { get; }

        public DigitalNumberDoesNotHave3LinesException(string[] digitalNumber) : base("Digital account should have 3 lines")
        {
            DigitalNumber = digitalNumber;
        }
    }
}