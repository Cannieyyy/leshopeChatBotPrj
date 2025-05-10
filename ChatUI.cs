using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace chatBotPrj
{
    public class ChatUI
    {
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


        public ChatUI(CyberBot botInstance)
        {
            bot = botInstance;
        }//end of chatUI constructor

        public void StartChat()
        {
            DisplayWelcome();
            GetUserName();
            RenameBot();
            ChatLoop();
        }

        private void DisplayWelcome()
        {
            //welcome display when the class starts to execute
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("========================================");
            Console.WriteLine("Welcome to Cyber Security Awareness Chat");
            Console.WriteLine("========================================");
            Console.ResetColor();
        }

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
                bot.userName = Prompt($"{bot.botName}: ");
                bot.userName = bot.FilterUnwantedWords(bot.userName) ;               
            }
        }

        private void RenameBot()
        {
            // Asking the user if they want to change the bot's name
            bot.TypingEffect(bot.botName + $": Would you like to give me a name {bot.userName}? (yes/no) ", ConsoleColor.Green);

            string changeName = Prompt(bot.userName).ToLower();
            
            //if statement for if the user types in yes
            if (changeName.Contains("yes"))
            {

                bot.TypingEffect(bot.botName + $": What would you like to call me {bot.userName}? ", ConsoleColor.Green);

                bot.botName = Prompt(bot.userName);
                
                // Process user input and remove unnecessary words
                bot.botName = bot.FilterUnwantedWords(bot.botName);

                while (!bot.isValidName(bot.botName))// the while loop makes sure the user enters an input to avoid errors
                {

                    bot.TypingEffect(bot.botName + $": This field cannot be empty and cannot contain a number or characters! please give me a name {bot.userName}!", ConsoleColor.Red);

                    
                    
                    bot.botName = Prompt($"{bot.userName}");
                    

                    // Process user input and remove unnecessary words
                    bot.botName = bot.FilterUnwantedWords(bot.botName);
                }
            }
        }

        private void ChatLoop()
        {
            //declaring a random object
            Random random = new Random();

            var responses = bot.LoadResponsesFromFile("responses.txt");

            // Chat loop (this will keep the program running until the user types in the keyword exit or bye)
            while (true)
            {
                //declared a variable to generate questions based on the stored list 
                string randomQuestions = questions[random.Next(questions.Count)];

                bot.TypingEffect(bot.botName + $": {randomQuestions} {bot.userName}? Type 'exit' or 'bye' to end the chat.", ConsoleColor.Green);

                
                String userInput = Prompt(bot.userName).ToLower();
                

                if (userInput == "exit" || userInput == "bye") //if the user types exit or bye, the program stops
                {

                    bot.TypingEffect(bot.botName + $": Goodbye! Have a great day {bot.userName}!", ConsoleColor.Green);
                    break; //sentinel value that stops the loop if the user typed exit
                }

                if (!bot.FindBestResponse(responses, userInput))
                {
                    bot.TypingEffect($"{bot.botName}: Sorry, I cannot answer that. Try asking about passwords, phishing, or safe browsing.", ConsoleColor.Red);
                }
            }

        }

        private string Prompt(string speaker)
        {
            string input;
            do
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write($"{speaker}: ");
                input = Console.ReadLine();
                Console.ResetColor();
            } while (string.IsNullOrEmpty(input));

            return input;
        }
    }
}