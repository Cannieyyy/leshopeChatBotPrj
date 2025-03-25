using System.Collections;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

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
            string botName = "ChatBot";
            string userName = "You";
            string userInput; //this declaration is for the input of the user

            // Welcome message
            TypingEffect ( "\n" + botName + ": Hello!I'm here to help you stay safe online.",ConsoleColor.Green);
            TypingEffect( botName + ": What is your name? ",ConsoleColor.Green);
           

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(userName + ": ");
            userName = Console.ReadLine();
            Console.ResetColor();

            // Ask for user's name
            while (string.IsNullOrEmpty(userName))// the while loop makes sure the user enters their name
            {
                
                TypingEffect(botName + $": This field cannot be empty! please enter your name {userName}!", ConsoleColor.Red);
                

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("You" + ": ");
                userName = Console.ReadLine();
                Console.ResetColor();
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

                while (string.IsNullOrEmpty(botName))// the while loop makes sure the user enters an input to avoid errors
                {
                    
                    TypingEffect(botName + $": This field cannot be empty! please enter give me a name {userName}!", ConsoleColor.Red);

                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("You" + ": ");
                    botName = Console.ReadLine();
                    Console.ResetColor();
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
                        Console.ForegroundColor = ConsoleColor.Green;
                        TypingEffect(botName + $": Goodbye! Have a great day {userName}!", ConsoleColor.Green);
                        Console.ResetColor();
                        break; //sentinel value that stops the loop if the user typed exit
                    }



                    //Declaring a variable and assigning it to a method FindBestResponse
                    FindBestResponse(responseList, userInput, botName);
                    
                    
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
            }

           

            // Message when no response is found
            string responseNotFound = "Sorry, I cannot help you. My developers created me to provide information about cyber security only. TIP: you can ask me about cyber security, passwords, phishing or safe browsing.";

            // Print the message with red color
            TypingEffect(botName + ": " + responseNotFound, ConsoleColor.Red);

            


        }//end of FindBestResponse


        // a methods that adds a typing effect to the chatbot's response
        private void TypingEffect(string message, ConsoleColor color ,int speed = 20)// this method parses a string, a colour and int as a parameter
        {
            Console.ForegroundColor = color;
            foreach (char c in message)
            {
                Console.Write(c);
                System.Threading.Thread.Sleep(speed); // Adjust speed for effect
            }
            Console.WriteLine();
            Console.ResetColor ();
        }

    }
}