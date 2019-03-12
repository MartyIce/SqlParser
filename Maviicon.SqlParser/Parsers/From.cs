
using System.Collections.Generic;

namespace Maviicon.SqlParser.Parsers
{
    public class From : TokenParser
    {
        public override bool MatchParser(string token)
        {
            return token == "from";
        }
        public override bool Parse(List<string> parsedTokens, string token)
        {
            parsedTokens.Add(token);

            return false;
        }
    }
}