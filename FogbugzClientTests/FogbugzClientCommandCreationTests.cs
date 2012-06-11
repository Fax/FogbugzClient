using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Fourth.Tradesimple.Fogbugz;
using Moq;
using Should;
using Xunit;

namespace FogbugzClientTests
{
    public class CreationTestAuthorisedCommand : AuthorisedFogbugzCommand
    {
        public override string FogbugzCommandName
        {
            get { return "authcommand"; }
        }

        protected override void AddCommandSpecificParameters(IDictionary<string, string> parameters)
        {
        }
    }

    public class FogbugzClientCommandCreationTests
    {
        [Fact]
        public void FogbugzClient_can_create_command_objects()
        {
            var token = "24dsg34lok43un23";
            var client = SetupLoggedOnClient(token);
            FogbugzCommand command = client.CreateCommand<ListFiltersCommand>();
            command.ShouldBeType<ListFiltersCommand>();
        }

        private static FogbugzClient SetupLoggedOnClient(string token)
        {
            Mock<IFogbugzHttpClient> clientStub = new Mock<IFogbugzHttpClient>();
            var responseString = string.Format("<response><token>{0}</token></response>", token);
            var response = XDocument.Parse(responseString);
            clientStub.Setup(c => c.ExecuteQuery(It.IsAny<string>())).Returns(response);
            var client = new FogbugzClient(clientStub.Object);
            client.Logon("email", "password");
            return client;
        }

        [Fact]
        public void FogbugzClient_can_create_command_objects_configured_with_a_token()
        {
            var token = "24dsg34lok43un23";
            var client = SetupLoggedOnClient(token);
            var command = client.CreateCommand<CreationTestAuthorisedCommand>();
            command.Token.ShouldEqual(token);
        }

        [Fact]
        public void FogbugzClient_does_not_allow_the_creation_of_command_objects_if_not_logged_on()
        {
            var client = new FogbugzClient(new Mock<IFogbugzHttpClient>().Object);
            Assert.Throws<InvalidOperationException>(() =>
            {
                client.CreateCommand<CreationTestAuthorisedCommand>();
            });
        }
    }
}
