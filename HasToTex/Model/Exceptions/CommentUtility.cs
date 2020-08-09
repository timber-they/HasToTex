using System.Collections.Generic;
using System.Linq;


namespace HasToTex.Model.Exceptions
{
    public static class CommentUtility
    {
        /// <summary>
        /// Inclusive
        /// </summary>
        public static List <(int, int)> GetHaskellCommentRanges (string trimmed)
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

            for (var i = 0; i < trimmed.Length; i++)
            {
                var c = trimmed [i];
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
                    case '|':
                        if (!inString && !inLineComment && !inBlockComment && hadMinus && hadCurlyBrace)
                        {
                            inBlockComment      = true;
                            hadMinus            = false;
                            hadCurlyBrace       = false;
                            currentCommentStart = i - 2;
                        }

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

        public static string RemoveComments (string trimmed)
        {
            var commentRanges = CommentUtility.GetHaskellCommentRanges (trimmed);
            trimmed = commentRanges.Aggregate (
                trimmed,
                (current, commentRange) =>
                    current.Remove (commentRange.Item1, commentRange.Item2 - commentRange.Item1 + 1));
            return trimmed;
        }
    }
}