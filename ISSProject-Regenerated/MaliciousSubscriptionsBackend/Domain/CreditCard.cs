using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ISSProject.Common;
namespace ISSProject.MaliciousSubscriptionsBackend.Domain
{
    internal class CreditCard : IKeyedEntity<int>
    {
        private readonly int ID;
        private int userID;
        private string creditCardHolder;
        private string creditCardNumber;
        private string expirationDate;
        private string CVV;

        public CreditCard(int ID, int userID, string creditCardHolder, string creditCardNumber, string expirationDate, string CVV)
        {
            this.ID = ID;
            this.userID = userID;
            this.creditCardHolder = creditCardHolder;
            this.creditCardNumber = creditCardNumber;
            this.expirationDate = expirationDate;
            this.CVV = CVV;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public int GetId()
        {
            return ID;
        }

        public int GetUserID() 
        {
            return userID;
        }

        public string GetCreditCardHolder() 
        {
            return creditCardHolder;
        }

        public string GetCreditCardNumber()
        {
            return creditCardNumber;
        }

        public string GetExpirationDate()
        {
            return expirationDate;
        }

        public string GetCVV()
        {
            return CVV;
        }
    }
}
