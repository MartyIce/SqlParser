using System;
using System.Collections.Generic;
using System.Linq;
using Maviicon.SqlParser.Builder;
using Maviicon.SqlParser.Model;

namespace Maviicon.SqlParser.Decomposers
{

    public static class BoolDecomposer {
        
        public static ChunkedBools Decompose(string input)
        {
            if(string.IsNullOrEmpty(input))
                return null;

            if(!Constants.BooleanOperators.Any(b => input.Contains(b)))
                return new ChunkedBools(input);

            var ret = new ChunkedBools(input);
            DecomposeChunk(ret);
            return ret;
        }

        private static void DecomposeChunk(ChunkedBools current)
        {
            bool didSplit = false;
            if (current.Statement.StartsWith("("))
            {
                var index = 0;
                var block = ParenBlockBuilder.ConsumeParenBlock(current.Statement, ref index);
                current.Children = new List<ChunkedBools>();
                current.Children.Add(new ChunkedBools(block.Substring(1, block.Length - 2)));
                var remaining = current.Statement.Substring(block.Length).Trim();
                var firstSpace = remaining.IndexOf(" ");
                var op = remaining.Substring(0, firstSpace);
                var statement = remaining.Substring(firstSpace + 1);
                current.Children.Add(new ChunkedBools(statement, op));
                didSplit = true;
            }
            else if (current.Statement.Contains("("))
            {
                var index = 0;
                var block = ParenBlockBuilder.ConsumeParenBlock(current.Statement, ref index);
                current.Children = new List<ChunkedBools>();
                var remaining = current.Statement.Substring(0, index).Trim();
                var space = remaining.LastIndexOf(" ");
                var statement = remaining.Substring(0, space);
                var op = remaining.Substring(space + 1);
                current.Children.Add(new ChunkedBools(statement));
                current.Children.Add(new ChunkedBools(block.Substring(1, block.Length - 2), op));
                didSplit = true;
            }
            else
                didSplit = CheckForOpAndSplit(current.Statement, "and", current) || CheckForOpAndSplit(current.Statement, "or", current);

            if (didSplit && current.Children != null)
            {
                current.Children.ForEach(c => DecomposeChunk(c));
            }
        }

        private static bool CheckForOpAndSplit(string input, string op, ChunkedBools ret)
        {
            if (input.Contains(op))
            {
                ret.Children = input.Split(new string[] {op}, StringSplitOptions.RemoveEmptyEntries)
                    .Select(i => new ChunkedBools(i.Trim())).ToList();
                for (int i = 1; i < ret.Children.Count; i++)
                    ret.Children[i].LeadingOp = op;
                return true;
            }

            return false;
        }
    }
}