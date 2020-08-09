using System.Collections.Generic;
using System.Linq;

using HasToTex.Model.Abstraction.Haskell.Statements;


namespace HasToTex.Model.Abstraction.Haskell
{
    public class HaskellAbstractProgram
    {
        public HaskellAbstractProgram (List <Statement> statements) => Statements = statements;
        public HaskellAbstractProgram () : this (new List <Statement> ()) {}
        public List <Statement> Statements { get; }

        public void AddStatement (Statement statement) => Statements.Add (statement);

        public static HaskellAbstractProgram Construct (IEnumerable <HaskellAbstractProgram> subPrograms)
            => new HaskellAbstractProgram (subPrograms.SelectMany (s => s.Statements).ToList ());
    }
}