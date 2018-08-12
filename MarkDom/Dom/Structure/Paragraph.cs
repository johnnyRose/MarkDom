namespace MarkDom.Dom.Structure
{
    public class Paragraph : DomItem
    {
        public override bool IsBlockLevelElement => true;

        public Paragraph(string value, DomItem parent, MarkdownParser recursiveParser)
            : base(value, parent, recursiveParser)
        {
        }

        public override string ToHtml()
        {
            return $"<p>{BuildChildHtml()}</p>";
        }
    }
}
