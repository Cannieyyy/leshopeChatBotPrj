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
            //Getting where sound file is
            string soundLocation = AppDomain.CurrentDomain.BaseDirectory;


            //Checking if it is getting the Directory
            Console.WriteLine(soundLocation);

            //Replacing the bin\debug so it can get the audio
            string updatedPath = soundLocation.Replace("bin\\Debug\\", "");

            //Combining the wav name as sound.wav with the updated path
            string fullPath = Path.Combine(updatedPath, "greet.wav");

            //Passing to the method playWav
            playWav(fullPath);


        }//end of constuctor
         //Creating a method to play the .wav sound file

        private void playWav(string fullPath)
        {
            //Try and catch

            try
            {
                //Playing the sound 
                using (SoundPlayer soundPlayer = new SoundPlayer(fullPath))
                {
                    //This is to play the sound till the end
                    soundPlayer.PlaySync();
                }

            }
            catch (Exception error)
            {
                //Displaying the error message
                Console.WriteLine(error.Message);

            }//end of try and catch 
        }

    }//end of class
}//end of namespace