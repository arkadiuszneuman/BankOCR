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

        public Task SaveAccountsToFile(string filePath, IEnumerable<OcrAccount> ocrAccounts, CancellationToken cancellationToken) =>
            File.WriteAllTextAsync(filePath, string.Join(Environment.NewLine, ocrAccounts), cancellationToken);
    }
}