using BankOCR.Domain.ValueObjects;
using FluentAssertions;
using NUnit.Framework;

namespace BankOcr.UnitTests.Domain.ValueObjects
{
    public class ValidatedAccountTests
    {
        [TestCase(ParsedAccountStatus.None, ExpectedResult = "123")]
        [TestCase(ParsedAccountStatus.ChecksumError, ExpectedResult = "123 ERR")]
        [TestCase(ParsedAccountStatus.IllegalCharacter, ExpectedResult = "123 ILL")]
        public string ToString_SimpleValues_StringCreated(ParsedAccountStatus parsedAccountStatus)
        {
            var digitalNumber = DigitalNumber.Create(" _ \r\n" +
                                                     "| |\r\n" +
                                                     "|_|");
            var parsedAccount = ParsedAccount.Create(digitalNumber, "123");
            var validatedAccount = new ValidatedAccount(parsedAccount, parsedAccountStatus);

            return validatedAccount.ToString();
        }
    }
}