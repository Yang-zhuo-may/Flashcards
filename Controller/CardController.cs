using System;
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

namespace Flashcards.Controller
{
    internal class CardController
    {
        static string? connectionString = ConfigHelper.GetConnectionString();
        public static void View(string StackName)
        {
            int isExists = 1;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                List<CardSession> tableCard = new List<CardSession>();

                do
                {
                    if (isExists == 0) Console.WriteLine($"{StackName} doesn't exists. Please enter another stack.");

                    var sqlQuery = connection.CreateCommand();
                    sqlQuery.CommandText = $"IF EXISTS (SELECT 1 FROM dbo.stacks WHERE StackName = '{StackName}') SELECT 1 ELSE SELECT 0;";
                    isExists = (int)sqlQuery.ExecuteScalar();

                    // Console.WriteLine("Please enter the stack's name");
                    // string userInput = Console.ReadLine();
                } while (isExists == 0);

                var tableComd = connection.CreateCommand();

                string stackId = StackController.GetStackId(StackName).ToString();
                tableComd.CommandText = $"SELECT CardId, Front, Back FROM dbo.cards WHERE Stack_id = {stackId};";

                SqlDataReader reader = tableComd.ExecuteReader();

                while (reader.Read())
                {
                    foreach (var card in tableCard)
                    {
                        card.Id = reader.GetInt32(0);
                        card.Front = reader.GetString(1);
                        card.Back = reader.GetString(2);
                        card.StackName = reader.GetString(3);
                    }
                }

                reader.Close();
                TableVisualisationEngine.ShowCardTable(tableCard);
            }
        }

        public static void Insert(string front, string back, string stackId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var tabtableCmd = connection.CreateCommand();
                tabtableCmd.CommandText = $"INSERT INTO cards (Front, Back, Stack_id) VALUES (N'{front}', N'{back}', '{stackId}');";

                tabtableCmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static void Update(string cardId)
        {
            int isExists = 1;
            bool validInput = true;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                do
                {
                    if (isExists == 0) Console.WriteLine($"Card-{cardId} doesn't exists. Please enter another stack.");
                    else if (validInput == false) Console.WriteLine("INVALID INPUT, please enter a integer.");

                    var sqlQuery = connection.CreateCommand();
                    sqlQuery.CommandText = $"IF EXISTS (SELECT 1 FROM dob.cards WHERE CardId = {cardId}) SELECT 1 ELSE SELECT 0;";
                    isExists = (int)sqlQuery.ExecuteScalar();

                    Console.WriteLine("Please enter the new name");
                    validInput = int.TryParse(Console.ReadLine(), out int Id);
                } while (isExists == 0);

                Console.WriteLine("Please the front");
                string front = Console.ReadLine();
                Console.WriteLine("Please the back");
                string back = Console.ReadLine();

                var tableComd = connection.CreateCommand();
                tableComd.CommandText = $"UPDATE dbo.cards SET Front = '{front}', Back = '{back}'";

                tableComd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static void Detele(string cardId)
        {
            int isExists = 1;
            bool validInput = true;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                do
                {
                    if (isExists == 0) Console.WriteLine($"Card-{cardId} doesn't exists. Please enter another stack.");
                    else if (validInput == false) Console.WriteLine("INVALID INPUT, please enter a integer.");

                    var sqlQuery = connection.CreateCommand();
                    sqlQuery.CommandText = $"IF EXISTS (SELECT 1 FROM dob.cards WHERE CardId = {cardId}) SELECT 1 ELSE SELECT 0;";
                    isExists = (int)sqlQuery.ExecuteScalar();

                    Console.WriteLine("Please enter the new name");
                    validInput = int.TryParse(Console.ReadLine(), out int Id);
                } while (isExists == 0);

                var tabtableCmd = connection.CreateCommand();
                tabtableCmd.CommandText = $"DELETE FROM cards WHERE StackName='{cardId}';";

                tabtableCmd.ExecuteNonQuery();
                connection.Close();
            }
        }

    }
}
