namespace ISSProject.MaliciousSubscriptionsBackend.Domain
{
    internal interface ISeverePostData
    {
        string CreditCardHolder { get; set; }
        string CreditCardNumber { get; set; }
        string CVV { get; set; }
        string ExpirationDate { get; set; }
    }
}