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

        public static Dictionary <TValue, TKey> Reverse <TKey, TValue> (this IDictionary <TKey, TValue> source)
        {
            var dictionary = new Dictionary <TValue, TKey> ();
            foreach (var (key, value) in source)
                if (!dictionary.ContainsKey (value))
                    dictionary.Add (value, key);

            return dictionary;
        }
    }
}