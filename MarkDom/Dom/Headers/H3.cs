namespace MarkDom.Dom.Headers
{
    public class H3 : HeaderBase
    {
        public override bool IsBlockLevelElement => true;

        public H3(MarkdownMatch match)
            : base(match)
        {
            match.RecursiveParser.ParseRecursive(this);
        }

        public override string ToHtml()
        {
            return "<h3>" + BuildChildHtml() + "</h3>";
        }
    }
}
