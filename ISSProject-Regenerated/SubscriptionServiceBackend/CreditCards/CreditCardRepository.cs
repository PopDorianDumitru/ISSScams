using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSProject;
using ISSProject.MaliciousSubscriptionsBackend.Domain;
using Microsoft.Data.SqlClient;
using SubscriptionServicePart.MVVM.ViewModel;

namespace ISSProject_Regenerated.SubscriptionServiceBackend.CreditCards
{
    internal class CreditCardRepository : ICreditCardRepository
    {
        public CreditCardRepository()
        {
        }

        public IEnumerable<CreditCard> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Insert(CreditCard card)
        {
            int userID = card.UserID;
            string holderName = card.HolderName;
            string expirationDate = card.ExpirationDate;
            string creditCardNumber = card.CreditCardNumber;
            string cvv = card.CVV;
            using (SqlConnection conn = new SqlConnection(ProgramConfig.DATABASE_CONNECTION_STRING))
            {
                conn.Open();
                string dbToInsert = "CreditCards";
                string queryString = $@"INSERT INTO {dbToInsert} VALUES (@UserID, @HolderName, @CreditCardNumber, @ExpirationDate, @CVV)";

                SqlCommand command = new SqlCommand(queryString, conn);
                command.Parameters.AddWithValue("@UserID", userID);
                command.Parameters.AddWithValue("@HolderName", holderName);
                command.Parameters.AddWithValue("@CreditCardNumber", creditCardNumber);
                command.Parameters.AddWithValue("@ExpirationDate", expirationDate);
                command.Parameters.AddWithValue("@CVV", cvv);
                command.ExecuteNonQuery();
                command.Dispose();
            }

            using (SqlConnection conn = new SqlConnection(ProgramConfig.DATABASE_CONNECTION_STRING))
            {
                conn.Open();
                string dbToInsert = "PremiumUsers";
                string queryString = $@"INSERT INTO {dbToInsert} VALUES (@UserID)";
                SqlCommand command = new SqlCommand(queryString, conn);
                command.Parameters.AddWithValue("@UserID", userID);
                command.ExecuteNonQuery();
                command.Dispose();
            }
        }
    }
}
