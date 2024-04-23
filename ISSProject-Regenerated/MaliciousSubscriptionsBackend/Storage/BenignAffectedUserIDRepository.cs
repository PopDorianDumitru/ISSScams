using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ISSProject.Common;
using ISSProject.Common.Cache;
using ISSProject.MaliciousSubscriptionsBackend.Domain;
using Microsoft.Data.SqlClient;

namespace ISSProject.MaliciousSubscriptionsBackend.Storage
{
    internal class BenignAffectedUserIDRepository : ICachedRepository<UserID, int>
    {
        public BenignAffectedUserIDRepository() : base()
        {
            cache = new SimpleKeyedMapCache<UserID, int>();
        }

        public override IEnumerable<UserID> All()
        {
            string connString = @"Data Source=DESKTOP-MAIN;" +
                      @"Initial Catalog=CelebrationOfCapitalism;" +
                      @"Integrated Security=true;";

            List<UserID> benignUserIDs = new List<UserID>();
            using (SqlConnection conn = new SqlConnection(ProgramConfig.DB_CONNECTION_STRING))
            {
                conn.Open();
                string queryString = "SELECT * FROM BenignFlaggedUserIDs";
                SqlCommand command = new SqlCommand(queryString, conn);

                CompanyToken companyToken;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = Convert.ToInt32(reader[0]);
                        benignUserIDs.Add(new UserID(id));
                    }
                }
            }

            return benignUserIDs;
        }

        public override UserID ById(int id)
        {
            return cache.ById(id);
        }

        public override bool Delete(UserID entity)
        {
            return cache.Remove(entity);
        }

        public override bool Insert(UserID entity)
        {
            return cache.Add(entity);
        }

        public override int Size()
        {
            return cache.GetCache().Count;
        }

        public override bool Update(UserID entity)
        {
            return cache.Update(entity);
        }
    }
}
