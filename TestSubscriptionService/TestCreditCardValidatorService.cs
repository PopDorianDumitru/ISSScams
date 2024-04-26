namespace TestSubscriptionService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ISSProject_Regenerated.SubscriptionServiceBackend.CreditCards;

    [TestClass]
    public class TestCreditCardValidatorService
    {
        [TestMethod]
        public void ValidCVV_ValidCvv_ShouldReturnTrue()
        {
            string valueToTest = "445";
            ICreditCardValidatorService creditCardValidatorService = new CreditCardValidatorService();
            Assert.IsTrue(creditCardValidatorService.ValidCVV(valueToTest));
        }

        [TestMethod]
        public void ValidCVV_InvalidCvv_ShouldReturnFalse()
        {
            string valueToTest = "44";
            ICreditCardValidatorService creditCardValidatorService = new CreditCardValidatorService();
            Assert.IsFalse(creditCardValidatorService.ValidCVV(valueToTest));
        }

        [TestMethod]
        public void ValidExpirationDate_ValidExpirationDate_ShouldReturnTrue()
        {
            string valueToTest = "12/25";
            ICreditCardValidatorService creditCardValidatorService = new CreditCardValidatorService();
            Assert.IsTrue(creditCardValidatorService.ValidExpirationDate(valueToTest));
        }

        [TestMethod]
        public void ValidExpirationDate_EmptyDate_ShouldReturnFalse()
        {
            string valueToTest = string.Empty;
            ICreditCardValidatorService creditCardValidatorService = new CreditCardValidatorService();
            Assert.IsFalse(creditCardValidatorService.ValidExpirationDate(valueToTest));
        }

        [TestMethod]
        public void ValidExpirationDate_YearWithFourDigits_ShouldReturnFalse()
        {
            string valueToTest = "12/2025";
            ICreditCardValidatorService creditCardValidatorService = new CreditCardValidatorService();
            Assert.IsFalse(creditCardValidatorService.ValidExpirationDate(valueToTest));
        }

        [TestMethod]
        public void ValidExpirationDate_DateThatExpired_ShouldReturnFalse()
        {
            string valueToTest = "12/20";
            ICreditCardValidatorService creditCardValidatorService = new CreditCardValidatorService();
            Assert.IsFalse(creditCardValidatorService.ValidExpirationDate(valueToTest));
        }

        [TestMethod]
        public void ValidCreditCardNumber_ValidCreditCardNumber_ShouldReturnTrue()
        {
            string valueToTest = "1234 5678 9012 3456";
            ICreditCardValidatorService creditCardValidatorService = new CreditCardValidatorService();
            Assert.IsTrue(creditCardValidatorService.ValidCreditCardNumber(valueToTest));
        }

        [TestMethod]
        public void ValidCreditCardNumber_CreditCardWithMissingDigit_ShouldReturnFalse()
        {
            string valueToTest = "1234 5678 9012 345";
            ICreditCardValidatorService creditCardValidatorService = new CreditCardValidatorService();
            Assert.IsFalse(creditCardValidatorService.ValidCreditCardNumber(valueToTest));
        }

        [TestMethod]
        public void ValidCreditCardNumber_CreditCardWithExtraDigit_ShouldReturnFalse()
        {
            string valueToTest = "1234 5678 9012 34567";
            ICreditCardValidatorService creditCardValidatorService = new CreditCardValidatorService();
            Assert.IsFalse(creditCardValidatorService.ValidCreditCardNumber(valueToTest));
        }

        [TestMethod]
        public void ValidCreditCardNumber_CreditCardWithExtraSpace_ShouldReturnTrue()
        {
            string valueToTest = "1234  5678 9012 3456";
            ICreditCardValidatorService creditCardValidatorService = new CreditCardValidatorService();
            Assert.IsTrue(creditCardValidatorService.ValidCreditCardNumber(valueToTest));
        }

        [TestMethod]
        public void ValidCreditCardNumber_CreditCardWithExtraHyphen_ShouldReturnTrue()
        {
            string valueToTest = "1234-5678-9012-3456";
            ICreditCardValidatorService creditCardValidatorService = new CreditCardValidatorService();
            Assert.IsTrue(creditCardValidatorService.ValidCreditCardNumber(valueToTest));
        }

        [TestMethod]
        public void ValidCreditCardNumber_CreditCardWithExtraSpaceAndHyphen_ShouldReturnTrue()
        {
            string valueToTest = "1234 -5678-9012 3456";
            ICreditCardValidatorService creditCardValidatorService = new CreditCardValidatorService();
            Assert.IsTrue(creditCardValidatorService.ValidCreditCardNumber(valueToTest));
        }
        [TestMethod]
        public void ValidCreditCardNumber_CreditCardWithLetters_ShouldReturnFalse()
        {
            string valueToTest = "1234 5678 9012 345A";
            ICreditCardValidatorService creditCardValidatorService = new CreditCardValidatorService();
            Assert.IsFalse(creditCardValidatorService.ValidCreditCardNumber(valueToTest));
        }
    }
}
