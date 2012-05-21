using Fourth.Tradesimple.Fogbugz;
using Should;
using Xunit;

namespace FogbugzClientTests
{
    public class TestAuthorisedCommand : AuthorisedFogbugzCommand
    {
        public override string FogbugzCommandName
        {
            get { return "testauthorised"; }
        }

        protected override void AddParameters(System.Collections.Generic.IDictionary<string, string> parameters)
        {
            throw new System.NotImplementedException();
        }

        public override string Token
        {
            get { return this.token; }
        }

        private string token;

        public TestAuthorisedCommand(string token)
        {
            this.token = token;
        }
    }

    public class AuthorisedCommandTests
    {
        [Fact]
        public void Authorisedcommand_must_require_a_token()
        {
            var command = new TestAuthorisedCommand("sometoken");
            command.Token.ShouldEqual("sometoken");
        }
    }
}
