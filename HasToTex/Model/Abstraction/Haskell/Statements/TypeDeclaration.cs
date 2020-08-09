using System.Collections.Generic;
using System.Linq;

using HasToTex.Model.Abstraction.Haskell.Types;
using HasToTex.Model.Exceptions;


namespace HasToTex.Model.Abstraction.Haskell.Statements
{
    /// <summary>
    /// A type declaration of the form f :: x -> y or similar
    /// </summary>
    public class TypeDeclaration : Statement
    {
        /// <inheritdoc />
        public TypeDeclaration (
            string                    code,
            string                    typeName,
            HashSet <TypeRestriction> restrictions,
            List <GenericType>        parameterTypes,
            GenericType               returnType) : base (code)
        {
            if (!code.Contains ("::") ||
                !code.Contains (typeName) ||
                parameterTypes.Any (t => !code.Contains (t.Name)) ||
                !code.Contains (returnType.Name) ||
                restrictions.Any (r => !code.Contains (r.Name) ||
                                       !code.Contains (r.TypeClass.ToString ())))
                throw new InvalidCodeException (code, Expected);
            TypeName       = typeName;
            Restrictions   = restrictions;
            ParameterTypes = parameterTypes;
            ReturnType     = returnType;
        }

        public string                    TypeName       { get; }
        public HashSet <TypeRestriction> Restrictions   { get; }
        public List <GenericType>        ParameterTypes { get; }
        public GenericType               ReturnType     { get; }

        private static string Expected { get; } = "[f] :: ([Eq] [a]) => [x] -> [y] -> ... -> [z]";


        public class GenericType
        {
            public GenericType (string name, Type? type)
            {
                if (type != null && type.ToString () != name)
                    throw new InvalidCodeException ("name", type.ToString ());
                Name = name;
                Type = type;
            }

            public GenericType (string name) : this (name, null) {}
            public GenericType (Type type) : this (type.ToString (), type) {}

            public string Name { get; }

            /// <summary>
            /// Null if generic (-> if the name is lower case)
            /// </summary>
            public Type? Type { get; }
        }
    }
}