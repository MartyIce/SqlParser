using System.Collections.Generic;
using Maviicon.SqlParser.Model;

namespace Maviicon.SqlParser.Builders
{
    public abstract class TokenBuilder
    {
        public abstract bool Match(string token, List<string> tokens, int i);

        public virtual void Build(ParsedSql ret, List<string> tokens, ref int i)
        {
        }
    }
}