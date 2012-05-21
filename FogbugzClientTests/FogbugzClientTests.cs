using System.Xml.Linq;
using Fourth.Tradesimple.Fogbugz;
using Moq;
using Should;
using Xunit;

namespace FogbugzClientTests
{
    public class FogbugzClientTests
    {
        [Fact]
        public void FogbugzClient_saves_the_token_from_logging_on()
        {
            Mock<IFogbugzHttpClient> clientStub = new Mock<IFogbugzHttpClient>();
            var response = XDocument.Parse("<response><token>24dsg34lok43un23</token></response>");
            clientStub.Setup(c => c.ExecuteQuery(It.IsAny<string>())).Returns(response);
            var client = new FogbugzClient(clientStub.Object);

            client.Logon("simon.jefford@gmail.com", "password");

            client.Token.ShouldEqual("24dsg34lok43un23");
        }
    }
}
