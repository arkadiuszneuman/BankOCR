using System.Collections.Generic;
using System.Linq;
using BankOCR.Services;
using BankOCR.Services.Possibilities;
using NUnit.Framework;

namespace BankOcr.UnitTests.Services.Possibilities
{
    public class SignPossibilitiesFinderTests : BaseUnitTest
    {
        [TestCase(" _ " +
                  "| |" +
                  "|_|", ExpectedResult = new[] { '8' })]
        [TestCase(" _ " +
                  "|_ " +
                  " _|", ExpectedResult = new[] { '9', '6' })]
        public IEnumerable<char> FindOtherPossibilitiesOfASign_SimpleValues_Found(string digitalSign)
        {
            var sut = new SignPossibilitiesFinder(new DigitParserService());
            return sut.FindOtherPossibilitiesOfASign(digitalSign);
        }
    }
}