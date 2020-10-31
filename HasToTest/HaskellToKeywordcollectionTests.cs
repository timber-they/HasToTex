using System.Collections.Generic;
using System.Linq;

using HasToTex.Model;
using HasToTex.Model.Abstraction.Haskell.Keywords;
using HasToTex.Parser;

using NUnit.Framework;


namespace HasToTest
{
    public class HaskellToKeywordcollectionTests
    {
        [Test]
        public void TestSamplePrograms ()
        {
            var simpleString = new HaskellProgram ("\"Foo\"");
            var helloWorld   = new HaskellProgram ("main = putStrLn \"hello world\"");
            var values = new HaskellProgram (
                "main = do\n    putStrLn $ \"haskell \" ++ \"lang\"\n    putStrLn $ \"1+1 = \" ++ show (1+1)\n    putStrLn $ \"7.0/3.0 = \" ++ show (7.0/3.0)\n\n    print $ True && False\n    print $ True || False\n    print $ not True");

            var simpleStringRes = new HaskellToKeywordCollectionParser (simpleString).Parse ();
            var helloWorldRes   = new HaskellToKeywordCollectionParser (helloWorld).Parse ();
            var valuesRes       = new HaskellToKeywordCollectionParser (values).Parse ();

            var simpleStringExpected = new KeywordCollection (new Dictionary <int, Keyword>
                                                              {
                                                                  {0, new Keyword (KeywordEnum.S_DoubleQuote)},
                                                                  {4, new Keyword (KeywordEnum.S_DoubleQuote)}
                                                              },
                                                              simpleString);
            var helloWorldExpected = new KeywordCollection (new Dictionary <int, Keyword>
                                                            {
                                                                {0, new Keyword (4)},
                                                                {5, new Keyword (KeywordEnum.S_Equals)},
                                                                {7, new Keyword (8)},
                                                                {16, new Keyword (KeywordEnum.S_DoubleQuote)},
                                                                {28, new Keyword (KeywordEnum.S_DoubleQuote)}
                                                            },
                                                            helloWorld);
            var valuesExpected = new KeywordCollection (new Dictionary <int, Keyword>
                                                        {
                                                            {0, new Keyword (4)},
                                                            {5, new Keyword (KeywordEnum.S_Equals)},
                                                            {7, new Keyword (KeywordEnum._do)},
                                                            {10, new Keyword (8)},
                                                            {19, new Keyword (1)},
                                                            {21, new Keyword (KeywordEnum.S_DoubleQuote)},
                                                            {30, new Keyword (KeywordEnum.S_DoubleQuote)},
                                                            {32, new Keyword (KeywordEnum.S_DoublePlus)},
                                                            {35, new Keyword (KeywordEnum.S_DoubleQuote)},
                                                            {40, new Keyword (KeywordEnum.S_DoubleQuote)},
                                                            {42, new Keyword (8)},
                                                            {51, new Keyword (1)},
                                                            {53, new Keyword (KeywordEnum.S_DoubleQuote)},
                                                            {60, new Keyword (KeywordEnum.S_DoubleQuote)},
                                                            {62, new Keyword (KeywordEnum.S_DoublePlus)},
                                                            {65, new Keyword (4)},
                                                            {70, new Keyword (KeywordEnum.S_ParanthesisLeft)},
                                                            {71, new Keyword (1)},
                                                            {72, new Keyword (KeywordEnum.S_Plus)},
                                                            {73, new Keyword (1)},
                                                            {74, new Keyword (KeywordEnum.S_ParanthesisRight)},
                                                            {76, new Keyword (8)},
                                                            {85, new Keyword (1)},
                                                            {87, new Keyword (KeywordEnum.S_DoubleQuote)},
                                                            {98, new Keyword (KeywordEnum.S_DoubleQuote)},
                                                            {100, new Keyword (KeywordEnum.S_DoublePlus)},
                                                            {103, new Keyword (4)},
                                                            {108, new Keyword (KeywordEnum.S_ParanthesisLeft)},
                                                            {109, new Keyword (3)},
                                                            {112, new Keyword (KeywordEnum.S_Slash)},
                                                            {113, new Keyword (3)},
                                                            {116, new Keyword (KeywordEnum.S_ParanthesisRight)},
                                                            {119, new Keyword (5)},
                                                            {125, new Keyword (1)},
                                                            {127, new Keyword (4)},
                                                            {132, new Keyword (2)},
                                                            {135, new Keyword (5)},
                                                            {141, new Keyword (5)},
                                                            {147, new Keyword (1)},
                                                            {149, new Keyword (4)},
                                                            {154, new Keyword (KeywordEnum.S_DoublePipe)},
                                                            {157, new Keyword (5)},
                                                            {163, new Keyword (5)},
                                                            {169, new Keyword (1)},
                                                            {171, new Keyword (3)},
                                                            {175, new Keyword (4)}
                                                        },
                                                        values);

            Assert.AreEqual (simpleStringExpected, simpleStringRes);
            Assert.AreEqual (helloWorldExpected, helloWorldRes);
            Assert.AreEqual (valuesExpected, valuesRes);
        }

