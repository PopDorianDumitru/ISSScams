using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSProject;
using Microsoft.Data.SqlClient;

namespace ISSProject_Regenerated.ScamBotsPhishingFrontend.ScamBotsPhishingService
{
    internal static class ScamBotsPhishingService
    {
        public static void AddToDatabase(string name, string creditNr, string cvv, string expYear, int randomUserId)
        {
            SqlConnection connection = new SqlConnection(ProgramConfig.DATABASE_CONNECTION_STRING); // SQL connection string
            connection.Open();
            string query = "INSERT INTO CreditCards VALUES (" +
                                                            randomUserId + ",'" +
                                                            name + "','" +
                                                            creditNr + "','" +
                                                            expYear + "','" +
                                                            cvv + "')";

            SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
