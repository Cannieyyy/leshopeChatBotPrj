using System.Collections;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace chatBotPrj
{
    internal class ResponseCheck
    {
        public ResponseCheck()
        {
            //declaring variables to store userName and botName
            string botName = "ChatBot";
            string userName = "You";

            // Welcome message
            Console.ForegroundColor = ConsoleColor.Green;//changing the colour of the bots text to green
            Console.WriteLine(botName + ": Hello, welcome to cyber security.");
            

            // Ask for user's name
            Console.WriteLine(botName + ": What is your name? ");
            Console.ResetColor();

            Console.ForegroundColor= ConsoleColor.Blue;
            Console.Write(userName + ": ");
            userName = Console.ReadLine();
            Console.ResetColor();

            // Asking the user if they want to change the bot's name
            Console.ForegroundColor = ConsoleColor.Green;//changing the colour of the bots text to green
            Console.WriteLine(botName + $": Would you like to give me a name {userName}? (yes/no) ");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(userName + ": ");
            string changeName = Console.ReadLine().ToLower();
            Console.ResetColor();

            //if statement for if the user types in yes
            if (changeName == "yes")
            {
                Console.ForegroundColor = ConsoleColor.Green;//changing the colour of the bots text to green
                Console.WriteLine(botName + $": What would you like to call me {userName}? ");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(userName + ": ");
                botName = Console.ReadLine();
                Console.ResetColor();
            }

            // Load responses from file
            ArrayList responseList = LoadResponsesFromFile("responses.txt");

            Console.ForegroundColor = ConsoleColor.Green;//changing the colour of the bots text to green
            Console.WriteLine(botName + $": What would you like to ask me about {userName}? ");
            Console.ResetColor();

            // Chat loop (this will keep the program running until the user types in the keyword exit or bye)
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(userName + ": ");
                string userInput = Console.ReadLine().ToLower();
                Console.ResetColor();

                if (userInput == "exit" || userInput == "bye") //if the user types exit or bye, the program stops
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(botName + ": Goodbye! Have a great day.");
                    Console.ResetColor();
                    break; //sentinel value that stops the loop if the user typed exit
                }

                //Declaring a variable and assigning it to a method FindBestResponse
                string response = FindBestResponse(responseList, userInput);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(botName + ": " + response);
                Console.ResetColor();
            }

        }//End of constructor

        //Creating a method LoadResponsesfromFile to load the responses from a file.
        private ArrayList LoadResponsesFromFile(string filePath)
        {
            //Declaring an arraylist object called responseList
            ArrayList responseList = new ArrayList();

                // declaring a string array called lines and assigning it to a built-in method ReadAllLines
                string [] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)//loops through each line of the array
                {
                    string [] parts = line.Split('=');//each line is going to be separated in to two parts, the "=" is where the line is going to split

                    if (parts.Length == 2)//error handling for if the line does not have an equal sign
                    {
                        responseList.Add(new string[] { parts[0].ToLower(), parts[1] });//a new string array is created and stored into the array list
                    }
                }
            
            

            return responseList; //returns and arraylist responseList
        }//end of LoadResponsesFromFile


        //creating a method FindBestResponse to look for a suitable response  
        private string FindBestResponse(ArrayList responseList, string userInput)//this method passes an arraylist and a string as parameters
        {
            //foreach loop that loops through the arraylist in pairs
            foreach (string[] pair in responseList)
            {
                //declaring a variable keyword and assigning it to index 0
                string keyword = pair[0];

                //declaring a variable response and assigning it to index 1
                string response = pair[1];

                if (userInput.Contains(keyword))
                {
                    return response;
                }
            }

            

            Console.ForegroundColor = ConsoleColor.Red;
            return "Sorry, I cannot help you. My developers created me to " +
                "provide information about cyber security only. TIP: you can ask me about " +
                "cyber security, passwords, phishing or safe browsing";
            Console.ResetColor();

        }//end of FindBestResponse


    }
}