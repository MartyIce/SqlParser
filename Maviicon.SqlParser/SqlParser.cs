
using System.Collections.Generic;
using System.Linq;
using Maviicon.SqlParser.Model;
using Maviicon.SqlParser.Parsers;

namespace Maviicon.SqlParser
{
    public class SqlParser
    {
        public ParsedSql Parse(string sql)
        {
            var tokens = Tokenize(sql);

            var ret = new ParsedSql
            {
            };

            var builders = new List<Builders.TokenBuilder>
            {
                new Builders.Select(),
            };
            for (int i = 0; i < tokens.Count; i++)
            {
                foreach (var builder in builders)
                {
                    if (builder.Match(tokens[i], tokens, i))
                    {
                        builder.Build(ret, tokens, ref i);
                    }
                }

            }

            return ret;
        }

        private static List<string> Tokenize(string sql)
        {
            var lowSql = sql.ToLower();
            var tokens = lowSql.Replace("\r\n", "").Split(' ').Where(t => !string.IsNullOrEmpty(t)).ToList();
            var parsedTokens = new List<string>();
            List<TokenParser> parsers = new List<TokenParser>
            {
                new Parsers.LeftParen(),
                new Parsers.RightParen(),
                new Parsers.Select(),
                new Parsers.When(),
                new Parsers.Comma(),
            };

            bool keepGoing = true;
            while (keepGoing)
            {
                keepGoing = false;
                foreach (var token in tokens)
                {
                    bool parserMatched = false;
                    foreach (var parser in parsers)
                    {
                        if (parser.MatchParser(token))
                        {
                            parserMatched = true;
                            keepGoing |= parser.Parse(parsedTokens, token);
                            break;
                        }
                    }

                    if (!parserMatched)
                    {
                        parsedTokens.Add(token);
                    }
                }

                tokens = new List<string>(parsedTokens);
                parsedTokens = new List<string>();
            }

            return tokens;
        }
    }
}