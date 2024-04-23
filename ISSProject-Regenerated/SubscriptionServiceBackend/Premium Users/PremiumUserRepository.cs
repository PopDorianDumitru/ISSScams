using ISSProject.Common.Mock;
using ISSProject.Common.Wrapper;

using Microsoft.Data.SqlClient;
namespace ISSProject.Common.Mikha.Premium_Users
{
    internal class PremiumUserRepository : IRepository<UserWrapper, int>
    {
        public IEnumerable<UserWrapper> All()
        {
            string queryString = "SELECT * From PremiumUsersView";

            using (SqlConnection connection = new SqlConnection(ProgramConfig.DATABASE_CONNECTION_STRING))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(queryString, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        MockUser currentUser = new MockUser();
                        currentUser.Id = (int)reader[0];
                        currentUser.Password = (string)reader[1];
                        currentUser.Email = (string)reader[2];
                        currentUser.FirstName = (string)reader[3];
                        currentUser.LastName = (string)reader[4];
                        currentUser.Birthdate = (DateTime)reader[5];
                        currentUser.PhoneNumber = (string)reader[6];

                        UserWrapper currentUserWrapper = new UserWrapper(currentUser);
                        yield return currentUserWrapper;
                    }
                }

                connection.Close();
            }
        }

        public UserWrapper ById(int id)
        {
            string queryString = "SELECT * From PremiumUsersView WHERE mockuser_id = @id";
            MockUser newUser = null;
            UserWrapper newUserWrapper = null;

            using (SqlConnection connection = new SqlConnection(ProgramConfig.DATABASE_CONNECTION_STRING))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.Parameters.AddWithValue("@id", id);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int userId = (int)reader[0];
                        string password = (string)reader[1];
                        string email = (string)reader[2];
                        string firstName = (string)reader[3];
                        string lastName = (string)reader[4];
                        DateTime birthdate = (DateTime)reader[5];
                        string phoneNumber = (string)reader[6];
                        newUser = new MockUser(userId, password, email, firstName, lastName, birthdate, phoneNumber);

                        newUserWrapper = new UserWrapper(newUser);
                    }
                }

                connection.Close();
            }

            if (newUser == null)
            {
                newUserWrapper = null;
            }
            // throw new UserRepositoryException("An error occured while trying to get user from the database: an user with specified id does not exist.");
            return newUserWrapper;
        }

        public bool Delete(UserWrapper entity)
        {
            int result = 0;
            string queryString = "DELETE FROM PremiumUsers WHERE mockuser_id = @mockuser_id";

            try
            {
                using (SqlConnection connection = new SqlConnection(ProgramConfig.DATABASE_CONNECTION_STRING))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();

                    command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@mockuser_id", entity.GetPureReference().Id);
                    result = command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new UserRepositoryException("An error occured while trying to delete user from the database: " + ex.Message);
            }

            return result >= 1;
        }

        public bool Insert(UserWrapper entity)
        {
            int result = 0;
            string queryString = "INSERT INTO PremiumUsers(mockuser_id) VALUES(@mockuser_id)";

            try
            {
                using (SqlConnection connection = new SqlConnection(ProgramConfig.DATABASE_CONNECTION_STRING))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();

                    MockUser entity_aux = entity.GetPureReference();
                    command.Parameters.AddWithValue("@mockuser_id", entity_aux.GetId());
                    result = command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new UserRepositoryException("An error occured while trying to insert user into the database: " + ex.Message);
            }

            return result >= 1;
        }

        public bool Update(UserWrapper entity)
        {
            return false;
        }
    }
}
