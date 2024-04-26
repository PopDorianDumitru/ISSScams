using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSProject.MaliciousSubscriptionsBackend.Storage;
using ISSProject_Regenerated.SubscriptionServiceBackend.Controllers;
using ISSProject_Regenerated.SubscriptionServiceBackend.CreditCards;
using Moq;
using SubscriptionServicePart.MVVM.ViewModel;

namespace TestSubscriptionService
{
    [TestClass]
    public class TestMainMenuViewModel
    {
        private Mock<ICreditCardValidatorService> creditCardInformationValidator;
        private Mock<ICreditCardController> creditCardController;
        private Mock<ISSProject_Regenerated.SubscriptionServiceBackend.CreditCards.ICreditCardRepository> creditCardRepository;
        private MainViewModel mainViewModel;
        [TestInitialize]
        public void TestInitializer()
        {
            creditCardRepository = new Mock<ISSProject_Regenerated.SubscriptionServiceBackend.CreditCards.ICreditCardRepository>();
            creditCardInformationValidator = new Mock<ICreditCardValidatorService>();
            creditCardController = new Mock<ICreditCardController>();
            mainViewModel = new MainViewModel(creditCardController.Object, creditCardInformationValidator.Object);
        }
        [TestMethod]
        public void ValidCreditCardInformation_ValidInformation_ShouldReturnTrue()
        {
            string creditCardNumber = "1234 5678 9012 3456";
            string expirationDate = "12/25";
            string cVV = "123";
            creditCardInformationValidator.Setup(validator => validator.ValidCreditCardNumber(creditCardNumber)).Returns(true);
            creditCardInformationValidator.Setup(validator => validator.ValidExpirationDate(expirationDate)).Returns(true);
            creditCardInformationValidator.Setup(validator => validator.ValidCVV(cVV)).Returns(true);
            bool expectedResult = true;
            bool result = mainViewModel.ValidCreditCardInformation(creditCardNumber, expirationDate, cVV);
            // Assert
            Assert.AreEqual(expectedResult, result);
        }
        [TestMethod]
        public void ValidCreditCardInformation_InvalidInformation_ShouldReturnFalse()
        {
            string creditCardNumber = "1234 5678 9012 345";
            string expirationDate = "12/25";
            string cVV = "123";
            bool expectedResult = false;
            creditCardInformationValidator.Setup(validator => validator.ValidCreditCardNumber(creditCardNumber)).Returns(false);
            creditCardInformationValidator.Setup(validator => validator.ValidExpirationDate(expirationDate)).Returns(false);
            creditCardInformationValidator.Setup(validator => validator.ValidCVV(cVV)).Returns(false);
            bool result = mainViewModel.ValidCreditCardInformation(creditCardNumber, expirationDate, cVV);
            // Assert
            Assert.AreEqual(expectedResult, result);
        }
        [TestMethod]
        public void ValidCreditCardInformation_InvalidExpirationDate_ShouldReturnFalse()
        {
            string creditCardNumber = "1234 5678 9012 3456";
            string expirationDate = "12/20";
            string cVV = "123";
            bool expectedResult = false;
            creditCardInformationValidator.Setup(validator => validator.ValidCreditCardNumber(creditCardNumber)).Returns(true);
            creditCardInformationValidator.Setup(validator => validator.ValidExpirationDate(expirationDate)).Returns(false);
            creditCardInformationValidator.Setup(validator => validator.ValidCVV(cVV)).Returns(true);
            bool result = mainViewModel.ValidCreditCardInformation(creditCardNumber, expirationDate, cVV);
            // Assert
            Assert.AreEqual(expectedResult, result);
        }
        [TestMethod]
        public void ValidCreditCardInformation_InvalidCVV_ShouldReturnFalse()
        {
            string creditCardNumber = "1234 5678 9012 3456";
            string expirationDate = "12/25";
            string cVV = "12";
            bool expectedResult = false;
            creditCardInformationValidator.Setup(validator => validator.ValidCreditCardNumber(creditCardNumber)).Returns(true);
            creditCardInformationValidator.Setup(validator => validator.ValidExpirationDate(expirationDate)).Returns(true);
            creditCardInformationValidator.Setup(validator => validator.ValidCVV(cVV)).Returns(false);
            bool result = mainViewModel.ValidCreditCardInformation(creditCardNumber, expirationDate, cVV);
            Assert.AreEqual(expectedResult, result);
        }
        [TestMethod]
        public void ValidCreditCardInformation_ExpirationDateWrongFormat_ShouldReturnFalse()
        {
            string creditCardNumber = "1234 5678 9012 3456";
            string expirationDate = "12.25";
            string cVV = "123";
            bool expectedResult = false;
            creditCardInformationValidator.Setup(validator => validator.ValidCreditCardNumber(creditCardNumber)).Returns(true);
            creditCardInformationValidator.Setup(validator => validator.ValidExpirationDate(expirationDate)).Returns(false);
            creditCardInformationValidator.Setup(validator => validator.ValidCVV(cVV)).Returns(true);
            bool result = mainViewModel.ValidCreditCardInformation(creditCardNumber, expirationDate, cVV);
            Assert.AreEqual(expectedResult, result);
        }
        [TestMethod]
        public void SaveCreditCard_CreditCardInformationIsInvalid_ShouldReturnFalse()
        {
            string creditCardNumber = "1234 5678 9012 345";
            string expirationDate = "12/20";
            string cVV = "123";
            creditCardInformationValidator.Setup(validator => validator.ValidCreditCardNumber(creditCardNumber)).Returns(false);
            creditCardInformationValidator.Setup(validator => validator.ValidExpirationDate(expirationDate)).Returns(false);
            creditCardInformationValidator.Setup(validator => validator.ValidCVV(cVV)).Returns(false);
            creditCardController.Setup(controller => controller.SaveCard(58, string.Empty, creditCardNumber, expirationDate, cVV));
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
            creditCardInformationValidator.Setup(validator => validator.ValidCreditCardNumber(creditCardNumber)).Returns(true);
            creditCardInformationValidator.Setup(validator => validator.ValidExpirationDate(expirationDate)).Returns(true);
            creditCardInformationValidator.Setup(validator => validator.ValidCVV(cVV)).Returns(true);
            creditCardController.Setup(controller => controller.SaveCard(58, string.Empty, creditCardNumber, expirationDate, cVV));
            bool result = mainViewModel.SaveCreditCard(string.Empty, creditCardNumber, cVV, expirationDate);
            Assert.AreEqual(expectedResult, result);
        }
    }
}
