using System.Linq;

namespace BankOCR.Services
{
    public class AccountValidatorService
    {
        public bool IsAccountValid(string accountNumber)
        {
            if (accountNumber.Length != 9)
                return false;

            var sum = 0;
            for (int i = 0; i < accountNumber.Length; i++)
            {
                var charNumber = accountNumber[i];
                if (!char.IsDigit(charNumber))
                    return false;

                sum += (int)char.GetNumericValue(charNumber) * (9 - i);
            }

            var checksum = sum % 11;

            return checksum == 0;
        }
    }
}