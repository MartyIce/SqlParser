namespace Maviicon.SqlParser.Parsers
{
    public class RightParen : SpecialCharParser
    {
        protected override string SpecialChar { get { return ")"; } }
    }
}