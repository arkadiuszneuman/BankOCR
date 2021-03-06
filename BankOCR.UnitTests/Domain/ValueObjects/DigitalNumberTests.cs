using System;
using System.IO;
using System.Threading.Tasks;
using BankOCR.Domain.ValueObjects;
using BankOCR.Exceptions;
using BankOCR.Extensions;
using BankOcr.UnitTests.TestsData;
using FluentAssertions;
using NUnit.Framework;

namespace BankOcr.UnitTests.Domain.ValueObjects
{
    public class DigitalNumberTests : BaseUnitTest
    {
        [TestCase("More_than_3_lines")]
        [TestCase("Less_than_3_lines")]
        public async Task Create_MoreOrLessThan3Lines_ExceptionThrown(string testDataName)
        {
            var testData = (await TestDataLoader.LoadTestData(Path.Join("DigitalAccount", testDataName))).SplitByNewLine();

            Action result = () => DigitalNumber.Create(testData);

            result.Should().ThrowExactly<DigitalNumberDoesNotHave3LinesException>()
                .And.DigitalNumber.Should().BeEquivalentTo(testData);
        }

        [TestCase("First_line_longer")]
        [TestCase("Second_line_longer")]
        [TestCase("Third_line_longer")]
        public async Task Create_LinesLenghtDifferent_ExceptionThrown(string testDataName)
        {
            var testData = (await TestDataLoader.LoadTestData(Path.Join("DigitalAccount", testDataName))).SplitByNewLine();

            Action result = () => DigitalNumber.Create(testData);

            result.Should().ThrowExactly<DigitalNumberDoesNotHaveSameLenghtForEveryLineException>()
                .And.DigitalNumber.Should().BeEquivalentTo(testData);
        }

        [TestCase("7_signs")]
        [TestCase("8_signs")]
        public async Task Create_LenghtNotDivisibleBy3_ExceptionThrown(string testDataName)
        {
            var testData = (await TestDataLoader.LoadTestData(Path.Join("DigitalAccount", testDataName))).SplitByNewLine();

            Action result = () => DigitalNumber.Create(testData);

            result.Should().ThrowExactly<DigitalNumberLenghtIsNotDivisibleBy3Exception>()
                .And.DigitalNumber.Should().BeEquivalentTo(testData);
        }

        [Test]
        public async Task Create_SimpleValues_CreatesNewInstance()
        {
            var testData = (await TestDataLoader.LoadTestData("01")).SplitByNewLine();

            var result = DigitalNumber.Create(testData);

            result.Number.Should().BeEquivalentTo(testData);
            result.Digits.Should().BeEquivalentTo(
                " _ " +
                "| |" +
                "|_|",
                "   " +
                "  |" +
                "  |"
            );
        }
    }
}