namespace BankOCR.Exceptions
{
    public class DigitalNumberDoesNotHaveSameLenghtForEveryLineException : BaseBankOcrException
    {
        public string[] DigitalNumber { get; }

        public DigitalNumberDoesNotHaveSameLenghtForEveryLineException(string[] digitalNumber) 
            : base("Digital Account doesn't have same lenght for every line")
        {
            DigitalNumber = digitalNumber;
        }
    }
}