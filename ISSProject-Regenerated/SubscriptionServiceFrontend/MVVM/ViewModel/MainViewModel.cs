using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SubscriptionServicePart.MVVM.ViewModel
{
    public class MainViewModel : INotifyDataErrorInfo
    {
        public Dictionary<string, List<string>> Errors = new Dictionary<string, List<string>>();

        public bool HasErrors => Errors.Count > 0;

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public IEnumerable GetErrors(string? propertyName)
        {
            if (Errors.ContainsKey(propertyName))
            {
                return Errors[propertyName];
            }
            else
            {
                return Enumerable.Empty<string>();
            }
        }

        public void Validate(string propertyName, object propertyValue)
        {
            var results = new List<ValidationResult>();

            Validator.TryValidateProperty(propertyValue, new ValidationContext(this) { MemberName = propertyName }, results);

            if (results.Any())
            {
                Errors.Add(propertyName, results.Select(r => r.ErrorMessage).ToList());
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }
            else
            {
                Errors.Remove(propertyName);
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }
        }

        private string creditCardHolder;

        [Required(ErrorMessage = "Name is Required")]
        public string CreditCardHolder
        {
            get
            {
                return creditCardHolder;
            }

            set
            {
                creditCardHolder = value;
                Validate(nameof(CreditCardHolder), value);
            }
        }

        private string creditCardNumber;

        [Required(ErrorMessage = "Card number is Required")]
        public string CreditCardNumber
        {
            get
            {
                return creditCardNumber;
            }

            set
            {
                creditCardNumber = value;
                Validate(nameof(CreditCardNumber), value);
            }
        }

        private string expirationDate;

        [Required(ErrorMessage = "Expiration date is Required")]
        public string ExpirationDate
        {
            get
            {
                return expirationDate;
            }

            set
            {
                expirationDate = value;
                Validate(nameof(ExpirationDate), value);
            }
        }

        private string cvv;

        [Required(ErrorMessage = "CVV is Required")]
        public string CVV
        {
            get
            {
                return cvv;
            }

            set
            {
                cvv = value;
                Validate(nameof(CVV), value);
            }
        }

        public MainViewModel()
        {
        }

        private bool CanSubmit(object obj)
        {
            return Validator.TryValidateObject(this, new ValidationContext(this), null);
        }

        private void Submit(object obj)
        {
            MessageBox.Show("Submitted");
        }
    }
}
