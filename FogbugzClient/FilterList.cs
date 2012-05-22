using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Fourth.Tradesimple.Fogbugz
{
    public class FilterList : Collection<Filter>
    {
        public FilterList(XDocument filterDoc)
        {
            this.AddRange(filterDoc.XPathSelectElements("/response/filters/filter")
                                   .Select(e => new Filter(e)));
        }

        public void AddRange(IEnumerable<Filter> filters)
        {
            foreach (var filter in filters)
            {
                this.Add(filter);
            }
        }

        public Filter CurrentFilter { get; private set; }

        protected override void InsertItem(int index, Filter item)
        {
            if (item.IsCurrent)
            {
                this.CurrentFilter = item;
            }

            base.InsertItem(index, item);
        }
    }
}
