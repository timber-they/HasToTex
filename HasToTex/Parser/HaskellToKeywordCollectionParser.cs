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

        private KeywordCollection _collection;
        private HaskellProgram    _normalized;
        private HashSet <Match>   _matches;
        private LiteralMatch      _literalMatch;
        private string            _current;
        private RegionManager     _regionManager;
        private bool              _regionStarted;
        private int               _iterationIndex;

        /// <inheritdoc />
        public override KeywordCollection Parse ()
        {
            // TODO: Validate that not normalized has to be used here
            _collection = new KeywordCollection (From);

            // We don't care about indentation
            _normalized   = From.Normalize ();
            _matches      = Match.GetMatches (KeywordMapping.TextualKeywords, KeywordMapping.SpecialKeywords);
            _literalMatch = new LiteralMatch ();
            _current      = "";
            _regionManager = new RegionManager (new (KeywordEnum? Start, KeywordEnum? End, char? Escape) []
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
            _regionStarted = false;

            for (_iterationIndex = 0; _iterationIndex < _normalized.Length; _iterationIndex++)
                Iterate ();

            return _collection;
        }

        private void Iterate ()
        {
            var c = _normalized.Get (_iterationIndex);

            if (IterateRegion (c))
                return;

            if (_current.Length == 0 && char.IsWhiteSpace (c))
                // Skip initial whitespace
                return;

            _current += c;

            if (IterateMatch ())
                return;

            if (_regionStarted)
                // Apparently the start of the region didn't finish any keyword, so reset wasn't called yet
                Reset ();
        }

        /// <returns>True if the iteration is done (a match was completed), false otherwise.<br/>
        /// In other words, returns whether <see cref="Reset"/> was called</returns>
        private bool IterateMatch ()
        {
            _matches = _matches.Where (m => m.Matches (_current)).ToHashSet ();
            if (_literalMatch != null && !_literalMatch.Matches (_current))
                _literalMatch = null;

            return _matches.Count == 0 ? HandleLiteralMatch () : HandleKeywordMatch ();
        }

        /// <returns>True if the iteration is done (a keyword was completed), false otherwise</returns>
        private bool HandleKeywordMatch ()
        {
            var done = _matches.FirstOrDefault (m => m.Done (_current));
            if (done == null)
                return false;

            _collection.Add (_iterationIndex - _current.Length + 1, new Keyword (KeywordMapping.KeywordToEnum [done.Goal]));
            Reset ();
            return true;
        }

        /// <summary>
        /// Assumes that no keyword match was found
        /// </summary>
        /// <returns>True if the iteration is done (a match was completed), false otherwise</returns>
        private bool HandleLiteralMatch ()
        {
            if (_literalMatch == null)
                throw new NoMatchException (_current);

            if (!_literalMatch.Done (_current))
                return false;

            // TODO: Validate
            _collection.Add (_iterationIndex - _current.Length + 1, new Keyword (_current.Length - 1));
            Reset ();
            return true;
        }

        /// <returns>True if the iteration is done, false otherwise</returns>
        private bool IterateRegion (char c)
        {
            var keyword = _regionManager.Register (c);
            if (keyword == null)
                // If the keyword is null, there was no change in region, so the region hasn't just started (if active)
                return _regionManager.InRegion ();

            _collection.Add (_iterationIndex - KeywordMapping.EnumToKeyword [keyword.Value].Length + 1, new Keyword (keyword.Value));
            _regionStarted = _regionManager.InRegion ();

            if (_regionStarted)
                // At the start of the region, the region start serves as a separator, completing the last keyword
                // Hence, if the region just started, the iteration is not yet done
                return !_regionStarted && _regionManager.InRegion ();

            // If the region ended, there's no old match to complete
            // We didn't have any separator
            _iterationIndex++;
            Reset ();
            return true;
        }

        private void Reset ()
        {
            _matches      = Match.GetMatches (KeywordMapping.TextualKeywords, KeywordMapping.SpecialKeywords);
            _literalMatch = new LiteralMatch ();
            _current      = "";
            if (!_regionStarted)
                // Start with the separator, as it might be the start of the next keyword
                // If a region started however, we already registered the region start
                _iterationIndex--;

            _regionStarted = false;
        }
    }
}