namespace HasToTex.Model.Abstraction.Haskell.Keywords
{
    public class Keyword
    {
        /// <summary>
        /// Creates a keyword (the length is calculated automatically)
        /// </summary>
        /// <param name="keywordEnum">The type of keyword</param>
        public Keyword (KeywordEnum keywordEnum)
        {
            KeywordEnum = keywordEnum;
            Length      = KeywordMapping.EnumToKeyword [keywordEnum].Length;
        }

        /// <summary>
        /// Creates a literal (literals don't have a KeywordEnum)
        /// </summary>
        /// <param name="length">The length of the literal</param>
        public Keyword (int length) => Length = length;

        public KeywordEnum? KeywordEnum { get; }
        public int          Length      { get; }

        public bool IsLiteral () => KeywordEnum == null;
    }
}