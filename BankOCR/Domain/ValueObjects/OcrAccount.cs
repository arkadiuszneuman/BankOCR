using System.Collections.Generic;
using System.Linq;
using BankOCR.Exceptions;

namespace BankOCR.Domain.ValueObjects
{
    public class OcrAccount
    {
        public ValidatedAccount OriginalAccount { get; }
        public IReadOnlyList<string> Possibilities { get; }

        private OcrAccount(ValidatedAccount originalAccount, IReadOnlyList<string> possibilities = null)
        {
            OriginalAccount = originalAccount;
            Possibilities = possibilities ?? new List<string>();
        }

        public static OcrAccount Create(ValidatedAccount originalAccount, IReadOnlyList<string> possibilities = null)
        {
            if (originalAccount.AccountValidationResult == ParsedAccountStatus.None &&
                possibilities != null)
            {
                throw new PossibilitiesCanOnlyExistsWhenAccountHasValidationError();
            }

            return new OcrAccount(originalAccount, possibilities);
        }

        public override string ToString()
        {
            if (OriginalAccount.AccountValidationResult == ParsedAccountStatus.None)
                return OriginalAccount.ToString();

            if (Possibilities.Count == 1)
                return Possibilities.Single();

            return $"{OriginalAccount.AccountNumber} AMB [{string.Join(", ", Possibilities.Select(p => $"'{p}'"))}]";
        }
    }
}