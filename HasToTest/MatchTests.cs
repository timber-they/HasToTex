using System.Collections.Generic;

using HasToTex.Parser.Matcher;

using NUnit.Framework;

namespace HasToTest
{
    /// <summary>
    /// Essentially tests <see cref="TextualMatch"/> and <see cref="SpecialMatch"/> too
    /// </summary>
    public class MatchTests
    {
        [Test]
        public void Matches ()
        {
            var separators = new List <char>
            {
                '1', '2'
            };

            var goal1 = "";
            var goal2 = "3";
            var goal3 = "1";
            var goal4 = "5243";

            var match1 = new TestMatch (goal1, separators);
            var match2 = new TestMatch (goal2, separators);
            var match3 = new TestMatch (goal3, separators);
            var match4 = new TestMatch (goal4, separators);

            var partial1c = "";
            var partial1s = "1";
            var partial1n = "3";
            var partial2  = "";
            var partial2c = "3";
            var partial2s = "3 ";
            var partial2n = "4";
            var partial3  = "";
            var partial3c = "1";
            var partial3s = "11";
            var partial3n = "211";
            var partial4  = "52";
            var partial4c = "5243";
            var partial4s = "52432";
            var partial4n = "52422";

            Assert.True (match1.Matches (partial1c));
            Assert.True (match1.Matches (partial1s));
            Assert.False (match1.Matches (partial1n));
            Assert.True (match2.Matches (partial2));
            Assert.True (match2.Matches (partial2c));
            Assert.True (match2.Matches (partial2s));
            Assert.False (match2.Matches (partial2n));
            Assert.True (match3.Matches (partial3));
            Assert.True (match3.Matches (partial3c));
            Assert.True (match3.Matches (partial3s));
            Assert.False (match3.Matches (partial3n));
            Assert.True (match4.Matches (partial4));
            Assert.True (match4.Matches (partial4c));
            Assert.True (match4.Matches (partial4s));
            Assert.False (match4.Matches (partial4n));
        }

        [Test]
        public void Separated ()
        {
            var separators = new List <char>
            {
                '1', '2'
            };

            var goal1 = "";
            var goal2 = "3";
            var goal3 = "1";
            var goal4 = "5243";

            var match1 = new TestMatch (goal1, separators);
            var match2 = new TestMatch (goal2, separators);
            var match3 = new TestMatch (goal3, separators);
            var match4 = new TestMatch (goal4, separators);

            var partial1c = "";
            var partial1s = "1";
            var partial1n = "3";
            var partial2  = "";
            var partial2c = "3";
            var partial2s = "3 ";
            var partial2n = "4";
            var partial3  = "";
            var partial3c = "1";
            var partial3s = "11";
            var partial3n = "211";
            var partial4  = "52";
            var partial4c = "5243";
            var partial4s = "52432";
            var partial4n = "52422";

            Assert.False (match1.TestSeparated (partial1c));
            Assert.True (match1.TestSeparated (partial1s));
            Assert.False (match1.TestSeparated (partial1n));
            Assert.False (match2.TestSeparated (partial2));
            Assert.False (match2.TestSeparated (partial2c));
            Assert.True (match2.TestSeparated (partial2s));
            Assert.False (match2.TestSeparated (partial2n));
            Assert.False (match3.TestSeparated (partial3));
            Assert.True (match3.TestSeparated (partial3c));
            Assert.True (match3.TestSeparated (partial3s));
            Assert.True (match3.TestSeparated (partial3n));
            Assert.True (match4.TestSeparated (partial4));
            Assert.False (match4.TestSeparated (partial4c));
            Assert.True (match4.TestSeparated (partial4s));
            Assert.True (match4.TestSeparated (partial4n));
        }

        [Test]
        public void Done ()
        {
            var separators = new List <char>
            {
                '1', '2'
            };

            var goal1 = "";
            var goal2 = "3";
            var goal3 = "1";
            var goal4 = "5243";

            var match1 = new TestMatch (goal1, separators);
            var match2 = new TestMatch (goal2, separators);
            var match3 = new TestMatch (goal3, separators);
            var match4 = new TestMatch (goal4, separators);

            var partial1c = "";
            var partial1s = "1";
            var partial1n = "3";
            var partial2  = "";
            var partial2c = "3";
            var partial2s = "3 ";
            var partial2n = "4";
            var partial3  = "";
            var partial3c = "1";
            var partial3s = "11";
            var partial3n = "211";
            var partial4  = "52";
            var partial4c = "5243";
            var partial4s = "52432";
            var partial4n = "52422";

            Assert.False (match1.Done (partial1c));
            Assert.True (match1.Done (partial1s));
            Assert.False (match1.Done (partial1n));
            Assert.False (match2.Done (partial2));
            Assert.False (match2.Done (partial2c));
            Assert.True (match2.Done (partial2s));
            Assert.False (match2.Done (partial2n));
            Assert.False (match3.Done (partial3));
            Assert.False (match3.Done (partial3c));
            Assert.True (match3.Done (partial3s));
            Assert.False (match3.Done (partial3n));
            Assert.False (match4.Done (partial4));
            Assert.False (match4.Done (partial4c));
            Assert.True (match4.Done (partial4s));
            Assert.False (match4.Done (partial4n));
        }


        private class TestMatch : Match
        {
            /// <inheritdoc />
            public TestMatch (string goal, IEnumerable <char> separators) : base (goal, separators) {}

            public bool TestSeparated (string current) => Separated (current);
        }
    }
}