using System;
using System.Threading.Tasks;
using BankOCR.Domain.ValueObjects;
using BankOCR.Exceptions;
using FluentAssertions;
using NUnit.Framework;

namespace BankOcr.UnitTests.Domain.ValueObjects
{
    public class ParsedAccountTests : BaseUnitTest
    {
        [TestCase("1")]
        [TestCase("123")]
        [TestCase("1?3")]
        public void Create_SimpleValues_CreatesNewInstance(string accountNumber)
        {
            var digitalNumber = DigitalNumber.Create(" _ \r\n" +
                                                     "| |\r\n" +
                                                     "|_|");
            var result = ParsedAccount.Create(digitalNumber, accountNumber);

            result.DigitalNumber.Should().Be(digitalNumber);
            result.Number.Should().Be(accountNumber);
        }
        
        [TestCase("a")]
        [TestCase("1a3")]
        public void Create_InvalidValues_ThrowsException(string accountNumber)
        {
            Action result = () => ParsedAccount.Create(DigitalNumber.Create(" _ \r\n" +
                                                                            "| |\r\n" +
                                                                            "|_|"), accountNumber);

            result.Should().ThrowExactly<ParsedAccountNumberShouldHaveOnlyDigitsOrQuestionMarkException>()
                .And.AccountNumber.Should().Be(accountNumber);
        }
    }
}