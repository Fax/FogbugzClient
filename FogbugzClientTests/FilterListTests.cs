using System.Xml.Linq;
using Fourth.Tradesimple.Fogbugz;
using Should;
using Xunit;

namespace FogbugzClientTests
{
    public class FilterListTests
    {
        private FilterList list;

        public FilterListTests()
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

            this.list = new FilterList(filterDoc);
        }

        [Fact]
        public void FilterList_extracts_contained_filters_from_the_returned_xml()
        {
            this.list.Count.ShouldEqual(2);
            this.list[0].FogbugzFilterDescriptor.ShouldEqual("ex349");
            this.list[1].FogbugzFilterDescriptor.ShouldEqual("304");
        }

        [Fact]
        public void FilterList_tracks_the_active_filter()
        {
            this.list.CurrentFilter.ShouldBeSameAs(this.list[1]);
        }
    }
}
