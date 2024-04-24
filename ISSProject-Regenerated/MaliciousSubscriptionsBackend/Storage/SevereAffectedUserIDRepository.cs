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
    internal class SevereAffectedUserIDRepository : ICachedRepository<UserID, int>, ISevereAffectedUserIDRepository
    {
        public SevereAffectedUserIDRepository() : base()
        {
            cache = new SimpleKeyedMapCache<UserID, int>();
        }

        public override IEnumerable<UserID> All()
        {
            string connString = @"Data Source=DESKTOP-MAIN;" +
          @"Initial Catalog=CelebrationOfCapitalism;" +
          @"Integrated Security=true;";

            List<UserID> severeUserIDs = new List<UserID>();
            using (SqlConnection conn = new SqlConnection(ProgramConfig.DATABASE_CONNECTION_STRING))
            {
                conn.Open();
                string queryString = "SELECT * FROM SevereFlaggedUserIDs";
                SqlCommand command = new SqlCommand(queryString, conn);

                CompanyToken companyToken;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = Convert.ToInt32(reader[0]);
                        severeUserIDs.Add(new UserID(id));
                    }
                }
            }

            return severeUserIDs;
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
