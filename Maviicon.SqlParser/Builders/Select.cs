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
            var from = new From();
            var comma = new Comma();
            var selectField = new SelectField();

            while (i < tokens.Count)
            {
                var token = tokens[i];
                if (from.Match(token, tokens, i))
                {
                    from.Build(ret, tokens, ref i);
                    break;
                }
                else if (comma.Match(tokens[i], tokens, i))
                {
                    i++;
                }
                else
                {
                    selectField.Build(ret, tokens, ref i);
                }
            }

        }
    }
}