using System.Collections.Generic;
using System.Linq;

using HasToTex.Model.Exceptions;


namespace HasToTex.Model.Abstraction.Haskell.Statements
{
    public class GuardedFunction : Statement
    {
        /// <inheritdoc />
        public GuardedFunction (string code, string name, List <string> parameters, List <Guard> guards) : base (code)
        {
            if (!code.Contains ("|") ||
                !code.Contains ("=") ||
                !code.Contains (name) ||
                parameters.Any (p => !code.Contains (p)) ||
                guards.Any (g => !code.Contains (g.Predicate?.Code ?? "otherwise") ||
                                 !code.Contains (g.Body.Code)))
                throw new InvalidCodeException (code, Expected);
            Name       = name;
            Parameters = parameters;
            Guards     = guards;
        }

        public string        Name       { get; }
        public List <string> Parameters { get; }
        public List <Guard>  Guards     { get; }

        private static string Expected = "[f] [x] ...\n" +
                                         "| [p] = [y]\n" +
                                         "| ...\n" +
                                         "| otherwise = [z]";


        public class Guard
        {
            public Guard (Statement predicate, Statement body)
            {
                Predicate = predicate;
                Body      = body;
            }

            /// <summary>
            /// A predicate of null indicates the otherwise guard
            /// </summary>
            public Statement Predicate { get; }

            public Statement Body { get; }
        }
    }
}