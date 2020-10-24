using System.Collections.Generic;

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
    }
}