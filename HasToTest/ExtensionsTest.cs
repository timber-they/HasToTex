using System;
using System.Collections.Generic;
using System.Linq;

using HasToTex.Util;

using NUnit.Framework;


namespace HasToTest
{
    public class ExtensionsTest
    {
        [Test]
        public void Range ()
        {
            var start1 = '1';
            var start2 = 'a';
            var start3 = '9';

            var end1 = '9';
            var end2 = 'i';
            var end3 = '1';

            var res1 = Extensions.Range (start1, end1).ToList ();
            var res2 = Extensions.Range (start2, end2).ToList ();
            Assert.Throws <ArgumentOutOfRangeException> (() => Extensions.Range (start3, end3));

            Assert.AreEqual (9, res1.Count);
            Assert.AreEqual (9, res2.Count);

            for (var i = 0; i < 9; i++)
                Assert.AreEqual ('1' + i, res1 [i]);
            for (var i = 0; i < 8; i++)
                Assert.AreEqual ('a' + i, res2 [i]);
        }

        [Test]
        public void AddAll ()
        {
            var base1 = new HashSet <int> ();
            var base2 = new HashSet <int> {1, 2, 3};

            var add1 = new HashSet <int> {1, 2, 4};
            var add2 = new List <int> ();

            base1.AddAll (add1);
            base2.AddAll (add2);

            Assert.AreEqual (3, base1.Count);
            Assert.AreEqual (3, base2.Count);

            Assert.True (base1.Contains (1));
            Assert.True (base1.Contains (2));
            Assert.True (base1.Contains (4));
            Assert.True (base2.Contains (1));
            Assert.True (base2.Contains (2));
            Assert.True (base2.Contains (3));

            base1.AddAll (add2);
            base2.AddAll (add1);

            Assert.AreEqual (3, base1.Count);
            Assert.AreEqual (4, base2.Count);

            Assert.True (base1.Contains (1));
            Assert.True (base1.Contains (2));
            Assert.True (base1.Contains (4));
            Assert.True (base2.Contains (1));
            Assert.True (base2.Contains (2));
            Assert.True (base2.Contains (3));
            Assert.True (base2.Contains (4));
        }
    }
}