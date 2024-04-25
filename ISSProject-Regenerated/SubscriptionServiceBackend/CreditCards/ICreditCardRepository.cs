namespace ISSProject_Regenerated.SubscriptionServiceBackend.CreditCards
{
    internal interface ICreditCardRepository
    {
        void Insert(CreditCard card);
        IEnumerable<CreditCard> GetAll();
    }
}