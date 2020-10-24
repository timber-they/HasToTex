using System.Collections.Generic;
using System.Linq;

using HasToTex.Util;


namespace HasToTex.Parser.Matcher
{
    public class SpecialMatch : Match
    {
        /// <inheritdoc />
        public SpecialMatch (string goal) : base (goal,
                                                  new HashSet <char>
                                                      {
                                                          '_', '"', '\'', '{', '}', '[', ']', '(', ')'
                                                      }
                                                      .Concat (Extensions.Range ('a', 'z'))
                                                      .Concat (Extensions.Range ('A', 'Z'))) {}

        public static HashSet <Match> CreateMatches (IEnumerable <string> keywords)
            => keywords.Select (s => (Match) new SpecialMatch (s)).ToHashSet ();
    }
}