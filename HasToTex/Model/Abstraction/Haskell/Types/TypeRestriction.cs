namespace HasToTex.Model.Abstraction.Haskell.Types
{
    public class TypeRestriction
    {
        public TypeRestriction (string name, TypeClass typeClass)
        {
            Name = name;
            TypeClass = typeClass;
        }
        public string Name { get; }
        public TypeClass TypeClass { get; }
    }
}