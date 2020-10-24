using HasToTex.Model;
using HasToTex.Parser;
using HasToTex.Parser.Validator;

using NUnit.Framework;


namespace HasToTest
{
    public class ValidatorTests
    {
        [Test]
        public void DyckValidatorPositive ()
        {
            var trivial = "";
            var simple  = "()";
            var complex = "({()([]){}}[(){}])";

            var trivialKc = GetKeywordCollection (trivial);
            var simpleKc  = GetKeywordCollection (simple);
            var complexKc = GetKeywordCollection (complex);

            // Sanity check
            Assert.AreEqual (0, trivialKc.Count);
            Assert.AreEqual (2, simpleKc.Count);
            Assert.AreEqual (18, complexKc.Count);

            var validator = DyckValidator.Default ();

            var trivialRes = validator.Validate (trivialKc);
            var simpleRes  = validator.Validate (simpleKc);
            var complexRes = validator.Validate (complexKc);

            Assert.True (trivialRes);
            Assert.True (simpleRes);
            Assert.True (complexRes);
        }

        [Test]
        public void DyckValidatorNegative ()
        {
            var simple1     = "(";
            var simple2     = ")";
            var wrongCount1 = "([]))";
            var wrongCount2 = "([[])";
            var wrongOrder1 = "([)]";
            var wrongOrder2 = "({[)]}";

            var simple1Kc     = GetKeywordCollection (simple1);
            var simple2Kc     = GetKeywordCollection (simple2);
            var wrongCount1Kc = GetKeywordCollection (wrongCount1);
            var wrongCount2Kc = GetKeywordCollection (wrongCount2);
            var wrongOrder1Kc = GetKeywordCollection (wrongOrder1);
            var wrongOrder2Kc = GetKeywordCollection (wrongOrder2);

            // Sanity check
            Assert.AreEqual (simple1.Length, simple1Kc.Count);
            Assert.AreEqual (simple2.Length, simple2Kc.Count);
            Assert.AreEqual (wrongCount1.Length, wrongCount1Kc.Count);
            Assert.AreEqual (wrongCount2.Length, wrongCount2Kc.Count);
            Assert.AreEqual (wrongOrder1.Length, wrongOrder1Kc.Count);
            Assert.AreEqual (wrongOrder2.Length, wrongOrder2Kc.Count);

            var validator = DyckValidator.Default ();

            var simple1Res     = validator.Validate (simple1Kc);
            var simple2Res     = validator.Validate (simple2Kc);
            var wrongCount1Res = validator.Validate (wrongCount1Kc);
            var wrongCount2Res = validator.Validate (wrongCount2Kc);
            var wrongOrder1Res = validator.Validate (wrongOrder1Kc);
            var wrongOrder2Res = validator.Validate (wrongOrder2Kc);

            Assert.False (simple1Res);
            Assert.False (simple2Res);
            Assert.False (wrongCount1Res);
            Assert.False (wrongCount2Res);
            Assert.False (wrongOrder1Res);
            Assert.False (wrongOrder2Res);
        }

        private static KeywordCollection GetKeywordCollection (string s)
            => new HaskellToKeywordCollectionParser (new HaskellProgram (s)).Parse ();
    }
}