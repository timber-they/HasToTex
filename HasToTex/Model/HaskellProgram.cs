using System.Collections.Generic;
using System.Linq;


namespace HasToTex.Model
{
    public class HaskellProgram
    {
        public HaskellProgram (string content) => Content = content;
        public string Content { get; }

        public int Length => Content?.Length ?? 0;
        public char Get (int i) => Content [i];

        /// <summary>
        /// Trims every line. Note that this might alter multiline comments
        /// </summary>
        /// <returns></returns>
        public HaskellProgram Trim ()
        {
            var split = Content.Split ('\n');
            var trimmed = split.Select (s => s.Trim (' ', '\t'));
            var combined = string.Join ("\n", trimmed);
            return new HaskellProgram (combined);
        }
    }
}