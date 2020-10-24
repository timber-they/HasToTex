using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using HasToTex.Model;
using HasToTex.Model.Abstraction.Haskell.Keywords;


namespace HasToTex.Parser
{
    public class KeywordCollection
    {
        public KeywordCollection (Dictionary <int, Keyword> keywords, HaskellProgram program)
        {
            Keywords = keywords;
            Program  = program;
        }

        public KeywordCollection (HaskellProgram program)
        {
            Program  = program;
            Keywords = new Dictionary <int, Keyword> ();
        }

        /// <summary>
        /// A keywordEnum of null indicates that it's not a keyword (but another literal)
        /// </summary>
        private Dictionary <int, Keyword> Keywords { get; }

        public IEnumerable <Keyword> GetKeywords () => Keywords.Values;

        public int Count => Keywords.Count;

        /// <summary>
        /// The original program regarding this keyword collection
        /// </summary>
        private HaskellProgram Program { get; }

        public void Add (int startingIndex, Keyword keyword)
        {
            if (Keywords.ContainsKey (startingIndex))
                Debugger.Break ();

            Keywords.Add (startingIndex, keyword);
        }

        public IOrderedEnumerable <int> OrderedIndices () => Keywords.Keys.OrderBy (i => i);

        public Keyword Get (int index) => Keywords [index];

        public string Substring (int index, int length) => Program.Content.Substring (index, length);
    }
}