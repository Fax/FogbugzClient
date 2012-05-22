using System.Collections.Generic;

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

        protected override void AddParameters(IDictionary<string, string> parameters)
        {
        }
    }
}
