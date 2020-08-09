using System;

using HasToTex.Io;


namespace HasToTex
{
    public class Program
    {
        static void Main (string [] args)
        {
            var file = Communicator.AskForTexFile ();
        }
    }
}