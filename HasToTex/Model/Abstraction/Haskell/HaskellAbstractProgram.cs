using System.Collections.Generic;


namespace HasToTex.Model.Abstraction.Haskell
{
    public class HaskellAbstractProgram
    {
        public HaskellAbstractProgram (List <Statement> statements) => Statements = statements;
        public HaskellAbstractProgram () : this (new List <Statement> ()) {}
        public List <Statement> Statements { get; }

        public void AddStatement (Statement statement) => Statements.Add (statement);
    }
}