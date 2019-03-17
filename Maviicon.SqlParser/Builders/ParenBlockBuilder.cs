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
        public static string ConsumeParenBlock(string input, ref int index)
        {
            index = input.IndexOf("(");
            int nestedCount = 0;
            var sb = new StringBuilder();
            for (int i = index; i < input.Length; i++)
            {
                if (input[i] == '(')
                {
                    nestedCount++;
                } else if (input[i] == ')')
                {
                    nestedCount--;
                }

                sb.Append(input[i]);

                if (nestedCount == 0)
                    break;

            }

            return sb.ToString();
        }

    }
}