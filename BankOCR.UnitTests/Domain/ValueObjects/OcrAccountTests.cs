using System;
using System.Collections.Generic;
using BankOCR.Domain.ValueObjects;
using BankOCR.Exceptions;
using FluentAssertions;
using NUnit.Framework;

namespace BankOcr.UnitTests.Domain.ValueObjects
{
    public class OcrAccountTests : BaseUnitTest
    {
        [Test]
        public void Create_ParsedAccountStatusNone_StringCreated()
        {
            var accountNumber = "123456789";
            var validatedAccount = new ValidatedAccount(accountNumber, ParsedAccountStatus.None);
            var ocrAccount = OcrAccount.Create(validatedAccount);

            ocrAccount.ToString().Should().Be("123456789");
        }
        
        [TestCase(ParsedAccountStatus.ChecksumError)]
        [TestCase(ParsedAccountStatus.IllegalCharacter)]
        public void Create_ParsedAccountStatusInvalidAndManyPossibilities_StringCreatedWithPossibilities(ParsedAccountStatus status)
        {
            var accountNumber = "123456789";
            var validatedAccount = new ValidatedAccount(accountNumber, status);
            var ocrAccount = OcrAccount.Create(validatedAccount, new []{ "123456780", "123456788" });

            ocrAccount.ToString().Should().Be("123456789 AMB ['123456780', '123456788']");
        }
        
        [TestCase(ParsedAccountStatus.ChecksumError)]
        [TestCase(ParsedAccountStatus.IllegalCharacter)]
        public void Create_ParsedAccountStatusInvalidAndOnePossibility_StringCreatedWithPossibilities(ParsedAccountStatus status)
        {
            var accountNumber = "123456789";
            var validatedAccount = new ValidatedAccount(accountNumber, status);
            var ocrAccount = OcrAccount.Create(validatedAccount, new []{ "123456780" });

            ocrAccount.ToString().Should().Be("123456780");
        }

        [Test]
        public void Create_ParsedAccountStatusNoneAndHasPossibilities_ThrowsException()
        {
            var accountNumber = "123456789";
            var validatedAccount = new ValidatedAccount(accountNumber, ParsedAccountStatus.None);
            Action result = () => OcrAccount.Create(validatedAccount, new []{ "123456780" });

            result.Should().ThrowExactly<PossibilitiesCanOnlyExistsWhenAccountHasValidationError>();
        }
    }
}