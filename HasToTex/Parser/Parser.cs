namespace HasToTex.Parser
{
    public abstract class Parser <TFrom, TTo>
    {
        public TFrom From { get; }

        protected Parser (TFrom from) => From = from;

        public abstract TTo Parse ();
    }
}