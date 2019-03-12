using System.Collections.Generic;
using Maviicon.SqlParser.Model;

namespace Maviicon.SqlParser.Builders
{
    public class Join : TokenBuilder
    {
        public override bool Match(string token, List<string> tokens, int i)
        {
            return token == "join";
        }

        public override void Build(ParsedSql ret, List<string> tokens, ref int i)
        {
            i++;

            (new Table()).Build(ret, tokens, ref i);
        }
    }
}