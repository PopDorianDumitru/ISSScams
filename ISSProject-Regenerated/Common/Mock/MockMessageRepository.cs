using ISSProject.Common.Mock;
using ISSProject.ScamBots;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

using Microsoft.Data.SqlClient;
namespace ISSProject.Common.Mock
{
    internal class MockMessageRepository : ISizedRepository<MockMessage, int>
    {
        private static MockMessageRepository _singleton;
        public static MockMessageRepository Provided()
        {
            if (_singleton == null) _singleton = new MockMessageRepository();
            return _singleton;
        }
        public MockMessageRepository() { }

        public IEnumerable<MockMessage> All()
        {
            string queryString = "SELECT * From MockMessages";

            using (SqlConnection connection = new SqlConnection(ProgramConfig.DB_CONNECTION_STRING))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(queryString, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int messsageId = (int)reader[0];
                        int senderId = (int)reader[1];
                        int receiverId = (int)reader[2];
                        DateTime communicationDate = (DateTime)reader[3];
                        string messageContent = (string)reader[4];
                        MockMessage currentMessage = new MockMessage(messsageId, senderId, receiverId, messageContent, communicationDate);

                        yield return currentMessage;
                    }
                }

                connection.Close();
            }
        }

        public MockMessage ById(int id)
        {
            string queryString = "SELECT * From MockMessages WHERE message_id = @id";
            MockMessage message = null;

            using (SqlConnection connection = new SqlConnection(ProgramConfig.DB_CONNECTION_STRING))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.Parameters.AddWithValue("@id", id);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int messsageId = (int)reader[0];
                        int senderId = (int)reader[1];
                        int receiverId = (int)reader[2];
                        DateTime communicationDate = (DateTime)reader[3];
                        string messageContent = (string)reader[4];
                        message = new MockMessage(messsageId, senderId, receiverId, messageContent, communicationDate);
                    }
                }

                connection.Close();
            }

            if (message == null)
                throw new MessageRepositoryException("An error occured while trying to get message from the database: a message with specified id does not exist.");

            return message;
        }

        public bool Delete(MockMessage entity)
        {
            int result = 0;
            string queryString = "DELETE FROM MockMessages WHERE message_id = @message_id";

            try
            {
                using (SqlConnection connection = new SqlConnection(ProgramConfig.DB_CONNECTION_STRING))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();

                    command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@message_id", entity.Id);
                    result = command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new MessageRepositoryException("An error occured while trying to delete message from the database: " + ex.Message);
            }

            return result >= 1;
        }

        public bool Insert(MockMessage entity)
        {
            int result = 0;
            string queryString = "INSERT INTO MockMessages(sender_id, receiver_id, communication_date, message_content) VALUES(@sender_id, @receiver_id, @communication_date, @message_content)";

            try
            {
                using (SqlConnection connection = new SqlConnection(ProgramConfig.DB_CONNECTION_STRING))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();

                    command.Parameters.AddWithValue("@sender_id", entity.SenderId);
                    command.Parameters.AddWithValue("@receiver_id", entity.ReceiverId);
                    command.Parameters.AddWithValue("@communication_date", entity.CommunicationDate);
                    command.Parameters.AddWithValue("@message_content", entity.MessageContent);

                    result = command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new MessageRepositoryException("An error occured while trying to insert message into the database: " + ex.Message);
            }


            return result >= 1;
        }

        public bool Update(MockMessage entity)
        {
            int result = 0;
            string queryString = "UPDATE MockMessages SET sender_id = @sender_id, receiver_id = @receiver_id, communication_date = @communication_date, message_content = @message_content WHERE message_id = @message_id";

            try
            {
                using (SqlConnection connection = new SqlConnection(ProgramConfig.DB_CONNECTION_STRING))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();

                    command.Parameters.AddWithValue("@message_id", entity.Id);
                    command.Parameters.AddWithValue("@sender_id", entity.SenderId);
                    command.Parameters.AddWithValue("@receiver_id", entity.ReceiverId);
                    command.Parameters.AddWithValue("@communication_date", entity.CommunicationDate);
                    command.Parameters.AddWithValue("@message_content", entity.MessageContent);

                    result = command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new MessageRepositoryException("An error occured while trying to update message into the database: " + ex.Message);
            }

            return result >= 1;
        }

        public int Size()
        {
            string queryString = "SELECT COUNT(message_id) From MockMessages";
            int result = 0;

            using (SqlConnection connection = new SqlConnection(ProgramConfig.DB_CONNECTION_STRING))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                        result = (int)reader[0];
                }

                connection.Close();
            }

            return result;
        }

        /// <summary>
        /// Returns the ID of a message, by searching for it based on its context. Message contexts are unique.
        /// </summary>
        /// <param name="senderId">The sender ID from the context.</param>
        /// <param name="receiverId">The receiver ID from the context.</param>
        /// <param name="communicationDate">The date time when the message was created.</param>
        /// <returns>The ID of a message with specified context, or -1 if such a message does not exist.</returns>
        public int getMessageIdByConversationStatus(int senderId, int receiverId, DateTime communicationDate)
        {
            string queryString = "SELECT message_id From MockMessages WHERE sender_id = @sender_id AND receiver_id = @receiver_id AND communication_date = @communication_date";
            int messageId = -1;

            using (SqlConnection connection = new SqlConnection(ProgramConfig.DB_CONNECTION_STRING))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.Parameters.AddWithValue("@sender_id", senderId);
                command.Parameters.AddWithValue("@receiver_id", receiverId);
                command.Parameters.AddWithValue("@communication_date", communicationDate);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        messageId = (int)reader[0];
                    }
                }

                connection.Close();
            }

            return messageId;
        }

        /// <summary>
        /// Returns all messages between a sender and receiver.
        /// </summary>
        /// <param name="senderId">The id of the sender</param>
        /// <param name="receiverId">The id of the receiver</param>
        /// <returns>A list of ids of the messages from sender to receiver</returns>
        public List<int> GetMessages(int senderId, int receiverId)
        {
            string queryString = "SELECT message_id From MockMessages WHERE sender_id = @sender_id AND receiver_id = @receiver_id";
            List<int> messages = new List<int>();

            using (SqlConnection connection = new SqlConnection(ProgramConfig.DB_CONNECTION_STRING))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.Parameters.AddWithValue("@sender_id", senderId);
                command.Parameters.AddWithValue("@receiver_id", receiverId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var messageId = (int) reader[0];
                        messages.Add(messageId);
                    }
                }

                connection.Close();
            }

            return messages;
        }

        /// <summary>
        /// Returns all receivers who have received messages from sender.
        /// </summary>
        /// <param name="senderId">The id of the sender</param>
        /// <returns>A list of ids of the receivers that were messaged by sender</returns>
        public List<int> GetReceiversOfSender(int senderId)
        {
            string queryString = "SELECT receiver_id From MockMessages WHERE sender_id = @sender_id";
            List<int> receivers = new List<int>();

            using (SqlConnection connection = new SqlConnection(ProgramConfig.DB_CONNECTION_STRING))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.Parameters.AddWithValue("@sender_id", senderId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var receiverId = (int)reader[0];
                        receivers.Add(receiverId);
                    }
                }

                connection.Close();
            }

            return receivers;
        }
    }
}
