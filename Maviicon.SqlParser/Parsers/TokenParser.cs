using System.Collections.Generic;
using Maviicon.SqlParser.Model;

namespace Maviicon.SqlParser.Parsers
{
    public abstract class TokenParser
    {
        public abstract bool MatchParser(string token);

        public abstract bool Parse(List<string> parsedTokens, string token);

        protected static bool SplitOnCharacter(List<string> parsedTokens, string token, string specialChar)
        {
            if (token.Contains(specialChar))
            {
                int index = token.IndexOf(specialChar);
                if (index > 0)
                    parsedTokens.Add(token.Substring(0, index));
                parsedTokens.Add(specialChar);
                if (index < token.Length - 1)
                    parsedTokens.Add(token.Substring(index + 1, token.Length - index - 1));
                return true;
            }

            return false;
        }

        public virtual void Build(ParsedSql ret, List<string> tokens, ref int i)
        {
        }
    }
}