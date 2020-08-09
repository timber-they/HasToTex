using HasToTex.Model;
using HasToTex.Model.Abstraction.Haskell;
using HasToTex.Model.Exceptions;


namespace HasToTex.Parser
{
    public class HaskellAbstractionParser : Parser <HaskellProgram, HaskellAbstractProgram>
    {
        /// <inheritdoc />
        public HaskellAbstractionParser (HaskellProgram from) : base (from) {}

        /// <inheritdoc />
        public override HaskellAbstractProgram Parse ()
        {
            var abstractProgram = new HaskellAbstractProgram ();
            var withoutComments = From.WithoutComments ();
            // We don't care about indentation
            var trimmed = withoutComments.Trim ();

            return abstractProgram;
        }
    }
}