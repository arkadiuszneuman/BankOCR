namespace BankOCR.Services
{
    public class AccountValidator
    {
        public bool IsAccountValid(string accountNumber)
        {
            if (accountNumber.Length != 9)
                return false;
            
            return true;
        }
    }
}