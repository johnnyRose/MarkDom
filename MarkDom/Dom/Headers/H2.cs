﻿namespace MarkDom.Dom.Headers
{
    public class H2 : HeaderBase
    {
        public override bool IsBlockLevelElement => true;

        public H2(MarkdownMatch match)
            : base(match)
        {
        }

        public override string ToHtml()
        {
            return "<h2>" + BuildChildHtml() + "</h2>";
        }
    }
}
