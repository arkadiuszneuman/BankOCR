using BankOCR.Services;
using NUnit.Framework;

namespace BankOcr.UnitTests.Services
{
    public class AccountValidatorTests : BaseUnitTest<AccountValidator>
    {
        [TestCase("457508000", ExpectedResult = true)]
        [TestCase("457508?00", ExpectedResult = false)]
        [TestCase("01234567", ExpectedResult = false)]
        [TestCase("0123456789", ExpectedResult = false)]
        public bool IsAccountValid_AccountNumber_CheckIsValid(string accountNumber)
        {
            return Sut.IsAccountValid(accountNumber);
        }
    }
}