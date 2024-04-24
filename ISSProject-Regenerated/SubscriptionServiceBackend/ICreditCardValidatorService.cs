namespace ISSProject_Regenerated.SubscriptionServiceBackend
{
    internal interface ICreditCardValidatorService
    {
        bool ValidCreditCardNumber(string creditCardNumber);
        bool ValidCVV(string cvvPattern, string cvv);
        bool ValidExpirationDate(string expirationDate);
    }
}