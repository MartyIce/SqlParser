using System.Collections.Generic;
using Maviicon.SqlParser.Model;

namespace Maviicon.SqlParser.Builders
{
    public class On : TokenBuilder
    {
        public override bool Match(string token, List<string> tokens, int i)
        {
            return token == "on";
        }

        static List<string> operators = new List<string> { "=", "!=", ">", "<"};
        public void Process(ref int i, List<string> tokens, SelectStatement ss)
        {
            i++;
            bool onLeft = true;
            JoinStatement js = new JoinStatement();
            var join = new Join();
            var where = new Where();
            var on = new On();
            while (i < tokens.Count)
            {
                var token = tokens[i];
                if (join.Match(token, tokens, i))
                {
                    join.Process(ref i, tokens, ss);
                }
                else if (where.Match(token, tokens, i))
                {
                    where.Process(ref i, tokens, ss);
                }
                else if (on.Match(token, tokens, i))
                {
                    on.Process(ref i, tokens, ss);
                }
                else if (operators.Contains(token))
                {
                    js.Operator = token;
                    onLeft = false;
                    i++;
                }
                else if (token == "and")
                {
                    js = new JoinStatement();
                    i++;
                }
                else if(onLeft)
                {
                    js.LeftClause = token;
                    i++;
                }
                else
                {
                    js.RightClause = token;
                    ss.Joins.Add(js);
                    i++;
                }
            }
        }
    }
}