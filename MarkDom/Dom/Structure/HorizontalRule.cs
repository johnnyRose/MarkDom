namespace MarkDom.Dom.Structure
{
    public class HorizontalRule : DomItem
    {
        public override bool IsBlockLevelElement => true;

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
