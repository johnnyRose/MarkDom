namespace MarkDom.Dom.Styling
{
    public class Blockquote : DomItem
    {
        public override bool IsBlockLevelElement => true;

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
