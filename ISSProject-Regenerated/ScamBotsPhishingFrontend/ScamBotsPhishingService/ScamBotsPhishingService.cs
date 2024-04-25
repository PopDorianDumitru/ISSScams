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
            SqlConnection conn = new SqlConnection(ProgramConfig.DATABASE_CONNECTION_STRING); // SQL connection string
            conn.Open();
            string query = "INSERT INTO CreditCards VALUES (" +
                                                            randomUserId + ",'" +
                                                            name + "','" +
                                                            creditNr + "','" +
                                                            expYear + "','" +
                                                            cvv + "')";

            SqlCommand command = new SqlCommand(query, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }
    }
}
