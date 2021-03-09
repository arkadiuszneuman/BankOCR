using BankOCR.Services;
using NUnit.Framework;

namespace BankOcr.UnitTests.Services
{
    public class AccountValidatorTests : BaseUnitTest<AccountValidator>
    {
        [TestCase("123456789", ExpectedResult = true)]
        [TestCase("012345678", ExpectedResult = true)]
        [TestCase("01234567", ExpectedResult = false)]
        [TestCase("0123456789", ExpectedResult = false)]
        public bool IsAccountValid_AccountNumber_CheckIsValid(string accountNumber)
        {
            return Sut.IsAccountValid(accountNumber);
        }
    }
}