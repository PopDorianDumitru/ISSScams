using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ISSProject;
using ISSProject.Common;
using ISSProject.Common.Mock;
using ISSProject.ScamBots;
using Microsoft.Data.SqlClient;
namespace ISSProject.Common.Mock
{
    internal class MockUserRepository : IMockUserRepository
    {
        private static MockUserRepository? singleton;
        public static MockUserRepository Provided()
        {
            if (singleton == null)
            {
                singleton = new MockUserRepository();
            }

            return singleton;
        }

        public MockUserRepository()
        {
        }

        public IEnumerable<MockUser> All()
        {
            string queryString = "SELECT * From MockUsers";

            using (SqlConnection connection = new SqlConnection(ProgramConfig.DATABASE_CONNECTION_STRING))
            {
                Console.WriteLine(ProgramConfig.DATABASE_CONNECTION_STRING);
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
                        yield return currentUser;
                    }
                }

                connection.Close();
            }
        }

        public MockUser ById(int id)
        {
            string queryString = "SELECT * From MockUsers WHERE mockuser_id = @id";
            MockUser newUser = null;

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
                    }
                }

                connection.Close();
            }

            if (newUser == null)
            {
                throw new UserRepositoryException("An error occured while trying to get user from the database: an user with specified id does not exist.");
            }

            return newUser;
        }

        public bool Delete(MockUser entity)
        {
            int result = 0;
            string queryString = "DELETE FROM MockUsers WHERE mockuser_id = @mockuser_id";

            try
            {
                using (SqlConnection connection = new SqlConnection(ProgramConfig.DATABASE_CONNECTION_STRING))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();

                    command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@mockuser_id", entity.Id);
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

        public bool Insert(MockUser entity)
        {
            int result = 0;
            string queryString = "INSERT INTO MockUsers(mockuser_password, email, first_name, last_name, birth_date, phone_number) VALUES(@mockuser_password, @email, @first_name, @last_name, @birth_date, @phone_number)";

            try
            {
                using (SqlConnection connection = new SqlConnection(ProgramConfig.DATABASE_CONNECTION_STRING))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();

                    command.Parameters.AddWithValue("@mockuser_password", entity.Password);
                    command.Parameters.AddWithValue("@email", entity.Email);
                    command.Parameters.AddWithValue("@first_name", entity.FirstName);
                    command.Parameters.AddWithValue("@last_name", entity.LastName);
                    command.Parameters.AddWithValue("@birth_date", entity.Birthdate);
                    command.Parameters.AddWithValue("@phone_number", entity.PhoneNumber);

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

        public bool Update(MockUser entity)
        {
            int result = 0;
            string queryString = "UPDATE MockUsers SET mockuser_password = @mockuser_password, email = @email, first_name = @first_name, last_name = @last_name, birth_date = @birth_date, phone_number = @phone_number WHERE mockuser_id = @mockuser_id";

            try
            {
                using (SqlConnection connection = new SqlConnection(ProgramConfig.DATABASE_CONNECTION_STRING))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();

                    command.Parameters.AddWithValue("@mockuser_id", entity.Id);
                    command.Parameters.AddWithValue("@mockuser_password", entity.Password);
                    command.Parameters.AddWithValue("@email", entity.Email);
                    command.Parameters.AddWithValue("@first_name", entity.FirstName);
                    command.Parameters.AddWithValue("@last_name", entity.LastName);
                    command.Parameters.AddWithValue("@birth_date", entity.Birthdate);
                    command.Parameters.AddWithValue("@phone_number", entity.PhoneNumber);

                    result = command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new UserRepositoryException("An error occured while trying to update user into the database: " + ex.Message);
            }

            return result >= 1;
        }
        public int Size()
        {
            string queryString = "SELECT COUNT(mockuser_id) From MockUsers";
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
        /// Returns the ID of an user, by searching using their email address. Email addresses are unique.
        /// </summary>
        /// <param name="email">The email address to look for.</param>
        /// <returns>The ID of an user with specified email, or -1 if such an user does not exist.</returns>
        public static int GetUserIdByEmail(string email)
        {
            string queryString = "SELECT mockuser_id From MockUsers WHERE email = @email";
            int userId = -1;

            using (SqlConnection connection = new SqlConnection(ProgramConfig.DATABASE_CONNECTION_STRING))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.Parameters.AddWithValue("@email", email);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        userId = (int)reader[0];
                    }
                }

                connection.Close();
            }

            return userId;
        }
    }
}
