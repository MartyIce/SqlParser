using System.Collections.Generic;
using Maviicon.SqlParser.Model;

namespace Maviicon.SqlParser.Builders
{
    public class Join : TokenBuilder
    {
        public override bool Match(string token, List<string> tokens, int i)
        {
            return token == "join";
        }

        public void Process(ref int i, List<string> tokens, SelectStatement ss)
        {
            i++;

            (new Table()).Process(ref i, tokens, ss);
        }
    }
}