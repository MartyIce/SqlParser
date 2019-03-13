using System.Collections.Generic;
using Maviicon.SqlParser.Model;

namespace Maviicon.SqlParser.Builders
{
    public class WhereStatementBuilder : TokenBuilder
    {
        private bool _onLeft;
        private WhereStatement _ws;
        static List<string> operators = new List<string> { "=", "!=", ">", "<"};

        public WhereStatementBuilder()
        {
            _onLeft = true;
            _ws = new WhereStatement();
        }

        public override bool Match(string token, List<string> tokens, int i)
        {
            return true;
        }

        public override void Build(ParsedSql ret, List<string> tokens, ref int i)
        {
            var token = tokens[i];
            if (operators.Contains(token))
            {
                _ws.Operator = token;
                _onLeft = false;
                i++;
            }
            else if (token == "and" || token == "or")
            {
                _ws = new WhereStatement();
                _onLeft = true;
                i++;
            }
            else if(_onLeft)
            {
                _ws.LeftClause = token;
                i++;
            }
            else
            {
                _ws.RightClause = token;
                ret.Select.Wheres.Add(_ws);
                i++;
            }
        }
    }
}