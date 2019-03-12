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
        public override void Build(ParsedSql ret, List<string> tokens, ref int i)
        {
            i++;
            bool onLeft = true;
            JoinStatement js = new JoinStatement();
            var join = new Join();
            var where = new Where();
            var on = new On();

//            var bi = new BuilderIterator();
//            bi.AddBuilder(new Join());
//            bi.AddBuilder(new Where());
//            bi.AddBuilder(new On());
//            bi.Build(ret, tokens, ref i);


            while (i < tokens.Count)
            {
                var token = tokens[i];
                if (join.Match(token, tokens, i))
                {
                    join.Build(ret, tokens, ref i);
                }
                else if (where.Match(token, tokens, i))
                {
                    where.Build(ret, tokens, ref i);
                }
                else if (on.Match(token, tokens, i))
                {
                    on.Build(ret, tokens, ref i);
                }
                else {
                    if (operators.Contains(token))
                    {
                        js.Operator = token;
                        onLeft = false;
                        i++;
                    }
                    if (token == "and" || token == "or")
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
                        ret.Select.Joins.Add(js);
                        i++;
                    }
                }
            }
        }
    }
}