using System.Linq;
using System.Text.RegularExpressions;

namespace MarkDom.Dom.Lists
{
    public class MarkdownList : DomItem
    {
        public bool IsOrdered { get; set; }

        public string ItemDelimiterRegex { get; set; }

        public override bool IsBlockLevelElement => true;

        protected internal override bool IsPreFormatted => true;

        public MarkdownList(MarkdownMatch match)
            : base(match)
        {
            var itemDelimiterRegex = match.Groups["listitemdelimiter"].Value;
            char firstNonWhiteSpaceCharacter = itemDelimiterRegex.Trim()[0];
            this.IsOrdered = char.IsNumber(firstNonWhiteSpaceCharacter);

            if (this.IsOrdered)
            {
                itemDelimiterRegex = itemDelimiterRegex
                    .Replace(firstNonWhiteSpaceCharacter.ToString(), @"\d+")
                    .Replace(".", @"\.")
                    .Replace(")", @"\)");
            }
            else
            {
                itemDelimiterRegex = itemDelimiterRegex.Replace("*", @"\*");
            }

            string rawListMarkdown = Regex.Replace(match.MatchValue, @"^" + itemDelimiterRegex, string.Empty);
            this.ItemDelimiterRegex = @"(\r\n|\n)+" + itemDelimiterRegex;

            this.Children = Regex.Split(rawListMarkdown, this.ItemDelimiterRegex)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => (DomItem)new MarkdownListItem(x, parent: this, recursiveParser: match.RecursiveParser))
                .ToList();

            this.Value = string.Join(string.Empty, this.Children.Select(x => x.UniqueKey));
        }

        public override string ToHtml()
        {
            var childHtml = BuildChildHtml();
            return IsOrdered ? ($"<ol>{childHtml}</ol>") : ($"<ul>{childHtml}</ul>");
        }
    }
}
