using System.Collections.Generic;


namespace HasToTex.Model.Abstraction.Haskell
{
    public class HaskellProgram
    {
        public HaskellProgram (List <Statement> statements) => Statements = statements;
        public HaskellProgram () : this (new List <Statement> ()) {}
        public List <Statement> Statements { get; }

        public void AddStatement (Statement statement) => Statements.Add (statement);
    }
}