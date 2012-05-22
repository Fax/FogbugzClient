using System;
using System.Net;
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

        private static FogbugzClient SetupStubbedClient(Exception e)
        {
            Mock<IFogbugzHttpClient> clientStub = new Mock<IFogbugzHttpClient>();
            clientStub.Setup(c => c.ExecuteQuery(It.IsAny<string>())).Throws(e);
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
        public void FogbugzClient_throws_a_properly_populated_FogbugzException_when_logon_fails_at_the_server()
        {
            this.client = SetupStubbedClient("<response><error code=\"1\">ERROR!</error></response>");

            var exception = Assert.Throws<FogbugzException>(() =>
            {
                this.client.Logon("simon.jefford@gmail.com", "password");
            });
            exception.Message.ShouldEqual("ERROR!");
            exception.ErrorCode.ShouldEqual("1");
        }

        [Fact]
        public void FogbugzClient_throws_a_property_populated_FogbugzException_when_logon_fails_at_the_client()
        {
            WebException e = new WebException("Could not connect to Fogbugz");
            this.client = SetupStubbedClient(e);
            var exception = Assert.Throws<FogbugzException>(() =>
            {
                this.client.Logon("simon.jefford@gmail.com", "password");
            });
            exception.InnerException.ShouldBeSameAs(e);
            exception.Message.ShouldEqual("An error occurred while communicating with Fogbugz");
        }
    }
}
