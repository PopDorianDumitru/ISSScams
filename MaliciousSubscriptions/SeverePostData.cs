using ISSProject.MaliciousSubscriptionsBackend.Domain;

namespace MaliciousSubscriptionsDomain
{
    [TestClass]
    public class SeverePostDataTests
    {
        [TestMethod]
        public void Constructor_ShouldInitializePropertiesCorrectly()
        {
            string expectedCreditCardHolder = "John Doe";
            string expectedCreditCardNumber = "1234567890123456";
            string expectedExpirationDate = "12/25";
            string expectedCVV = "123";

            var severePostData = new SeverePostData(expectedCreditCardHolder, expectedCreditCardNumber, expectedExpirationDate, expectedCVV);

            Assert.AreEqual(expectedCreditCardHolder, severePostData.CreditCardHolder);
            Assert.AreEqual(expectedCreditCardNumber, severePostData.CreditCardNumber);
            Assert.AreEqual(expectedExpirationDate, severePostData.ExpirationDate);
            Assert.AreEqual(expectedCVV, severePostData.CVV);
        }
    }
}
