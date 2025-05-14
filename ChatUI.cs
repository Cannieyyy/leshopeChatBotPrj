using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace chatBotPrj
{
    public class ChatUI
    {
        //creating an instance of a class memory manager to access class methods
        private readonly MemoryManager memory = new MemoryManager();

        //a generic collections list to store the favourite topics
        private List<string> favoriteTopics = new List<string>();

        //declaring an instance to access the chatbot class methods and attributes
        private CyberBot bot;

        //decalre a list to store the random questions
        private readonly List<string> questions = new List<string>
           {
                //stroring the random responses
                "What would you like to know about cyber security today?",
                "How can I assist you with your security concerns?",
                "What topic about online safety interests you?",
                "Is there something you'd like to learn about passwords, phishing, or safe browsing?",
                "Do you have any questions about protecting yourself online?"
           };


        //constructor
        public ChatUI(CyberBot botInstance)
        {
            bot = botInstance; //assign the instance for cyber bot to bot instance 
        }//end of chatUI constructor


        //a method that executes the functions of ui class
        public void StartChat()
        {
            DisplayWelcome();
            GetUserName();
            ChatLoop();
        }

        //a method dedicated to only displaying the welocome message
        private void DisplayWelcome()
        {
            //welcome display when the class starts to execute
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("========================================");
            Console.WriteLine("Welcome to Cyber Security Awareness Chat");
            Console.WriteLine("========================================");
            Console.ResetColor();
        }

        //a method to get the username from the user
        private void GetUserName()
        {
            // Greeting message
            bot.TypingEffect(bot.botName + ": Hello! My name is CyberBot.", ConsoleColor.Green);

            //prompting user for their name
            bot.TypingEffect(bot.botName + ": What is your name? ", ConsoleColor.Green);
            bot.userName = Prompt($"{bot.userName}");

            // Process user input and remove unnecessary words
            bot.userName = bot.FilterUnwantedWords(bot.userName);

            // Ask for user's name
            while (!bot.isValidName(bot.userName))// the while loop makes sure the user enters their name and that their name does not contain any characters or numbers
            {
                bot.TypingEffect(bot.botName + $": This field cannot be empty or contain numbers/special characters! Please enter your name!", ConsoleColor.Red);
                bot.userName = Prompt("You");
                bot.userName = bot.FilterUnwantedWords(bot.userName);
            }

            LoadUser();

        }//end of get username method

        //a method that runs only if the user is remembered
        private void LoadUser()
        {

            // Load the saved names from memory
            string savedName = memory.LoadData("userName");
            string savedBotName = memory.LoadData("botName");

            //if statement that checks if the username entered by the user is found on the memory file
            if (!string.IsNullOrEmpty(savedName) && savedName.Equals(bot.userName, StringComparison.OrdinalIgnoreCase))
            {
                bot.TypingEffect($"{bot.botName}: Welcome back, {bot.userName}!", ConsoleColor.Green);

                //if the bot name is found on the file, ask user if they want to change that name or stick with the one stored
                if (!string.IsNullOrEmpty(savedBotName))
                {
                    bot.TypingEffect($"{bot.botName}: Would you still like my name to be {savedBotName}, {bot.userName}? Choose yes or no.", ConsoleColor.Green);
                    string reuseBotName = Prompt(bot.userName).ToLower();//user inputs yes or no

                    //if user input has yes, let the user change name, if not name remains
                    if (reuseBotName.Contains("yes"))
                    {
                        bot.botName = savedBotName;
                        bot.TypingEffect($"{bot.botName}: Fantastic! My name remains {savedBotName}", ConsoleColor.Green);
                    }
                    else
                    {
                        RenameBot();//calling method to rename bot if user didnt type "yes"
                        memory.SaveData("botName", bot.botName);
                    }
                }

                PreviousSentiment();//method call to check sentiment stored on memory
            }
            else
            {
                WelcomeNewUser();//welcoming new user
            }
        }//end of load user method

        //a method to check sentiment
        private void PreviousSentiment()
        {
            //Store the last sentiment stored to the last sentiment variable from the memory
            string lastSentiment = memory.LoadData("lastSentiment");

            //If the last sentiment contains one of the keywords
            if (!string.IsNullOrEmpty(lastSentiment) && (lastSentiment == "worried" || lastSentiment == "frustrated" || lastSentiment == "confusted"))
            {

                //assigning the last topic stored on memory to the last topic variable 
                string lastTopic = memory.LoadData("lastTopic");
                bot.TypingEffect($"{bot.botName}: How are you feeling today {bot.userName}? Last time you were {lastSentiment} Are you still feeling {lastSentiment}?", ConsoleColor.Yellow);
            }
        }

        //a method to welcome new user
        private void WelcomeNewUser()
        {

            bot.TypingEffect($"{bot.botName}: Nice to meet you {bot.userName}!", ConsoleColor.Green);

            memory.ClearFile();//clear the file for new user
            memory.SaveData("userName", bot.userName);
           

            // Asking the user if they want to change the bot's name
            bot.TypingEffect($"{bot.botName}: Would you like to give me a name {bot.userName}?", ConsoleColor.Green);

            string changeName = Prompt(bot.botName).ToLower();//user input yes or no

            //if else statement for if the user types in yes or something else
            if (changeName.Contains("yes"))
            {
                RenameBot();//method call to rename bot
            }
            else
            {
                bot.TypingEffect($"{bot.botName}: Awesome! My name will remain{bot.botName}", ConsoleColor.Green);
            }
        }//end of welcome new user method

        //method to rename bot
        private void RenameBot()
        {
            bot.TypingEffect(bot.botName + $": That's awesome! What would you like to call me {bot.userName}? ", ConsoleColor.Green);
            bot.botName = Prompt(bot.userName);

            // Process user input and remove unnecessary words
            bot.botName = bot.FilterUnwantedWords(bot.botName);

            // the while loop makes sure the user enters an input with no space, characters or numbers
            while (!bot.isValidName(bot.botName))
            {
                bot.TypingEffect(bot.botName + $"You: This field cannot be empty, contain a number or characters! Please give me a name {bot.userName}!", ConsoleColor.Red);
                bot.botName = Prompt($"{bot.userName}");

                // Process user input and remove unnecessary words
                bot.botName = bot.FilterUnwantedWords(bot.botName);
            }

            //save the bot name to memory
            memory.SaveData("botName", bot.botName);
            bot.TypingEffect(bot.botName + $": {bot.botName} is a wonderful name {bot.userName}! I will definetly remember this name! ", ConsoleColor.Green);
        }//end of rename bot method

        private void ChatLoop()
        {
            //declaring a random object
            Random random = new Random();

            //load and store to responses form a file
            var responses = bot.LoadResponsesFromFile("responses.txt");

            // Chat loop (this will keep the program running until the user types in the keyword exit or bye)
            while (true)
            {
                //declared a variable to generate questions based on the stored list 
                string randomQuestions = questions[random.Next(questions.Count)];
                bot.TypingEffect(bot.botName + $": {randomQuestions} {bot.userName}? Type 'exit' or 'bye' to end the chat.", ConsoleColor.Green);

                string userInput = Prompt(bot.userName).ToLower();
                if (userInput == "exit" || userInput == "bye") //if the user types exit or bye, the program stops
                {
                    bot.TypingEffect(bot.botName + $": Goodbye! Have a great day {bot.userName}!", ConsoleColor.Green);
                    break; //sentinel value that stops the loop if the user typed exit
                }
                ProcessUserInput(responses, userInput);
            }
        }

        //method to process the user input
        private void ProcessUserInput(List<(string Keyword, List<string> Responses)> responses, string userInput)
        {

            while (!bot.isValidName(userInput))
            {
                bot.TypingEffect($"{bot.botName}: This field cannot be empty or contain numbers or characters! Please provide a valid input.", ConsoleColor.Red);
                userInput = Prompt(bot.userName).ToLower();
            }//end of while loop

            //if keyword is not found
            if (!bot.FindBestResponse(responses, userInput))
                {
                    bot.TypingEffect($"{bot.botName}: Sorry, I cannot answer that. Try asking about passwords, phishing, or safe browsing.", ConsoleColor.Red);
                }
                else // if found
                {
                    // Try to identify the topic (using keywords matched in file)
                    foreach (var (keyword, response) in responses)
                    {
                        if (userInput.Contains(keyword))
                        {
                            //add to list of favourite topics
                            memory.AppendToList($"favouriteTopics_{bot.userName}", keyword);
                            break;
                        }
                    }//end of foreach loop
                }
            
        }//end of process user input

        //method that prompts the user
        private string Prompt(string speaker)
        {
            string input;
            
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write($"{speaker}: ");
                input = Console.ReadLine();
                Console.ResetColor();
            

            return input;
        }
    }
}