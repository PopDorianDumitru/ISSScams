using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSfixed.ISSProject.Common.Mikha.Premium_Messages;
using ISSProject.Common.Cache;
using ISSProject.Common.Mock;
using ISSProject.Common.Wrapper;
using ISSProject.ScamBots;
using ISSProject_Regenerated.SubscriptionServiceBackend.Premium_Messages;
using ISSProject_Regenerated.SubscriptionServiceBackend.Premium_Users;
using Microsoft.Data.SqlClient;
namespace ISSProject.Common.Mikha.Premium_Messages
{
    internal class PremiumMessageRepository : IPremiumMessageRepository
    {
        public IEnumerable<MessageWrapper> All()
        {
            string queryString = "SELECT * From PremiumMessagesView";

            using (SqlConnection connection = new SqlConnection(ProgramConfig.DATABASE_CONNECTION_STRING))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(queryString, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = (int)reader[0];
                        int sender_id = (int)reader[1];
                        int reciever_id = (int)reader[2];
                        DateTime communication_Date = (DateTime)reader[3];
                        string content = (string)reader[4];
                        MockMessage currentMessage = new MockMessage(sender_id, reciever_id, content, communication_Date);

                        MessageWrapper currentMessageWrapper = new MessageWrapper(currentMessage);
                        yield return currentMessageWrapper;
                    }
                }

                connection.Close();
            }
        }

        public MessageWrapper ById(int id)
        {
            string queryString = "SELECT * From PremiumMessagesView WHERE message_id = @id";
            MockMessage newMessage = null;
            MessageWrapper newMessageWrapper = null;

            using (SqlConnection connection = new SqlConnection(ProgramConfig.DATABASE_CONNECTION_STRING))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.Parameters.AddWithValue("@id", id);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int message_id = (int)reader[0];
                        int sender_id = (int)reader[1];
                        int reciever_id = (int)reader[2];
                        DateTime communication_Date = (DateTime)reader[3];
                        string content = (string)reader[4];
                        newMessage = new MockMessage(sender_id, reciever_id, content, communication_Date);

                        newMessageWrapper = new MessageWrapper(newMessage);
                    }
                }

                connection.Close();
            }

            if (newMessage == null)
            {
                throw new PremiumMessageRepositoryError("An error occured while trying to get message from the database: a message with specified id does not exist.");
            }

            return newMessageWrapper;
        }

        public bool Delete(MessageWrapper entity)
        {
            int result = 0;
            string queryString = "DELETE FROM PremiumMessages WHERE message_id = @message_id";

            try
            {
                using (SqlConnection connection = new SqlConnection(ProgramConfig.DATABASE_CONNECTION_STRING))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();

                    command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@message_id", entity.GetPureReference().Id);
                    result = command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new PremiumMessageRepositoryError("An error occured while trying to delete message from the database: " + ex.Message);
            }

            return result >= 1;
        }

        public bool Insert(MessageWrapper entity)
        {
            int result = 0;
            string queryString = "INSERT INTO PremiumMessages(message_id) VALUES(@message_id)";

            try
            {
                using (SqlConnection connection = new SqlConnection(ProgramConfig.DATABASE_CONNECTION_STRING))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();

                    MockMessage entity_aux = entity.GetPureReference();

                    command.Parameters.AddWithValue("@message_id", entity_aux.GetId());
                    result = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new PremiumMessageRepositoryError("An error occured while trying to insert message into the database: " + ex.Message);
            }

            return result >= 1;
        }

        public bool Update(MessageWrapper entity)
        {
            return false;
        }
    }
}
