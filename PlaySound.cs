using System.Collections;
using System.Media;

namespace chatBotPrj
{
    public class PlaySound
    {
        public PlaySound()
        { 
            SoundPlayer player = new SoundPlayer("greet.wav");
            player.Play();
        }
    }
}