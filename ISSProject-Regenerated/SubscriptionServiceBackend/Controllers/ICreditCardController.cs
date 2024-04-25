using ISSProject_Regenerated.SubscriptionServiceBackend.CreditCards;

namespace ISSProject_Regenerated.SubscriptionServiceBackend.Controllers
{
    public interface ICreditCardController
    {
        void SaveCard(int userID, string holderName, string creditCardNumber, string expirationDate, string cvv);
        IEnumerable<CreditCard> GetAll();
    }
}