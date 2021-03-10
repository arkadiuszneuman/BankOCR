namespace BankOCR.Exceptions
{
    public class DigitalNumberLenghtIsNotDivisibleBy3Exception : BaseBankOcrException
    {
        public string DigitalNumber { get; }

        public DigitalNumberLenghtIsNotDivisibleBy3Exception(string digitalNumber) : base("Digital number should have every line divisible by 3")
        {
            DigitalNumber = digitalNumber;
        }
    }
}