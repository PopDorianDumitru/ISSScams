namespace TestSubscriptionService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ISSProject.Common.Mikha.Controllers;
    using ISSProject.Common.Repository;
    using ISSProject_Regenerated.SubscriptionServiceBackend.Controllers;
    using ISSProject_Regenerated.SubscriptionServiceBackend.CreditCards;
    using ISSProject_Regenerated.SubscriptionServiceBackend.Premium_Messages;
    using ISSProject_Regenerated.SubscriptionServiceBackend.Premium_Users;
    using Moq;

    [TestClass]
    public class TestCreditCardController
    {
        private Mock<ICreditCardRepository> creditCardRepository;
        private ICreditCardController creditCardController;

        [TestInitialize]
        public void TestInitializer()
        {
            creditCardRepository = new Mock<ICreditCardRepository>();
            creditCardController = new CreditCardController(creditCardRepository.Object);
        }

        [TestMethod]
        public void SaveCard_CreditCardObject_ShouldReturnTheSavedCard()
        {
            int expectedId = 1;
            string expectedHolderName = "Dorian";
            string expectedCreditCardNumber = "1234 5678 9012 3456";
            string expectedCVV = "000";
            string expectedExpirationDate = "27/5";
            CreditCard cardToBeInserted = new CreditCard(expectedId, expectedHolderName, expectedCreditCardNumber, expectedCVV, expectedExpirationDate);

            creditCardRepository.Setup(repo => repo.Insert(cardToBeInserted));
            creditCardController.SaveCard(expectedId, expectedHolderName, expectedCreditCardNumber, expectedExpirationDate, expectedCVV);

            List<CreditCard> mockCreditCards = new List<CreditCard>();
            mockCreditCards.Add(cardToBeInserted);
            IEnumerable<CreditCard> mockCreditCardsEnumerable = mockCreditCards;
            creditCardRepository.Setup(repo => repo.GetAll()).Returns(new List<CreditCard>(mockCreditCardsEnumerable));

            IEnumerable<CreditCard> cards = creditCardController.GetAll();
            CreditCard card = cards.First();
            Assert.IsTrue(cardToBeInserted.Equals(card));
        }

        [TestMethod]
        public void GetAll_InsertingTwoCards_ShouldReturnBackTheTwoSavedCards()
        {
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

            CreditCard expectedCreditCard = new CreditCard(expectedId, expectedHolderName, expectedCreditCardNumber, expectedExpirationDate, expectedCVV);
            CreditCard secondExpectedCreditCard = new CreditCard(secondExpectedId, secondExpectedHolderName, secondExpectedCreditCardNumber, secondExpectedExpirationDate, secondExpectedCVV);
            List<CreditCard> mockCreditCards = new List<CreditCard>();
            mockCreditCards.Add(expectedCreditCard);
            mockCreditCards.Add(secondExpectedCreditCard);
            IEnumerable<CreditCard> mockCreditCardsEnumerable = mockCreditCards;
            creditCardRepository.Setup(repo => repo.GetAll()).Returns(mockCreditCardsEnumerable);

            creditCardRepository.Setup(repo => repo.Insert(expectedCreditCard));
            creditCardRepository.Setup(repo => repo.Insert(secondExpectedCreditCard));
            creditCardController.SaveCard(expectedId, expectedHolderName, expectedCreditCardNumber, expectedExpirationDate, expectedCVV);
            creditCardController.SaveCard(secondExpectedId, secondExpectedHolderName, secondExpectedCreditCardNumber, secondExpectedExpirationDate, secondExpectedCVV);
            IEnumerable<CreditCard> cards = creditCardController.GetAll();
            CreditCard card = cards.First();
            Assert.IsTrue(card.Equals(expectedCreditCard));

            CreditCard secondCard = cards.ElementAt(1);
            Assert.IsTrue(secondCard.Equals(secondExpectedCreditCard));
        }
    }
}
