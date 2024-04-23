using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSfixed.ISSProject.Common.Mikha.Post;
using ISSProject.Common.Cache;
using ISSProject.Common.Mock;
using ISSProject.Common.Wrapper;
using ISSProject.ScamBots;
using Microsoft.Data.SqlClient;
namespace ISSProject.Common.Mikha
{
    internal class PremiumPostRepository : IRepository<PostWrapper, int>
    {
        private SimpleKeyedMapCache<PostWrapper, int> SimpleKeyedMapCache { get; set; }

        public IEnumerable<PostWrapper> All()
        {
            string queryString = "SELECT * From PremiumPostsView";

            using (SqlConnection connection = new SqlConnection(ProgramConfig.DB_CONNECTION_STRING))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(queryString, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = (int)reader[0];
                        string title = (string)reader[1];
                        string content = (string)reader[2];
                        int poster_id = (int)reader[3];
                        DateTime post_date = (DateTime)reader[4];
                        MockPost currentPost = new MockPost(id, poster_id, title, content, post_date);

                        PostWrapper currentUserWrapper = new PostWrapper(currentPost);
                        yield return currentUserWrapper;
                    }
                }

                connection.Close();
            }
        }

        public PostWrapper ById(int id)
        {
            string queryString = "SELECT * From PremiumPostsView WHERE mockpost_id = @id";
            MockPost newPost = null;
            PostWrapper newPostWrapper = null;

            using (SqlConnection connection = new SqlConnection(ProgramConfig.DB_CONNECTION_STRING))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.Parameters.AddWithValue("@id", id);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int post_id = (int)reader[0];
                        string title = (string)reader[1];
                        string content = (string)reader[2];
                        int poster_id = (int)reader[3];
                        DateTime post_date = (DateTime)reader[4];
                        newPost = new MockPost(post_id, poster_id, title, content, post_date);

                        newPostWrapper = new PostWrapper(newPost);
                    }
                }

                connection.Close();
            }

            if (newPost == null)
            {
                newPostWrapper = null;
            }
            // throw new PremiumPostRepositoryException("An error occured while trying to get user from the database: a post with specified id does not exist.");
            return newPostWrapper;
        }

        public bool Delete(PostWrapper entity)
        {
            int result = 0;
            string queryString = "DELETE FROM PremiumPosts WHERE mockpost_id = @mockpost_id";

            try
            {
                using (SqlConnection connection = new SqlConnection(ProgramConfig.DB_CONNECTION_STRING))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();

                    command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@mockpost_id", entity.GetPureReference().Id);
                    result = command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new PremiumPostRepositoryException("An error occured while trying to delete post from the database: " + ex.Message);
            }

            return result >= 1;
        }

        public bool Insert(PostWrapper entity)
        {
            int result = 0;
            string queryString = "INSERT INTO PremiumPosts(mockpost_id) VALUES(@mockpost_id)";

            try
            {
                using (SqlConnection connection = new SqlConnection(ProgramConfig.DB_CONNECTION_STRING))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();

                    MockPost entity_aux = entity.GetPureReference();
                    command.Parameters.AddWithValue("@mockpost_id", entity_aux.Id);
                    result = command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new PremiumPostRepositoryException("An error occured while trying to insert post into the database: " + ex.Message);
            }
            return result >= 1;
        }

        public bool Update(PostWrapper entity)
        {
            return false;
        }
    }
}

