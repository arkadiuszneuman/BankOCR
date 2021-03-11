using System.Collections.Generic;
using System.Linq;
using BankOCR.Domain.ValueObjects;

namespace BankOCR.Services.Possibilities
{
    public interface IAccountPossibilitiesFinderService
    {
        OcrAccount FindPossibilities(ParsedAccount parsedAccount);
    }

    public class AccountPossibilitiesFinderService : IAccountPossibilitiesFinderService
    {
        private readonly ISignPossibilitiesFinder _signPossibilitiesFinder;
        private readonly IAccountValidatorService _accountValidatorService;

        public AccountPossibilitiesFinderService(ISignPossibilitiesFinder signPossibilitiesFinder,
            IAccountValidatorService accountValidatorService)
        {
            _signPossibilitiesFinder = signPossibilitiesFinder;
            _accountValidatorService = accountValidatorService;
        }

        public OcrAccount FindPossibilities(ParsedAccount parsedAccount)
        {
            var validatedParsedAccount = _accountValidatorService.ValidateAccountNumber(parsedAccount.Number);
            if (validatedParsedAccount.AccountValidationResult == ParsedAccountStatus.None)
                return OcrAccount.Create(validatedParsedAccount);

            var possibleDigits = GetAllPossibilitiesOfEveryDigitalNumber(parsedAccount).ToArray();
            var possibleAccountNumbers = CreatePossibleAccountNumbers(parsedAccount.Number, possibleDigits).ToList();

            var foundPossibilities = new HashSet<string>();
            
            foreach (var possibleAccountNumber in possibleAccountNumbers)
            {
                var validatedAccount = _accountValidatorService.ValidateAccountNumber(possibleAccountNumber);
                if (validatedAccount.AccountValidationResult == ParsedAccountStatus.None)
                    foundPossibilities.Add(validatedAccount.AccountNumber);
            }

            return OcrAccount.Create(validatedParsedAccount, foundPossibilities.ToList());
        }

        private IEnumerable<IReadOnlyList<char>> GetAllPossibilitiesOfEveryDigitalNumber(ParsedAccount parsedAccount)
        {
            foreach (var digit in parsedAccount.DigitalNumber.Digits)
                yield return _signPossibilitiesFinder.FindOtherPossibilitiesOfASign(digit).ToList();
        }

        private static IEnumerable<string> CreatePossibleAccountNumbers(string parsedAccountNumber, IReadOnlyList<char>[] listOfLists)
        {
            for (var i = 0; i < listOfLists.Length; i++)
            {
                foreach (var possibleChar in listOfLists[i])
                {
                    var accountNumberEnumerable = parsedAccountNumber.Take(i)
                        .Concat(new[] {possibleChar})
                        .Concat(parsedAccountNumber.Skip(i + 1).Take(listOfLists.Length - i));

                    yield return string.Concat(accountNumberEnumerable);
                }
            }
        }
    }
}