using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flashcards.Data;
using Flashcards.Modes;
using Flashcards.View;
using Microsoft.Data.SqlClient;
using Spectre.Console;
using static System.Net.Mime.MediaTypeNames;

namespace Flashcards.Controller
{
    internal static class StackController
    {
        static string? connectionString = ConfigHelper.GetConnectionString();
        public static void View()
        {
            List<TaskSession> stackTable = new List<TaskSession>();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var tableComd = connection.CreateCommand();
                tableComd.CommandText = "SELECT StackId, StackName FROM dbo.stacks;";

                SqlDataReader reader = tableComd.ExecuteReader();

                while(reader.Read())
                {
                    stackTable.Add(new TaskSession
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                    });
                }

                reader.Close();
                TableVisualisationEngine.ShowStackTable(stackTable);
            }
        }

        public static int GetStackId(string stackName)
        {
            Dictionary<string, int> stackTable = new Dictionary<string, int>();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var tableComd = connection.CreateCommand();
                tableComd.CommandText = "SELECT StackId, StackName FROM dbo.stacks;";

                SqlDataReader reader = tableComd.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    stackTable.Add(name, id);
                }

                reader.Close();
            }
            return stackTable[stackName];
        }

        public static void Insert(string stackName)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var tableComd = connection.CreateCommand();
                tableComd.CommandText = $"INSERT INTO dbo.stacks (StackName) VALUES ('{stackName}');";

                tableComd.ExecuteNonQuery();

                connection.Close();
            }
        }

        public static void Update(int stackId)
        {
            string stackName = null;
            int exists = 1;
            bool validInput = true;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                do
                {
                    if (validInput == false) Console.WriteLine("INVALID INPUT, please enter a integer.");
                    else if (exists == 0) Console.WriteLine($"Stack-{stackId} doesn't exists. Please enter another stack.");

                    var sqlQuery = connection.CreateCommand();
                    sqlQuery.CommandText = $"IF EXSIST (SELECT 1 FROM dbo.stacks WHERE StackId = '{stackId}') SELECT 1 ELSE SELECT 0";

                    exists = (int)sqlQuery.ExecuteScalar();

                    Console.WriteLine("Please enter the new name");
                    validInput = int.TryParse(Console.ReadLine(), out int Id); 

                } while (exists == 0 || validInput == false);

                    var tableComd = connection.CreateCommand();
                    tableComd.CommandText = $"UPDATE dbo.stacks SET StackName = '{stackName} WHERE StackId = {stackId}';";

                tableComd.ExecuteNonQuery();

                connection.Close();
            }
        }

        public static void Detele(int stackId)
        {
            int exists = 1;
            bool validInput = true;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                do
                {
                    if (validInput == false)  Console.WriteLine("INVALID INPUT, please enter a integer.");
                    else if (exists == 0) Console.WriteLine($"Stack-{stackId} doesn't exists. Please enter another stack.");

                    var sqlQuery = connection.CreateCommand();
                    sqlQuery.CommandText = $"IF EXSIST (SELECT 1 FROM dbo.stacks WHERE StackId = '{stackId}') SELECT 1 ELSE SELECT 0";

                    exists = (int)sqlQuery.ExecuteScalar();
                    validInput = int.TryParse(Console.ReadLine(), out int Id);

                } while (exists == 0 || validInput == false);

                var tableComd = connection.CreateCommand();
                tableComd.CommandText = $"DELETE FROM dbo.stacks WHERE StackId='{stackId}';";


                tableComd.ExecuteNonQuery();

                connection.Close();
            }
        }
    }
}
