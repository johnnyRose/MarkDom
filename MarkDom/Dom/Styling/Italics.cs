namespace MarkDom.Dom.Styling
{
    public class Italics : DomItem
    {
        public override bool IsBlockLevelElement => false;

        public Italics(MarkdownMatch match)
            : base(match)
        {
        }

        public override string ToHtml()
        {
            return "<em>" + BuildChildHtml() + "</em>";
        }
    }
}
