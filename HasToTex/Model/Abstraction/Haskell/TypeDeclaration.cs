using System.Collections.Generic;
using System.Linq;

using HasToTex.Model.Abstraction.Haskell.Types;
using HasToTex.Model.Exceptions;


namespace HasToTex.Model.Abstraction.Haskell
{
    /// <summary>
    /// A type declaration of the form f :: x -> y or similar
    /// </summary>
    public class TypeDeclaration : Statement
    {
        /// <inheritdoc />
        public TypeDeclaration (
            string code, string typeName, HashSet <TypeRestriction> restrictions, List <Type> parameterTypes, Type returnType) : base (code)
        {
            if (!code.Contains ("::") ||
                !code.Contains (typeName) ||
                restrictions.Any (r => !code.Contains (r.Name)))
                throw new InvalidCodeException (code, Expected);
            TypeName       = typeName;
            Restrictions   = restrictions;
            ParameterTypes = parameterTypes;
            ReturnType     = returnType;
        }

        public string                    TypeName       { get; }
        public HashSet <TypeRestriction> Restrictions   { get; }
        public List <Type>               ParameterTypes { get; }
        public Type                      ReturnType     { get; }

        private static string Expected { get; } = "[f] :: [x] -> [y] -> ... -> [z]";
    }
}