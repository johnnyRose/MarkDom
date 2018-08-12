namespace MarkDom.Dom.Structure
{
    public class HorizontalRule : DomItem
    {
        public override bool IsBlockLevelElement => true;

        protected internal override bool IsPreFormatted => true;

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
