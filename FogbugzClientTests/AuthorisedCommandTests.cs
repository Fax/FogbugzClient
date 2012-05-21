using System.Collections.Generic;
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

        public override string Token
        {
            get { return this.token; }
        }

        private string token;

        public TestAuthorisedCommand(string token)
        {
            this.token = token;
        }

        protected override void AddParameters(IDictionary<string, string> parameters)
        {
            if (!string.IsNullOrEmpty(this.SomeOtherParam))
            {
                parameters.Add("someotherparam", this.SomeOtherParam);
            }
        }

        public string SomeOtherParam { get; set; }
    }

    public class AuthorisedCommandTests
    {
        private TestAuthorisedCommand command = new TestAuthorisedCommand("sometoken");

        [Fact]
        public void Authorisedcommand_must_require_a_token()
        {
            this.command.Token.ShouldEqual("sometoken");
        }

        [Fact]
        public void Authorisedcommand_includes_the_token_in_the_query_string()
        {
            this.command.ToQueryString().ShouldEqual("cmd=testauthorised&token=sometoken");
        }

        [Fact]
        public void Authorisedcommand_still_includes_other_parameters_in_the_query_string()
        {
            this.command.SomeOtherParam = "42";
            this.command.ToQueryString().ShouldEqual("cmd=testauthorised&someotherparam=42&token=sometoken");
        }
    }
}
