using System.Collections.Generic;
using Maviicon.SqlParser.Model;

namespace Maviicon.SqlParser.Builders
{
    public class GroupBy : TokenBuilder
    {
        private Model.GroupByField _groupByStatement;

        public GroupBy()
        {
            _groupByStatement = new Model.GroupByField();
        }

        public override bool Match(string token, List<string> tokens, int i)
        {
            return tokens[i] == "group" && (i < tokens.Count - 2) && tokens[i+1] == "by";
        }

        public override void Build(ParsedSql ret, List<string> tokens, ref int i)
        {
            i++;
            i++;

            // Select
            var bi = new BuilderIterator();
            bi.AddBuilder(new Comma());
            bi.AddBuilder(new GroupByField(), false);
            bi.Build(ret, tokens, ref i);
        }
    }
}