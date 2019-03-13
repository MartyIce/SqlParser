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

        public override void Build(ParsedSql ret, List<string> tokens, ref int i)
        {
            i++;

            
            var bi = new BuilderIterator();
            bi.AddBuilder(new GroupBy(), true, true);
            bi.AddBuilder(new WhereStatementBuilder());
            bi.Build(ret, tokens, ref i);
        }
    }
}