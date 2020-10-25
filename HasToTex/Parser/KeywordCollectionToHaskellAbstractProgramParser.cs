using System.Collections.Generic;
using System.Linq;

using HasToTex.Model.Abstraction.Haskell;
using HasToTex.Model.Abstraction.Haskell.Statements;
using HasToTex.Model.Builders;

namespace HasToTex.Parser
{
    public class KeywordCollectionToHaskellAbstractProgramParser : Parser <KeywordCollection, HaskellAbstractProgram>
    {
        /// <inheritdoc />
        public KeywordCollectionToHaskellAbstractProgramParser (KeywordCollection from) : base (from) {}

        /// <inheritdoc />
        public override HaskellAbstractProgram Parse ()
        {
            var         res         = new HaskellAbstractProgram ();
            CallBuilder callBuilder = null;

            List <int> indices = From.OrderedIndices ().ToList ();

            for (var i = 0; i < indices.Count; i++)
            {
                var index   = indices [i];
                var keyword = From.Get (index);

                if (keyword.IsLiteral ())
                {
                    var code = From.Substring (index, keyword.Length);
                    if (callBuilder == null)
                        // We're not currently in a call, so we got the call name itself
                        callBuilder = CallBuilder.Create ().Name (code);
                    else
                        // We're currently in a call, so we got an atomic parameter (new calls would've to be embraced by ())
                        callBuilder.Parameter (new Atomic (code, code));

                    continue;
                }

                // TODO: Handle keywords
            }

            return res;
        }
    }
}