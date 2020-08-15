using System.Collections;
using System.Collections.Generic;
using System.Linq;

using HasToTex.Util;


namespace HasToTex.Parser.Matcher
{
    public abstract class Match
    {
        public Match (string goal, IEnumerable <char> separators)
        {
            Goal       = goal;
            Separators = separators.Concat (new HashSet <char> {' ', '\n', '\t', '\r'}).ToHashSet ();
        }

        public string         Goal       { get; }
        public HashSet <char> Separators { get; }

        /// <summary>
        /// Whether the current has a chance to complete this match, i.e. whether it's the start of a full match
        /// </summary>
        /// <param name="current">The current string</param>
        public virtual bool Matches (string current) => Goal.StartsWith (current) || Done (current);

        /// <summary>
        /// Whether the current ends with a separator
        /// </summary>
        /// <param name="current">The current string</param>
        protected bool Separated (string current) => current.Length > 0 && Separators.Contains (current [current.Length - 1]);

        /// <summary>
        /// Whether the current is done, i.e. separated and completed
        /// </summary>
        /// <param name="current">The current string</param>
        public virtual bool Done (string current) => Separated (current) && Goal == current.Substring (0, current.Length - 1);

        /// <summary>
        /// Creates all match instances, for the textual and special keywords
        /// </summary>
        /// <param name="textualKeywords"></param>
        /// <param name="specialKeywords"></param>
        /// <returns></returns>
        public static HashSet <Match> GetMatches (IEnumerable <string> textualKeywords, IEnumerable <string> specialKeywords)
        {
            var textual = TextualMatch.CreateMatches (textualKeywords);
            var special = SpecialMatch.CreateMatches (specialKeywords);
            var all = new HashSet <Match> (textual.Count * 2 + special.Count * 2);
            all.AddAll (textual);
            all.AddAll (special);

            return all;
        }
    }
}