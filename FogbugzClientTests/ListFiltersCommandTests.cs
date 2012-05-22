using Fourth.Tradesimple.Fogbugz;
using Should;
using Xunit;

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
    }
}
