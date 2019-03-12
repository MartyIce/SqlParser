namespace Maviicon.SqlParser.Model
{
    public class SelectField
    {
        public string Name;
        public string Alias;
        public override string ToString() => Name + (Alias != null ? " AS " + Alias : "");
    }
}