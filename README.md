                                            Cyber Security Awareness ChatBot

Overview
***********************************

This project is a console-based chatbot application developed in C#. It aims to promote and raise awareness about cybersecurity best practices. 
The chatbot simulates a conversation with the user, helping them understand important cybersecurity concepts such as phishing, safe password use, 
malware prevention, social engineering, and more.

It is designed to provide an interactive and educational experience where the user can ask cybersecurity-related questions and receive informative, 
predefined answers. The bot also supports personalization, including changing the bot’s name for a more engaging interaction.


Features
***********************************

- **User Interaction**: The chatbot greets users, asks for their names, and continues with a friendly, engaging dialogue.
- **Name Recognition & Validation**: The bot validates and filters unnecessary words to extract a valid name.
- **Customizable Bot Name**: Users can rename the chatbot to personalize their experience.
- **Cyber Security Knowledge Base**: Provides answers to commonly asked questions about passwords, phishing, ransomware, safe browsing, and more.
- **Typing Effect**: Includes a typing effect that makes the bot's responses appear character-by-character to mimic real-time conversation.
- **Sentiment Detection**: Responds empathetically based on user mood (e.g., confused, frustrated, anxious).
- **File-Based Memory**: Stores data such as user name, bot name, favorite topics, last topic discussed, and last sentiment detected using text files.
- **Dynamic Follow-Ups**: If the user asks for more information, the bot intelligently follows up on previous topics.
- **Exit Command**: Typing “exit” or “bye” will end the session gracefully.


Prerequisites
***********************************

Before running the project, make sure your system has:

- **responses.txt**: A properly formatted plain text file containing keyword-response mappings. Format each line as:
- keyword=Response 1|Response 2|Response 3|...
  - Example: phishing=Phishing is a scam...|Beware of emails that...
Setup and Installation
***********************************

1. **Clone the Repository**  
 Clone the source code to your local machine using Git or download the ZIP.

2. **Open in IDE**  
 Launch your preferred C# IDE (e.g., Visual Studio, Rider, or Visual Studio Code).

3. **Prepare the `responses.txt` File**  
 Make sure the `responses.txt` file exists in the root directory. You can manually create it if it's missing. 
 Use the format mentioned in the "Prerequisites" section.

4. **Build the Project**  
 Compile the project using your IDE’s build function or using the `dotnet build` command in the terminal.

5. **Run the Application**  
 Start the program from the IDE or terminal. The console window will open and guide you through the interaction.


How to Use
***********************************

1. **Startup**  
 When the program launches:
 - A **voice greeting**  will play.
 - The **CyberBot logo** will be displayed in ASCII art.
 - A **welcome message** will greet the user.

2. **Name Input**  
 The bot will ask for your name. If the input is invalid (e.g., numbers, empty, or special characters), it will prompt you again.

3. **Bot Renaming**  
 After entering your name, the bot will ask if you'd like to rename it. If you respond “yes”, you'll be asked to provide a new name.

4. **Conversation Start**  
 You can now ask cybersecurity questions like:
 - “What is phishing?”
 - “Tell me about safe browsing.”
 - “How can I create a strong password?”

5. **Sentiment Handling**  
 If you type something like “I’m confused” or “I feel worried”, the bot will respond empathetically and provide tips.

6. **Follow-Up Questions**  
 If you ask for more information (e.g., “Tell me more”), the bot remembers the last topic and gives additional details.

7. **Ending the Chat**  
 Type **“exit”** or **“bye”** to close the session. The bot will say goodbye and stop running.


Code Explanation
***********************************

- **CyberBot Class**  
The main logic class. Handles name filtering, input parsing, chatbot memory, and response generation.

- **MemoryManager Class**  
Handles saving and retrieving persistent data from text files, such as usernames, bot names, user preferences, and chat history.

- **LoadResponsesFromFile()**  
Loads and parses the `responses.txt` file, storing keyword-response mappings in memory for fast access.

- **FindBestResponse()**  
Matches the user’s message with relevant keywords and provides an appropriate response. Also supports sentiment detection and dynamic follow-up.

- **TypingEffect()**  
Simulates a typewriter-style message display to enhance realism during conversation.

- **FilterUnwantedWords()**  
Removes non-essential words from user input to extract key details like names.


Example Interaction
***********************************

- On start, the voice greeting and ASCII logo will appear:

![Startup](https://github.com/user-attachments/assets/f550cc98-eca7-4d6b-ae64-67c89ea2494d)

- The chatbot welcomes the user and requests their name:

![Enter Name](https://github.com/user-attachments/assets/863fc5bc-e926-4f9a-a3ff-761393a69e5b)

- If the name input is invalid, it prompts again:

![Invalid Name](https://github.com/user-attachments/assets/820321cb-bfa4-4171-bdc9-1ae9bf6a6244)

- After a valid name is entered, the bot offers renaming:

![Bot Rename](https://github.com/user-attachments/assets/79d90339-bee6-453d-a40b-b083c388b619)

- Once named, the bot begins answering questions:

![Start Questions](https://github.com/user-attachments/assets/11a33b71-b347-4525-87f7-65817a72b4e3)

- Example response to a user inquiry:

![Response Example](https://github.com/user-attachments/assets/3ef8ed9a-284d-454b-9c3f-769112348259)

Contribution
***********************************

Contributions are welcome! Here's how you can help:

- Fork the repository and submit pull requests.
- Report bugs or suggest new features by opening issues.
- Add more cybersecurity content to `responses.txt`.

