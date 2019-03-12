using System.Collections.Generic;
using Maviicon.SqlParser.Model;

namespace Maviicon.SqlParser.Builders
{
    public class Where : TokenBuilder
    {
        public override bool Match(string token, List<string> tokens, int i)
        {
            return token == "where";
        }

        static List<string> operators = new List<string> { "=", "!=", ">", "<"};
        public void Process(ref int i, List<string> tokens, SelectStatement ss)
        {
            i++;
            bool onLeft = true;
            var ws = new WhereStatement();
            while (i < tokens.Count)
            {
                var token = tokens[i];
                if (operators.Contains(token))
                {
                    ws.Operator = token;
                    onLeft = false;
                    i++;
                }
                else if (token == "and" || token == "or")
                {
                    ws = new WhereStatement();
                    onLeft = true;
                    i++;
                }
                else if(onLeft)
                {
                    ws.LeftClause = token;
                    i++;
                }
                else
                {
                    ws.RightClause = token;
                    ss.Wheres.Add(ws);
                    i++;
                }
            }
        }
    }
}