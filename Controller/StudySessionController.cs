using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flashcards.Data;
using Flashcards.Modes;
using Flashcards.View;
using Microsoft.Data.SqlClient;

namespace Flashcards.Controller
{
    internal class StudySessionController
    {
        static string? connectionString = ConfigHelper.GetConnectionString();
        
        public static void Insert(string stackName, int Score, DateTime dateTime)
        {
            int stackId = int.Parse(StackController.GetStackId(stackName));
            string sqlQuery = "INSERT INTO dbo.stack_session (Date, Score, Stack_id, StackName) VALUES (@dateTime, @Score, @stackId, @stackName);";

            using (var connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.AddWithValue("@dateTime", dateTime);
                command.Parameters.AddWithValue("@Score", Score);
                command.Parameters.AddWithValue("@stackId", stackId);
                command.Parameters.AddWithValue("@stackName", stackName);

                try
                {
                    connection.Open();

                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public static void View()
        {
            List<StudySessionSession> sessions = new List<StudySessionSession>();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var tableComd = connection.CreateCommand();
                tableComd.CommandText = $"SELECT SessionId, Date, Score, StackName FROM dbo.stack_session;";

                SqlDataReader reader = tableComd.ExecuteReader();

                while (reader.Read())
                {
                    sessions.Add(new StudySessionSession
                    {
                        SessionId = reader.GetInt32(0),
                        Date = reader.GetDateTime(1),
                        Score = reader.GetInt32(2),
                        StackName = reader.GetString(3)
                    });
                }

                reader.Close();

                TableVisualisationEngine.ShowRecord(sessions);
            }
        }
    }
}
