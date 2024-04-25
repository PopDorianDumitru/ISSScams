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
        public void ValidCVV_ValidCvv()
        {
            string valueToTest = "445";
            ICreditCardValidatorService creditCardValidatorService = new CreditCardValidatorService();
            Assert.IsTrue(creditCardValidatorService.ValidCVV(valueToTest));
        }

        [TestMethod]
        public void ValidCVV_InvalidCvv()
        {
            string valueToTest = "44";
            ICreditCardValidatorService creditCardValidatorService = new CreditCardValidatorService();
            Assert.IsFalse(creditCardValidatorService.ValidCVV(valueToTest));
        }

        [TestMethod]
        public void ValidExpirationDate_ValidExpirationDate()
        {
            string valueToTest = "12/25";
            ICreditCardValidatorService creditCardValidatorService = new CreditCardValidatorService();
            Assert.IsTrue(creditCardValidatorService.ValidExpirationDate(valueToTest));
        }

        [TestMethod]
        public void ValidExpirationDate_EmptyDate()
        {
            string valueToTest = string.Empty;
            ICreditCardValidatorService creditCardValidatorService = new CreditCardValidatorService();
            Assert.IsFalse(creditCardValidatorService.ValidExpirationDate(valueToTest));
        }

        [TestMethod]
        public void ValidExpirationDate_YearWithFourDigits()
        {
            string valueToTest = "12/2025";
            ICreditCardValidatorService creditCardValidatorService = new CreditCardValidatorService();
            Assert.IsFalse(creditCardValidatorService.ValidExpirationDate(valueToTest));
        }

        [TestMethod]
        public void ValidExpirationDate_DateThatExpired()
        {
            string valueToTest = "12/20";
            ICreditCardValidatorService creditCardValidatorService = new CreditCardValidatorService();
            Assert.IsFalse(creditCardValidatorService.ValidExpirationDate(valueToTest));
        }

        [TestMethod]
        public void ValidCreditCardNumber_ValidCreditCardNumber()
        {
            string valueToTest = "1234 5678 9012 3456";
            ICreditCardValidatorService creditCardValidatorService = new CreditCardValidatorService();
            Assert.IsTrue(creditCardValidatorService.ValidCreditCardNumber(valueToTest));
        }

        [TestMethod]
        public void ValidCreditCardNumber_CreditCardWithMissingDigit()
        {
            string valueToTest = "1234 5678 9012 345";
            ICreditCardValidatorService creditCardValidatorService = new CreditCardValidatorService();
            Assert.IsFalse(creditCardValidatorService.ValidCreditCardNumber(valueToTest));
        }

        [TestMethod]
        public void ValidCreditCardNumber_CreditCardWithExtraDigit()
        {
            string valueToTest = "1234 5678 9012 34567";
            ICreditCardValidatorService creditCardValidatorService = new CreditCardValidatorService();
            Assert.IsFalse(creditCardValidatorService.ValidCreditCardNumber(valueToTest));
        }

        [TestMethod]
        public void ValidCreditCardNumber_CreditCardWithExtraSpace()
        {
            string valueToTest = "1234  5678 9012 3456";
            ICreditCardValidatorService creditCardValidatorService = new CreditCardValidatorService();
            Assert.IsTrue(creditCardValidatorService.ValidCreditCardNumber(valueToTest));
        }

        [TestMethod]
        public void ValidCreditCardNumber_CreditCardWithExtraHyphen()
        {
            string valueToTest = "1234-5678-9012-3456";
            ICreditCardValidatorService creditCardValidatorService = new CreditCardValidatorService();
            Assert.IsTrue(creditCardValidatorService.ValidCreditCardNumber(valueToTest));
        }

        [TestMethod]
        public void ValidCreditCardNumber_CreditCardWithExtraSpaceAndHyphen()
        {
            string valueToTest = "1234 -5678-9012 3456";
            ICreditCardValidatorService creditCardValidatorService = new CreditCardValidatorService();
            Assert.IsTrue(creditCardValidatorService.ValidCreditCardNumber(valueToTest));
        }
        [TestMethod]
        public void ValidCreditCardNumber_CreditCardWithLetters()
        {
            string valueToTest = "1234 5678 9012 345A";
            ICreditCardValidatorService creditCardValidatorService = new CreditCardValidatorService();
            Assert.IsFalse(creditCardValidatorService.ValidCreditCardNumber(valueToTest));
        }

        // [TestMethod]
        // public void ValidExpirationDate(string expirationDate)
        // {
        // }
        // [TestMethod]
        // public void ValidCreditCardNumber(string creditCardNumber)
        // {
        // }
    }
}
