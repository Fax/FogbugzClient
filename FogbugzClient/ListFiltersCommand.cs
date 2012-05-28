using System.Collections.Generic;
using System.Xml.Linq;

namespace Fourth.Tradesimple.Fogbugz
{
    public class ListFiltersCommand : AuthorisedFogbugzCommand
    {
        public ListFiltersCommand(string token) : base(token)
        {
        }

        public override string FogbugzCommandName
        {
            get { return "listFilters"; }
        }

        protected override void AddCommandSpecificParameters(IDictionary<string, string> parameters)
        {
        }

        public FilterList CreateFilterList(XDocument filterXml)
        {
            return new FilterList(filterXml);
        }
    }
}
