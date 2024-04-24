namespace ISSProject.MaliciousSubscriptionsBackend.Domain
{
    internal interface ICreditCard
    {
        object Clone();
        string GetCreditCardHolder();
        string GetCreditCardNumber();
        string GetCVV();
        string GetExpirationDate();
        int GetId();
        int GetUserID();
    }
}