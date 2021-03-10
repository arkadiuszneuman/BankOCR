using BankOCR.Domain.ValueObjects;
using BankOCR.Services;
using NUnit.Framework;

namespace BankOcr.UnitTests.Services
{
    public class AccountValidatorTests : BaseUnitTest<AccountValidatorService>
    {
        [TestCase("457508000", ExpectedResult = AccountValidationResult.Success)]
        [TestCase("457508001", ExpectedResult = AccountValidationResult.ChecksumError)]
        [TestCase("457508?00", ExpectedResult = AccountValidationResult.IllegalCharacter)]
        [TestCase("01234567", ExpectedResult = AccountValidationResult.InvalidAccountLenght)]
        [TestCase("0123456789", ExpectedResult = AccountValidationResult.InvalidAccountLenght)]
        public AccountValidationResult IsAccountValid_AccountNumber_CheckIsValid(string accountNumber)
        {
            var digitalNumber = DigitalNumber.Create(" _ \r\n" +
                                                     "| |\r\n" +
                                                     "|_|");
            
            var parsedAccount = ParsedAccount.Create(digitalNumber, accountNumber);
            
            return Sut.ValidateParsedAccount(parsedAccount).AccountValidationResult;
        }
    }
}