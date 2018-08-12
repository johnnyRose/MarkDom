namespace MarkDom.Dom.Headers
{
    public class H5 : HeaderBase
    {
        public override bool IsBlockLevelElement => true;

        public H5(MarkdownMatch match)
            : base(match)
        {
            match.RecursiveParser.ParseRecursive(this);
        }

        public override string ToHtml()
        {
            return "<h5>" + BuildChildHtml() + "</h5>";
        }
    }
}
