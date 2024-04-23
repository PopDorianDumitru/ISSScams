using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSProject;
using Microsoft.Data.SqlClient;
namespace SubscriptionServicePart.MVVM.ViewModel
{
    public static class CommitToDatabase
    {
        static public void Commit(int userID, string holderName, string creditCardNumber, string expirationDate, string cvv)
        {
            using (SqlConnection conn = new SqlConnection(ProgramConfig.DB_CONNECTION_STRING))
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

            using (SqlConnection conn = new SqlConnection(ProgramConfig.DB_CONNECTION_STRING))
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
