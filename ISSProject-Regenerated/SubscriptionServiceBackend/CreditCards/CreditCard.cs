using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSProject.Common.Mock;
using ISSProject.MaliciousSubscriptionsBackend.Domain;
using Microsoft.VisualBasic.ApplicationServices;

namespace ISSProject_Regenerated.SubscriptionServiceBackend.CreditCards
{
    public class CreditCard
    {
        private int userID;
        private string holderName;
        private string creditCardNumber;
        private string expirationDate;
        private string cvv;

        public override bool Equals(object comparisonObject)
        {
            // Check for null and compare run-time types.
            if ((comparisonObject == null) || !this.GetType().Equals(comparisonObject.GetType()))
            {
                return false;
            }
            else
            {
                CreditCard creditCard = (CreditCard)comparisonObject;
                return (UserID == creditCard.UserID) && (HolderName == creditCard.HolderName) && (CreditCardNumber == creditCard.CreditCardNumber) && (ExpirationDate == creditCard.ExpirationDate) && (CVV == creditCard.CVV);
            }
        }

        public CreditCard(int userID, string holderName, string creditCardNumber, string expirationDate, string cvv)
        {
            this.userID = userID;
            this.holderName = holderName;
            this.creditCardNumber = creditCardNumber;
            this.expirationDate = expirationDate;
            this.cvv = cvv;
        }

        public int UserID { get => userID; set => userID = value; }
        public string HolderName { get => holderName; set => holderName = value; }
        public string CreditCardNumber { get => creditCardNumber; set => creditCardNumber = value; }
        public string ExpirationDate { get => expirationDate; set => expirationDate = value; }
        public string CVV { get => cvv; set => cvv = value; }
    }
}
