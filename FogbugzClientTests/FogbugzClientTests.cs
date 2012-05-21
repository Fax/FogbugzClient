using System.Xml.Linq;
using Fourth.Tradesimple.Fogbugz;
using Moq;
using Should;
using Xunit;

namespace FogbugzClientTests
{
    public class FogbugzClientTests
    {
        private FogbugzClient client;

        private static FogbugzClient SetupStubbedClient(string responseString)
        {
            Mock<IFogbugzHttpClient> clientStub = new Mock<IFogbugzHttpClient>();
            var response = XDocument.Parse(responseString);
            clientStub.Setup(c => c.ExecuteQuery(It.IsAny<string>())).Returns(response);
            return new FogbugzClient(clientStub.Object);
        }

        [Fact]
        public void FogbugzClient_saves_the_token_from_logging_on()
        {
            this.client = SetupStubbedClient("<response><token>24dsg34lok43un23</token></response>");

            this.client.Logon("simon.jefford@gmail.com", "password");

            this.client.Token.ShouldEqual("24dsg34lok43un23");
        }

        [Fact]
        public void FogbugzClient_throws_a_properly_populated_FogbugzException_when_logon_fails()
        {
            this.client = SetupStubbedClient("<response><error code=\"1\">ERROR!</error></response>");

            var exception = Assert.Throws<FogbugzException>(() => this.client.Logon("simon.jefford@gmail.com", "password"));
            exception.Message.ShouldEqual("ERROR!");
        }
    }
}
