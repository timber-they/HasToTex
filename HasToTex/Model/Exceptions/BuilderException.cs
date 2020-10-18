using System;


namespace HasToTex.Model.Exceptions
{
    public class BuilderException : Exception
    {
        public BuilderException (string context) : base ($"A problem whilst building occurred: {context}") {}
    }
}