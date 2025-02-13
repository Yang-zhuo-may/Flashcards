using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Flashcards.Data
{
    internal class DatabaseManager
    {
        internal void CreateDatabase(string? connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var tableCmd0 = connection.CreateCommand();
                tableCmd0.CommandText = @"IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'Flashcards')
                    BEGIN
                        CREATE DATABASE Flashcards;
                    END;";
                tableCmd0.ExecuteNonQuery();

                var tableCmd1 = connection.CreateCommand();
                tableCmd1.CommandText = @"USE Flashcards";
                tableCmd1.ExecuteNonQuery();

                var tableCmd2 = connection.CreateCommand();
                tableCmd2.CommandText = @"IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.stacks') AND type = N'U')
                BEGIN
                    CREATE TABLE dbo.stacks (
                        StackId INTEGER PRIMARY KEY IDENTITY(1,1),
                        StackName NVARCHAR(100) NOT NULL
                    );
                END;";
                tableCmd2.ExecuteNonQuery();

                var tableCmd3 = connection.CreateCommand();
                tableCmd3.CommandText = @"IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.cards') AND type = N'U')
                BEGIN
                    CREATE TABLE dbo.cards (
                        CardId INTEGER PRIMARY KEY IDENTITY(1,1),
                        Front NVARCHAR(255) NOT NULL,
                        Back NVARCHAR(255) NOT NULL,
                        Stack_id INTEGER NOT NULL,
                        CONSTRAINT fk_stack_card
                            FOREIGN KEY (Stack_id) 
                            REFERENCES stacks(StackId)
                            ON DELETE CASCADE
                            ON UPDATE CASCADE
                    )
                END;";
                tableCmd3.ExecuteNonQuery();

                var tableCmd4 = connection.CreateCommand();
                tableCmd4.CommandText = @"IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.stack_session') AND type = N'U')
                BEGIN
                    CREATE TABLE dbo.stack_session  (
                        SessionId INTEGER PRIMARY KEY IDENTITY(1,1),
                        Date DATETIME NOT NULL,
                        Score INT,
                        StackName NVARCHAR(100) NOT NULL, 
                        Stack_id INTEGER NOT NULL,
                        CONSTRAINT fk_stack_session
                            FOREIGN KEY (Stack_id) 
                            REFERENCES stacks(StackId)
                            ON DELETE CASCADE
                            ON UPDATE CASCADE
                    )
                END;";
                tableCmd4.ExecuteNonQuery();

                connection.Close();
            }
        }
    }
}
