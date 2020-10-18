namespace HasToTex.Model.Exceptions
{
    public class BuilderOrderException : BuilderException
    {
        /// <inheritdoc />
        public BuilderOrderException () : base ("Wrong order") {}
    }
}