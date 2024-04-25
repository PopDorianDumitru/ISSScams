using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSProject.MaliciousSubscriptionsBackend.Storage;
using ISSProject_Regenerated.SubscriptionServiceBackend.Controllers;
using ISSProject_Regenerated.SubscriptionServiceBackend.CreditCards;
using SubscriptionServicePart.MVVM.ViewModel;

namespace TestSubscriptionService
{
    [TestClass]
    public class TestMainMenuViewModel
    {
        private ICreditCardValidatorService creditCardInformationValidator;
        private ICreditCardController creditCardController;
        private ISSProject_Regenerated.SubscriptionServiceBackend.CreditCards.ICreditCardRepository creditCardRepository;
        private MainViewModel mainViewModel;
        [TestInitialize]
        public void TestInitializer()
        {
            creditCardRepository = new CreditCardInMemoryRepository();
            creditCardInformationValidator = new CreditCardValidatorService();
            creditCardController = new CreditCardController(creditCardRepository);
            mainViewModel = new MainViewModel(creditCardController, creditCardInformationValidator);
        }
        [TestMethod]
        public void ValidCreditCardInformation_ValidDataReturnsTrue()
        {
            string creditCardNumber = "1234 5678 9012 3456";
            string expirationDate = "12/25";
            string cVV = "123";
            bool expectedResult = true;
            bool result = mainViewModel.ValidCreditCardInformation(creditCardNumber, expirationDate, cVV);
            // Assert
            Assert.AreEqual(expectedResult, result);
        }
        [TestMethod]
        public void ValidCreditCardInformation_InvalidCreditCardNumberReturnsFalse()
        {
            string creditCardNumber = "1234 5678 9012 345";
            string expirationDate = "12/25";
            string cVV = "123";
            bool expectedResult = false;
            bool result = mainViewModel.ValidCreditCardInformation(creditCardNumber, expirationDate, cVV);
            // Assert
            Assert.AreEqual(expectedResult, result);
        }
        [TestMethod]
        public void ValidCreditCardInformation_InvalidExpirationDateReturnsFalse()
        {
            string creditCardNumber = "1234 5678 9012 3456";
            string expirationDate = "12/20";
            string cVV = "123";
            bool expectedResult = false;
            bool result = mainViewModel.ValidCreditCardInformation(creditCardNumber, expirationDate, cVV);
            // Assert
            Assert.AreEqual(expectedResult, result);
        }
        [TestMethod]
        public void ValidCreditCardInformation_InvalidCVVReturnsFalse()
        {
            string creditCardNumber = "1234 5678 9012 3456";
            string expirationDate = "12/25";
            string cVV = "12";
            bool expectedResult = false;
            bool result = mainViewModel.ValidCreditCardInformation(creditCardNumber, expirationDate, cVV);
            Assert.AreEqual(expectedResult, result);
        }
        [TestMethod]
        public void ValidCreditCardInformation_ExpirationDateWrongFormatReturnsFalse()
        {
            string creditCardNumber = "1234 5678 9012 3456";
            string expirationDate = "12.25";
            string cVV = "123";
            bool expectedResult = false;
            bool result = mainViewModel.ValidCreditCardInformation(creditCardNumber, expirationDate, cVV);
            Assert.AreEqual(expectedResult, result);
        }
        [TestMethod]
        public void SaveCreditCard_ReturnsFalseWhenCreditCardInformationIsInvalid()
        {
            string creditCardNumber = "1234 5678 9012 345";
            string expirationDate = "12/20";
            string cVV = "123";
            bool expectedResult = false;
            bool result = mainViewModel.SaveCreditCard(string.Empty, creditCardNumber, cVV, expirationDate);
            Assert.AreEqual(expectedResult, result);
        }
        [TestMethod]
        public void SaveCreditCard_ReturnsTrueWhenCreditCardInformationIsValid()
        {
            string creditCardNumber = "1234 5678 9012 3456";
            string expirationDate = "12/25";
            string cVV = "123";
            bool expectedResult = true;
            bool result = mainViewModel.SaveCreditCard(string.Empty, creditCardNumber, cVV, expirationDate);
            Assert.AreEqual(expectedResult, result);
        }
    }
}
