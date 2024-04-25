using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ISSProject_Regenerated.SubscriptionServiceBackend.CreditCards
{
    internal class CreditCardValidatorService : ICreditCardValidatorService
    {
        public CreditCardValidatorService()
        {
        }
        public bool ValidCVV(string cvv)
        {
            string cvvPattern = @"^\d{3}$";
            Regex regexCVV = new Regex(cvvPattern);
            MatchCollection matchesCVV = regexCVV.Matches(cvv);
            return matchesCVV.Count > 0;
        }

        public bool ValidExpirationDate(string expirationDate)
        {
            string[] dateComponent = expirationDate.Split('/');
            if (dateComponent.Length == 1)
            {
                return false;
            }
            int month, year;

            var successfullyParsedMonth = int.TryParse(dateComponent[0], out month);
            var successfullyParsedYear = int.TryParse(dateComponent[1], out year);
            return successfullyParsedMonth && successfullyParsedYear && dateComponent[0].Length == 2 && dateComponent[1].Length == 2 && month <= 12 && year >= 25;
        }

        public bool ValidCreditCardNumber(string creditCardNumber)
        {
            string numberPattern = @"^(?:\d[ -]*?){16}$";
            Regex regexCard = new Regex(numberPattern);
            MatchCollection matchesCardNumber = regexCard.Matches(creditCardNumber);
            return matchesCardNumber.Count > 0;
        }
    }
}
