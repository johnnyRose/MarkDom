namespace MarkDom.Dom.Lists
{
    public class MarkdownListItem : DomItem
    {
        public override bool IsBlockLevelElement => false;

        public MarkdownListItem(string value, DomItem parent, MarkdownParser recursiveParser)
            : base(value, parent, recursiveParser)
        {
            this.FullMatchValue = value;
        }

        public override string ToHtml()
        {
            return $"<li>{BuildChildHtml()}</li>";
        }
    }
}
