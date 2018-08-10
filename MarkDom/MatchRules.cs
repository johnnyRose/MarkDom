using MarkDom.Dom;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MarkDom
{
    public sealed class MatchRules : Dictionary<Regex, Func<MarkdownMatch, DomItem>>
    {
        internal MarkdownMatch GetEarliestMatch(string markdown, DomItem parent, MarkdownParser recursiveParser)
        {
            Match minPositionMatch = null;
            Func<MarkdownMatch, DomItem> minRule = null;

            foreach (KeyValuePair<Regex, Func<MarkdownMatch, DomItem>> rule in this)
            {
                Match match = rule.Key.Match(markdown);

                if (match.Success && (minPositionMatch == null || match.Index < minPositionMatch.Index))
                {
                    minPositionMatch = match;
                    minRule = rule.Value;
                }
            }

            if (minPositionMatch != null)
            {
                return new MarkdownMatch(markdown, minPositionMatch, minRule, parent, recursiveParser);
            }

            return null;
        }
    }
}
