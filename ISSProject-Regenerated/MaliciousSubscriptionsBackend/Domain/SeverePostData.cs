using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSProject.MaliciousSubscriptionsBackend.Domain
{
    internal class SeverePostData
    {
        public string CreditCardHolder { get; set; }
        public string CreditCardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public string CVV { get; set; }

        public SeverePostData(string creditCardHolder, string creditCardNumber, string expirationDate, string cvv)
        {
            CreditCardHolder = creditCardHolder;
            CreditCardNumber = creditCardNumber;
            ExpirationDate = expirationDate;
            CVV = cvv;
        }
    }
}
