using System;
using System.Linq;

using HasToTex.Model;
using HasToTex.Model.Abstraction.Haskell;
using HasToTex.Model.Abstraction.Haskell.Keywords;


namespace HasToTex.Parser
{
    public class KeywordCollectionToHaskellAbstractProgramParser : Parser <KeywordCollection, HaskellAbstractProgram>
    {
        /// <inheritdoc />
        public KeywordCollectionToHaskellAbstractProgramParser (KeywordCollection from) : base (from) {}

        /// <inheritdoc />
        public override HaskellAbstractProgram Parse () => throw new NotImplementedException ();
    }
}