namespace MarkDom.Dom.Structure
{
    public class Paragraph : DomItem
    {
        public override bool IsValidTopLevelTag => true;

        public Paragraph(string value, DomItem parent)
            : base(value, parent)
        {
        }

        public override string ToHtml()
        {
            return $"<p>{BuildChildHtml()}</p>";
        }
    }
}
