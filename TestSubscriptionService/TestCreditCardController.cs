using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSProject_Regenerated.SubscriptionServiceBackend.Controllers;
using ISSProject_Regenerated.SubscriptionServiceBackend.CreditCards;

namespace TestSubscriptionService
{
    [TestClass]
    public class TestCreditCardController
    {
        [TestMethod]
        public void CreditCardController_SaveCard()
        {
            CreditCardController controller = new CreditCardController(new CreditCardInMemoryRepository());
            int expectedId = 1;
            string expectedHolderName = "Dorian";
            string expectedCreditCardNumber = "1234 5678 9012 3456";
            string expectedCVV = "000";
            string expectedExpirationDate = "27/5";
            controller.SaveCard(expectedId, expectedHolderName, expectedCreditCardNumber, expectedExpirationDate, expectedCVV);
            IEnumerable<CreditCard> cards = controller.GetAll();
            CreditCard card = cards.First();
            Assert.AreEqual(expectedId, card.UserID);
            Assert.AreEqual(expectedHolderName, card.HolderName);
            Assert.AreEqual(expectedCreditCardNumber, card.CreditCardNumber);
            Assert.AreEqual(expectedCVV, card.CVV);
            Assert.AreEqual(expectedExpirationDate, card.ExpirationDate);
        }

        [TestMethod]
        public void CreditCardController_GetAll()
        {
            CreditCardController controller = new CreditCardController(new CreditCardInMemoryRepository());
            int expectedId = 1;
            string expectedHolderName = "Dorian";
            string expectedCreditCardNumber = "1234 5678 9012 3456";
            string expectedCVV = "000";
            string expectedExpirationDate = "27/5";

            int secondExpectedId = 2;
            string secondExpectedHolderName = "Larisa";
            string secondExpectedCreditCardNumber = "1234 5678 9012 3456";
            string secondExpectedCVV = "111";
            string secondExpectedExpirationDate = "26/5";

            int expectedNumberOfCards = 2;
            controller.SaveCard(expectedId, expectedHolderName, expectedCreditCardNumber, expectedExpirationDate, expectedCVV);
            controller.SaveCard(secondExpectedId, secondExpectedHolderName, secondExpectedCreditCardNumber, secondExpectedExpirationDate, secondExpectedCVV);
            IEnumerable<CreditCard> cards = controller.GetAll();
            CreditCard card = cards.First();
            Assert.AreEqual(expectedId, card.UserID);
            Assert.AreEqual(expectedHolderName, card.HolderName);
            Assert.AreEqual(expectedCreditCardNumber, card.CreditCardNumber);
            Assert.AreEqual(expectedCVV, card.CVV);
            Assert.AreEqual(expectedExpirationDate, card.ExpirationDate);
            CreditCard secondCard = cards.ElementAt(1);

            Assert.AreEqual(secondExpectedId, secondCard.UserID);
            Assert.AreEqual(secondExpectedHolderName, secondCard.HolderName);
            Assert.AreEqual(secondExpectedCreditCardNumber, secondCard.CreditCardNumber);
            Assert.AreEqual(secondExpectedCVV, secondCard.CVV);
            Assert.AreEqual(secondExpectedExpirationDate, secondCard.ExpirationDate);

            Assert.AreEqual(expectedNumberOfCards, cards.Count());
        }
    }
}
