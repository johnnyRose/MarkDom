namespace MarkDom.Dom
{
    public class DomSection : DomItem
    {
        public override bool IsValidTopLevelTag => false;

        public DomSection(string text)
            : base(match: null)
        {
            this.Value = text;
        }

        public override string ToHtml()
        {
            return BuildChildHtml();
        }
    }
}
