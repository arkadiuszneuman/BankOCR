using System.Linq;
using System.Threading.Tasks;
using BankOCR.Domain.ValueObjects;
using BankOCR.Extensions;
using BankOCR.Services;
using BankOCR.Services.Possibilities;
using BankOcr.UnitTests.TestsData;
using FluentAssertions;
using NUnit.Framework;

namespace BankOcr.UnitTests.Services.Possibilities
{
    public class AccountPossibilitiesFinderServiceTests : BaseUnitTest
    {
        [Test]
        public async Task FindPossibilities_AccountNumber88888888_Found4Possibilities()
        {
            var data = (await TestDataLoader.LoadTestData("888888888")).SplitByNewLine();

            var sut = new AccountPossibilitiesFinderService(new SignPossibilitiesFinder(new DigitParserService()), new AccountValidatorService());
            var parsedAccount = ParsedAccount.Create(DigitalNumber.Create(data), "888888888");

            var possibilities = sut.FindPossibilities(parsedAccount);

            possibilities.Possibilities.Should().BeEquivalentTo("888886888", "888888880", "888888988");
        }
    }
}