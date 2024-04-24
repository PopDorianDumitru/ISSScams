using ISSProject.MaliciousSubscriptionsBackend.Domain;

namespace MaliciousSubscriptions
{
    [TestClass]
    public class CreditCardTests
    {
        [TestMethod]
        public void Constructor_ValidArguments_ShouldInitializeCorrectly()
        {
            int expectedId = 1;
            int expectedUserId = 123;
            string expectedCreditCardHolder = "John Doe";
            string expectedCreditCardNumber = "1234567890123456";
            string expectedExpirationDate = "12/25";
            string expectedCVV = "123";

            var creditCard = new CreditCard(expectedId, expectedUserId, expectedCreditCardHolder, expectedCreditCardNumber, expectedExpirationDate, expectedCVV);

            Assert.AreEqual(expectedId, creditCard.GetId());
            Assert.AreEqual(expectedUserId, creditCard.GetUserID());
            Assert.AreEqual(expectedCreditCardHolder, creditCard.GetCreditCardHolder());
            Assert.AreEqual(expectedCreditCardNumber, creditCard.GetCreditCardNumber());
            Assert.AreEqual(expectedExpirationDate, creditCard.GetExpirationDate());
            Assert.AreEqual(expectedCVV, creditCard.GetCVV());
        }
    }
}