namespace MarkDom.Dom.Headers
{
    public class H6 : HeaderBase
    {
        public override bool IsBlockLevelElement => true;

        public H6(MarkdownMatch match)
            : base(match)
        {
            match.RecursiveParser.ParseRecursive(this);
        }

        public override string ToHtml()
        {
            return "<h6>" + BuildChildHtml() + "</h6>";
        }
    }
}
