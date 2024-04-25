namespace ISSProject_Regenerated.SubscriptionServiceBackend.CreditCards
{
    public interface ICreditCardValidatorService
    {
        bool ValidCreditCardNumber(string creditCardNumber);
        bool ValidCVV(string cvv);
        bool ValidExpirationDate(string expirationDate);
    }
}