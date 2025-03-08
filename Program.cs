using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Media;
using System.ComponentModel;

namespace chatBotPrj
{
    internal class Program
    {
        static void Main(string[] args)
        {





            string logo = File.ReadAllText("logo.txt");
            Console.WriteLine(logo);


            SoundPlayer player = new SoundPlayer("greet.wav");
            player.Play();



            string botName = "ChatBot";
            string userName = "You";

            // Welcome message
            Console.WriteLine(botName + ": Hello, welcome to cyber security.");

            // Ask for user's name
            Console.WriteLine(botName + ": What is your name? ");
            Console.Write(userName + ": ");
            userName = Console.ReadLine();

            // Asking the user if they want to change the bot's name
            Console.WriteLine(botName + $": Would you like to give me a name {userName}? (yes/no) ");
            Console.Write(userName + ": ");
            string changeName = Console.ReadLine().ToLower();

            if (changeName == "yes")
            {
                Console.WriteLine(botName + $": What would you like to call me {userName}? ");
                Console.Write(userName + ": ");
                botName = Console.ReadLine();
            }

            // Load responses from file
            ArrayList responseList = LoadResponsesFromFile("responses.txt");

            // Chat loop
            while (true)
            {
                Console.Write(userName + ": ");
                string userInput = Console.ReadLine().ToLower();

                if (userInput == "exit" || userInput == "bye")
                {
                    Console.WriteLine(botName + ": Goodbye! Have a great day.");
                    break;
                }

                // Find best response using keyword matching
                string response = FindBestResponse(responseList, userInput);
                Console.WriteLine(botName + ": " + response);
            }
        }

        static ArrayList LoadResponsesFromFile(string filePath)
        {
            ArrayList responseList = new ArrayList();

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    string[] parts = line.Split('=');

                    if (parts.Length == 2)
                    {
                        responseList.Add(new string[] { parts[0].ToLower(), parts[1] });
                    }
                }
            }
            else
            {
                Console.WriteLine("Error: Response file not found.");
            }

            return responseList;
        }

        static string FindBestResponse(ArrayList responseList, string userInput)
        {
            foreach (string[] pair in responseList)
            {
                string keyword = pair[0];
                string response = pair[1];

                if (userInput.Contains(keyword))
                {
                    return response;
                }
            }

            return "Sorry, I cannot help you. My developers created me to provide only cyber security information.";
        }
    }
}
