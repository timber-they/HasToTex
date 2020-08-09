using System.Collections.Generic;
using System.Linq;


namespace HasToTex.Model
{
    public class HaskellProgram
    {
        public HaskellProgram (string content) => Content = content;
        public string Content { get; }

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

        /// <summary>
        /// Evaluates the same program, but with all comments removed
        /// </summary>
        public HaskellProgram WithoutComments ()
        {
            var commentRanges = GetCommentRanges ();
            var n = Content;
            var removed = 0;
            foreach (var (start, end) in commentRanges)
            {
                var count = end - start + 1;
                n = n.Remove (start - removed, count);
                removed += count;
            }
            return new HaskellProgram (n);
        }

        /// <summary>
        /// Inclusive
        /// </summary>
        private List <(int, int)> GetCommentRanges ()
        {
            // You can't comment whilst being in a string
            var inString = false;
            // You can't start a string whilst being in a comment
            var inLineComment  = false;
            var inBlockComment = false;
            // For detection of -- and {-|
            var hadMinus = false;
            // For detection of {-|
            var hadCurlyBrace = false;

            var commentRanges       = new List <(int, int)> ();
            var currentCommentStart = -1;

            for (var i = 0; i < Content.Length; i++)
            {
                var c = Content [i];
                switch (c)
                {
                    case '"':
                        if (!inLineComment && !inBlockComment)
                            inString = !inString;
                        hadMinus      = false;
                        hadCurlyBrace = false;
                        break;
                    case '-':
                        if (!inLineComment && !inString)
                        {
                            if (hadMinus)
                            {
                                inLineComment       = true;
                                currentCommentStart = i - 1;
                            }
                            else if (!inBlockComment && hadCurlyBrace)
                            {
                                inBlockComment      = true;
                                hadCurlyBrace       = false;
                                currentCommentStart = i - 1;
                            }

                            hadMinus = !hadMinus;
                        }

                        break;
                    case '{':
                        if (!inLineComment && !inBlockComment && !inString)
                            hadCurlyBrace = true;
                        hadMinus = false;
                        break;
                    case '}':
                        if (!inString && !inLineComment && hadMinus && inBlockComment)
                        {
                            inBlockComment = false;
                            commentRanges.Add ((currentCommentStart, i));
                        }

                        hadMinus = false;
                        break;
                    case '\n':
                        if (inLineComment)
                        {
                            inLineComment = false;
                            commentRanges.Add ((currentCommentStart, i - 1));
                        }

                        break;
                    default:
                        hadMinus      = false;
                        hadCurlyBrace = false;
                        break;
                }
            }

            return commentRanges;
        }
    }
}