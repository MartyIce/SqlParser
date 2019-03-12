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
            i++;

            // Select
            var bi = new BuilderIterator();
            bi.AddBuilder(new Where(), true, true);
            bi.AddBuilder(new On());
            bi.AddBuilder(new Join());
            bi.AddBuilder(new Table());
            bi.Build(ret, tokens, ref i);
            
        }
    }
}