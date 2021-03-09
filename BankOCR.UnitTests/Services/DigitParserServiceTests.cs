using BankOCR.Services;
using FluentAssertions;
using NUnit.Framework;

namespace BankOcr.UnitTests.Services
{
    public class DigitParserServiceTests
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

        public int? ParseDigit_simpleValues_Parsed(string digit)
        {
            var sut = new DigitParserService();
            return sut.ParseDigit(digit);
        }
    }
}