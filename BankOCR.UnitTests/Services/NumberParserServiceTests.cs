using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BankOCR.Services;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace BankOcr.UnitTests.Services
{
    public class NumberParserServiceTests : BaseUnitTest<NumberParserService>
    {
        [Test]
        public async Task ParseNumber_From_0_to_9_Group_by_digit()
        {
            var receivedDigits = new List<string>();
            var digitParserServiceMock = Mock<IDigitParserService>();
            digitParserServiceMock
                .ParseDigit(default)
                .ReturnsForAnyArgs(1)
                .AndDoes(x => receivedDigits.Add(x.Arg<string>()));
            
            var data = await File.ReadAllTextAsync(Path.Join(Directory.GetCurrentDirectory(), "TestsData", "0123456789.txt"));
            
            var result = Sut.ParseNumber(data);

            receivedDigits.Should().ContainInOrder(
                " _ " +
                "| |" +
                "|_|",
                "   " +
                "  |" +
                "  |",
                " _ " +
                " _|" +
                "|_ ", 
                " _ " +
                " _|" +
                " _|", 
                "   " +
                "|_|" +
                "  |", 
                " _ " +
                "|_ " +
                " _|", 
                " _ " +
                "|_ " +
                "|_|", 
                " _ " +
                "  |" +
                "  |", 
                " _ " +
                "|_|" +
                "|_|", 
                " _ " +
                "|_|" +
                " _|");

            result.Should().Be("1111111111");
        }
    }
}