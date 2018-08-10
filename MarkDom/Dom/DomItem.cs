using MarkDom.Transforms;
using System;
using System.Collections.Generic;

namespace MarkDom.Dom
{
    public abstract class DomItem : IHtmlTransform
    {
        public string Value { get; set; }

        public string FullMatchValue { get; set; }

        public virtual bool IsPreFormatted { get; }

        public abstract bool IsValidTopLevelTag { get; }

        public DomItem Parent { get; set; }

        public virtual List<DomItem> Children { get; set; }

        public string UniqueKey { get; }

        public bool IsTopLevel => GetType() == typeof(DomSection);

        public DomItem(MarkdownMatch match)
            : this(match?.Groups["capture"].Value, match?.Parent)
        {
            this.FullMatchValue = match?.MatchValue;
        }

        public DomItem(string value, DomItem parent)
        {
            this.Value = value;
            this.Parent = parent;
            this.Children = new List<DomItem>();
            this.UniqueKey = "{{{" + Guid.NewGuid().ToString() + "}}}";
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

            if (this.IsTopLevel && !this.IsValidTopLevelTag)
            {
                value = $"<p>{value}</p>";
            }

            return value;
        }
    }
}
