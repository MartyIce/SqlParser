using System.Collections.Generic;
using Maviicon.SqlParser.Model;

namespace Maviicon.SqlParser.Builders
{
    public class BuilderIterator
    {
        private class BuilderInfo
        {
            public bool MustMatch;
            public TokenBuilder Builder;
            public bool BreakAfterProcess;
        }
        private List<BuilderInfo> _bis = new List<BuilderInfo>();
        public void AddBuilder(TokenBuilder tb, bool mustMatch = true, bool breakAfterProcess = false)
        {
            _bis.Add(new BuilderInfo
            {
                Builder = tb,
                MustMatch = mustMatch,
                BreakAfterProcess = breakAfterProcess,
            });
        }

        public void Build(ParsedSql ret, List<string> tokens, ref int i)
        {
            while (i < tokens.Count)
            {
                var token = tokens[i];

                bool breakAfterProcess = false;
                foreach (var bi in _bis)
                {
                    if (!bi.MustMatch || bi.Builder.Match(token, tokens, i))
                    {
                        bi.Builder.Build(ret, tokens, ref i);
                        breakAfterProcess = bi.BreakAfterProcess;
                        break;
                    }
                }

                if (breakAfterProcess)
                    break;
            }
        }
    }
}