using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSProject.Common;
using ISSProject.Common.Mock;
using ISSProject.ScamBots.Model;
using Microsoft.Data.SqlClient;

namespace ISSProject.ScamBots
{
    internal class ScamMessageRepository : IScamMessageRepository
    {
        public ScamMessageRepository()
        {
        }

        public IEnumerable<ScamMessageTemplate> All()
        {
            string queryString = "SELECT * From ScamMessageTemplates";

            using (SqlConnection connection = new SqlConnection(ProgramConfig.DATABASE_CONNECTION_STRING))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(queryString, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int templateId = (int)reader[0];
                        string templateContent = (string)reader[1];
                        ScamMessageTemplate currentMessageTemplate = new ScamMessageTemplate(templateId, templateContent);

                        yield return currentMessageTemplate;
                    }
                }

                connection.Close();
            }
        }

        public ScamMessageTemplate ById(int id)
        {
            string queryString = "SELECT * From ScamMessageTemplates WHERE template_id = @id";
            ScamMessageTemplate messageTemplate = null;

            using (SqlConnection connection = new SqlConnection(ProgramConfig.DATABASE_CONNECTION_STRING))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.Parameters.AddWithValue("@id", id);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string content = (string)reader[1];
                        messageTemplate = new ScamMessageTemplate(id, content);
                    }
                }

                connection.Close();
            }

            if (messageTemplate == null)
            {
                throw new ScamMessageRepositoryException("An error occured while trying to get message template from the database: a template with specified id does not exist.");
            }

            return messageTemplate;
        }

        public bool Delete(ScamMessageTemplate entity)
        {
            int result = 0;
            string queryString = "DELETE FROM ScamMessageTemplates WHERE template_id = @template_id";

            try
            {
                using (SqlConnection connection = new SqlConnection(ProgramConfig.DATABASE_CONNECTION_STRING))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();

                    command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@template_id", entity.Id);
                    result = command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new ScamMessageRepositoryException("An error occured while trying to delete message template from the database: " + ex.Message);
            }

            return result >= 1;
        }

        public bool Insert(ScamMessageTemplate entity)
        {
            int result = 0;
            string queryString = "INSERT INTO ScamMessageTemplates(template_content) VALUES(@template_content)";

            try
            {
                using (SqlConnection connection = new SqlConnection(ProgramConfig.DATABASE_CONNECTION_STRING))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();

                    command.Parameters.AddWithValue("@template_content", entity.MessageContent);
                    result = command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new ScamMessageRepositoryException("An error occured while trying to insert message template into the database: " + ex.Message);
            }

            return result >= 1;
        }

        public bool Update(ScamMessageTemplate entity)
        {
            int result = 0;
            string queryString = "UPDATE ScamMessageTemplates SET template_content = @template_content WHERE template_id = @template_id";

            try
            {
                using (SqlConnection connection = new SqlConnection(ProgramConfig.DATABASE_CONNECTION_STRING))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();

                    command.Parameters.AddWithValue("@template_id", entity.Id);
                    command.Parameters.AddWithValue("@template_content", entity.MessageContent);
                    result = command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new ScamMessageRepositoryException("An error occured while trying to update message template into the database: " + ex.Message);
            }

            return result >= 1;
        }

        public int Size()
        {
            string queryString = "SELECT COUNT(template_id) From ScamMessageTemplates";
            int result = 0;

            using (SqlConnection connection = new SqlConnection(ProgramConfig.DATABASE_CONNECTION_STRING))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = (int)reader[0];
                    }
                }

                connection.Close();
            }

            return result;
        }

        /// <summary>
        /// Returns the ID of a message template, by searching for the template text. Templates texts are unique.
        /// </summary>
        /// <param name="templateContent">The template to look for.</param>
        /// <returns>The ID of a message with specified template, or -1 if such a message does not exist.</returns>
        public int MessageIdByTemplate(string templateContent)
        {
            string queryString = "SELECT template_id From ScamMessageTemplates WHERE template_content = @template_content";
            int messageId = -1;

            using (SqlConnection connection = new SqlConnection(ProgramConfig.DATABASE_CONNECTION_STRING))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.Parameters.AddWithValue("@template_content", templateContent);

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
        /// Returns a randomly chosen message template.
        /// </summary>
        /// <returns>A randomly chosen message template, or an empty string if there are no templates in the database.</returns>
        public string GetMessageTemplateRandomly()
        {
            string queryString = "SELECT TOP 1 template_content From ScamMessageTemplates ORDER BY NEWID()";
            string result = string.Empty;

            using (SqlConnection connection = new SqlConnection(ProgramConfig.DATABASE_CONNECTION_STRING))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = (string)reader[0];
                    }
                }

                connection.Close();
            }

            return result;
        }
    }
}
