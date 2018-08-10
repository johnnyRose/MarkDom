namespace MarkDom.Dom.Headers
{
    public class H1 : HeaderBase
    {
        public override bool IsValidTopLevelTag => true;

        public H1(MarkdownMatch match)
            : base(match)
        {
        }

        public override string ToHtml()
        {
            return "<h1>" + BuildChildHtml() + "</h1>";
        }
    }
}
