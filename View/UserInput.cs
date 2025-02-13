using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Flashcards.Controller;
using Flashcards.DTOs;
using Spectre.Console;
using static System.Formats.Asn1.AsnWriter;

namespace Flashcards.View
{
    public static class UserInput
    {
        public static void GetUserInput()
        {
            Console.Clear();
            bool closeApp = false;

            string name, Id;

            while (closeApp == false)
            {
                Console.WriteLine("FlashCards");
                Console.WriteLine("------------------");
                Console.WriteLine("0 Exit the app");
                Console.WriteLine("1 Manage Stacks");
                Console.WriteLine("2 Manage FlashCards");
                Console.WriteLine("3 Study");
                Console.WriteLine("4 View study session data");
                Console.WriteLine("------------------");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "0":
                        closeApp = true;
                        break;

                    case "1":
                        StackMenu();
                        Console.Clear();
                        break;

                    case "2":
                       
                        break;

                    case "3":
                        StudyMenu();
                        Console.Clear();
                        break;

                    case "4":
                    StudySessionController.View();
                    break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Invalidate input.");
                        break;
                }
            }
        }

        public static void StackMenu()
        {
            Console.Clear();
            bool mainMenu = false;

            while (mainMenu == false)
            {
                StackController.View();
                Console.WriteLine("FlashCards");
                Console.WriteLine("------------------");
                Console.WriteLine("0 Return the main manu");
                Console.WriteLine("1 Create a new stack");
                Console.WriteLine("2 Delete a stack");
                Console.WriteLine("3 Change the name of a stack");
                Console.WriteLine("4 Choses one to interacte with");
                Console.WriteLine("------------------");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "0":
                        mainMenu = true;
                        Console.Clear();
                        break;

                    case "1":
                        Console.Clear();
                        StackController.View();
                        Console.WriteLine("Please enter the name of new stacks");
                        userInput = Console.ReadLine();
                        StackController.Insert(userInput);
                        Console.Clear();
                        break;

                    case "2":
                        Console.Clear();
                        StackController.View();
                        Console.WriteLine("Please enter the id of the stack you want to delete");
                        StackController.Detele();
                        StackController.View();
                        Console.Clear();
                        break;

                    case "3":
                        Console.Clear();
                        StackController.View();
                        Console.WriteLine("Please enter the id of the stack you want to change the name");
                        StackController.Update();
                        StackController.View();
                        Console.Clear();
                        break;

                    case "4":
                        Console.Clear();
                        StackController.View();
                        Console.WriteLine("Enter a name of stack.");
                        string userChoice = Console.ReadLine();
                        if (userChoice == "0") return;
                        
                        if (StackController.GetStackId(userChoice) == "") Console.ReadKey();
                        else
                        ChoosenStackMenu(userChoice);

                        Console.Clear();
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid command.");
                        break;
                }
            }
        }
        public static void ChoosenStackMenu(string stackName)
        {
            Console.Clear();
            bool UpperMenu = false;
            string id = StackController.GetStackId(stackName);

            while (UpperMenu == false)
            {
                Console.Clear();
                Console.WriteLine($"FlashCards - {stackName}");
                Console.WriteLine("---------------");
                Console.WriteLine("0 Return to upper menu");
                Console.WriteLine("1 Change current stack");
                Console.WriteLine("2 View all Flashcards in stack");
                Console.WriteLine("3 View X amount of cards in stack");
                Console.WriteLine("4 Creat a Flashcard in current stack");
                Console.WriteLine("5 Edit a Flashcard");
                Console.WriteLine("6 Delete a Flashcard");
                Console.WriteLine("------------------");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "0":
                        Console.Clear();
                        UpperMenu = true;
                        Console.Clear();
                        break;

                    case "1":
                        Console.Clear();
                        StackController.View();
                        Console.WriteLine("Enter a name of stack.");
                        string userChoice = Console.ReadLine();
                        if (userChoice == "0") return;
                        ChoosenStackMenu(userChoice);
                        Console.Clear();
                        break;

                    case "2":
                        Console.Clear();
                        CardController.View(stackName);
                        Console.WriteLine("Press 0 to return");
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    case "4":
                        Console.Clear();
                        Console.WriteLine("Please enter the front of the card");
                        string front = Console.ReadLine();
                        if (front == "0") return;
                        Console.WriteLine("Please enter the back of the card");
                        string back = Console.ReadLine();
                        if (back == "0") return;
                        CardController.Insert(front, back, id);
                        Console.Clear();
                        break;

                    case "5":
                        Console.Clear();
                        CardController.View(stackName);
                        Console.WriteLine("Please enter the id of the card");
                        string updateID = Console.ReadLine();
                        if (updateID == "0") return;
                        CardController.Update(updateID);
                        Console.Clear();
                        break;

                    case "6":
                        Console.Clear();
                        CardController.View(stackName);
                        Console.WriteLine("Please enter the id of the card");
                        string deteleId = Console.ReadLine();
                        if (deteleId == "0") return;
                        CardController.Detele(deteleId);
                        CardController.View(stackName);
                        Console.ReadKey();
                        Console.Clear();
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Invalidate input.");
                        break;
                }
            }
        }

        public static void StudyMenu()
        {
            int score = 0;
            List<CardDTO> cardDTOs = new List<CardDTO>();

            Console.Clear();
            StackController.View();
            Console.WriteLine("Enter a name of stack.");
            string userChoice = Console.ReadLine();

            cardDTOs = CardController.Study(userChoice);

            foreach (var card in cardDTOs)
            {
                Console.Clear();
                TableVisualisationEngine.ShowSingleCard("Front", card.Front);
                Console.WriteLine("Please enter your answer to this card \n Or 0 to exit");
                string userInput = Console.ReadLine();

                if (userInput != "0")
                {
                    Console.Clear();
                    string answer = card.Back;
                    TableVisualisationEngine.ShowSingleCard("Back", answer);
                    string checking = (answer != userInput)? "wrong" : "right";

                    if (answer == userInput) 
                    {
                        score++;
                        AnsiConsole.Markup($"[green]{userInput}[/]\n");
                    } else
                    {
                        AnsiConsole.Markup($"[red]{userInput}[/]\n");
                    }

                    Console.WriteLine($"Your answer was {checking}. Press any key to continue.");
                    Console.ReadKey();
                } else
                {
                    Console.WriteLine($"-----------------------");
                    Console.WriteLine($"Exiting Study Session.");
                    Console.WriteLine($"You got {score} right out of {cardDTOs.Count}.");
                    Console.WriteLine($"Press any key to return.");
                    Console.ReadKey();
                    return;
                }
            }

            StudySessionController.Insert(userChoice, score, DateTime.Now);

            Console.WriteLine($"Finished Study Session.\n You got {score} right out of {cardDTOs.Count}.\n Press any key to return.");
            Console.ReadKey();
        }
    }
}
