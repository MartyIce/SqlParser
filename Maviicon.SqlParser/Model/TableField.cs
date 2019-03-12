namespace Maviicon.SqlParser.Model
{
    public class TableField
    {
        public string Name;
        public string Alias;
        public override string ToString() => Name + (Alias != null ? " " + Alias : "");
    }
}