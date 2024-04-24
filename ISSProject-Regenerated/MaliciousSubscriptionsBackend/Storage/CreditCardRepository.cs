using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSProject.Common;
using ISSProject.Common.Cache;
using ISSProject.Common.Wrapper;
using ISSProject.MaliciousSubscriptionsBackend.Domain;
using Microsoft.Data.SqlClient;
namespace ISSProject.MaliciousSubscriptionsBackend.Storage
{
    internal class CreditCardRepository : ICachedRepository<CreditCard, int>, ICreditCardRepository
    {
        public CreditCardRepository() : base()
        {
            cache = new SimpleKeyedMapCache<CreditCard, int>();
        }

        public override IEnumerable<CreditCard> All()
        {
            string connString = @"Data Source=DESKTOP-MAIN;" +
                      @"Initial Catalog=CelebrationOfCapitalism;" +
                      @"Integrated Security=true;";

            List<CreditCard> creditCards = new List<CreditCard>();
            using (SqlConnection conn = new SqlConnection(ProgramConfig.DATABASE_CONNECTION_STRING))
            {
                conn.Open();
                string queryString = "SELECT * FROM CreditCards";
                SqlCommand command = new SqlCommand(queryString, conn);

                CreditCard creditCard;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int cardId = (int)reader[0];
                        int userholderId = (int)reader[1];
                        string userholderName = (string)reader[2];
                        string creditcardNumber = (string)reader[3];
                        string expirationDate = (string)reader[4];
                        string cvv = (string)reader[5];

                        creditCard = new CreditCard(cardId, userholderId, userholderName, creditcardNumber, expirationDate, cvv);
                        creditCards.Add(creditCard);
                    }
                }
            }

            return creditCards;
        }

        public override CreditCard ById(int id)
        {
            return cache.ById(id);
        }

        public override bool Delete(CreditCard entity)
        {
            return cache.Remove(entity);
        }

        public override bool Insert(CreditCard entity)
        {
            return cache.Add(entity);
        }

        public override int Size()
        {
            return cache.GetCache().Count;
        }

        public override bool Update(CreditCard entity)
        {
            return cache.Update(entity);
        }
    }
}
