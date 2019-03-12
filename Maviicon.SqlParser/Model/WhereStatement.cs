namespace Maviicon.SqlParser.Model
{
    public class WhereStatement
    {
        public string LeftClause;
        public string RightClause;
        public string Operator;
        public override string ToString() => LeftClause + " " + Operator + " " + RightClause;
    }
}