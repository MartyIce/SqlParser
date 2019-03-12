using System.Collections.Generic;
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
            var on = new On();
            var join = new Join();
            TableField t = new TableField();
            t.Name = tokens[i++];
            if (!join.Match(tokens[i], tokens, i) && !on.Match(tokens[i], tokens, i))
                t.Alias = tokens[i++];

            ret.Select.Tables.Add(t);
        }
    }
}