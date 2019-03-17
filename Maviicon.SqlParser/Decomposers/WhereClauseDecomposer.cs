using System.Linq;
using Maviicon.SqlParser.Model;

namespace Maviicon.SqlParser.Decomposers
{
    public class WhereClauseDecomposer : SqlDecomposer
    {
        public override void Decompose(ParsedSql ret)
        {
            while(ret.Select.Wheres.Any(w => w.CompositesExist()))
            {
                ret.Select.Wheres.Where(w => w.CompositesExist()).ToList().ForEach(w => { w.Decompose(); });
            }
        }
    }
}