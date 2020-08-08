using HasToTex.Model.Exceptions;


namespace HasToTex.Model.Abstraction.Haskell
{
    public class If : Statement
    {
        /// <inheritdoc />
        public If (string code, Statement predicate, Statement then, Statement @else) : base (code)
        {
            if (!code.Contains("if") ||
                !code.Contains("then") ||
                !code.Contains("else") ||
                !code.Contains(Predicate.Code) ||
                !code.Contains(Then.Code) ||
                !code.Contains(Else.Code))
                throw new InvalidCodeException(code, Expected);
            Predicate = predicate;
            Then = then;
            Else = @else;
        }

        public Statement Predicate { get; }
        public Statement Then { get; }
        public Statement Else { get; }

        private static string Expected = "if [p] then [x] else [y]";
    }
}