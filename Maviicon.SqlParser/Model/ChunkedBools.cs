using System.Collections.Generic;

namespace Maviicon.SqlParser.Model
{
    public class ChunkedBools
    {
        public string LeadingOp { get; set;  }
        public string Statement { get; }

        public ChunkedBools()
        {
        }
        public ChunkedBools(string statement) : this()
        {
            Statement = statement.Trim();
        }
        public ChunkedBools(string statement, string leadingOp) : this(statement)
        {
            LeadingOp = leadingOp.Trim();
        }

        public List<ChunkedBools> Children { get; set; }

        public override string ToString() => Statement + (string.IsNullOrEmpty(LeadingOp) ? "" : $"({LeadingOp})");
    }
}