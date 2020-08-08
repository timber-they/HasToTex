using System;


namespace HasToTex.Model.Exceptions
{
    public class InvalidCodeException : Exception
    {
        /// <inheritdoc />
        public InvalidCodeException (string actual, string expected)
            : base ($"Expected code like {expected}, but was {actual}")
        {
            Actual   = actual;
            Expected = expected;
        }

        public string Actual   { get; }
        public string Expected { get; }
    }
}