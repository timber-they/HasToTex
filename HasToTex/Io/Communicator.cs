using System;
using System.IO;


namespace HasToTex.Io
{
    public static class Communicator
    {
        /// <summary>
        /// Asks for the LateX input file and returns the filename (which is bound to exist)
        /// </summary>
        /// <returns>The filename</returns>
        public static string AskForTexFile ()
        {
            Console.WriteLine ("Please enter the name of the LateX file.");
            var file = Console.ReadLine ();
            while (!File.Exists (file))
            {
                Console.Error.WriteLine ("The file doesn't exist - please try again.");
                file = Console.ReadLine ();
            }

            return file;
        }
    }
}