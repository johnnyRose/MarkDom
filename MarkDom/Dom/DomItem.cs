using MarkDom.Transforms;
using System;
using System.Collections.Generic;

namespace MarkDom.Dom
{
    public abstract class DomItem : IHtmlTransform
    {
        public string Value { get; set; }

        public string FullMatchValue { get; set; }

        protected internal virtual bool IsPreFormatted { get; }

        public abstract bool IsBlockLevelElement { get; }

        public DomItem Parent { get; set; }

        public List<DomItem> Children { get; set; }

        public string UniqueKey { get; }

        public DomItem(MarkdownMatch match)
            : this(match?.Groups["capture"].Value, match?.Parent, match?.RecursiveParser)
        {
            this.FullMatchValue = match?.MatchValue;
        }

        public DomItem(string value, DomItem parent, MarkdownParser recursiveParser)
        {
            this.Value = value;
            this.Parent = parent;
            this.Children = new List<DomItem>();
            this.UniqueKey = "{{{" + Guid.NewGuid().ToString() + "}}}";

            if (!this.IsPreFormatted && recursiveParser != null)
            {
                recursiveParser.ParseRecursive(this);
            }
        }

        public abstract string ToHtml();

        protected string BuildChildHtml(bool topLevel = false, string overrideValue = null)
        {
            string value = overrideValue ?? Value;

            foreach (var child in Children)
            {
                string childHtml = child.ToHtml();
                value = value.Replace(child.UniqueKey, childHtml);
            }

            return value;
        }
    }
}
