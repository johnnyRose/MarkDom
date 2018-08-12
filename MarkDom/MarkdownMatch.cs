using MarkDom.Dom;
using System;
using System.Text.RegularExpressions;

namespace MarkDom
{
    public sealed class MarkdownMatch
    {
        public string Original { get; }

        public string MatchValue { get; }

        public GroupCollection Groups { get; }

        private readonly Func<MarkdownMatch, DomItem> _transform;

        public DomItem Parent { get; }

        internal MarkdownParser RecursiveParser { get; }

        public MarkdownMatch(string original, Match match, Func<MarkdownMatch, DomItem> transform, DomItem parent, MarkdownParser recursiveParser)
        {
            this.Original = original;
            this.MatchValue = match.Value;
            this.Groups = match.Groups;
            this._transform = transform;
            this.Parent = parent;
            this.RecursiveParser = recursiveParser;
        }

        public DomItem DoTransform()
        {
            return _transform(this);
        }
    }
}
