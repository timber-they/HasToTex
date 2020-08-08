using System.Collections.Generic;
using System.Linq;

using HasToTex.Model.Exceptions;


namespace HasToTex.Model.Abstraction.Haskell
{
    /// <summary>
    /// A function declaration, like f x y = x + y
    /// Note that the function is not treat like returning a function (i.e. not how it actually works, like
    ///     f :: a -> b -> c, but rather like f :: a, b -> c
    /// </summary>
    public class Function : Statement
    {
        /// <inheritdoc />
        public Function (string code, string name, List <string> parameters, Statement body) : base (code)
        {
            if (!code.Contains ("=") || !code.Contains (name) || parameters.Any (p => !code.Contains (p)))
                throw new InvalidCodeException (code, Expected);
            Name       = name;
            Parameters = parameters;
            Body       = body;
        }

        public string        Name       { get; }
        public List <string> Parameters { get; }
        public Statement     Body       { get; }

        private static string Expected { get; } = "[f] [x1] [x_2] ... = <Statement>";
    }
}