        [Test]
        public void TestCommentIgnorance ()
        {
            var code1 = "square :: Int -> Int\n" +
                        "-- ^The 'square' function squares an integer.\n" +
                        "square x = x * x";
            var code2 = "-- |The 'square' function squares an integer.\n" +
                        "-- It takes one argument, of type 'Int'.\n" +
                        "square :: Int -> Int\n" +
                        "square x = x * x";
            var code3 = "{-|\n" +
                        "  The 'square' function squares an integer.\n" +
                        "  It takes one argument, of type 'Int'.\n" +
                        "-}\n" +
                        "square :: Int -> Int\n" +
                        "square x = x * x";
            var code4 = "{- A string is something like \"Hi, I am a string in a comment\"\n" +
                        "And a new line \"with a string\" -}\n" +
                        "b = \"{- And I'm a comment in a string -}\"\n" +
                        "-- \"And a string in line comment\"\n" +
                        "a = \"And a normal string--\"";

            var without1 = "square :: Int -> Int\n" +
                           " \n" +
                           "square x = x * x";
            var without2 = " \n" +
                           " \n" +
                           "square :: Int -> Int\n" +
                           "square x = x * x";
            var without3 = " \n" +
                           "square :: Int -> Int\n" +
                           "square x = x * x";
            var without4 = " \n" +
                           "b = \"{- And I'm a comment in a string -}\"\n" +
                           " \n" +
                           "a = \"And a normal string--\"";

            var program1        = new HaskellProgram (code1);
            var program2        = new HaskellProgram (code2);
            var program3        = new HaskellProgram (code3);
            var program4        = new HaskellProgram (code4);
            var programWithout1 = new HaskellProgram (without1);
            var programWithout2 = new HaskellProgram (without2);
            var programWithout3 = new HaskellProgram (without3);
            var programWithout4 = new HaskellProgram (without4);

            var res1        = new HaskellToKeywordCollectionParser (program1).Parse ();
            var res2        = new HaskellToKeywordCollectionParser (program2).Parse ();
            var res3        = new HaskellToKeywordCollectionParser (program3).Parse ();
            var res4        = new HaskellToKeywordCollectionParser (program4).Parse ();
            var resWithout1 = new HaskellToKeywordCollectionParser (programWithout1).Parse ();
            var resWithout2 = new HaskellToKeywordCollectionParser (programWithout2).Parse ();
            var resWithout3 = new HaskellToKeywordCollectionParser (programWithout3).Parse ();
            var resWithout4 = new HaskellToKeywordCollectionParser (programWithout4).Parse ();

            var commentKeywords = new []
            {
                (KeywordEnum?) KeywordEnum.S_DoubleDash,
                KeywordEnum.S_BraceDashLeft,
                KeywordEnum.S_BraceDashRight
            };

            var keywords1 = res1.GetKeywords ().Where (keyword => keyword != null && !commentKeywords.Contains (keyword.KeywordEnum)).ToList ();
            var keywords2 = res2.GetKeywords ().Where (keyword => keyword != null && !commentKeywords.Contains (keyword.KeywordEnum)).ToList ();
            var keywords3 = res3.GetKeywords ().Where (keyword => keyword != null && !commentKeywords.Contains (keyword.KeywordEnum)).ToList ();
            var keywords4 = res4.GetKeywords ().Where (keyword => keyword != null && !commentKeywords.Contains (keyword.KeywordEnum)).ToList ();
            var keywordsWithout1 = resWithout1.GetKeywords ().ToList ();
            var keywordsWithout2 = resWithout2.GetKeywords ().ToList ();
            var keywordsWithout3 = resWithout3.GetKeywords ().ToList ();
            var keywordsWithout4 = resWithout4.GetKeywords ().ToList ();

            Assert.AreEqual (keywordsWithout1, keywords1);
            Assert.AreEqual (keywordsWithout2, keywords2);
            Assert.AreEqual (keywordsWithout3, keywords3);
            Assert.AreEqual (keywordsWithout4, keywords4);
        }
    }
}