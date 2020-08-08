using System.Collections.Generic;
using System.Linq;

using HasToTex.Model.Exceptions;


namespace HasToTex.Model.Abstraction.Haskell
{
    public class Tuple : Statement
    {
        /// <inheritdoc />
        public Tuple (string code, List <Statement> elements) : base (code)
        {
            if (!code.Contains ("(") ||
                !code.Contains (")") ||
                elements.Any (e => !code.Contains (e.Code)))
                throw new InvalidCodeException (code, Expected);
            Elements = elements;
        }

        public List <Statement> Elements { get; }

        private static string Expected = "([x], [y], ..., [z])";
    }
}