namespace HasToTex.Model.Abstraction.Haskell.Keywords
{
    public enum KeywordEnum
    {
        S_Exclamation,
        S_SingleQuote,
        S_DoubleQuote,
        S_Dash,
        S_DoubleDash,
        S_DashLt,
        S_DashDoubleLt,
        S_DashGt,
        S_DoubleColon,
        S_Semicolon,
        S_LtDash,
        S_Comma,
        S_Equals,
        S_EqualsGt,
        S_Gt,
        S_Question,
        S_Hash,
        S_Star,
        S_At,
        S_BracketPipeLeft,
        S_BracketPipeRight,
        S_Backslash,
        S_Underscore,
        S_Apostrophe,
        S_BraceLeft,
        S_BraceRight,
        S_BraceDashLeft,
        S_BraceDashRight,
        S_Pipe,
        S_Tilde,
        // The following specials aren't official Haskell keywords
        S_BracketLeft,
        S_BracketRight,
        S_ParanthesisLeft,
        S_ParanthesisRight,
        // TODO: Maybe comparison operators shouldn't be interpreted as keywords, as they're really just functions
        S_Lt,
        S_LtEquals,
        S_GtEquals,
        // Literals made of keywords suck, so we interpret the following as keywords
        S_DoublePipe,
        S_Plus,
        S_Slash,
        S_DoublePlus,
        _as,
        _case,
        _of,
        _class,
        _data,
        _family,
        _instance,
        _default,
        _deriving,
        _do,
        _forall,
        _foreign,
        _hiding,
        _if,
        _then,
        _else,
        _import,
        _infix,
        _infixl,
        _infixr,
        _let,
        _in,
        _mdo,
        _module,
        _newtype,
        _proc,
        _qualified,
        _rec,
        _type,
        _where
    }
}