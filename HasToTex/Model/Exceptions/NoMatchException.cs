using System;


namespace HasToTex.Model.Exceptions
{
    public class NoMatchException : Exception
    {
        public NoMatchException (string s) : base ($"String {s} didn't match anything") {}
    }
}