using System.Collections.Generic;
using System.Linq;

using HasToTex.Model;
using HasToTex.Model.Abstraction.Haskell.Keywords;
using HasToTex.Model.Exceptions;
using HasToTex.Parser.Matcher;


namespace HasToTex.Parser
{
    public class HaskellToKeywordCollectionParser : Parser <HaskellProgram, KeywordCollection>
    {
        /// <inheritdoc />
        public HaskellToKeywordCollectionParser (HaskellProgram from) : base (from) {}

        /// <inheritdoc />
        public override KeywordCollection Parse ()
        {
            var collection = new KeywordCollection ();

            // We don't care about comments
            var withoutComments = From.WithoutComments ();
            // We don't care about indentation
            var trimmed        = withoutComments.Trim ();
            var matches        = Match.GetMatches (KeywordMapping.TextualKeywords, KeywordMapping.SpecialKeywords);
            var literalMatch   = new LiteralMatch ();
            var current        = "";
            var inDoubleQuotes = false;
            var inSingleQuotes = false;

            for (var i = 0; i < trimmed.Length; i++)
            {
                if (current.Length == 0 && char.IsWhiteSpace (trimmed.Get (i)))
                    // Skip initial whitespace
                    continue;

                var skip = inDoubleQuotes || inSingleQuotes;
                var c    = trimmed.Get (i);

                switch (c)
                {
                    case '"' when /* Escaped? */ !(inDoubleQuotes && trimmed.Get (i - 1) == '\\'):
                        collection.Add (i, KeywordEnum.S_DoubleQuote);
                        inDoubleQuotes = !inDoubleQuotes;
                        break;
                    case '\'' when /* Escaped? */ !(inSingleQuotes && trimmed.Get (i - 1) == '\\'):
                        collection.Add (i, KeywordEnum.S_SingleQuote);
                        inSingleQuotes = !inSingleQuotes;
                        break;
                }

                if (skip)
                    continue;

                current += c;

                matches = matches.Where (m => m.Matches (current)).ToHashSet ();
                if (literalMatch != null && !literalMatch.Matches (current))
                    literalMatch = null;

                if (matches.Count == 0)
                {
                    if (literalMatch == null)
                        throw new NoMatchException (current);

                    collection.Add (i - current.Length + 1, null);
                    Reset ();
                    continue;
                }

                var done = matches.FirstOrDefault (m => m.Done (current));
                if (done != null)
                {
                    collection.Add (i - current.Length + 1, KeywordMapping.KeywordToEnum [done.Goal]);
                    Reset ();
                    continue;
                }

                void Reset ()
                {
                    matches      = Match.GetMatches (KeywordMapping.TextualKeywords, KeywordMapping.SpecialKeywords);
                    literalMatch = new LiteralMatch ();
                    current      = "";
                    // Start with the separator, as it might be the start of the next keyword
                    i--;
                }
            }

            return collection;
        }
    }
}