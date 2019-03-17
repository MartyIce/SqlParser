using Maviicon.SqlParser.Model;

namespace Maviicon.SqlParser.Decomposers
{
    public abstract class SqlDecomposer
    {
        public abstract void Decompose(ParsedSql ret);
    }
}