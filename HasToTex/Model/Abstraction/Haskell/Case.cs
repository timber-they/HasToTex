using System.Collections.Generic;
using System.Linq;

using HasToTex.Model.Exceptions;


namespace HasToTex.Model.Abstraction.Haskell
{
    public class Case : Statement
    {
        /// <inheritdoc />
        public Case (string code, Statement caseStatement, List <Pattern> patterns) : base (code)
        {
            if (!code.Contains ("case") ||
                !code.Contains ("of") ||
                !code.Contains ("->") ||
                !code.Contains (caseStatement.Code) ||
                Patterns.Any (p => !code.Contains (p.Match.Code) || !code.Contains (p.Then.Code)))
                throw new InvalidCodeException (code, Expected);
            CaseStatement = caseStatement;
            Patterns      = patterns;
        }

        public  Statement      CaseStatement { get; }
        private List <Pattern> Patterns      { get; }

        private static string Expected = "case [x] of\n" +
                                         "[a] -> [b]\n" +
                                         "...\n" +
                                         "_ -> [c]";


        public class Pattern
        {
            public Pattern (Statement match, Statement then)
            {
                Match = match;
                Then  = then;
            }

            /// <summary>
            /// Null indicates the default (_) match
            /// </summary>
            public Statement Match { get; }

            public Statement Then { get; }
        }
    }
}