using System.Xml.Linq;
using Fourth.Tradesimple.Fogbugz;
using Moq;
using Should;
using Xunit;

namespace FogbugzClientTests
{
    public class FogbugzClientExecutionTests
    {
        private static FogbugzClient SetupStubbedClient(string responseString)
        {
            Mock<IFogbugzHttpClient> clientStub = new Mock<IFogbugzHttpClient>();
            var response = XDocument.Parse(responseString);
            clientStub.Setup(c => c.ExecuteQuery(It.IsAny<string>())).Returns(response);
            return new FogbugzClient(clientStub.Object);
        }

        [Fact]
        public void FogbugzClient_allows_custom_formatting_of_the_response_into_objects()
        {
            var client = SetupStubbedClient("<response><someresponse>hello</someresponse></response>");
            var command = new Mock<FogbugzCommand>().Object;

            var result = client.ExecuteCommand(command, (doc) => doc.Root.ToString());

            result.ShouldContain("hello");
        }
    }
}
