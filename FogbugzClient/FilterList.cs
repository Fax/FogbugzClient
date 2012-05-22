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
    }
}
