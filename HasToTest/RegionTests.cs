using System.Collections.Generic;
using System.Linq;

using HasToTex.Model.Abstraction.Haskell.Keywords;
using HasToTex.Parser.Regions;

using NUnit.Framework;


namespace HasToTest
{
    public class RegionTests
    {
        [Test]
        public void StringTest ()
        {
            // 6 - 13
            var code1 = "foo = \"\\\"bar\\\"\"";
            // Note that comments don't escape for a region alone
            // 30 - 60, 78 - 91, 101 - 136, 142 - 170, 177 - 198
            var code2 = "{- A string is something like \"Hi, I am a string in a comment\"\n" +
                        "And a new line \"with a string\" -}\n" +
                        "b = \"{- And I'm a comment in a string -}\"\n" +
                        "-- \"And a string in line comment\"\n" +
                        "a = \"And a normal string--\"";

            var region1 = new Region (KeywordEnum.S_DoubleQuote, KeywordEnum.S_DoubleQuote, '\\');
            var region2 = new Region (KeywordEnum.S_DoubleQuote, KeywordEnum.S_DoubleQuote, '\\');

            // Contains all indices, in which InRegion is true
            var res1 = new List <int> ();
            var res2 = new List <int> ();

            for (var i1 = 0; i1 < code1.Length; i1++)
                if (region1.Register (code1 [i1]))
                    res1.Add (i1);
            for (var i2 = 0; i2 < code2.Length; i2++)
                if (region2.Register (code2 [i2]))
                    res2.Add (i2);

            Assert.AreEqual (8, res1.Count);
            Assert.True (Enumerable.Range (6, 8).All (i => res1.Contains (i)),
                         "res1 was supposed to contain all numbers from 6 to 13");

            Assert.AreEqual (31 + 14 + 36 + 29 + 22, res2.Count);
            Assert.True (Enumerable.Range (30, 31)
                                   .Concat (Enumerable.Range (78, 14))
                                   .Concat (Enumerable.Range (101, 36))
                                   .Concat (Enumerable.Range (142, 29))
                                   .Concat (Enumerable.Range (177, 22))
                                   .All (i => res2.Contains (i)),
                         "res2 was supposed to contain quite a few ranges");
        }
    }
}