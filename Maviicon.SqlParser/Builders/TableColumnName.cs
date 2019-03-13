using System.Collections.Generic;
using Maviicon.SqlParser.Builder;
using Maviicon.SqlParser.Model;

namespace Maviicon.SqlParser.Builders
{
    public class TableColumnName<T> : TokenBuilder where T : TableColumnName, new()
    {
        public override bool Match(string token, List<string> tokens, int i)
        {
            // Not really capable of identifying it's relevance from one token.  Relies on outer builder identification
            return true;
        }

        public virtual void ContinuedBuildAppend(ParsedSql ret, List<string> tokens, ref int i, T field)
        {

        }
        public override void Build(ParsedSql ret, List<string> tokens, ref int i)
        {
            var token = tokens[i];

            var sf = new T();

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

            ContinuedBuildAppend(ret, tokens, ref i, sf);

        }
    }
}