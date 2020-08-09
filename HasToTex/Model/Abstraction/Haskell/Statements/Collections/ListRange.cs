using System.Collections.Generic;
using System.Linq;

using HasToTex.Model.Exceptions;


namespace HasToTex.Model.Abstraction.Haskell.Statements.Collections
{
    public class ListRange : Statement
    {
        /// <inheritdoc />
        public ListRange (string code, List <Statement> firstElements, Statement lastElement) : base (code)
        {
            if (!code.Contains ("[") ||
                !code.Contains ("]") ||
                !code.Contains ("..") ||
                firstElements.Any (e => !code.Contains (e.Code)) ||
                !code.Contains (lastElement.Code))
                throw new InvalidCodeException (code, Expected);
            FirstElements = firstElements;
            LastElement   = lastElement;
        }

        /// <summary>
        /// Can consist of only one element
        /// </summary>
        private List <Statement> FirstElements { get; }

        /// <summary>
        /// Can be null
        /// </summary>
        private Statement LastElement { get; }

        private static string Expected = "[[x],[y]..[z]]|[[x]..[y]]|[[x],[y],..]|[[x],..]";
    }
}