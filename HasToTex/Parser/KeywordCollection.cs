using System.Collections.Generic;

using HasToTex.Model.Abstraction.Haskell.Keywords;


namespace HasToTex.Parser
{
    public class KeywordCollection
    {
        public KeywordCollection (Dictionary <int, KeywordEnum?> keywords) => Keywords = keywords;

        public KeywordCollection () => Keywords = new Dictionary <int, KeywordEnum?> ();

        /// <summary>
        /// A keywordEnum of null indicates that it's not a keyword (but another literal)
        /// </summary>
        private Dictionary <int, KeywordEnum?> Keywords { get; }

        public void Add (int startingIndex, KeywordEnum? keywordEnum) => Keywords.Add (startingIndex, keywordEnum);
    }
}