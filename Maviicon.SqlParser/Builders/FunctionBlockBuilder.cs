using System.Collections.Generic;
using System.Text;
using Maviicon.SqlParser.Builder;

namespace Maviicon.SqlParser.Builders
{
    public static class FunctionBlockBuilder
    {
        private static List<string> _functions = new List<string>
        {
            "sum", "count", "avg" // TODO
        };
        public static bool Match(string token, List<string> tokens, int i)
        {
            bool followedByParen = (tokens.Count > (i + 1) && tokens[i + 1] == "(");
            return _functions.Contains(token) && followedByParen;
        }

        public static string ConsumeFunctionBlock(List<string> tokens, ref int i)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(tokens[i++] + " ");
            sb.Append(ParenBlockBuilder.ConsumeParenBlock(tokens, ref i));
            return sb.ToString();
        }
    }
}