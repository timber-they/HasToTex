using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace HasToTex.Parser.Matcher
{
    public class TextualMatch : Match
    {
        /// <inheritdoc />
        public TextualMatch (string goal) : base (goal,
                                                  new HashSet <char>
                                                  {
                                                      '!',
                                                      '\'',
                                                      '"',
                                                      '-',
                                                      ':',
                                                      ';',
                                                      '<',
                                                      '>',
                                                      ',',
                                                      '=',
                                                      '?',
                                                      '#',
                                                      '*',
                                                      '[',
                                                      ']',
                                                      '\\',
                                                      '`',
                                                      '{',
                                                      '}',
                                                      '-',
                                                      '|',
                                                      '~',
                                                      '[',
                                                      ']',
                                                      '(',
                                                      ')'
                                                  }) {}

        public static HashSet <Match> CreateMatches (IEnumerable <string> keywords)
            => keywords.Select (s => (Match) new TextualMatch (s)).ToHashSet ();
    }
}