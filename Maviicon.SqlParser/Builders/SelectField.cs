using System.Collections.Generic;
using Maviicon.SqlParser.Model;

namespace Maviicon.SqlParser.Builders
{
    public class SelectField : TableColumnName<Model.SelectField>
    {
        public override void ContinuedBuildAppend(ParsedSql ret, List<string> tokens, ref int i, Model.SelectField field)
        {
            if (i < tokens.Count)
            {
                if (tokens[i] == "as")
                {
                    field.Alias = tokens[++i];
                    i++;
                }
            }
            ret.Select.Fields.Add(field);
        }
    }
}