using System.Collections.Generic;
using System.Linq;


namespace HasToTex.Parser
{
    public class Match
    {
        public Match (string goal)
        {
            Goal    = goal;
            Current = "";
        }

        public static HashSet <Match> CreateMatches (IEnumerable <string> goals) =>
            goals.Select (keyword => new Match (keyword)).ToHashSet ();

        public string Goal    { get; }
        public string Current { get; protected set; }

        /// <summary>
        /// Returns iff Current + c still matches the Goal
        /// </summary>
        public virtual bool Add (char c)
        {
            Current += c;
            return Goal.StartsWith (Current);
        }

        public bool WouldMatch (char c) => Goal.StartsWith (Current + c);

        /// <summary>
        /// Returns iff current fully matches the goal
        /// </summary>
        public bool Complete () => Goal == Current;
    }
}