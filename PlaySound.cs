using System;
using System.Collections;
using System.IO;
using System.Media;

namespace chatBotPrj
{
    public class PlaySound
    {
        public PlaySound()
        {
            //declaring a variable to store the file path of the sound
            string greeting = "greet.wav";

            if (File.Exists(greeting))
            {
                //creating a sound player object that allows me to play the sound
                SoundPlayer player = new SoundPlayer(greeting);//this object parses a string as a parameter
                player.Play();//play the sound
            }
            else
            {
                Console.WriteLine("File of the sound does not exist!");//this message is diaplyed if the file does not exist
            }
            
        }
    }
}