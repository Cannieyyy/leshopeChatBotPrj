
                                             Cyber Security Awareness ChatBot
                                            

Overview
***********************************

This project is a chatbot application designed to raise awareness about cyber security. The chatbot is built in C# and provides information 
related to safe online practices, passwords, phishing, and more. It engages with the user in a conversational manner, 
guiding them through various cyber security topics. The bot can also accept a custom name if the user wishes to personalize it.



Features
***********************************

- User Interaction: The chatbot interacts with the user, asking for their name and guiding them through a conversation about cyber security.
- Customizable Bot Name: The bot allows the user to customize its name.
- Cyber Security Awareness: The bot responds to questions related to cyber security (e.g., passwords, phishing, safe browsing, malware).
- Typing Effect: The bot uses a typing effect to simulate a real-time conversation.
- Exit Functionality: The user can end the chat by typing "exit" or "bye".


Prerequisites
***********************************

Before running the project, ensure that you have the following installed on your machine:

- .NET Framework (or any .NET runtime compatible with your version of C#)
- A Text File for Responses: A file named `responses.txt` with the responses formatted as `keyword=response`.


Setup and Installation
***********************************

1. Clone this repository to your local machine.
2. Open the project in your preferred C# IDE.
3. Ensure that the `responses.txt` file is placed in the root directory of your project. If this file is missing, you can create it 
   with keyword-response pairs for the bot to recognize (e.g., `phishing=Phishing is a fraudulent attempt to obtain sensitive information...`).
4. Build and run the project in your IDE.


How to Use
***********************************

1. Run the application.
2. The bot will greet you and ask for your name.
3. You can choose to rename the bot.
4. Start asking questions about cyber security (e.g., "What is phishing?" or "How do I create a strong password?").
5. The bot will respond based on the questions and the data in the `responses.txt` file.
6. To end the conversation, type "exit" or "bye".

Code Explanation
***********************************

- ResponseCheck Class: This is the main class that handles user interaction, loading responses, and managing the conversation flow.
- LoadResponsesFromFile Method: Loads predefined responses from a `responses.txt` file where each line contains a keyword and its 
  corresponding response.
- FindBestResponse` Method: Matches user input to available keywords and provides the most relevant response.
- TypingEffect` Method: Simulates a typing effect when the bot sends a message.
 

Example Interaction
***********************************
- When the program starts to execute, the  voice greeting will play and then the logo will display 
![image](https://github.com/user-attachments/assets/f550cc98-eca7-4d6b-ae64-67c89ea2494d)


- After the logo display welcome message will display, along with a greeting message and the program asks the user for thier name
![image](https://github.com/user-attachments/assets/863fc5bc-e926-4f9a-a3ff-761393a69e5b)

- The user must enter their name, if the do not enter their name the program will not continue to the next prompt
![image](https://github.com/user-attachments/assets/820321cb-bfa4-4171-bdc9-1ae9bf6a6244)

- After the user has entered their name, the program will filter all uneccessary words and only store the name
- Then the chats will personalise using the users name
- Then a question asking the user if they want to give the bot a name or not
![image](https://github.com/user-attachments/assets/79d90339-bee6-453d-a40b-b083c388b619)

- If the user types in yes the bot will ask a follow up question asking the user what would they like to name the bot
- Then the user will rename the bot and the program will personalise the chat using the name that the user typed
- After this event, the program will start to ask the user what they would like to ask the bot
![image](https://github.com/user-attachments/assets/11a33b71-b347-4525-87f7-65817a72b4e3)

- Bot will then answer accordingly
![image](https://github.com/user-attachments/assets/3ef8ed9a-284d-454b-9c3f-769112348259)







Contribution
***********************************
Feel free to fork this project, submit pull requests, or open issues if you have suggestions or find bugs.

