﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSProject.Common.Mock;
using ISSProject.MaliciousSubscriptionsBackend.Domain;
using Microsoft.Data.SqlClient;

namespace ISSProject.MaliciousSubscriptionsBackend.Service
{
    internal static class ExtensionUtils
    {
        public static List<UserID> FilterOutAlreadySubscribed(this List<UserID> userIDs, ICompanyToken company)
        {
            List<UserID> discrepancyIDs = new List<UserID>();
            using (SqlConnection conn = new SqlConnection(ProgramConfig.DATABASE_CONNECTION_STRING))
            {
                conn.Open();
                string databaseToSelect = (company.GetServiceSeverity() == 0) ? "BenignFlaggedCrossedWithCompany" : "SevereFlaggedCrossedWithCompany";
                string queryString = $"SELECT * FROM {databaseToSelect} WHERE company_id = {company.GetId()}";
                SqlCommand command = new SqlCommand(queryString, conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int userId = (int)reader[0];
                        int companyId = (int)reader[1];

                        discrepancyIDs.Add(new UserID(userId));
                    }
                }
            }

            foreach (UserID id in discrepancyIDs)
            {
                userIDs.Remove(id);
            }

            return userIDs;
        }
        public static List<T> PerformFisherYatesShuffle<T>(this List<T> list)
        {
            Random randomNumber = new Random();
            int shuffles = list.Count;
            while (shuffles > 1)
            {
                shuffles--;
                int swapper = randomNumber.Next(0, shuffles + 1);
                T value = list[swapper];
                list[swapper] = list[shuffles];
                list[shuffles] = value;
            }
            return list;
        }
    }
}
