using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankOCR.Domain.ValueObjects;
using BankOCR.Extensions;
using BankOCR.Repositories;
using BankOCR.Services.Possibilities;

namespace BankOCR.Services
{
    public class OcrService
    {
        private readonly IAccountsFileRepository _accountsFileRepository;
        private readonly INumberParserService _numberParserService;
        private readonly IAccountPossibilitiesFinderService _accountPossibilitiesFinderService;

        public OcrService(IAccountsFileRepository accountsFileRepository,
            INumberParserService numberParserService,
            IAccountPossibilitiesFinderService accountPossibilitiesFinderService)
        {
            _accountsFileRepository = accountsFileRepository;
            _numberParserService = numberParserService;
            _accountPossibilitiesFinderService = accountPossibilitiesFinderService;
        }
        
        public async Task OcrFile(string filePath)
        {
            var loadedScan = await _accountsFileRepository.LoadAccountsScan(filePath);
            
            var loadedScansByAccountNumber = GroupByScannedAccountNumbers(loadedScan);
            var ocrAccounts = GetOcrAccounts(loadedScansByAccountNumber);

            await _accountsFileRepository.SaveAccountsToFile("results.txt", ocrAccounts);
        }

        private IEnumerable<OcrAccount> GetOcrAccounts(IEnumerable<string[]> loadedScansByAccountNumber)
        {
            foreach (var loadedScanByAccountNumber in loadedScansByAccountNumber)
            {
                var parsedAccount = _numberParserService.ParseNumber(DigitalNumber.Create(loadedScanByAccountNumber));
                yield return _accountPossibilitiesFinderService.FindPossibilities(parsedAccount);
            }
        }

        private static IEnumerable<string[]> GroupByScannedAccountNumbers(string loadedScan)
        {
            var lines = loadedScan.SplitByNewLine();
            for (int i = 0; i < lines.Length; i += 4)
            {
                yield return lines.Skip(i).Take(3).ToArray();
            }
        }
    }
}