using System;


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

        /// <inheritdoc />
        public override string ToString () => KeywordEnum == null ? $"<{Length}>" : KeywordMapping.EnumToKeyword [KeywordEnum.Value];

        protected bool Equals (Keyword other) => KeywordEnum == other.KeywordEnum && Length == other.Length;

        /// <inheritdoc />
        public override bool Equals (object obj)
        {
            if (ReferenceEquals (null, obj))
                return false;
            if (ReferenceEquals (this, obj))
                return true;
            if (obj.GetType () != this.GetType ())
                return false;
            return Equals ((Keyword) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode () => HashCode.Combine (KeywordEnum, Length);
    }
}