namespace BankOCR.Exceptions
{
    public class DigitalNumberLenghtIsNotDivisibleBy3 : BaseBankOcrException
    {
        public string DigitalNumber { get; }

        public DigitalNumberLenghtIsNotDivisibleBy3(string digitalNumber) : base("Digital number should have every line divisible by 3")
        {
            DigitalNumber = digitalNumber;
        }
    }
}