using System.Collections.Generic;
using Maviicon.SqlParser.Model;

namespace Maviicon.SqlParser.Builders
{
    public class GroupByField : TableColumnName<Model.GroupByField>
    {
        public override void ContinuedBuildAppend(ParsedSql ret, List<string> tokens, ref int i, Model.GroupByField field)
        {
            ret.Select.GroupBys.Add(field);
        }
    }
}