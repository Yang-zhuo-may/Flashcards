using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System;
using Microsoft.Extensions.Configuration;
using System.Runtime.CompilerServices;
using Flashcards.Data;
using Flashcards.Controller;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;

namespace Flashcards.View
{
    class Program
    {
        static string? connectionString = ConfigHelper.GetConnectionString();
        static void Main(string[] args)
        {
            DatabaseManager databaseManager = new DatabaseManager();
            databaseManager.CreateDatabase(connectionString);

            // Default Stack
            StackController.Insert("English");
            string stackId = StackController.GetStackId("English");
            CardController.Insert("Hello", "Bonjour", stackId);
            CardController.Insert("World", "le monde", stackId);
            CardController.Insert("Love", "amour", stackId);
            CardController.Insert("Peace", "la paix", stackId);

            UserInput.GetUserInput();
        }
    }
}