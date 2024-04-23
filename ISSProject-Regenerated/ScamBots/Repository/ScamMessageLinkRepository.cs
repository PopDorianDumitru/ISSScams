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
    internal class ScamMessageLinkRepository : ISizedRepository<ScamMessageLink, int>
    {
        public ScamMessageLinkRepository()
        {
        }

        public IEnumerable<ScamMessageLink> All()
        {
            string queryString = "SELECT * From ScamMessageLinks";

            using (SqlConnection connection = new SqlConnection(ProgramConfig.DATABASE_CONNECTION_STRING))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(queryString, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int linkId = (int)reader[0];
                        string linkUrl = (string)reader[1];
                        ScamMessageLink currentLink = new ScamMessageLink(linkId, linkUrl);

                        yield return currentLink;
                    }
                }

                connection.Close();
            }
        }

        public ScamMessageLink ById(int id)
        {
            string queryString = "SELECT * From ScamMessageLinks WHERE link_id = @id";
            ScamMessageLink messageLink = null;

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
                        messageLink = new ScamMessageLink(id, content);
                    }
                }

                connection.Close();
            }

            if (messageLink == null)
            {
                throw new ScamMessageRepositoryException("An error occured while trying to get scam URL from the database: a link with specified id does not exist.");
            }

            return messageLink;
        }

        public bool Delete(ScamMessageLink entity)
        {
            int result = 0;
            string queryString = "DELETE FROM ScamMessageLinks WHERE link_id = @link_id";

            try
            {
                using (SqlConnection connection = new SqlConnection(ProgramConfig.DATABASE_CONNECTION_STRING))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();

                    command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@link_id", entity.Id);
                    result = command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new ScamMessageRepositoryException("An error occured while trying to delete scam URL from the database: " + ex.Message);
            }

            return result >= 1;
        }

        public bool Insert(ScamMessageLink entity)
        {
            int result = 0;
            string queryString = "INSERT INTO ScamMessageLinks(link_url) VALUES(@link_url)";

            try
            {
                using (SqlConnection connection = new SqlConnection(ProgramConfig.DATABASE_CONNECTION_STRING))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();

                    command.Parameters.AddWithValue("@link_url", entity.LinkUrl);
                    result = command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new ScamMessageRepositoryException("An error occured while trying to insert scam URL into the database: " + ex.Message);
            }

            return result >= 1;
        }

        public bool Update(ScamMessageLink entity)
        {
            int result = 0;
            string queryString = "UPDATE ScamMessageLinks SET link_url = @link_url WHERE link_id = @link_id";

            try
            {
                using (SqlConnection connection = new SqlConnection(ProgramConfig.DATABASE_CONNECTION_STRING))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();

                    command.Parameters.AddWithValue("@link_id", entity.Id);
                    command.Parameters.AddWithValue("@link_url", entity.LinkUrl);
                    result = command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new ScamMessageRepositoryException("An error occured while trying to update scam URL into the database: " + ex.Message);
            }

            return result >= 1;
        }

        public int Size()
        {
            string queryString = "SELECT COUNT(link_id) From ScamMessageLinks";
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
        /// Returns the ID of a link to a scam website, by searching for the site URL. URLs are unique.
        /// </summary>
        /// <param name="linkUrl">The URL to look for.</param>
        /// <returns>The ID of a link with specified URL, or -1 if such an address does not exist.</returns>
        public int LinkIdByUrl(string linkUrl)
        {
            string queryString = "SELECT link_id From ScamMessageLinks WHERE link_url = @link_url";
            int linkId = -1;

            using (SqlConnection connection = new SqlConnection(ProgramConfig.DATABASE_CONNECTION_STRING))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.Parameters.AddWithValue("@link_url", linkUrl);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        linkId = (int)reader[0];
                    }
                }

                connection.Close();
            }

            return linkId;
        }

        /// <summary>
        /// Returns a randomly chosen scam website link.
        /// </summary>
        /// <returns>A randomly chosen link, or an empty string if there are no websites in the database.</returns>
        public string GetMessageLinkRandomly()
        {
            string queryString = "SELECT TOP 1 link_url From ScamMessageLinks ORDER BY NEWID()";
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
