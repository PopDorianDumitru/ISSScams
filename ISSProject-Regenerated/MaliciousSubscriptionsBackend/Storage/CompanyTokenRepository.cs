﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSProject.Common;
using ISSProject.Common;
using ISSProject.Common.Cache;
using ISSProject.Common.Mock;
using ISSProject.Common.Wrapper;
using ISSProject.MaliciousSubscriptionsBackend.Domain;
using Microsoft.Data.SqlClient;
namespace ISSProject.MaliciousSubscriptionsBackend.Storage
{
    internal class CompanyTokenRepository : ICachedRepository<CompanyToken, int>, ICompanyTokenRepository
    {
        public CompanyTokenRepository() : base()
        {
            cache = new SimpleKeyedMapCache<CompanyToken, int>();
        }

        public override IEnumerable<CompanyToken> All()
        {
            string connString = @"Data Source=DESKTOP-MAIN;" +
                      @"Initial Catalog=CelebrationOfCapitalism;" +
                      @"Integrated Security=true;";

            List<CompanyToken> companyTokens = new List<CompanyToken>();
            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();
                string queryString = "SELECT * FROM CompanyTokens";
                SqlCommand command = new SqlCommand(queryString, connection);

                CompanyToken companyToken;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int companyId = (int)reader[0];
                        string companyName = (string)reader[1];
                        string linkToAPI = (string)reader[2];
                        string token = (string)reader[3];
                        int severity = (int)reader[4];

                        companyToken = new CompanyToken(severity, companyName, linkToAPI, token, severity);
                        companyTokens.Add(companyToken);
                    }
                }
            }

            return companyTokens;
        }

        public override CompanyToken ById(int id)
        {
            return cache.ById(id);
        }

        public override bool Delete(CompanyToken entity)
        {
            return cache.Remove(entity);
        }

        public override bool Insert(CompanyToken entity)
        {
            return cache.Add(entity);
        }

        public override int Size()
        {
            return cache.GetCache().Count;
        }

        public override bool Update(CompanyToken entity)
        {
            return cache.Update(entity);
        }
    }
}
