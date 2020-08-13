using System.Collections.Generic;
using System.Linq;


namespace HasToTex.Util
{
    public static class Extensions
    {
        public static IEnumerable <char> Range (char start, char end)
            => Enumerable.Range (start, end - start + 1).Select (i => (char) i);

        public static void AddAll <T> (this ISet <T> t, IEnumerable <T> all)
        {
            foreach (var e in all)
                t.Add (e);
        }
    }
}