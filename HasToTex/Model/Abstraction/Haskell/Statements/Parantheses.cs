using HasToTex.Model.Exceptions;


namespace HasToTex.Model.Abstraction.Haskell.Statements
{
    /// <summary>
    /// Note that, contrary to tuples, this is not a collection of statements, but contains just one statement
    /// </summary>
    public class Parantheses : Statement
    {
        /// <inheritdoc />
        public Parantheses (string code, Statement inner) : base (code)
        {
            if (!code.Contains ("(") ||
                !code.Contains (")") ||
                !code.Contains (inner.Code))
                throw new InvalidCodeException (code, Expected);
            Inner = inner;
        }

        public Statement Inner { get; }

        private static string Expected = "(f x y ...)";
    }
}