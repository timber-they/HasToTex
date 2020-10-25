using System.Collections.Generic;
using System.Linq;

using HasToTex.Util;

namespace HasToTex.Model.Abstraction.Haskell.Keywords
{
    public static class KeywordMapping
    {
        public static readonly Dictionary <string, KeywordEnum> KeywordToEnum = new Dictionary <string, KeywordEnum>
        {
            {"!", KeywordEnum.S_Exclamation},
            {"'", KeywordEnum.S_SingleQuote},
            {"\"", KeywordEnum.S_DoubleQuote},
            {"-", KeywordEnum.S_Dash},
            {"--", KeywordEnum.S_DoubleDash},
            {"-<", KeywordEnum.S_DashLt},
            {"-<<", KeywordEnum.S_DashDoubleLt},
            {"->", KeywordEnum.S_DashGt},
            {"::", KeywordEnum.S_DoubleColon},
            {";", KeywordEnum.S_Semicolon},
            {"<-", KeywordEnum.S_LtDash},
            {",", KeywordEnum.S_Comma},
            {"=", KeywordEnum.S_Equals},
            {"=>", KeywordEnum.S_EqualsGt},
            {">", KeywordEnum.S_Gt},
            {"?", KeywordEnum.S_Question},
            {"#", KeywordEnum.S_Hash},
            {"*", KeywordEnum.S_Star},
            {"@", KeywordEnum.S_At},
            {"[|", KeywordEnum.S_BracketPipeLeft},
            {"|]", KeywordEnum.S_BracketPipeRight},
            {"\\", KeywordEnum.S_Backslash},
            // We use underscores as variable names
            //{"_", KeywordEnum.S_Underscore},
            {"`", KeywordEnum.S_Apostrophe},
            {"{", KeywordEnum.S_BraceLeft},
            {"}", KeywordEnum.S_BraceRight},
            {"{-", KeywordEnum.S_BraceDashLeft},
            {"-}", KeywordEnum.S_BraceDashRight},
            {"|", KeywordEnum.S_Pipe},
            {"~", KeywordEnum.S_Tilde},
            {"[", KeywordEnum.S_BracketLeft},
            {"]", KeywordEnum.S_BracketRight},
            {"(", KeywordEnum.S_ParanthesisLeft},
            {")", KeywordEnum.S_ParanthesisRight},
            {"<", KeywordEnum.S_Lt},
            {"<=", KeywordEnum.S_LtEquals},
            {">=", KeywordEnum.S_GtEquals},
            {"||", KeywordEnum.S_DoublePipe},
            {"+", KeywordEnum.S_Plus},
            {"/", KeywordEnum.S_Slash},
            {"++", KeywordEnum.S_DoublePlus},
            {"as", KeywordEnum._as},
            {"case", KeywordEnum._case},
            {"of", KeywordEnum._of},
            {"class", KeywordEnum._class},
            {"data", KeywordEnum._data},
            {"family", KeywordEnum._family},
            {"instance", KeywordEnum._instance},
            {"default", KeywordEnum._default},
            {"deriving", KeywordEnum._deriving},
            {"do", KeywordEnum._do},
            {"forall", KeywordEnum._forall},
            {"foreign", KeywordEnum._foreign},
            {"hiding", KeywordEnum._hiding},
            {"if", KeywordEnum._if},
            {"then", KeywordEnum._then},
            {"else", KeywordEnum._else},
            {"import", KeywordEnum._import},
            {"infix", KeywordEnum._infix},
            {"infixl", KeywordEnum._infixl},
            {"infixr", KeywordEnum._infixr},
            {"let", KeywordEnum._let},
            {"in", KeywordEnum._in},
            {"mdo", KeywordEnum._mdo},
            {"module", KeywordEnum._module},
            {"newtype", KeywordEnum._newtype},
            {"proc", KeywordEnum._proc},
            {"qualified", KeywordEnum._qualified},
            {"rec", KeywordEnum._rec},
            {"type", KeywordEnum._type},
            {"where", KeywordEnum._where}
        };

        public static Dictionary <KeywordEnum, string> EnumToKeyword = KeywordToEnum.Reverse ();

        public static IEnumerable <string> TextualKeywords => KeywordToEnum.Where (pair => pair.Value.ToString ().StartsWith ("_"))
                                                                           .Select (pair => pair.Key);

        public static IEnumerable <string> SpecialKeywords => KeywordToEnum.Where (pair => pair.Value.ToString ().StartsWith ("S"))
                                                                           .Select (pair => pair.Key);
    }
}