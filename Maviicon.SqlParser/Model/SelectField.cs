namespace Maviicon.SqlParser.Model
{
    public class SelectField : TableColumnName
    {
        public SelectField()
        {
        }

        public string Alias;
        public override string ToString() => Name + (Alias != null ? " AS " + Alias : "");
    }
}