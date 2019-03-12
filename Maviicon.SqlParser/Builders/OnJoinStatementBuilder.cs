using System.Collections.Generic;
using Maviicon.SqlParser.Model;

namespace Maviicon.SqlParser.Builders
{
    public class OnJoinStatementBuilder : TokenBuilder
    {
        private JoinStatement _js;
        private bool _onLeft;
        static List<string> operators = new List<string> { "=", "!=", ">", "<"};

        public OnJoinStatementBuilder()
        {
            _onLeft = true;
            _js = new JoinStatement();
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
                _js.Operator = token;
                _onLeft = false;
                i++;
            }
            if (token == "and" || token == "or")
            {
                _js = new JoinStatement();
                i++;
            }
            else if(_onLeft)
            {
                _js.LeftClause = token;
                i++;
            }
            else
            {
                _js.RightClause = token;
                ret.Select.Joins.Add(_js);
                i++;
            }
        }
    }
}