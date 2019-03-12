using System.Collections.Generic;
using System.Text;

namespace Maviicon.SqlParser.Builder
{
    public static class ParenBlockBuilder
    {
        public static string ConsumeParenBlock(List<string> tokens, ref int i)
        {
            StringBuilder sb = new StringBuilder(tokens[i++]);
            int nestedCount = 1;
            while (nestedCount > 0 && i < tokens.Count)
            {
                if (tokens[i] == "(")
                {
                    nestedCount++;
                } else if (tokens[i] == ")")
                {
                    nestedCount--;
                }

                sb.Append(" " + tokens[i++]);
            }

            return sb.ToString();
        }

    }
}