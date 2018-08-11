using MarkDom.Dom;
using MarkDom.Dom.Code;
using MarkDom.Dom.Headers;
using MarkDom.Dom.Lists;
using MarkDom.Dom.Structure;
using MarkDom.Dom.Styling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MarkDom
{
    public sealed class MarkdownParser
    {
        public MarkdownParser()
            : this(_defaultMatchRules)
        {
        }

        public MarkdownParser(MatchRules matchRules)
        {
            this._matchRules = matchRules;
        }

        private static readonly TimeSpan _matchTimeout = TimeSpan.FromMilliseconds(10);

        private readonly MatchRules _matchRules;
        private static readonly MatchRules _defaultMatchRules = new MatchRules()
        {
            { BuildRegex(@"```(?<capture>.*?(\r\n|\n)(.|\s)+?)```"), match =>
            {
                return new BlockCode(match);
            } },

            { BuildRegex(@"`(?<capture>.*?)`"), match => new InlineCode(match) },

            { BuildRegex(@"^#{6}(?<capture>[^#]+)"), match => new H6(match) },
            { BuildRegex(@"^#{5}(?<capture>[^#]+)"), match => new H5(match) },
            { BuildRegex(@"^#{4}(?<capture>[^#]+)"), match => new H4(match) },
            { BuildRegex(@"^#{3}(?<capture>[^#]+)"), match => new H3(match) },
            { BuildRegex(@"^#{2}(?<capture>[^#]+)"), match => new H2(match) },
            { BuildRegex(@"^#{1}(?<capture>[^#]+)"), match => new H1(match) },

            { BuildRegex(@"^\s*(?<capture>-{3,}|\*{3,}|_{3,})$"), match => new HorizontalRule(match) },

            { BuildRegex(@"(?<capture>!\[(?<alttext>.+)\]\((?<url>.+?)\))"), match => new Image(match) },

            { BuildRegex(@"(?<capture>\[(?<linktext>.+)\]\((?<url>.+?)\))"), match => new Link(match) },

            { BuildRegex(@"\*(?<capture>.*?)\*"), match => new Bold(match) },

            // Match the beginning of the string or a word boundary, followed by _some stuff_, followed by the end of the string or a word boundary.
            // This prevents matching and wrecking URLs in links and images.
            // Stack Overflow doesn't handle this issue for the "*" character, so we probably just need to handle it here... Hopefully.
            { BuildRegex(@"(^|\b)_(?<capture>.*?)_($|\b)"), match => new Italics(match) },

            { BuildRegex(@"^(?<capture>((?<listitemdelimiter>\s*(-|\*|(\d\.|\d\))) )(.|\s)*)+)"), match => new MarkdownList(match) },

            { BuildRegex(@"~~(?<capture>.*)~~"), match => new Strikethrough(match) },

            { BuildRegex(@"^>(?<capture>(.|\s)+)"), match => new Blockquote(match) },

            { BuildRegex(@"^(?<capture><.+)"), match => new HtmlTag(match) },

            { BuildRegex(@"^\s+$"), token => token.Parent },
        };

        private static Regex BuildRegex(string pattern)
        {
            return new Regex(pattern, RegexOptions.Multiline, _matchTimeout);
        }

        public DomDocument Parse(string markdown)
        {
            var domSections = new List<DomItem>();

            var sections = GetDocumentSections(markdown);

            foreach (string section in sections)
            {
                DomItem domSection = ParseRecursive(new DomSection(section));

                if (domSection.Children.Count == 0)
                {
                    DomSection newDomSection = new DomSection(domSection.Value);
                    Paragraph paragraph = new Paragraph(newDomSection.Value, newDomSection);
                    newDomSection.Children.Add(paragraph);
                    newDomSection.Value = paragraph.UniqueKey;
                    domSection = newDomSection;
                }
                else if (!domSection.Children.First().IsBlockLevelElement)
                {
                    Paragraph newChild = new Paragraph(domSection.Value, domSection);

                    foreach (var child in domSection.Children)
                    {
                        child.Parent = newChild;
                        newChild.Children.Add(child);
                    }

                    domSection.Children.Clear();
                    domSection.Children.Add(newChild);
                    domSection.Value = newChild.UniqueKey;
                }

                domSections.Add(domSection);
            }

            return new DomDocument(domSections);
        }

        private List<string> GetDocumentSections(string markdown)
        {
            return Regex.Split(markdown, @"(^#+[^#]+?(\n|\r\n))|(```.*(\r\n|\n)(.|\s)+?```)|(\r\n\r\n)|(\n\n)", RegexOptions.Multiline)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x.Replace("\t", "    ").Trim('\r', '\n')) // Replace all tabs with 4 spaces
                .ToList();
        }

        internal DomItem ParseRecursive(DomItem parent, bool lookingForSiblings = false)
        {
            string markdownInput = parent.Value;
            DomItem domItem = _matchRules.GetEarliestMatch(markdownInput, parent, recursiveParser: this)?.DoTransform();

            if (domItem == null)
            {
                return parent;
            }

            parent.Children.Add(domItem);
            parent.Value = parent.Value.Replace(domItem.FullMatchValue, domItem.UniqueKey);

            if (!domItem.IsPreFormatted)
            {
                ParseRecursive(domItem);
            }

            ParseRecursive(parent, lookingForSiblings: true);

            return parent;
        }
    }
}
