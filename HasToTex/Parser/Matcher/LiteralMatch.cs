using System.Collections.Generic;
using System.Linq;


namespace HasToTex.Parser.Matcher
{
    public class LiteralMatch : TextualMatch
    {
        /// <inheritdoc />
        public LiteralMatch () : base (null) {}

        private bool NoSeparator (string c) => Separators.All (s => !c.Contains (s));

        /// <inheritdoc />
        public override bool Matches (string current) => NoSeparator (current) || Done (current);

        /// <inheritdoc />
        public override bool Done (string current) => Separated (current) &&
                                                      NoSeparator (current.Substring (0, current.Length - 1));
    }
}