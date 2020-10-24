using System.Collections.Generic;
using System.Linq;

using HasToTex.Model.Abstraction.Haskell.Keywords;


namespace HasToTex.Parser.Validator
{
    public class DyckValidator : Validator <KeywordCollection>
    {
        public static DyckValidator Default ()
            => new DyckValidator (new HashSet <(KeywordEnum Open, KeywordEnum Close)>
            {
                (KeywordEnum.S_BraceLeft, KeywordEnum.S_BraceRight),
                (KeywordEnum.S_BracketLeft, KeywordEnum.S_BracketRight),
                (KeywordEnum.S_ParanthesisLeft, KeywordEnum.S_ParanthesisRight)
            });

        public DyckValidator (HashSet <(KeywordEnum Open, KeywordEnum Close)> bracketTypes)
            => BracketTypes = bracketTypes;

        private HashSet <(KeywordEnum Open, KeywordEnum Close)> BracketTypes { get; }

        /// <inheritdoc />
        public bool Validate (KeywordCollection input)
        {
            var stack = new Stack <KeywordEnum> ();
            foreach (var keyword in input.GetKeywords ())
            {
                if (keyword?.KeywordEnum == null)
                    continue;

                var keywordEnum = keyword.KeywordEnum.Value;

                var pair = BracketTypes.Select (tuple => ((KeywordEnum Open, KeywordEnum Close)?) tuple)
                                       .FirstOrDefault (p
                                                            => p.Value.Open == keywordEnum ||
                                                               p.Value.Close == keywordEnum);
                if (pair == null)
                    continue;

                if (pair.Value.Open == keywordEnum)
                    // We got an opening bracket
                    stack.Push (keywordEnum);
                else if (stack.Count == 0 || stack.Pop () != pair.Value.Open)
                    // We got a closing bracket that doesn't match the last opening bracket
                    return false;
            }

            // If the stacks count isn't 0, it still contains open brackets
            return stack.Count == 0;
        }
    }
}