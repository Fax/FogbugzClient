using System.Xml.Linq;
using Fourth.Tradesimple.Fogbugz;
using Should;
using Xunit;

namespace FogbugzClientTests
{
    public class FilterTests
    {
        private Filter builtinFilter;

        public FilterTests()
        {
            var element = new XElement("filter",
                                new XAttribute("type", "builtin"),
                                new XAttribute("sFilter", "ex349"),
                                "My Cases");
            this.builtinFilter = new Filter(element);
        }

        [Fact]
        public void Filter_extracts_the_type_from_an_XElement_containing_some_filter_xml()
        {
            this.builtinFilter.Type.ShouldEqual("builtin");
        }

        [Fact]
        public void Filter_extracts_the_description_from_an_XElement_containing_some_filter_xml()
        {
            this.builtinFilter.Description.ShouldEqual("My Cases");
        }

        [Fact]
        public void Filter_extracts_the_fogbugz_descriptor_from_an_XElement_containing_some_filter_xml()
        {
            this.builtinFilter.FogbugzFilterDescriptor.ShouldEqual("ex349");
        }
    }
}
