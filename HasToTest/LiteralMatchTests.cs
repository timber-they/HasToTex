using HasToTex.Parser.Matcher;

using NUnit.Framework;


namespace HasToTest
{
    public class LiteralMatchTests
    {
        [Test]
        public void Matches ()
        {
            var match = new LiteralMatch ();

            var partial1 = "";
            var partial2 = "foo";
            var partial3 = "foo_bar";
            var partial4 = "foo!bar";
            var partial5 = "foo_bar ";
            var partial6 = "foo!";
            var partial7 = "f,o!o*";

            Assert.True (match.Matches (partial1));
            Assert.True (match.Matches (partial2));
            Assert.True (match.Matches (partial3));
            Assert.False (match.Matches (partial4));
            Assert.True (match.Matches (partial5));
            Assert.True (match.Matches (partial6));
            Assert.False (match.Matches (partial7));
        }

        [Test]
        public void Done ()
        {
            var match = new LiteralMatch ();

            var partial1 = "";
            var partial2 = "foo";
            var partial3 = "foo_bar";
            var partial4 = "foo!bar";
            var partial5 = "foo_bar ";
            var partial6 = "foo!";
            var partial7 = "f,o!o*";

            Assert.False (match.Done (partial1));
            Assert.False (match.Done (partial2));
            Assert.False (match.Done (partial3));
            Assert.False (match.Done (partial4));
            Assert.True (match.Done (partial5));
            Assert.True (match.Done (partial6));
            Assert.False (match.Done (partial7));
        }
    }
}