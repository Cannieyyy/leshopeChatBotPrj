
Cyber Security Awareness ChatBot
***********************************

Overview

This project is a chatbot application designed to raise awareness about cyber security. The chatbot is built in C# and provides information 
related to safe online practices, passwords, phishing, and more. It engages with the user in a conversational manner, 
guiding them through various cyber security topics. The bot can also accept a custom name if the user wishes to personalize it.

====================================================================================================================================
Features

- User Interaction: The chatbot interacts with the user, asking for their name and guiding them through a conversation about cyber security.
- Customizable Bot Name: The bot allows the user to customize its name.
- Cyber Security Awareness: The bot responds to questions related to cyber security (e.g., passwords, phishing, safe browsing, malware).
- Typing Effect: The bot uses a typing effect to simulate a real-time conversation.
- Exit Functionality: The user can end the chat by typing "exit" or "bye".

====================================================================================================================================
Prerequisites

Before running the project, ensure that you have the following installed on your machine:

- .NET Framework (or any .NET runtime compatible with your version of C#)
- A Text File for Responses: A file named `responses.txt` with the responses formatted as `keyword=response`.

=====================================================================================================================================

Setup and Installation

1. Clone this repository to your local machine.
2. Open the project in your preferred C# IDE.
3. Ensure that the `responses.txt` file is placed in the root directory of your project. If this file is missing, you can create it 
   with keyword-response pairs for the bot to recognize (e.g., `phishing=Phishing is a fraudulent attempt to obtain sensitive information...`).
4. Build and run the project in your IDE.

======================================================================================================================================
How to Use

1. Run the application.
2. The bot will greet you and ask for your name.
3. You can choose to rename the bot.
4. Start asking questions about cyber security (e.g., "What is phishing?" or "How do I create a strong password?").
5. The bot will respond based on the questions and the data in the `responses.txt` file.
6. To end the conversation, type "exit" or "bye".
=======================================================================================================================================
Code Explanation

- ResponseCheck Class: This is the main class that handles user interaction, loading responses, and managing the conversation flow.
- LoadResponsesFromFile Method: Loads predefined responses from a `responses.txt` file where each line contains a keyword and its 
  corresponding response.
- FindBestResponse` Method: Matches user input to available keywords and provides the most relevant response.
- TypingEffect` Method: Simulates a typing effect when the bot sends a message.
=======================================================================================================================================  
Example Interaction


ChatBot: Hello! My name is CyberBot.
ChatBot: What is your name?
You: My name is John
ChatBot: Would you like to give me a name John? (yes/no)
You: yes
ChatBot: What would you like to call me John?
You: I would like to call you SecBot
ChatBot: What would you like to ask me about John? Type 'exit' or 'bye' to end the chat.
You: What is phishing?
ChatBot: Phishing is a fraudulent attempt to obtain sensitive information...
You: exit
ChatBot: Goodbye! Have a great day John!
=========================================================================================================================================

Contribution

Feel free to fork this project, submit pull requests, or open issues if you have suggestions or find bugs.
=========================================================================================================================================
License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.