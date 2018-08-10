namespace MarkDom.Dom.Structure
{
    public class HorizontalRule : DomItem
    {
        public override bool IsValidTopLevelTag => true;

        public override bool IsPreFormatted => true;

        public HorizontalRule(MarkdownMatch match)
            : base(match)
        {
        }

        public override string ToHtml()
        {
            return "<hr />";
        }
    }
}
