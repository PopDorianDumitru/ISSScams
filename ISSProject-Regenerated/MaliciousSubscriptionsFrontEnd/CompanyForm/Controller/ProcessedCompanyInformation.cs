using System;
using Microsoft.Data.SqlClient;
using ISSProject.MaliciousSubscriptionsBackend.Domain;

namespace ISSProject.CompanyForm.Controller
{
    internal class ProcessedCompanyInformation : IProcessedCompanyInformation
    {
        private CompanyToken companyInfo;
        private static int lastId = 100;

        public ProcessedCompanyInformation(string companyName, string linkAPI, int severity, string token)
        {
            lastId = lastId + 1;
            companyInfo = new CompanyToken(lastId, companyName, linkAPI, token, severity);
        }

        public bool ValidateCompanyToken()
        {
            if (!companyInfo.GetLinkToAPI().StartsWith("http://"))
            {
                return false;
            }

            if (companyInfo.GetServiceSeverity() != 0 && companyInfo.GetServiceSeverity() != 1)
            {
                return false;
            }

            return true;
        }

        public bool CommitTokenToDatabase()
        {
            try
            {
                SqlConnection conn = new SqlConnection(ProgramConfig.DB_CONNECTION_STRING);
                conn.Open();

                // Inserting without id because id increments automatically
                string insertQuery = "INSERT INTO CompanyTokens VALUES ('" +
                    companyInfo.GetCompanyName() + "','" + companyInfo.GetLinkToAPI()
                    + "','" + companyInfo.GetToken() + "'," + companyInfo.GetServiceSeverity() + ")";

                SqlCommand cmd = new SqlCommand(insertQuery, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
