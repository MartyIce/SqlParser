using System.Collections.Generic;
using System.Linq;

namespace Maviicon.SqlParser.Model
{    
    public class WhereStatement    
    {
        public string LeftClause;
        public string RightClause;
        public string Operator;
        public override string ToString() => LeftClause + " " + Operator + " " + RightClause;
        public string PrecedingOperator;
        public List<WhereStatement> Wheres;

        public WhereStatement()
        {
            Wheres = new List<WhereStatement>();
        }

        public bool CompositesExist()
        {
            return Constants.BooleanOperators.Any(o => !string.IsNullOrEmpty(LeftClause) && LeftClause.Contains(" " + o + " ")) ||
                Constants.BooleanOperators.Any(o => !string.IsNullOrEmpty(RightClause) && RightClause.Contains(" " + o + " "));
        }

        public void Decompose()
        {
            var tokens = LeftClause.Split(' ');

        }
    }
}