namespace MarkDom.Dom.Headers
{
    public class H6 : HeaderBase
    {
        public override bool IsValidTopLevelTag => true;

        public H6(MarkdownMatch match)
            : base(match)
        {
        }

        public override string ToHtml()
        {
            return "<h6>" + BuildChildHtml() + "</h6>";
        }
    }
}
