using System.Collections.Generic;
using Maviicon.SqlParser.Model;

namespace Maviicon.SqlParser.Builders
{
    public class Select : TokenBuilder
    {
        public override bool Match(string token, List<string> tokens, int i)
        {
            return token == "select";
        }

        public override void Build(ParsedSql ret, List<string> tokens, ref int i)
        {
            SelectStatement selectStatement = new SelectStatement();
            ret.Select = selectStatement;
            i++;

            // Select
            var bi = new BuilderIterator();
            bi.AddBuilder(new From());
            bi.AddBuilder(new Comma());
            bi.AddBuilder(new SelectField(), false);
            bi.Build(ret, tokens, ref i);

        }
    }
}