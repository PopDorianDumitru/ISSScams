using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ISSProject.Common;
namespace ISSProject.MaliciousSubscriptionsBackend.Domain
{
    internal class CreditCard : IKeyedEntity<int>, ICreditCard
    {
        private readonly int iD;
        private int userID;
        private string creditCardHolder;
        private string creditCardNumber;
        private string expirationDate;
        private readonly string cVV;

        public CreditCard(int iD, int userID, string creditCardHolder, string creditCardNumber, string expirationDate, string cVV)
        {
            this.iD = iD;
            this.userID = userID;
            this.creditCardHolder = creditCardHolder;
            this.creditCardNumber = creditCardNumber;
            this.expirationDate = expirationDate;
            this.cVV = cVV;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public int GetId()
        {
            return iD;
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
            return cVV;
        }
    }
}
