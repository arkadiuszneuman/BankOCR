using System.Collections.Generic;
using System.Linq;
using BankOCR.Domain.ValueObjects;

namespace BankOCR.Services.Possibilities
{
    public class AccountPossibilitiesFinderService
    {
        private readonly ISignPossibilitiesFinder _signPossibilitiesFinder;
        private readonly IAccountValidatorService _accountValidatorService;

        public AccountPossibilitiesFinderService(ISignPossibilitiesFinder signPossibilitiesFinder,
            IAccountValidatorService accountValidatorService)
        {
            _signPossibilitiesFinder = signPossibilitiesFinder;
            _accountValidatorService = accountValidatorService;
        }

        public IEnumerable<ValidatedAccount> FindPossibilities(ParsedAccount parsedAccount)
        {
            var foundPossibilities = new HashSet<ValidatedAccount>();

            var validatedParsedAccount = _accountValidatorService.ValidateParsedAccount(parsedAccount.Number);
            if (validatedParsedAccount.AccountValidationResult == ParsedAccountStatus.None)
                foundPossibilities.Add(validatedParsedAccount);

            var possibleDigits = GetAllPossibilitiesOfEveryDigitalNumber(parsedAccount).ToArray();
            var possibleAccountNumbers = CreatePossibleAccountNumbers(parsedAccount.Number, possibleDigits).ToList();

            foreach (var possibleAccountNumber in possibleAccountNumbers)
            {
                var validatedAccount = _accountValidatorService.ValidateParsedAccount(possibleAccountNumber);
                if (validatedAccount.AccountValidationResult == ParsedAccountStatus.None)
                    foundPossibilities.Add(validatedAccount);
            }

            return foundPossibilities;
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