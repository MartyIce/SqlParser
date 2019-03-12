using System.Collections.Generic;
using Maviicon.SqlParser.Model;

namespace Maviicon.SqlParser.Parsers
{
    public class Select : TokenParser
    {
        public override bool MatchParser(string token)
        {
            return token == "select";
        }

        public override bool Parse(List<string> parsedTokens, string token)
        {
            parsedTokens.Add(token);

            return false;
        }

        public override void Build(ParsedSql ret, List<string> tokens, ref int i)
        {
            SelectStatement selectStatement = new SelectStatement();
            ret.Select = selectStatement;
            i++;

            var from = new From();
            while (i < tokens.Count)
            {
                if (from.MatchParser(tokens[i]))
                {

                }
                else
                {
                    var sf = new SelectField();
                    sf.Name = tokens[i++];
                    selectStatement.Fields.Add(sf);
                }
            }
        }
    }
}