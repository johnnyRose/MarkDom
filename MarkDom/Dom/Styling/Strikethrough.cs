namespace MarkDom.Dom.Styling
{
    public class Strikethrough : DomItem
    {
        public override bool IsValidTopLevelTag => false;

        public Strikethrough(MarkdownMatch match)
            : base(match)
        {
        }

        public override string ToHtml()
        {
            return "<s>" + BuildChildHtml() + "</s>";
        }
    }
}
