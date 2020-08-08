using System.Collections.Generic;
using System.Linq;

using HasToTex.Model.Exceptions;


namespace HasToTex.Model.Abstraction.Haskell
{
    /// <summary>
    /// A function call. Note that the function doesn't have to have parameters and hence can be an atomic value
    /// </summary>
    public class Call : Statement
    {
        /// <inheritdoc />
        public Call (string code, string name, List <Statement> parameters) : base (code)
        {
            if (!code.Contains (name) ||
                parameters.Any (s => !code.Contains (s.Code)))
                throw new InvalidCodeException (code, Expected);
            Name       = name;
            Parameters = parameters;
        }

        private string           Name       { get; }
        private List <Statement> Parameters { get; }

        private static string Expected = "[x] [y] ...l";
    }
}