using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSfixed.ISSProject.Common.Mikha.Post;
using ISSProject.Common.Mock;
using ISSProject.Common.Wrapper;
using ISSProject.ScamBots;
using ISSProject_Regenerated.SubscriptionServiceBackend.Post;
using Microsoft.Data.SqlClient;
namespace ISSProject.Common.Mikha
{
    internal class MockPostRepository : IMockPostRepository
    {
        /* Mock Holding Data */
        private static Dictionary<int, MockPost> mockDatabase = new Dictionary<int, MockPost>();

        public static void ResetMockDatabase()
        {
            mockDatabase = new Dictionary<int, MockPost>();
        }

        private static MockPostRepository singleton;
        public static MockPostRepository Provided()
        {
            if (singleton == null)
            {
                singleton = new MockPostRepository();
            }

            return singleton;
        }

        /* IRepository */

        public IEnumerable<MockPost> All()
        {
            string queryString = "SELECT * From MockPosts";

            using (SqlConnection connection = new SqlConnection(ProgramConfig.DATABASE_CONNECTION_STRING))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(queryString, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = (int)reader[0];
                        string post_title = (string)reader[1];
                        int user_id = (int)reader[3];
                        string post_content = (string)reader[2];
                        DateTime post_date = (DateTime)reader[4];
                        MockPost currentPost = new MockPost(id, user_id, post_title, post_content, post_date);

                        yield return currentPost;
                    }
                }

                connection.Close();
            }
        }

        public MockPost ById(int id)
        {
            string queryString = "SELECT * From MockPosts WHERE mockpost_id = @id";
            MockPost newPost = null;

            using (SqlConnection connection = new SqlConnection(ProgramConfig.DATABASE_CONNECTION_STRING))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.Parameters.AddWithValue("@id", id);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int post_id = (int)reader[0];
                        string post_title = (string)reader[1];
                        int user_id = (int)reader[2];
                        string post_content = (string)reader[3];
                        DateTime post_date = (DateTime)reader[4];
                        newPost = new MockPost(post_id, user_id, post_title, post_content, post_date);
                    }
                }

                connection.Close();
            }

            if (newPost == null)
            {
                throw new MockPostError("An error occured while trying to get group from the database: a post with specified id does not exist.");
            }

            return newPost;
        }

        public bool Delete(MockPost entity)
        {
            int result = 0;
            string queryString = "DELETE FROM MockPosts WHERE mockpost_id = @mockpost_id";

            try
            {
                using (SqlConnection connection = new SqlConnection(ProgramConfig.DATABASE_CONNECTION_STRING))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();

                    command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@mockpost_id", entity.Id);
                    result = command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception exception)
            {
                throw new MockPostError("An error occured while trying to delete post from the database: " + exception.Message);
            }

            return result >= 1;
        }

        public bool Insert(MockPost entity)
        {
            int result = 0;
            string queryString = "INSERT INTO MockPosts(mockpost_title, mockpost_content, mockpost_creator_id, mockpost_date) VALUES(@mockpost_title, @mockpost_content, @mockpost_creator_id, @mockpost_date)";

            try
            {
                using (SqlConnection connection = new SqlConnection(ProgramConfig.DATABASE_CONNECTION_STRING))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();

                    command.Parameters.AddWithValue("@mockpost_title", entity.PostTitle);
                    command.Parameters.AddWithValue("@mockpost_content", entity.PostContent);
                    command.Parameters.AddWithValue("@mockpost_creator_id", entity.PosterId);
                    command.Parameters.AddWithValue("@mockpost_date", entity.PostDate);

                    result = command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception exception)
            {
                throw new MockPostError("An error occured while trying to insert post into the database: " + exception.Message);
            }

            return result >= 1;
        }

        public bool Update(MockPost entity)
        {
            int result = 0;
            string queryString = "UPDATE MockPosts SET mockpost_title = @mockpost_title, mockpost_content = @mockpost_content, mockpost_creator_id = @mockpost_creator_id, mockpost_date = @mockpost_date WHERE mockpost_id = @mockpost_id";

            try
            {
                using (SqlConnection connection = new SqlConnection(ProgramConfig.DATABASE_CONNECTION_STRING))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();

                    command.Parameters.AddWithValue("@mockpost_id", entity.Id);
                    command.Parameters.AddWithValue("@mockpost_title", entity.PostTitle);
                    command.Parameters.AddWithValue("@mockpost_content", entity.PostContent);
                    command.Parameters.AddWithValue("@mockpost_creator_id", entity.PosterId);

                    result = command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception exception)
            {
                throw new MockPostError("An error occured while trying to update post into the database: " + exception.Message);
            }

            return result >= 1;
        }
    }
}
