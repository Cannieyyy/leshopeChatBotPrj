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
    public class CyberBot
    {
        //declaring and initialzing global variables to store userName and botName
        public string botName { get; set; } = "CyberBot";// this initialises the botName to a specific name before the user changes it
        public string userName { get; set; } = "You";
        string userInput; //this declaration is for the input of the user
        string pattern = "^[a-zA-Z ]+$";// this is the declaration of a pattern to validate input

        // Method to remove unnecessary words from a string
        public string FilterUnwantedWords(string input)
        {
            string[] wordsToRemove = { "my", "name", "is", "i", "am", "call", "me", "the", "would", "like", ".", ",", "to", "your", "you" };
            List<string> words = new List<string>(input.Split(' '));

            // Remove unwanted words
            words.RemoveAll(word => wordsToRemove.Contains(word.ToLower()));

            // Return the first remaining word or "Unknown" if empty
            return words.Count > 0 ? words[0].ToString() : "Unknown";
        }

        //a method that validates is a username contains letters only
        public bool isValidName(string name) => Regex.IsMatch(name, pattern);

        //Creating a method LoadResponsesfromFile to load the responses from a file.
        public List<(string Keyword, string Response)> LoadResponsesFromFile(string filePath)
        {
            //Declaring an list object called responseList
            var responseList = new List<(string,string)>();

            if (!File.Exists(filePath))
            {
                Console.WriteLine("File does not exist");
                return responseList;

            }
                
                foreach (var line in File.ReadAllLines(filePath))//loops through each line of the array
                {
                   var parts = line.Split('=');//each line is going to be separated in to two parts, the "=" is where the line is going to split

                    if (parts.Length == 2)//error handling for if the line does not have an equal sign
                    
                        responseList.Add((parts[0].ToLower(), parts[1]));//a new string array is created and stored into the array list
                    
                }

            return responseList; //returns and arraylist responseList
        }//end of LoadResponsesFromFile


        //creating a method FindBestResponse to look for a suitable response  
        public bool FindBestResponse(List<(string Keyword,String Response)> responseList, string userInput)//this method passes an arraylist and a string as parameters
        {
            //foreach loop that loops through the arraylist in pairs
            foreach (var (keyword,response) in responseList)
            {
                //declaring a variable pattern and assigning it to a regex expression
                var pattern = $"\\b{Regex.Escape(keyword)}\\b"; //this pattern helps us match the exact keyword


                if (Regex.IsMatch(userInput, pattern, RegexOptions.IgnoreCase))
                {
                    TypingEffect($"{ botName}: {response}", ConsoleColor.Green);
                    return true;
                }
            }

            return false;
        }//end of FindBestResponse

        public void TypingEffect(string message, ConsoleColor color)// this method parses a string, a colour and int as a parameter
        {
            int speed = 20;

            Console.ForegroundColor = color;

            foreach (char c in message)
            {
                Console.Write(c);
                System.Threading.Thread.Sleep(speed);
            }
            Console.WriteLine();
            Console.ResetColor();
        }//end of typing effect

        //A method only dedicated to displaying the welcome message
        
        public CyberBot()
        {
           
        }//End of constructor

        
        


        


        


        // a methods that adds a typing effect to the chatbot's response
       

    }//end of class

}//end of name space