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
            var validatedAccount = new ValidatedAccount("123", parsedAccountStatus);

            return validatedAccount.ToString();
        }
    }
}