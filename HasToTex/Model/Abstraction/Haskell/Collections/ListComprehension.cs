using System.Collections.Generic;
using System.Linq;

using HasToTex.Model.Exceptions;


namespace HasToTex.Model.Abstraction.Haskell.Collections
{
    public class ListComprehension : Statement
    {
        /// <inheritdoc />
        public ListComprehension (string code, Statement result, List <Assignment> assignments, List <Statement> predicates) : base (code)
        {
            if (!code.Contains ("[") ||
                !code.Contains ("]") ||
                !code.Contains ("|") ||
                !code.Contains (result.Code) ||
                assignments.Any (a => !code.Contains (a.Code)) ||
                predicates.Any (p => !code.Contains (p.Code)))
                throw new InvalidCodeException (code, Expected);
            Result      = result;
            Assignments = assignments;
            Predicates  = predicates;
        }

        private Statement         Result      { get; }
        private List <Assignment> Assignments { get; }
        private List <Statement>  Predicates  { get; }

        private static string Expected = "[[x] | [y] <- [z], ..., [p(x)], ...]";
    }
}