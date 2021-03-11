using System;
using BankOCR.Domain.ValueObjects;
using BankOCR.Exceptions;
using BankOCR.Services;
using FluentAssertions;
using NUnit.Framework;

namespace BankOcr.UnitTests.Services
{
    public class AccountValidatorTests : BaseUnitTest<AccountValidatorService>
    {
        [TestCase("457508000", ExpectedResult = ParsedAccountStatus.None)]
        [TestCase("457508001", ExpectedResult = ParsedAccountStatus.ChecksumError)]
        [TestCase("457508?00", ExpectedResult = ParsedAccountStatus.IllegalCharacter)]
        public ParsedAccountStatus ValidateAccountNumber_AccountNumber_CheckIsValid(string accountNumber)
        {
            return Sut.ValidateAccountNumber(accountNumber).AccountValidationResult;
        }
        
        [TestCase("01234567")]
        [TestCase("0123456789")]
        public void ValidateAccountNumber_InvalidAccountNumberLength_CheckIsValid(string accountNumber)
        {
            Action result = () => Sut.ValidateAccountNumber(accountNumber);

            result.Should().ThrowExactly<InvalidLengthOfAccountException>();
        }
    }
}