using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSfixed.ISSProject.Common.Mikha.Post;
using ISSfixed.ISSProject.Mikha.Groups;
using ISSProject_Regenerated.SubscriptionServiceBackend.Groups;
using Microsoft.Data.SqlClient;
namespace ISSProject.Common.Mikha.Groups
{
    internal class MockGroupRepository : IMockGroupRepository
    {
        /* Mock Holding Data */
        private static Dictionary<int, MockGroup> mockDatabase = new Dictionary<int, MockGroup>();

        // TODO: Check if it is enough to clear or if it needs new elements
        public static void ResetMockDatabase()
        {
            mockDatabase = new Dictionary<int, MockGroup>();
        }

        private static MockGroupRepository singleton;
        public static MockGroupRepository Provided()
        {
            if (singleton == null)
            {
                singleton = new MockGroupRepository();
            }

            return singleton;
        }

        public static int AddAllMembers(MockGroup group, SqlConnection connection)
        {
            string queryString = "SELECT * From GetUsersFromGroup(@group_id)";
            SqlCommand sqlCommand = new SqlCommand(queryString, connection);
            sqlCommand.Parameters.AddWithValue("@group_id", group.Id);
            int no_items = 0;
            using (SqlDataReader reader2 = sqlCommand.ExecuteReader())
            {
                while (reader2.Read())
                {
                    group.AddMember((int)reader2[0]);
                    no_items++;
                }
            }
            return no_items;
        }

        /* IRepository */

        public IEnumerable<MockGroup> All()
        {
            string queryString = "SELECT * From MockGroups";

            using (SqlConnection connection = new SqlConnection(ProgramConfig.DATABASE_CONNECTION_STRING))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(queryString, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int group_id = (int)reader[0];
                        string group_name = (string)reader[1];
                        bool is_private = (bool)reader[2];
                        MockGroup currentGroup = new MockGroup(group_id, group_name, is_private);

                        // AddAllMembers(currentGroup, connection); // ACCOMODATE FOR THIS IN THE FUTURE
                        yield return currentGroup;
                    }
                }

                connection.Close();
            }
        }

        public MockGroup ById(int id)
        {
            string queryString = "SELECT * From MockGroups WHERE mockgroup_id = @id";
            MockGroup newGroup = null;

            using (SqlConnection connection = new SqlConnection(ProgramConfig.DATABASE_CONNECTION_STRING))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.Parameters.AddWithValue("@id", id);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int group_id = (int)reader[0];
                        string group_name = (string)reader[1];
                        bool is_private = (bool)reader[2];
                        MockGroup currentGroup = new MockGroup(group_id, group_name, is_private);

                        // AddAllMembers(currentGroup, connection);  // ACCOMODATE FOR THIS IN THE FUTURE
                    }
                }

                connection.Close();
            }

            if (newGroup == null)
            {
                throw new GroupRepositoryError("An error occured while trying to get group from the database: a group with specified id does not exist.");
            }

            return newGroup;
        }

        public bool Delete(MockGroup entity)
        {
            int result = 0;
            string queryString = "DELETE FROM MockGroups WHERE mockgroup_id = @mockgroup_id";

            try
            {
                using (SqlConnection connection = new SqlConnection(ProgramConfig.DATABASE_CONNECTION_STRING))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();

                    command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@mockgroup_id", entity.Id);
                    result = command.ExecuteNonQuery();

                    SqlCommand command1 = new SqlCommand("Delete From GroupUserRelationship where mockgroup_id = @mockgroup_id", connection);
                    command1.Parameters.AddWithValue("@mockgroup_id", entity.Id);
                    command1.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new GroupRepositoryError("An error occured while trying to delete group from the database: " + ex.Message);
            }

            return result >= 1;
        }

        public bool Insert(MockGroup entity)
        {
            int result = 0;
            string queryString = "INSERT INTO MockGroups(mockgroup_name) VALUES(@mockgroup_name)";

            try
            {
                using (SqlConnection connection = new SqlConnection(ProgramConfig.DATABASE_CONNECTION_STRING))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();

                    command.Parameters.AddWithValue("@mockgroup_name", entity.GroupName);

                    result = command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new GroupRepositoryError("An error occured while trying to insert group into the database: " + ex.Message);
            }

            return result >= 1;
        }

        public bool Update(MockGroup entity)
        {
            int result = 0;
            string queryString = "UPDATE MockGroups SET mockgroup_name = @mockgroup_name WHERE mockgroup_id = @mockgroup_id ";

            try
            {
                using (SqlConnection connection = new SqlConnection(ProgramConfig.DATABASE_CONNECTION_STRING))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();

                    command.Parameters.AddWithValue("@mockgroup_id ", entity.Id);
                    command.Parameters.AddWithValue("@mockgroup_name", entity.GroupName);

                    result = command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new GroupRepositoryError("An error occured while trying to update group into the database: " + ex.Message);
            }

            return result >= 1;
        }
    }
}
