using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ISSProject.Common.Mock;
using ISSProject_Regenerated.SubscriptionServiceBackend.Controllers;
using ISSProject_Regenerated.SubscriptionServiceBackend.CreditCards;

namespace SubscriptionServicePart.MVVM.ViewModel
{
    public class MainViewModel
    {
        private MockUser userToInsertCreditCard = new MockUser(58, "8h717xii2u8", "nicol.nikolaus@yahoo.com", "Johnathan", "Bins", DateTime.Now, "+40780880054");
        public Dictionary<string, List<string>> Errors = new Dictionary<string, List<string>>();
        private ICreditCardValidatorService creditCardInformationValidator;
        private ICreditCardController creditCardController;
        public bool HasErrors => Errors.Count > 0;

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        public bool ValidCreditCardInformation(string creditCardNumber, string expirationDate, string cVV)
        {
            bool validCreditCradNumber = creditCardInformationValidator.ValidCreditCardNumber(creditCardNumber);
            bool validExpirationDate = creditCardInformationValidator.ValidExpirationDate(expirationDate);
            bool validCVV = creditCardInformationValidator.ValidCVV(cVV);
            return validCreditCradNumber && validExpirationDate && validCVV;
        }

        public MainViewModel(ICreditCardController creditCardController, ICreditCardValidatorService creditCardValidator)
        {
            this.creditCardInformationValidator = creditCardValidator;
            this.creditCardController = creditCardController;
        }

        public bool SaveCreditCard(string holderName, string cardNumber, string cvv, string expirationDate)
        {
            if (ValidCreditCardInformation(cardNumber, expirationDate, cvv))
            {
                creditCardController.SaveCard(userToInsertCreditCard.Id, holderName, cardNumber, expirationDate, cvv);
                return true;
            }
            return false;
        }
        public MainViewModel()
        {
        }
    }
}
