using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSProject;
using ISSProject.Common;
using ISSProject.Common.Mock;
using ISSProject.GraphAnalyser.Domain;
using ISSProject.ScamBots;
using ISSProject.ScamBots.Model;
using Microsoft.Data.SqlClient;
namespace ISSProject.GraphAnalyser.Repository
{
    internal class GraphAnalyserLogRepository : IRepository<GraphAnalyserLog, int>
    {
        private static GraphAnalyserLogRepository? singleton;
        public static GraphAnalyserLogRepository Provided()
        {
            if (singleton == null)
            {
                singleton = new GraphAnalyserLogRepository();
            }

            return singleton;
        }

        public IEnumerable<GraphAnalyserLog> All()
        {
            string queryString = "SELECT * From GraphAnalyzerLogs";

            using (SqlConnection connection = new SqlConnection(ProgramConfig.DATABASE_CONNECTION_STRING))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(queryString, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = (int)reader[0];
                        DateTime time = (DateTime)reader[1];
                        int sourceUser = (int)reader[2];
                        int targetUser = (int)reader[3];
                        int score = (int)reader[4];
                        string message = (string)reader[5];
                        var log = new GraphAnalyserLog(id, time, sourceUser, targetUser, score, message);

                        yield return log;
                    }
                }

                connection.Close();
            }
        }

        public GraphAnalyserLog ById(int id)
        {
            string queryString = "SELECT * From GraphAnalyzerLogs WHERE LogID=@id";
            GraphAnalyserLog log = null;

            using (SqlConnection connection = new SqlConnection(ProgramConfig.DATABASE_CONNECTION_STRING))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@id", id);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        DateTime time = (DateTime)reader[1];
                        int sourceUser = (int)reader[2];
                        int targetUser = (int)reader[3];
                        int score = (int)reader[4];
                        string message = (string)reader[5];
                        log = new GraphAnalyserLog(id, time, sourceUser, targetUser, score, message);
                    }
                }

                connection.Close();
            }

            return log;
        }

        public bool Delete(GraphAnalyserLog entity)
        {
            int result = 0;
            string queryString = "DELETE FROM GraphAnalyzerLogs WHERE LogID=@id";

            try
            {
                using (SqlConnection connection = new SqlConnection(ProgramConfig.DATABASE_CONNECTION_STRING))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();

                    command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@id", entity.GetId());

                    result = command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new GraphAnalyserLogRepositoryException(
                    "Something went wrong when trying to delete a graph " +
                    "analyser log from the database.\n" + ex.Message);
            }

            return result >= 1;
        }

        public bool Insert(GraphAnalyserLog entity)
        {
            int result = 0;
            string queryString = "INSERT INTO " +
                "GraphAnalyzerLogs(LogTime, SourceUserID, TargetUserID, Score, GeneratedMessage) " +
                "VALUES(@LogTime, @SourceUserID, @TargetUserID, @Score, @GeneratedMessage)";

            try
            {
                using (SqlConnection connection = new SqlConnection(ProgramConfig.DATABASE_CONNECTION_STRING))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();

                    command.Parameters.AddWithValue("@LogTime", entity.LogTime);
                    command.Parameters.AddWithValue("@SourceUserID", entity.SourceUserId);
                    command.Parameters.AddWithValue("@TargetUserID", entity.TargetUserId);
                    command.Parameters.AddWithValue("@Score", entity.Score);
                    command.Parameters.AddWithValue("@GeneratedMessage", entity.GeneratedMessage);

                    result = command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new GraphAnalyserLogRepositoryException(
                    "An error occured while trying to insert a new graph analyser " +
                    "log into the database.\n" + ex.Message);
            }

            return result >= 1;
        }

        public bool Update(GraphAnalyserLog entity)
        {
            int result = 0;
            string queryString = "UPDATE ScamMessageTemplates " +
                "SET LogTime=@LogTime, SourceUserID=@SourceUserId, " +
                    "TargetUserID=@TargetUserID, Score=@Score, " +
                    "GeneratedMessage=@GeneratedMessage" +
                "WHERE LogID=@id";

            try
            {
                using (SqlConnection connection = new SqlConnection(ProgramConfig.DATABASE_CONNECTION_STRING))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();

                    command.Parameters.AddWithValue("@LogID", entity.GetId());
                    command.Parameters.AddWithValue("@LogTime", entity.LogTime);
                    command.Parameters.AddWithValue("@SourceUserID", entity.SourceUserId);
                    command.Parameters.AddWithValue("@TargetUserID", entity.TargetUserId);
                    command.Parameters.AddWithValue("@Score", entity.Score);
                    command.Parameters.AddWithValue("@GeneratedMessage", entity.GeneratedMessage);

                    result = command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new GraphAnalyserLogRepositoryException(
                    "An error occured while trying to update a graph analyzer " +
                    "log in the database.\n" + ex.Message);
            }

            return result >= 1;
        }
    }
}
