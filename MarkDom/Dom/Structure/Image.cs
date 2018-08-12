namespace MarkDom.Dom.Structure
{
    public class Image : DomItem
    {
        public string Url { get; set; }

        public string AltText { get; set; }

        protected internal override bool IsPreFormatted => true;

        public override bool IsBlockLevelElement => false;

        public Image(MarkdownMatch match)
            : base(match)
        {
            this.Url = match.Groups["url"].Value;
            this.AltText = match.Groups["alttext"].Value;
        }

        public override string ToHtml()
        {
            return $"<img src=\"{Url}\" alt=\"{AltText}\" />";
        }
    }
}
