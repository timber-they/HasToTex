namespace HasToTex.Parser.Validator
{
    public interface IValidator <in T>
    {
        bool Validate (T input);
    }
}