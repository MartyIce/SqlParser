using System.Collections.Generic;

namespace Maviicon.SqlParser.Parsers
{
    public abstract class SpecialCharParser : TokenParser
    {
        protected abstract string SpecialChar { get; }
        public override bool MatchParser(string token)
        {
            return token.Length > 1 && token.Contains(SpecialChar);
        }

        public override bool Parse(List<string> parsedTokens, string token)
        {
            return SplitOnCharacter(parsedTokens, token, SpecialChar);
        }
    }
}