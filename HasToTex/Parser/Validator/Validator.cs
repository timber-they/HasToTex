namespace HasToTex.Parser.Validator
{
    public interface Validator <in T>
    {
        bool Validate (T input);
    }
}