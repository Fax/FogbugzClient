using Fourth.Tradesimple.Fogbugz;
using Should;
using Xunit;
using System.Xml.Linq;

namespace FogbugzClientTests
{
    public class ListFiltersCommandTests
    {
        [Fact]
        public void ListFiltersCommand_has_a_command_name_of_listFilters()
        {
            ListFiltersCommand command = new ListFiltersCommand("sometoken");
            command.FogbugzCommandName.ShouldEqual("listFilters");
        }

        [Fact]
        public void ListFiltersCommand_has_a_conversion_func_that_will_provide_a_filterlist()
        {
            XDocument filterDoc = new XDocument(
                new XElement("response",
                    new XElement("filters",
                        new XElement("filter",
                                        new XAttribute("type", "builtin"),
                                        new XAttribute("sFilter", "ex349"),
                                        "My Cases"),
                        new XElement("filter",
                                        new XAttribute("type", "saved"),
                                        new XAttribute("sFilter", "304"),
                                        new XAttribute("status", "current"),
                                        "Cases I should have closed months ago"))));

            var result = new ListFiltersCommand("sometoken").CreateFilterList(filterDoc);
            result.Count.ShouldEqual(2);
        }
    }
}
