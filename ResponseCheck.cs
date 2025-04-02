using System.Collections;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace chatBotPrj
{
    public class ResponseCheck
    {
        public ResponseCheck()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("========================================");
            Console.WriteLine("Welcome to Cyber Security Awareness Chat");
            Console.WriteLine("========================================");
            Console.ResetColor();


            //declaring and initialzing variables to store userName and botName
            string botName = "CyberBot";
            string userName = "You";
            string userInput; //this declaration is for the input of the user

            // Greeting message
            TypingEffect(botName + ": Hello! My name is CyberBot.",ConsoleColor.Green);
            TypingEffect( botName + ": What is your name? ",ConsoleColor.Green);
           

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(userName + ": ");
            userName = Console.ReadLine();
            Console.ResetColor();

            //  remove unnecessary words from the inuput
            userName = FilterUnwantedWords(userName, new string[] { "my", "name", "is", "i", "am", "call", "me", "the", ".", "," });
            string pattern = @"^[a-zA-Z., ]+$";
           
            // Ask for user's name
            while (string.IsNullOrEmpty(userName) || !Regex.IsMatch(userName,pattern))// the while loop makes sure the user enters their name and that their name does not contain any characters or numbers
            {
                
                TypingEffect(botName + $": This field cannot be empty or contain numbers/special characters! Please enter your name!", ConsoleColor.Red);
                

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("You" + ": ");
                userName = Console.ReadLine();
                Console.ResetColor();

                // Process user input and remove unnecessary words
                userName = FilterUnwantedWords(userName, new string[] { "my", "name", "is", "i", "am", "call", "me", "the" });

            }



            // Asking the user if they want to change the bot's name
            TypingEffect(botName + $": Would you like to give me a name {userName}? (yes/no) ", ConsoleColor.Green);
            

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(userName + ": ");
            string changeName = Console.ReadLine().ToLower();
            Console.ResetColor();

            while (string.IsNullOrEmpty(changeName))// the while loop makes sure the user enters an input to avoid errors
            {
                
                TypingEffect(botName + $": This field cannot be empty! please enter yes or no {userName}!", ConsoleColor.Red);
              

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("You" + ": ");
                changeName = Console.ReadLine().ToLower();
                Console.ResetColor();
            }

            //if statement for if the user types in yes
            if (changeName.Equals("yes"))
            {
                
                TypingEffect(botName + $": What would you like to call me {userName}? ", ConsoleColor.Green);
               

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(userName + ": ");
                botName = Console.ReadLine();
                Console.ResetColor();

                // Process user input and remove unnecessary words
                botName = FilterUnwantedWords(botName, new string[] { "would", "name", "like", "i", "to", "call", "you", "your", "is"});

                while (string.IsNullOrEmpty(botName))// the while loop makes sure the user enters an input to avoid errors
                {
                    
                    TypingEffect(botName + $": This field cannot be empty! please give me a name {userName}!", ConsoleColor.Red);

                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("You" + ": ");
                    botName = Console.ReadLine();
                    Console.ResetColor();

                    // Process user input and remove unnecessary words
                    botName = FilterUnwantedWords(botName, new string[] { "would", "name", "like", "i", "to", "call", "you", "your", "is" });
                }
            }

            // Load responses from file
            ArrayList responseList = LoadResponsesFromFile("responses.txt");

           

            // Chat loop (this will keep the program running until the user types in the keyword exit or bye)
            while (true)
            {
                
                    TypingEffect(botName + $": What would you like to ask me about {userName}? Type 'exit' or 'bye' to end the chat.", ConsoleColor.Green);

                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(userName + ": ");
                    userInput = Console.ReadLine().ToLower();
                    Console.ResetColor();


                while (string.IsNullOrEmpty(userInput))
                {
                    
                   TypingEffect(botName + $": This field cannot be empty! please ask a question {userName}!", ConsoleColor.Red);

                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(userName + ": ");
                    userInput = Console.ReadLine();
                    Console.ResetColor();
                }


                if (userInput == "exit" || userInput == "bye") //if the user types exit or bye, the program stops
                    {

                    TypingEffect(botName + $": Goodbye! Have a great day {userName}!", ConsoleColor.Green);
                        break; //sentinel value that stops the loop if the user typed exit
                    }



                    //Declaring a variable and assigning it to a method FindBestResponse
                    FindBestResponse(responseList, userInput, botName);
                    
                    
                }

            

        }//End of constructor

        
        // Method to remove unnecessary words from a string
        private string FilterUnwantedWords(string input, string[] wordsToRemove)
        {
            List<string> words = new List<string>(input.Split(' '));

            // Remove unwanted words
            words.RemoveAll(word => wordsToRemove.Contains(word.ToLower()));

            // Return the first remaining word or "Unknown" if empty
            return words.Count > 0 ? words[0].ToString() : "Unknown";
        }


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
        private void FindBestResponse(ArrayList responseList, string userInput, string botName)//this method passes an arraylist and a string as parameters
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
                    TypingEffect(botName + ": " + response, ConsoleColor.Green); 
                    return; 
                } 
            }//end findBestResponse method

           

            // Message when no response is found
            string responseNotFound = "Sorry, I cannot answer that question. TIP: you can ask me about cyber security, passwords, phishing or safe browsing.";

            // Print the message with red color
            TypingEffect(botName + ": " + responseNotFound, ConsoleColor.Red);




        }//end of FindBestResponse


        // a methods that adds a typing effect to the chatbot's response
        private void TypingEffect(string message, ConsoleColor color )// this method parses a string, a colour and int as a parameter
        {
            int speed = 20;

            Console.ForegroundColor = color;

            foreach (char c in message)
            {
                Console.Write(c);
                System.Threading.Thread.Sleep(speed); // Adjust speed for effect
            }
            Console.WriteLine();
            Console.ResetColor ();
        }//end of typing effect

    }//end of class

}//end of name space