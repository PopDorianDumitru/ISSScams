using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSProject_Regenerated.SubscriptionServiceBackend.CreditCards
{
    internal class CreditCardInMemoryRepository : ICreditCardRepository
    {
        private List<CreditCard> creditCards;
        public CreditCardInMemoryRepository()
        {
            creditCards = new List<CreditCard>();
        }

        public IEnumerable<CreditCard> GetAll()
        {
            return creditCards;
        }

        public void Insert(CreditCard card)
        {
            creditCards.Add(card);
        }
    }
}
