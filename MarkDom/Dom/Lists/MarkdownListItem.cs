﻿namespace MarkDom.Dom.Lists
{
    public class MarkdownListItem : DomItem
    {
        public override bool IsValidTopLevelTag => false;

        public MarkdownListItem(string value, DomItem parent, MarkdownParser recursiveParser)
            : base(value, parent)
        {
            this.FullMatchValue = value;
            recursiveParser.ParseRecursive(this);
        }

        public override string ToHtml()
        {
            return $"<li>{BuildChildHtml()}</li>";
        }
    }
}
