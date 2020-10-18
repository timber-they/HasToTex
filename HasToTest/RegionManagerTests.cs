using System.Collections.Generic;
using HasToTex.Model.Abstraction.Haskell.Keywords;
using HasToTex.Parser.Regions;
using NUnit.Framework;

namespace HasToTest
{
    public class RegionManagerTests
    {
        [Test]
        public void RegisterTests()
        {
            /*
             * 1: {-
             * 95: -}
             * 101: "
             * 137: "
             * 140: --
             * 172 (?): null
             * 177: "
             * 199: "
             */
            var code = "{- A string is something like \"Hi, I am a string in a comment\"\n" +
                       "And a new line \"with a string\" -}\n" +
                       "b = \"{- And I'm a comment in a string -}\"\n" +
                       "-- \"And a string in line comment\"\n" +
                       "a = " +
                       "\"And a normal string--\"";
            var regionManager = new RegionManager (new (KeywordEnum?, KeywordEnum?, char?) []
            {
                // Double quotes
                (KeywordEnum.S_DoubleQuote, KeywordEnum.S_DoubleQuote, '\\'),
                // Single quotes
                (KeywordEnum.S_SingleQuote, KeywordEnum.S_SingleQuote, '\\'),
                // Multi line comment
                (KeywordEnum.S_BraceDashLeft, KeywordEnum.S_BraceDashRight, null),
                // Single line comment
                (KeywordEnum.S_DoubleDash, null, null)
            });

            var keywords = new Dictionary<int, KeywordEnum?>
            {
                {1, KeywordEnum.S_BraceDashLeft},
                {95, KeywordEnum.S_BraceDashRight},
                {101, KeywordEnum.S_DoubleQuote},
                {137, KeywordEnum.S_DoubleQuote},
                {140, KeywordEnum.S_DoubleDash},
                {172, null},
                {177, KeywordEnum.S_DoubleQuote},
                {199, KeywordEnum.S_DoubleQuote}
            };
            var expectedRegion = false;

            for (var i = 0; i < code.Length; i++)
            {
                var resKeyword = regionManager.Register(code[i]);
                var resRegion = regionManager.InRegion();

                KeywordEnum? expectedKeyword = null;
                if (keywords.ContainsKey(i))
                {
                    expectedKeyword = keywords[i];
                    expectedRegion = !expectedRegion;
                }

                Assert.AreEqual(expectedKeyword, resKeyword, $"At position {i}");
                Assert.AreEqual(expectedRegion, resRegion, $"At position {i}");
            }
        }
    }
}