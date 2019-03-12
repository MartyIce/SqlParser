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

        public override void Build(ParsedSql ret, List<string> tokens, ref int i)
        {
            i++;

            var bi = new BuilderIterator();
            bi.AddBuilder(new Join());
            bi.AddBuilder(new Where());
            bi.AddBuilder(new On());
            bi.AddBuilder(new OnJoinStatementBuilder(), false);
            bi.Build(ret, tokens, ref i);
        }
    }
}