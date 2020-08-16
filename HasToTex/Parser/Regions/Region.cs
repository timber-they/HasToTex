using HasToTex.Model.Abstraction.Haskell.Keywords;


namespace HasToTex.Parser.Regions
{
    /// <summary>
    /// Represents a double quote string, a single quote string, a multiline comment, or a single line comment.
    /// A regions bounds are given as follows:<br/>
    /// ......|--|<br/>
    /// foo = "bar"
    /// </summary>
    public class Region
    {
        /// <summary>
        /// Initializes a new region
        /// </summary>
        /// <param name="start">The starting keyword, or null for a line break</param>
        /// <param name="end">The ending keyword, or null for a line break</param>
        /// <param name="escape">The escape character, or null if not escapable</param>
        public Region (KeywordEnum? start, KeywordEnum? end, char? escape)
        {
            Start   = start;
            End     = end;
            Escape  = escape;
            Escaped = false;
            Current = "";
        }

        public  KeywordEnum? Start    { get; }
        public  KeywordEnum? End      { get; }
        private bool         Escaped  { get; set; }
        public  bool         InRegion { get; private set; }
        private string       Current  { get; set; }
        private char?        Escape   { get; }

        private string GetStartString () => Start != null ? KeywordMapping.EnumToKeyword [Start.Value] : "\n";
        private string GetEndString ()   => End != null ? KeywordMapping.EnumToKeyword [End.Value] : "\n";

        /// <summary>
        /// Registers the given character. Note that you should only register characters that aren't in another region,
        /// as this region doesn't know of its brothers
        /// </summary>
        /// <param name="c">The character to register</param>
        /// <returns>Whether or not it's inRegion is true</returns>
        public bool Register (char c)
        {
            Current += c;

            if (Escaped)
                Escaped = false;
            else if (InRegion && Escape != null && c == Escape.Value)
                Escaped = true;
            else if (!InRegion && Current.EndsWith (GetStartString ()))
                InRegion = true;
            else if (InRegion && Current.EndsWith (GetEndString ()))
                InRegion = false;

            return InRegion;
        }
    }
}