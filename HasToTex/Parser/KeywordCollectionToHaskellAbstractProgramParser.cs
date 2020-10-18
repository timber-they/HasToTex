using System;
using System.Collections.Generic;
using System.Linq;

using HasToTex.Model.Abstraction.Haskell;
using HasToTex.Model.Abstraction.Haskell.Keywords;
using HasToTex.Model.Abstraction.Haskell.Statements;
using HasToTex.Model.Builders;


namespace HasToTex.Parser
{
    public class KeywordCollectionToHaskellAbstractProgramParser : Parser <KeywordCollection, HaskellAbstractProgram>
    {
        /// <inheritdoc />
        public KeywordCollectionToHaskellAbstractProgramParser (KeywordCollection from) : base (from) {}

        /// <inheritdoc />
        public override HaskellAbstractProgram Parse ()
        {
            var         res         = new HaskellAbstractProgram ();
            CallBuilder callBuilder = null;

            List <int> indices = From.OrderedIndices ().ToList ();

            for (var i = 0; i < indices.Count; i++)
            {
                var index   = indices [i];
                var keyword = From.Get (index);

                if (keyword.IsLiteral ())
                {
                    var code = From.Substring (index, keyword.Length);
                    if (callBuilder == null)
                        callBuilder = CallBuilder.Create ().Name (code);
                    else
                        callBuilder.Parameter (new Atomic (code, code));

                    continue;
                }

                switch (keyword.KeywordEnum)
                {
                    // TODO
                    case KeywordEnum.S_Exclamation:
                        break;
                    case KeywordEnum.S_SingleQuote:
                        break;
                    case KeywordEnum.S_DoubleQuote:
                        break;
                    case KeywordEnum.S_Dash:
                        break;
                    case KeywordEnum.S_DoubleDash:
                        break;
                    case KeywordEnum.S_DashLt:
                        break;
                    case KeywordEnum.S_DashDoubleLt:
                        break;
                    case KeywordEnum.S_DashGt:
                        break;
                    case KeywordEnum.S_DoubleColon:
                        break;
                    case KeywordEnum.S_Semicolon:
                        break;
                    case KeywordEnum.S_LtDash:
                        break;
                    case KeywordEnum.S_Comma:
                        break;
                    case KeywordEnum.S_Equals:
                        break;
                    case KeywordEnum.S_EqualsGt:
                        break;
                    case KeywordEnum.S_Gt:
                        break;
                    case KeywordEnum.S_Question:
                        break;
                    case KeywordEnum.S_Hash:
                        break;
                    case KeywordEnum.S_Star:
                        break;
                    case KeywordEnum.S_At:
                        break;
                    case KeywordEnum.S_BracketPipeLeft:
                        break;
                    case KeywordEnum.S_BracketPipeRight:
                        break;
                    case KeywordEnum.S_Backslash:
                        break;
                    case KeywordEnum.S_Underscore:
                        break;
                    case KeywordEnum.S_Apostrophe:
                        break;
                    case KeywordEnum.S_BraceLeft:
                        break;
                    case KeywordEnum.S_BraceRight:
                        break;
                    case KeywordEnum.S_BraceDashLeft:
                        break;
                    case KeywordEnum.S_BraceDashRight:
                        break;
                    case KeywordEnum.S_Pipe:
                        break;
                    case KeywordEnum.S_Tilde:
                        break;
                    case KeywordEnum._as:
                        break;
                    case KeywordEnum._case:
                        break;
                    case KeywordEnum._of:
                        break;
                    case KeywordEnum._class:
                        break;
                    case KeywordEnum._data:
                        break;
                    case KeywordEnum._family:
                        break;
                    case KeywordEnum._instance:
                        break;
                    case KeywordEnum._default:
                        break;
                    case KeywordEnum._deriving:
                        break;
                    case KeywordEnum._do:
                        break;
                    case KeywordEnum._forall:
                        break;
                    case KeywordEnum._foreign:
                        break;
                    case KeywordEnum._hiding:
                        break;
                    case KeywordEnum._if:
                        break;
                    case KeywordEnum._then:
                        break;
                    case KeywordEnum._else:
                        break;
                    case KeywordEnum._import:
                        break;
                    case KeywordEnum._infix:
                        break;
                    case KeywordEnum._infixl:
                        break;
                    case KeywordEnum._infixr:
                        break;
                    case KeywordEnum._let:
                        break;
                    case KeywordEnum._in:
                        break;
                    case KeywordEnum._mdo:
                        break;
                    case KeywordEnum._module:
                        break;
                    case KeywordEnum._newtype:
                        break;
                    case KeywordEnum._proc:
                        break;
                    case KeywordEnum._qualified:
                        break;
                    case KeywordEnum._rec:
                        break;
                    case KeywordEnum._type:
                        break;
                    case KeywordEnum._where:
                        break;
                    case null:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException ();
                }
            }

            return res;
        }
    }
}