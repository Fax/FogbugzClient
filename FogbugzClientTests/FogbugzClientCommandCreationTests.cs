using Fourth.Tradesimple.Fogbugz;
using Moq;
using Should;
using Xunit;

namespace FogbugzClientTests
{
    public class FogbugzClientCommandCreationTests
    {
        [Fact]
        public void FogbugzClient_can_create_command_objects()
        {
            var client = new FogbugzClient(new Mock<IFogbugzHttpClient>().Object);
            FogbugzCommand command = client.CreateCommand<ListFiltersCommand>();
            command.ShouldBeType<ListFiltersCommand>();
        }
    }
}
