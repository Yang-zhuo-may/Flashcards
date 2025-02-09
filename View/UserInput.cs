using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Flashcards.Controller;

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
                Console.WriteLine("2 Manage FlashCars");
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
                        break;

                    case "2":
                        StackController.View();
                        Console.WriteLine("For test, enter a name of stakc.");
                        string studyInput = Console.ReadLine();
                        ChoosenStackMenu(studyInput);
                        break;

                    case "3":
                        StudyMenu();
                        break;

                    default:
                        Console.WriteLine("Invalidate input.");
                        break;
                }
            }
        }

        public static void StudyMenu()
        {
            Console.Clear();

            StackController.View();
            Console.WriteLine("Choose a stack to interact with or press 0 to return main menu");
                
            string studyInput = Console.ReadLine();

            CardController.View(studyInput);


        }

        public static void StackMenu()
        {
            Console.Clear();
            bool mainMenu = false;
            // print all the stacks

            while (mainMenu == false)
            {
                StackController.View();
                Console.WriteLine("FlashCards");
                Console.WriteLine("------------------");
                Console.WriteLine("0 Return the main manu");
                Console.WriteLine("1 Create a new stack");
                Console.WriteLine("2 Deleted a stack");
                Console.WriteLine("3 Change the name of a stack");
                Console.WriteLine("------------------");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "0":
                        mainMenu = true;
                        break;

                    case "1":
                        StackController.View();
                        Console.WriteLine("Please enter the name of new stacks");
                        userInput = Console.ReadLine();
                        StackController.Insert(userInput);
                        break;

                    case "2":
                        // Stack deleted function
                        break;

                    case "3":
                        // Stack update function

                    default:
                        Console.WriteLine("Invalidate input.");
                        break;
                }
            }
        }
        public static void ChoosenStackMenu(string stackName)
        {
            Console.Clear();
            bool mainMenu = false;
            // print the stack table user choiced
            string id = StackController.GetStackId(stackName).ToString();

            while (mainMenu == false)
            {
                Console.WriteLine("FlashCards");
                Console.WriteLine("---------------");
                Console.WriteLine("0 Return to main menu");
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
                        mainMenu = true;
                        break;

                    case "2":
                        CardController.View(stackName);
                        break;

                    case "4":
                        Console.WriteLine("Please enter the front of the card");
                        string front = Console.ReadLine();
                        Console.WriteLine("Please enter the back of the card");
                        string back = Console.ReadLine();
                        CardController.Insert(front, back, id);
                        break;

                    default:
                        Console.WriteLine("Invalidate input.");
                        break;
                }
            }
        }

        public static void ChooseStack()
        {
            Console.Clear();
            // print all the stack

            Console.WriteLine("Please entre the name of the stack.");

            string userChoice = Console.ReadLine();

            // Check if this stack exsist
            // return 
        }
    }
}
