using System.Collections.Generic;
using System.Linq;

using HasToTex.Model.Exceptions;


namespace HasToTex.Model.Abstraction.Haskell.Statements.Collections
{
    /// <summary>
    /// We will interpret lists as sets, which is technically wrong
    /// </summary>
    public class FiniteList : Statement
    {
        /// <inheritdoc />
        public FiniteList (string code, List <Statement> elements) : base (code)
        {
            if (!code.Contains ("[") ||
                !code.Contains ("]") ||
                elements.Any (e => !code.Contains (e.Code)))
                throw new InvalidCodeException (code, Expected);
            Elements = elements;
        }

        public List <Statement> Elements { get; }

        private static string Expected = "[[a],[b],...]";
    }
}