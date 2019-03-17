using System;
using Maviicon.SqlParser.Decomposers;
using Xunit;

namespace Maviicon.SqlParser.Tests.Decomposers
{
    public class BoolDecomposerTests 
    {
        [Fact]
        public void Empty()
        {
            Assert.Null(BoolDecomposer.Decompose(""));
        }
        [Fact]
        public void NoBool()
        {
            var result = BoolDecomposer.Decompose("x = 1");
            Assert.Equal("x = 1", result.Statement);
        }
        [Fact]
        public void SimpleAnd()
        {
            var result = BoolDecomposer.Decompose("x = 1 and y = 2");
            Assert.Equal(2, result.Children.Count);
            Assert.Equal("x = 1", result.Children[0].Statement);
            Assert.Equal("y = 2", result.Children[1].Statement);
        }
        [Fact]
        public void AndOr()
        {
            var result = BoolDecomposer.Decompose("x = 1 and y = 2 or z = 3");
            Assert.Equal(2, result.Children.Count);
            Assert.Equal("x = 1", result.Children[0].Statement);
            Assert.Equal("y = 2 or z = 3", result.Children[1].Statement);
            Assert.Equal(2, result.Children[1].Children.Count);
            Assert.Equal("y = 2", result.Children[1].Children[0].Statement);
            Assert.Equal("z = 3", result.Children[1].Children[1].Statement);
        }
        [Fact]
        public void AndOrWithParensInFront()
        {
            var result = BoolDecomposer.Decompose("(x = 1 and y = 2) or z = 3");
            Assert.Equal(2, result.Children.Count);
            Assert.Equal("x = 1 and y = 2", result.Children[0].Statement);
            Assert.Equal("z = 3", result.Children[1].Statement);
            Assert.Equal(2, result.Children[0].Children.Count);
            Assert.Equal("x = 1", result.Children[0].Children[0].Statement);
            Assert.Equal("y = 2", result.Children[0].Children[1].Statement);
            Assert.Equal("and", result.Children[0].Children[1].LeadingOp);
        }
        [Fact]
        public void AndOrWithParensInBack()
        {
            var result = BoolDecomposer.Decompose("x = 1 and (y = 2 or z = 3)");
            Assert.Equal(2, result.Children.Count);
            Assert.Equal("x = 1", result.Children[0].Statement);
            Assert.Equal("y = 2 or z = 3", result.Children[1].Statement);
            Assert.Equal("and", result.Children[1].LeadingOp);
            Assert.Equal(2, result.Children[1].Children.Count);
            Assert.Equal("y = 2", result.Children[1].Children[0].Statement);
            Assert.Equal("z = 3", result.Children[1].Children[1].Statement);
            Assert.Equal("or", result.Children[1].Children[1].LeadingOp);
        }
    }
}
