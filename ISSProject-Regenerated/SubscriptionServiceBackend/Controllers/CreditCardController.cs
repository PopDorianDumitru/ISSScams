using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ISSProject_Regenerated.SubscriptionServiceBackend.CreditCards;

namespace ISSProject_Regenerated.SubscriptionServiceBackend.Controllers
{
    internal class CreditCardController : ICreditCardController
    {
        private ICreditCardRepository creditCardRepository;
        public CreditCardController(CreditCards.ICreditCardRepository creditCardRepository)
        {
            this.creditCardRepository = creditCardRepository;
        }
        public void SaveCard(int userID, string holderName, string creditCardNumber, string expirationDate, string cvv)
        {
            creditCardRepository.Insert(new CreditCard(userID, holderName, creditCardNumber, expirationDate, cvv));
        }

        public IEnumerable<CreditCard> GetAll()
        {
            return creditCardRepository.GetAll();
        }
    }
}
