using System.Collections.Generic;
using Maviicon.SqlParser.Model;

namespace Maviicon.SqlParser.Builders
{
    public class From : TokenBuilder
    {
        public override bool Match(string token, List<string> tokens, int i)
        {
            return token == "from";
        }

        public override void Build(ParsedSql ret, List<string> tokens, ref int i)
        {
            // From 
            i++;
            var on = new On();
            var join = new Join();
            var table = new Table();
            var where = new Where();
            while (i < tokens.Count)
            {
                var token = tokens[i];
                if (where.Match(token, tokens, i))
                {
                    where.Process(ref i, tokens, ret.Select);
                    break;
                }
                else if (on.Match(tokens[i], tokens, i))
                {
                    on.Process(ref i, tokens, ret.Select);
                } 
                else if (join.Match(tokens[i], tokens, i))
                {
                    join.Process(ref i, tokens, ret.Select);
                }
                else
                {
                    table.Process(ref i, tokens, ret.Select);
                }
            }
        }
    }
}