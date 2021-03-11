using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BankOCR.Domain.ValueObjects;

namespace BankOCR.Repositories
{
    public interface IAccountsFileRepository
    {
        Task<string> LoadAccountsScan(string filePath);
        Task SaveAccountsToFile(string filePath, IEnumerable<OcrAccount> ocrAccounts);
    }

    public class AccountsFileRepository : IAccountsFileRepository
    {
        public Task<string> LoadAccountsScan(string filePath) => 
            File.ReadAllTextAsync(filePath);

        public Task SaveAccountsToFile(string filePath, IEnumerable<OcrAccount> ocrAccounts)
        {
            return Task.CompletedTask;
        }
    }
}