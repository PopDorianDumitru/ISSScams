using ISSProject_Regenerated.SubscriptionServiceBackend.CreditCards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSubscriptionService
{
    [TestClass]
    public class TestCreditCard
    {
        [TestMethod]
        public void CreditCard_Constructor()
        {
            int expectedId = 1;
            string expectedHolderName = "Dorian";
            string expectedCreditCardNumber = "1234 5678 9012 3456";
            string expectedCVV = "000";
            string expectedExpirationDate = "27/5";
            CreditCard card = new CreditCard(expectedId, expectedHolderName, expectedCreditCardNumber, expectedExpirationDate, expectedCVV);
            Assert.AreEqual(expectedId, card.UserID);
            Assert.AreEqual(expectedHolderName, card.HolderName);
            Assert.AreEqual(expectedCreditCardNumber, card.CreditCardNumber);
            Assert.AreEqual(expectedCVV, card.CVV);
            Assert.AreEqual(expectedExpirationDate, card.ExpirationDate);
        }
    }
}
