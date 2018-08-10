﻿namespace MarkDom.Dom.Code
{
    public class BlockCode : CodeBase
    {
        public override bool IsValidTopLevelTag => true;

        public override bool IsPreFormatted => true;

        public BlockCode(MarkdownMatch match)
            : base(match)
        {
        }

        public override string ToHtml()
        {
            return "<pre><code>" + BuildChildHtml(overrideValue: SanitizeHtml()).Trim('\r', '\n') + "</code></pre>";
        }
    }
}
