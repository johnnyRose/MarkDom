namespace MarkDom.Dom.Structure
{
    public class Link : DomItem
    {
        public string Url { get; set; }

        public string LinkText { get; set; }

        protected internal override bool IsPreFormatted => true;

        public override bool IsBlockLevelElement => false;

        public Link(MarkdownMatch match)
            : base(match)
        {
            this.Url = match.Groups["url"].Value;
            this.LinkText = match.Groups["linktext"].Value;
        }

        public override string ToHtml()
        {
            return $"<a href=\"{Url}\">{LinkText}</a>";
        }
    }
}
