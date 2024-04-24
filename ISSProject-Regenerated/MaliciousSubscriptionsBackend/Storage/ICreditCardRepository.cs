using ISSProject.MaliciousSubscriptionsBackend.Domain;

namespace ISSProject.MaliciousSubscriptionsBackend.Storage
{
    internal interface ICreditCardRepository
    {
        IEnumerable<CreditCard> All();
        CreditCard ById(int id);
        bool Delete(CreditCard entity);
        bool Insert(CreditCard entity);
        int Size();
        bool Update(CreditCard entity);
    }
}