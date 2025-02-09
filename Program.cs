using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System;
using Microsoft.Extensions.Configuration;
using System.Runtime.CompilerServices;
using Flashcards.Data;

namespace Flashcards.View
{
    class Program
    {
        static string? connectionString = ConfigHelper.GetConnectionString();
        static void Main(string[] args)
        {
            DatabaseManager databaseManager = new DatabaseManager();
            databaseManager.CreateDatabase(connectionString);
            UserInput.GetUserInput();
        }
    }
}