using System.Collections.Generic;
using Maviicon.SqlParser.Builder;
using Maviicon.SqlParser.Model;

namespace Maviicon.SqlParser.Builders
{
    public class Table : TokenBuilder
    {
        public override bool Match(string token, List<string> tokens, int i)
        {
            return true;
        }
        
        public override void Build(ParsedSql ret, List<string> tokens, ref int i)
        {
            TableField t = new TableField();
            if (tokens[i] == "(")
            {
                t.Name = ParenBlockBuilder.ConsumeParenBlock(tokens, ref i);
            }
            else
            {
                t.Name = tokens[i++];
            }

            var on = new On();
            var join = new Join();
            var where = new Where();
            if (!join.Match(tokens[i], tokens, i) &&
                !on.Match(tokens[i], tokens, i) &&
                !where.Match(tokens[i], tokens, i))
            {
                var token = tokens[i++];
                if(token == "as")
                    token = tokens[i++];
                t.Alias = token;
            }


            ret.Select.Tables.Add(t);
        }
    }
}