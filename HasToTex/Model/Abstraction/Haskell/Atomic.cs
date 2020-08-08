using System.Collections.Generic;


namespace HasToTex.Model.Abstraction.Haskell
{
    /// <summary>
    /// A special call without any parameters
    /// </summary>
    public class Atomic : Call
    {
        /// <inheritdoc />
        public Atomic (string code, string name) : base (code, name, new List <Statement> ()) {}
    }
}