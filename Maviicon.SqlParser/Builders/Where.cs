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
        public override void Build(ParsedSql ret, List<string> tokens, ref int i)
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
                    ret.Select.Wheres.Add(ws);
                    i++;
                }
            }
        }
    }
}