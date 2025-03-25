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


            //creating an instance for a class DisplayLogo
            new DisplayLogo() { };

            //creating an instance for for a class PlaySound
            new PlaySound() { };


            //Creating an instance for class ResponseCheck
            new ResponseCheck();

             
            
        }

       
    }
}
