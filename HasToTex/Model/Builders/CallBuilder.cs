using System.Collections.Generic;

using HasToTex.Model.Abstraction.Haskell.Statements;
using HasToTex.Model.Exceptions;


namespace HasToTex.Model.Builders
{
    public class CallBuilder
    {
        private string CallName { get; set; }

        private List <Statement> CallParameters { get; set; } = new List <Statement> ();

        private string Code { get; set; }

        public static CallBuilder Create () => new CallBuilder ();

        private CallBuilder () {}

        public CallBuilder Name (string name)
        {
            if (!string.IsNullOrEmpty (Code))
                throw new BuilderOrderException ();

            Code     = name;
            CallName = name;

            return this;
        }

        public CallBuilder Parameter (Statement statement)
        {
            if (string.IsNullOrEmpty (Code))
                // The parameters always come after the name
                throw new BuilderOrderException ();

            Code += " " + statement.Code;
            CallParameters.Add (statement);

            return this;
        }

        public Call Build () => CallParameters.Count == 0
            ? new Atomic (Code, CallName)
            : new Call (Code, CallName, CallParameters);
    }
}