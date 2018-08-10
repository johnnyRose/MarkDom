using MarkDom.Transforms;
using System.Collections.Generic;
using System.Linq;

namespace MarkDom.Dom
{
    public class DomDocument : IHtmlTransform
    {
        public List<DomItem> DomSections { get; }

        public DomDocument(List<DomItem> domSections)
        {
            this.DomSections = domSections;
        }

        public string ToHtml()
        {
            return string.Join(string.Empty, this.DomSections.Select(x => x.ToHtml()));
        }
    }
}
