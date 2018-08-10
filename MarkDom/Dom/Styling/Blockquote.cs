namespace MarkDom.Dom.Styling
{
    public class Blockquote : DomItem
    {
        public override bool IsValidTopLevelTag => true;

        public Blockquote(MarkdownMatch match)
            : base(match)
        {
        }

        public override string ToHtml()
        {
            return "<blockquote><p>" + BuildChildHtml() + "</p></blockquote>";
        }
    }
}
