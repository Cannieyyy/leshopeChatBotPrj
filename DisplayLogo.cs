using System;
using System.IO;

namespace chatBotPrj
{
    public class DisplayLogo
    {
        public DisplayLogo()
        {
            string logo = File.ReadAllText("logo.txt");
            Console.WriteLine(logo);
        }


    }
}