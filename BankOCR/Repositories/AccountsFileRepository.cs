using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using BankOCR.Domain.ValueObjects;

namespace BankOCR.Repositories
{
    public interface IAccountsFileRepository
    {
        Task<string> LoadAccountsScan(string filePath, CancellationToken cancellationToken = default);
        Task SaveAccountsToFile(string filePath, IEnumerable<OcrAccount> ocrAccounts, CancellationToken cancellationToken = default);
    }

    public class AccountsFileRepository : IAccountsFileRepository
    {
        public Task<string> LoadAccountsScan(string filePath, CancellationToken cancellationToken) =>
            File.ReadAllTextAsync(filePath, cancellationToken);

        public async Task SaveAccountsToFile(string filePath, IEnumerable<OcrAccount> ocrAccounts, CancellationToken cancellationToken)
        {
            var dirPath = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(dirPath) && !Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);
            
            await File.WriteAllTextAsync(filePath, string.Join(Environment.NewLine, ocrAccounts), cancellationToken);
        }
    }
}