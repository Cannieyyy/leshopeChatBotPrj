using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Media;


namespace chatBotPrj
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //creating an instance for for a class PlaySound
            new PlaySound() { };

            //creating an instance for a class DisplayLogo
            new DisplayLogo() { };


            //Creating an instance for class ResponseCheck
            CyberBot bot = new CyberBot();

            //creating an instance for chat user interaction class
            ChatUI userInterface = new ChatUI(bot);
            userInterface.StartChat();
        }

       
    }
}
