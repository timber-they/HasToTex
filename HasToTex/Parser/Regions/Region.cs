using HasToTex.Model.Abstraction.Haskell.Keywords;


namespace HasToTex.Parser.Regions
{
    /// <summary>
    /// Represents a double quote string, a single quote string, a multiline comment, or a single line comment
    /// </summary>
    public class Region
    {
        /// <summary>
        /// Initializes a new region
        /// </summary>
        /// <param name="start">The starting keyword, or null for a line break</param>
        /// <param name="end">The ending keyword, or null for a line break</param>
        public Region (KeywordEnum? start, KeywordEnum? end)
        {
            Start   = start;
            End     = end;
            Escaped = false;
        }

        public  KeywordEnum? Start    { get; }
        public  KeywordEnum? End      { get; }
        private bool         Escaped  { get; set; }
        public  bool         InRegion { get; private set; }

        private string getStartString () => Start != null ? KeywordMapping.EnumToKeyword [Start.Value] : "\n";
        private string getEndString ()   => End != null ? KeywordMapping.EnumToKeyword [End.Value] : "\n";

        public void Register (char c, string current)
        {
            if (Escaped)
            {
                Escaped = false;
                return;
            }

            if (c == '\\')
            {
                Escaped = true;
                return;
            }

            if (!InRegion && current.EndsWith (getStartString ()))
            {
                InRegion = true;
                return;
            }

            if (InRegion && current.EndsWith (getEndString ()))
            {
                InRegion = false;
                return;
            }
        }
    }
}