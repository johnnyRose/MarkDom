namespace MarkDom.Dom.Headers
{
    public class H4 : HeaderBase
    {
        public override bool IsBlockLevelElement => true;

        public H4(MarkdownMatch match)
            : base(match)
        {
        }

        public override string ToHtml()
        {
            return "<h4>" + BuildChildHtml() + "</h4>";
        }
    }
}
