using BankOCR.Services;
using FluentAssertions;
using NUnit.Framework;

namespace BankOcr.UnitTests.Services
{
    public class DigitParserServiceTests : BaseUnitTest<DigitParserService>
    {
        [TestCase(" _ " +
                  "| |" +
                  "|_|", ExpectedResult = 0)]
        [TestCase("   " +
                  "  |" +
                  "  |", ExpectedResult = 1)]
        [TestCase(" _ " +
                  " _|" +
                  "|_ ", ExpectedResult = 2)]
        [TestCase(" _ " +
                  " _|" +
                  " _|", ExpectedResult = 3)]
        [TestCase("   " +
                  "|_|" +
                  "  |", ExpectedResult = 4)]
        [TestCase(" _ " +
                  "|_ " +
                  " _|", ExpectedResult = 5)]
        [TestCase(" _ " +
                  "|_ " +
                  "|_|", ExpectedResult = 6)]
        [TestCase(" _ " +
                  "  |" +
                  "  |", ExpectedResult = 7)]
        [TestCase(" _ " +
                  "|_|" +
                  "|_|", ExpectedResult = 8)]
        [TestCase(" _ " +
                  "|_|" +
                  " _|", ExpectedResult = 9)]
        [TestCase(" _ " +
                  "|_|" +
                  " _ ", ExpectedResult = null)]

        public int? ParseDigit_SimpleValues_Parsed(string digit)
        {
            return Sut.ParseDigit(digit);
        }
    }
}