using System.Media;

namespace chatBotPrj
{
    internal class PlaySound
    {
        public PlaySound()
        {
            SoundPlayer player = new SoundPlayer("greet.wav");
            player.Play();
        }
    }
}