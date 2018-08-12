using MarkDom.Dom.Structure;

namespace MarkDom.Dom
{
    public class DomSection : DomItem
    {
        public override bool IsBlockLevelElement => false;

        public DomSection(string text)
            : base(match: null)
        {
            this.Value = text;
        }

        public override string ToHtml()
        {
            return BuildChildHtml();
        }

        internal void EnsureIsValidTopLevel(MarkdownParser recursiveParser)
        {
            if (this.Children.Count == 0 || !this.Children[0].IsBlockLevelElement)
            {
                MakeValidTopLevelElement(recursiveParser);
            }
        }

        private void MakeValidTopLevelElement(MarkdownParser recursiveParser)
        {
            Paragraph newChild = new Paragraph(this.Value, this, recursiveParser);

            foreach (var child in this.Children)
            {
                child.Parent = newChild;
                newChild.Children.Add(child);
            }

            this.Value = newChild.UniqueKey;
            this.Children.Clear();
            this.Children.Add(newChild);
        }
    }
}
