using HasToTex.Model.Abstraction.Haskell;
using HasToTex.Model.Exceptions;


namespace HasToTex.Parser
{
    public class HaskellAbstractionParser : Parser <string, HaskellProgram>
    {
        /// <inheritdoc />
        public HaskellAbstractionParser (string from) : base (from) {}

        /// <inheritdoc />
        public override HaskellProgram Parse ()
        {
            var program         = new HaskellProgram ();
            var withoutComments = CommentUtility.RemoveComments (From);
            foreach (var line in withoutComments.Split ('\n'))
            {
                // We don't care about indentation
                var trimmed = line.Trim (' ', '\t');
            }

            return program;
        }
    }
}