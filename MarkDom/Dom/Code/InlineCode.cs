namespace MarkDom.Dom.Code
{
    public class InlineCode : CodeBase
    {
        public override bool IsBlockLevelElement => false;

        public InlineCode(MarkdownMatch match)
            : base(match)
        {
        }

        public override string ToHtml()
        {
            return "<code>" + BuildChildHtml(overrideValue: SanitizeHtml()) + "</code>";
        }
    }
}
