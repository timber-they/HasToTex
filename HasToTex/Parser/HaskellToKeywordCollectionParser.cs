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

        // TODO: Unit test
        // TODO: Less monolithic
        /// <inheritdoc />
        public override KeywordCollection Parse ()
        {
            // TODO: Validate that not normalized has to be used here
            var collection = new KeywordCollection (From);

            // We don't care about indentation
            var normalized   = From.Normalize ();
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
            var regionStarted = false;

            for (var i = 0; i < normalized.Length; i++)
            {
                if (current.Length == 0 && char.IsWhiteSpace (normalized.Get (i)))
                    // Skip initial whitespace
                    continue;

                var c = normalized.Get (i);

                var keyword = regionManager.Register (c);
                if (keyword != null)
                {
                    collection.Add (i - KeywordMapping.EnumToKeyword[keyword.Value].Length + 1, new Keyword (keyword.Value));
                    regionStarted = regionManager.InRegion ();
                    if (!regionStarted)
                    {
                        // If the region ended, there's no old match to complete
                        // We didn't have any separator
                        i++;
                        Reset ();
                        continue;
                    }
                }

                if (!regionStarted && regionManager.InRegion ())
                    // At the start of the region, the region start serves as a separator, completing the last keyword
                    continue;

                current += c;

                matches = matches.Where (m => m.Matches (current)).ToHashSet ();
                if (literalMatch != null && !literalMatch.Matches (current))
                    literalMatch = null;

                if (matches.Count == 0)
                {
                    if (literalMatch == null)
                        throw new NoMatchException (current);

                    if (!literalMatch.Done (current))
                        continue;

                    // TODO: Validate
                    collection.Add (i - current.Length + 1, new Keyword (current.Length - 1));
                    Reset ();
                    continue;
                }

                var done = matches.FirstOrDefault (m => m.Done (current));
                if (done != null)
                {
                    collection.Add (i - current.Length + 1, new Keyword (KeywordMapping.KeywordToEnum [done.Goal]));
                    Reset ();
                    continue;
                }

                if (regionStarted)
                    // Apparently the start of the region didn't finish any keyword
                    Reset ();

                void Reset ()
                {
                    matches      = Match.GetMatches (KeywordMapping.TextualKeywords, KeywordMapping.SpecialKeywords);
                    literalMatch = new LiteralMatch ();
                    current      = "";
                    if (!regionStarted)
                        // Start with the separator, as it might be the start of the next keyword
                        // If a region started however, we already registered the region start
                        i--;

                    regionStarted = false;
                }
            }

            return collection;
        }
    }
}
