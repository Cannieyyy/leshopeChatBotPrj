using System.Collections;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Data;

namespace chatBotPrj
{
    public class CyberBot
    {
        //cretaing an instance of a class memory manager to access class methods
        private MemoryManager fileManager = new MemoryManager();
        
        //declaring a dictionary to temporarly store response and keyword
        private Dictionary<string, string> memory;

        //declaring and initialzing global variables to store userName and botName
        public string botName { get; set; } = "CyberBot";// this initialises the botName to a specific name before the user changes it
        public string userName { get; set; } = "You";
        string userInput; //this declaration is for the input of the user
        string pattern = "^[a-zA-Z ]+$";// this is the declaration of a pattern to validate input


        // Method to remove unnecessary words from a string
        public string FilterUnwantedWords(string input)
        {
            //declaring an array to store all the unwanted words
            string[] wordsToRemove = { "my", "name", "is", "i", "am", "call", "me", "the", "would", "like", ".", ",", "to", "your", "you" };
            List<string> words = new List<string>(input.Split(' '));

            // Remove unwanted words
            words.RemoveAll(word => wordsToRemove.Contains(word.ToLower()));

            // Return the first remaining word or "Unknown" if empty
            return words.Count > 0 ? words[0].ToString() : "Unknown";
        }


        //a method that validates is a username contains letters only
        public bool isValidName(string name) 
        {
            return ( Regex.IsMatch(name, pattern) && !string.IsNullOrWhiteSpace(name));
        }

        //Creating a method LoadResponsesfromFile to load the responses from a file.
        public List<(string Keyword, List<string> Response)> LoadResponsesFromFile(string filePath)
        {
            //Declaring an list object called responseList
            var responseList = new List<(string, List<string>)>();

            if (!File.Exists(filePath))//check if file exists
            {
                Console.WriteLine("File does not exist");
                return responseList;

            }

            foreach (var line in File.ReadAllLines(filePath))//loops through each line of the array
            {
                var parts = line.Split('=');//each line is going to be separated in to two parts, the "=" is where the line is going to split

                if (parts.Length == 2)//error handling for if the line does not have an equal sign
                {
                    var keyword = parts[0].ToLower();
                    var responses = parts[1].Split('|').Select(r => r.Trim()).ToList();
                    responseList.Add((keyword, responses));//a new string array is created and stored into the array list
                }
            }

            return responseList; //returns and arraylist responseList
        }//end of LoadResponsesFromFile


        private string lastTopicAsked = null;


        //creating a method FindBestResponse to look for a suitable response  
        public bool FindBestResponse(List<(string Keyword, List<string> Response)> responseList, string userInput)//this method passes an arraylist and a string as parameters
        {
            //check if user input is not empty or contain number and charaecters
            if (!isValidName(userInput)) return false;


            userInput = userInput.ToLower();
            string sentiment = DetectSentiment(userInput);//assign sentiment variable to detect sentiment method

            //if sentiment found, store to file
            if (sentiment != null) fileManager.SaveData("lastSentiment", sentiment);

            //if a follow up keyword is detected - parsing methods
            if (IsFollowUp(userInput) && HandleFollowUpResponse(responseList))
                return true;

            //assigning matches topics amd matching responses to matches Topics method
            var (matchedTopics, matchingResponses) = MatchesTopics(responseList, userInput);
            return DisplayResponses(matchedTopics, matchingResponses, sentiment);
        }//end of find best response method

        //method to detect follow up keywords
        private bool IsFollowUp(string userInput)
        {
            //return follow up response if user input contains one keyword
            return (userInput.Contains("explain") || userInput.Contains("tell me more")
                || userInput.Contains("more info") || userInput.Contains("I dont understand")
                && !string.IsNullOrEmpty(lastTopicAsked));
        
        }//end of IsFollowUp method

        //method to handle follow up question
        private bool HandleFollowUpResponse(List<(string Keyword, List<string> Response)> responseList) 
        { 
                //find first keyword in the response list that matches lastTopicAsked
                var followUp = responseList.FirstOrDefault(r => r.Keyword == lastTopicAsked);
                if (followUp.Response != null && followUp.Response.Count > 0)
                {
                    //generate a random follow up response
                    string response = followUp.Response[new Random().Next(followUp.Response.Count)];
                    TypingEffect($"{botName}: Sure! Here's more about {lastTopicAsked}: {response}", ConsoleColor.Green);
                    return true;
                }
                return false;
         }//end of handleFollowUpResponse

        //method to find matched topics
        private (List<string> matchedTopics, List<string> matchingResponses) MatchesTopics(
           List<(string Keyword, List<string> Response)> responseList, string userInput)
        {
            var matchedTopics = new List<string>();
            var matchingResponses = new List<string>();

            //store sentiment keywords
            var sentimentKeywords = new List<string> { "worried", "frustrated", "curious", "anxious", "confused", "sad", "stressed", "interested" };

            //foreach loop that loops through the list in pairs
            foreach (var (keyword, responses) in responseList)
            {
                if (sentimentKeywords.Contains(keyword.ToLower())) continue;

                //declaring a variable pattern and assigning it to a regex expression
                var pattern = $"\\b{Regex.Escape(keyword)}\\b"; //this pattern helps us match the exact keyword

                //select the response randomly
                if (Regex.IsMatch(userInput, pattern, RegexOptions.IgnoreCase))
                {
                    matchedTopics.Add(keyword);

                    string response = responses[new Random().Next(responses.Count)];
                    var previousTopics = fileManager.LoadList($"favouriteTopics_{userName}");//load the favourite topics from file
                    bool isRemembered = previousTopics.Contains(keyword, StringComparer.OrdinalIgnoreCase);

                    if (isRemembered)
                    {
                        matchingResponses.Add($"{botName}: I remember we disscussed {keyword} earlier. {response}");
                    }
                    else
                    {
                        //Combine the favourite topic with other topics in the file
                        fileManager.AppendToList($"favouriteTopics_{userName}", keyword);
                        matchingResponses.Add($"{botName}: {response}");
                    }

                    //save the lastTopic to a file
                    fileManager.SaveData("lastTopic", keyword);
                    lastTopicAsked = keyword;
                }
            }
            return (matchedTopics, matchingResponses);
        }//end of find matching response method

        //method to display reponse
        private bool DisplayResponses(List<string> matchedTopics, List<string> matchingResponses, string sentiment)
        {
            if (matchedTopics.Count == 0) return false; //if no matched topic
            
                if (!string.IsNullOrEmpty(sentiment))//if sentiment id found
                {
                    string comfort = $"{botName}: I'm sorry you're feeling {sentiment}. Here are some tips that can help you.";
                    string tips = string.Join("\n", matchingResponses.Select(r => $"{r}"));
                    TypingEffect($"{comfort}\n{tips}", ConsoleColor.Yellow);

                }
                else
                {
                    foreach (var response in matchingResponses)
                    {
                        TypingEffect(response, ConsoleColor.Green);
                    }
                }

                return true;
            
        }//Diplay reposnes method
 
        //a method that detects sentiment
        private string DetectSentiment(string input)
        {
            input = input.ToLower();
            if (input.Contains("worried") || input.Contains("anxious")) return "worried";
            if (input.Contains("curious") || input.Contains("interested")) return "curious";
            if (input.Contains("frustrated") || input.Contains("confused") || input.Contains("sad") || input.Contains("stressed")) return "frustrated";
            return null;
        }

        // a methods that adds a typing effect to the chatbot's response
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

    }//end of class

}//end of name space