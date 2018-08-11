namespace MarkDom.Dom.Structure
{
    public class HtmlTag : DomItem
    {
        public override bool IsValidTopLevelTag => true;

        public override bool IsPreFormatted => true;

        public HtmlTag(MarkdownMatch match)
            : base(match)
        {
        }

        public override string ToHtml()
        {
            return Value;
        }
    }
}
