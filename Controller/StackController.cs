﻿using System;
using System.Collections;
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

        public static string GetStackId(string stackName)
        {
            int isExists = 1;
            Dictionary<string, int> stackTable = new Dictionary<string, int>();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                do
                {
                    if (isExists == 0)
                    {
                        Console.WriteLine($"{stackName} doesn't exists. Please enter another stack. Or 0 to return");
                        stackName = Console.ReadLine();
                        if (stackName == "0") return "";
                    }

                    var sqlQuery = connection.CreateCommand();
                    sqlQuery.CommandText = $"IF EXISTS (SELECT 1 FROM dbo.stacks WHERE StackName = @stackName) SELECT 1 ELSE SELECT 0;";
                    sqlQuery.Parameters.AddWithValue("@stackName", stackName);
                    isExists = (int)sqlQuery.ExecuteScalar();

                } while (isExists == 0);

                var tableComd = connection.CreateCommand();
                tableComd.CommandText = "SELECT StackId, StackName FROM dbo.stacks;";
                SqlDataReader reader = tableComd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        stackTable.Add(name, id);
                    }
                } else
                {
                    Console.WriteLine("The id doesn't exists in the database.");
                }

                reader.Close();
            }
            return stackTable[stackName].ToString();
        }

        public static void Insert(string stackName)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var tableComd = connection.CreateCommand();
                tableComd.CommandText = $"INSERT INTO dbo.stacks (StackName) VALUES (@stackName);";
                tableComd.Parameters.AddWithValue("@stackName", stackName);
                tableComd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static void Update()
        {
            int exists = 1;
            int Id;
            bool validInput = true;
            string stackId = null;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                do
                {
                    if (validInput == false) Console.WriteLine("INVALID INPUT.");
                    else if (exists == 0) Console.WriteLine($"Stack - {stackId} doesn't exists. ");

                    Console.WriteLine("Please enter the id of the stack or 0 to return.");
                    validInput = int.TryParse(Console.ReadLine(), out Id);

                    var sqlQuery = connection.CreateCommand();
                    sqlQuery.CommandText = $"SELECT CASE WHEN  EXISTS (SELECT 1 FROM dbo.stacks WHERE StackId = @Id) THEN 1 ELSE 0 END;";
                    sqlQuery.Parameters.AddWithValue ("@Id", Id.ToString());
                    exists = (int)sqlQuery.ExecuteScalar();

                } while (exists == 0 || validInput == false);

                Console.WriteLine("Please enter the new name or 0 to return");
                string stackName = Console.ReadLine();
                if (stackName == "0") return;

                var tableComd = connection.CreateCommand();
                    tableComd.CommandText = $"UPDATE dbo.stacks SET StackName = @stackName WHERE StackId = @Id;";
                tableComd.Parameters.AddWithValue("@stackName", stackName);
                tableComd.Parameters.AddWithValue("@Id", Id);
                tableComd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static void Detele()
        {
            int exists = 1;
            int Id;
            bool validInput = true;
            string stackId = null;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                do
                {
                    Console.WriteLine("Please enter the id of the stack");
                    validInput = int.TryParse(Console.ReadLine(), out Id);

                    if (Id == 0) return;
                    if (validInput == false)  Console.WriteLine("INVALID INPUT, please enter a integer.");
                    else if (exists == 0) Console.WriteLine($"Stack-{stackId} doesn't exists. Please enter another stack.");

                    var sqlQuery = connection.CreateCommand();
                    sqlQuery.CommandText = $"SELECT CASE WHEN  EXISTS (SELECT 1 FROM dbo.stacks WHERE StackId = @Id) THEN 1 ELSE 0 END;";
                    sqlQuery.Parameters.AddWithValue("@Id", Id.ToString());
                    exists = (int)sqlQuery.ExecuteScalar();

                } while (exists == 0 || validInput == false);

                var tableComd = connection.CreateCommand();
                tableComd.CommandText = $"DELETE FROM dbo.stacks WHERE StackId=@Id;";
                tableComd.Parameters.AddWithValue("@Id", Id.ToString());
                tableComd.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
