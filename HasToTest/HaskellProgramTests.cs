using HasToTex.Model;

using NUnit.Framework;


namespace HasToTest
{
    public class HaskellProgramTests
    {
        [Test]
        public void GetHaskellCommentRanges ()
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
                        "a = \"And a normal string\"";

            var without1 = "square :: Int -> Int\n" +
                           "\n" +
                           "square x = x * x";
            var without2 = "\n" +
                           "\n" +
                           "square :: Int -> Int\n" +
                           "square x = x * x";
            var without3 = "\n" +
                           "square :: Int -> Int\n" +
                           "square x = x * x";
            var without4 = "\n" +
                           "b = \"{- And I'm a comment in a string -}\"\n" +
                           "\n" +
                           "a = \"And a normal string\"";

            var program1 = new HaskellProgram (code1);
            var program2 = new HaskellProgram (code2);
            var program3 = new HaskellProgram (code3);
            var program4 = new HaskellProgram (code4);

            var res1 = program1.WithoutComments ();
            var res2 = program2.WithoutComments ();
            var res3 = program3.WithoutComments ();
            var res4 = program4.WithoutComments ();

            Assert.AreEqual (without1, res1.Content);
            Assert.AreEqual (without2, res2.Content);
            Assert.AreEqual (without3, res3.Content);
            Assert.AreEqual (without4, res4.Content);
        }
    }
}