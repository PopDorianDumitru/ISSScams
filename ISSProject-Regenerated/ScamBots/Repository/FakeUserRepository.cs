using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ISSProject.Common;
using ISSProject.Common.Mock;
using Microsoft.Data.SqlClient;
namespace ISSProject.ScamBots
{
    internal class FakeUserRepository : IFakeUserRepository
    {
        public FakeUserRepository()
        {
        }

        public IEnumerable<MockUser> All()
        {
            string queryString = "SELECT * From MockUsers WHERE EXISTS " +
                "(SELECT FakeUsers.fake_user_id FROM FakeUsers WHERE MockUsers.mockuser_id = FakeUsers.fake_user_id AND NOT EXISTS" +
                "(SELECT BannedUsers.mockuser_id FROM BannedUsers WHERE BannedUsers.mockuser_id = FakeUsers.fake_user_id))";

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
                        yield return currentUser;
                    }
                }
            }
        }

        public MockUser ById(int id)
        {
            string queryString = "SELECT * From MockUsers WHERE mockuser_id = (SELECT fake_user_id FROM FakeUsers WHERE fake_user_id = @id)";
            MockUser fakeUser = null;

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
                        fakeUser = new MockUser(userId, password, email, firstName, lastName, birthdate, phoneNumber);
                    }
                }

                connection.Close();
            }

            if (fakeUser == null)
            {
                throw new FakeUserRepositoryException("An error occured while trying to get fake user from the database: an user with specified id does not exist.");
            }

            return fakeUser;
        }

        public bool Delete(MockUser entity)
        {
            int result = 0;
            string queryString = "DELETE FROM FakeUsers WHERE fake_user_id = @fake_user_id";

            try
            {
                using (SqlConnection connection = new SqlConnection(ProgramConfig.DATABASE_CONNECTION_STRING))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();

                    command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@fake_user_id", entity.Id);
                    result = command.ExecuteNonQuery();

                    queryString = "DELETE FROM MockUsers WHERE mockuser_id = @mockuser_id";
                    command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@mockuser_id", entity.Id);
                    result = command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new FakeUserRepositoryException("An error occured while trying to delete fake user from the database: " + ex.Message);
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

                    if (result > 0)
                    {
                        int userId = UserIdByEmail(entity.Email);

                        queryString = "INSERT INTO FakeUsers(fake_user_id) VALUES(@fake_user_id)";
                        command = new SqlCommand(queryString, connection);
                        command.Parameters.AddWithValue("@fake_user_id", userId);

                        result = command.ExecuteNonQuery();
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new FakeUserRepositoryException("An error occured while trying to insert fake user into the database: " + ex.Message);
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
                    command.Parameters.AddWithValue("@mockuser_password", entity.Email);
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
                throw new FakeUserRepositoryException("An error occured while trying to update fake user into the database: " + ex.Message);
            }

            return result >= 1;
        }

        public int Size()
        {
            string queryString = "SELECT COUNT(fake_user_id) From FakeUsers WHERE NOT EXISTS (SELECT BannedUsers.mockuser_id FROM BannedUsers WHERE BannedUsers.mockuser_id = FakeUsers.fake_user_id)";
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

        public int NumberOfBannedFakeAccounts()
        {
            string queryString = "SELECT COUNT(fake_user_id) From FakeUsers WHERE EXISTS (SELECT BannedUsers.mockuser_id FROM BannedUsers WHERE BannedUsers.mockuser_id = FakeUsers.fake_user_id)";
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

        public int UserIdByEmail(string email)
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
