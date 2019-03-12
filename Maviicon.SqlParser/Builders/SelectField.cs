using System.Collections.Generic;
using Maviicon.SqlParser.Builder;
using Maviicon.SqlParser.Model;
using Maviicon.SqlParser.Parsers;

namespace Maviicon.SqlParser.Builders
{
    public class SelectField : TokenBuilder
    {
        public override bool Match(string token, List<string> tokens, int i)
        {
            // Not really capable of identifying it's relevance from one token.  Relies on outer builder identification
            return true;
        }

        public override void Build(ParsedSql ret, List<string> tokens, ref int i)
        {
            var token = tokens[i];

            var sf = new Maviicon.SqlParser.Model.SelectField();

            if (token == "(")
            {
                sf.Name = ParenBlockBuilder.ConsumeParenBlock(tokens, ref i);
            }
            else if (FunctionBlockBuilder.Match(token, tokens, i))
            {
                sf.Name = FunctionBlockBuilder.ConsumeFunctionBlock(tokens, ref i);
            }
            else
            {
                sf.Name = tokens[i++];
            }

            if (tokens[i] == "as")
            {
                sf.Alias = tokens[++i];
                i++;
            }

            ret.Select.Fields.Add(sf);
        }
    }
}