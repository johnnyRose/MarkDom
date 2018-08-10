namespace MarkDom.Dom.Styling
{
    public class Bold : DomItem
    {
        public override bool IsValidTopLevelTag => false;

        public Bold(MarkdownMatch match)
            : base(match)
        {
        }

        public override string ToHtml()
        {
            string childHtml = BuildChildHtml();
            return "<strong>" + childHtml + "</strong>";
        }
    }
}
