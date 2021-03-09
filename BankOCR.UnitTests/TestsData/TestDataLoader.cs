using System.IO;
using System.Threading.Tasks;

namespace BankOcr.UnitTests.TestsData
{
    public static class TestDataLoader
    {
        public static Task<string> LoadTestData(string testDataName) =>
            File.ReadAllTextAsync(Path.Join(Directory.GetCurrentDirectory(), "TestsData", $"{testDataName}.txt"));
    }
}