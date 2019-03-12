using System.Collections.Generic;

namespace Maviicon.SqlParser.Parsers
{
    public class When : TokenParser
    {
        public override bool MatchParser(string token)
        {
            return token == "when";
        }

        public override bool Parse(List<string> parsedTokens, string token)
        {
            parsedTokens.Add(token);

            return false;
        }
    }
}