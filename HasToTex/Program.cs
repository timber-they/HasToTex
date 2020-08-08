using System;

using HasToTex.Io;


namespace HasToTex
{
    class Program
    {
        static void Main (string [] args)
        {
            var file = Communicator.AskForTexFile ();
        }
    }
}