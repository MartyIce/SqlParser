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

        public void Process(ref int i, List<string> tokens, SelectStatement tf)
        {
            var on = new On();
            var join = new Join();
            TableField t = new TableField();
            t.Name = tokens[i++];
            if (!join.Match(tokens[i], tokens, i) && !on.Match(tokens[i], tokens, i))
                t.Alias = tokens[i++];

            tf.Tables.Add(t);
        }
    }
}