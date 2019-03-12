using System.Collections.Generic;

namespace Maviicon.SqlParser.Builders
{
    public class Comma : TokenBuilder
    {
        public override bool Match(string token, List<string> tokens, int i)
        {
            return token == ",";
        }
    }
}