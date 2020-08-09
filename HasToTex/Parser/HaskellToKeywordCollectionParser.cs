using System.Collections.Generic;
using System.Linq;

using HasToTex.Model;
using HasToTex.Model.Abstraction.Haskell.Keywords;


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
            var trimmed = withoutComments.Trim ();

            var   textualMatches        = Match.CreateMatches (KeywordMapping.TextualKeywords);
            var   specialMatches        = Match.CreateMatches (KeywordMapping.SpecialKeywords);
            var   currentStatementStart = 0;
            Match complete              = null;
            Match specialComplete       = null;
            var   inDoubleQuotes        = false;
            var   inSingleQuotes        = false;

            for (var i = 0; i < trimmed.Length; i++)
            {
                var c = trimmed.Get (i);

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

                if (inDoubleQuotes || inSingleQuotes)
                    // There are no keywords in quotes
                    continue;

                if (char.IsWhiteSpace (c) && i == currentStatementStart + 1)
                {
                    // Skip initial spaces
                    currentStatementStart = i;
                    continue;
                }

                var separating = IsSeparating (c, specialMatches);

                if (complete != null && separating)
                    // We're complete and separating
                    Done (KeywordMapping.KeywordToEnum [complete.Goal]);
                else if (specialComplete != null && specialMatches.All (sm => !sm.WouldMatch (c)))
                    // specialComplete is either the only special match, or all others are incompatible with the next character
                    //  -> we're separating
                    Done (KeywordMapping.KeywordToEnum [specialComplete.Goal]);
                else if (separating)
                    // No keyword is complete, but we're nevertheless separating - must be a literal
                    Done (null);
                else if (textualMatches.Count > 0 || specialMatches.Count > 0)
                {
                    // We're not yet separating, but still have keywords left to match

                    if (textualMatches.Count > 0)
                    {
                        textualMatches.RemoveWhere (match => !match.Add (c));
                        complete = textualMatches.FirstOrDefault (match => match.Complete ());
                        // If complete != null, we have a match. We're not done yet though, because we still need a separator
                    }

                    if (specialMatches.Count > 0)
                    {
                        specialMatches.RemoveWhere (match => !match.Add (c));
                        specialComplete = specialMatches.FirstOrDefault (match => match.Complete ());
                        // If specialComplete != null, we have a match. We're not done yet though, because we still need a separator
                    }
                }
                // else: We're in a literal, but not yet separating

                void Done (KeywordEnum? keyword)
                {
                    collection.Add (currentStatementStart, keyword);

                    // The next statement starts at the separator, as this might be a special keyword
                    currentStatementStart = i;

                    // Reset
                    textualMatches  = Match.CreateMatches (KeywordMapping.TextualKeywords);
                    specialMatches  = Match.CreateMatches (KeywordMapping.SpecialKeywords);
                    complete        = null;
                    specialComplete = null;
                }
            }

            return collection;
        }

        private static bool IsSeparating (char c, HashSet <Match> specialMatches)
        {
            // Whitespace characters are always interpreted as separators
            return char.IsWhiteSpace (c) ||
                   // We already had at least one non-whitespace character that indicated this wasn't a special keyword,
                   //  but a special keyword character start appeared, so the last thing (literal or textual keyword)
                   //  has ended now
                   specialMatches.Count == 0 && KeywordMapping.SpecialKeywords.Any (s => s [0] == c);
        }
    }
}