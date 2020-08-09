using System;


namespace HasToTex.Model.Exceptions
{
    public class HaskellAbstractionException : Exception
    {
        /// <inheritdoc />
        public HaskellAbstractionException (string message) : base (message) {}
    }
}