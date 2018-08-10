namespace MarkDom.Dom.Code
{
    public abstract class CodeBase : DomItem
    {
        public CodeBase(MarkdownMatch match)
            : base(match)
        {
        }

        protected string SanitizeHtml()
        {
            return this.Value.Replace("<", "&lt;").Replace(">", "&gt;");
        }
    }
}
