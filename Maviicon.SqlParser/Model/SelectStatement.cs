using System.Collections.Generic;

namespace Maviicon.SqlParser.Model
{
    public class SelectStatement
    {
        public List<SelectField> Fields;
        public List<TableField> Tables;
        public List<JoinStatement> Joins;
        public List<WhereStatement> Wheres;
        public List<GroupByField> GroupBys;

        public SelectStatement()
        {
            Fields = new List<SelectField>();
            Tables = new List<TableField>();
            Joins = new List<JoinStatement>();
            Wheres = new List<WhereStatement>();
            GroupBys = new List<GroupByField>();
        }

    }
}