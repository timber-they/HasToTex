using System.Collections.Generic;
using System.Linq;

using HasToTex.Model;
using HasToTex.Model.Abstraction.Haskell.Keywords;
using HasToTex.Model.Exceptions;
using HasToTex.Parser.Matcher;
using HasToTex.Parser.Regions;


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

            // We don't care about indentation
            var trimmed      = From.Trim ();
            var matches      = Match.GetMatches (KeywordMapping.TextualKeywords, KeywordMapping.SpecialKeywords);
            var literalMatch = new LiteralMatch ();
            var current      = "";
            var regionManager = new RegionManager (new (KeywordEnum?, KeywordEnum?, char?) []
            {
                // Double quotes
                (KeywordEnum.S_DoubleQuote, KeywordEnum.S_DoubleQuote, '\\'),
                // Single quotes
                (KeywordEnum.S_SingleQuote, KeywordEnum.S_SingleQuote, '\\'),
                // Multi line comment
                (KeywordEnum.S_BraceDashLeft, KeywordEnum.S_BraceDashRight, null),
                // Single line comment
                (KeywordEnum.S_DoubleDash, null, null)
            });

            for (var i = 0; i < trimmed.Length; i++)
            {
                if (current.Length == 0 && char.IsWhiteSpace (trimmed.Get (i)))
                    // Skip initial whitespace
                    continue;

                var c = trimmed.Get (i);

                var keyword = regionManager.Register (c);
                if (keyword != null)
                {
                    collection.Add (i - current.Length + 1, keyword.Value);
                    // We didn't have any separator
                    i++;
                    Reset ();
                    continue;
                }

                if (regionManager.InRegion ())
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