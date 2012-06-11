using System;
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

        protected override void AddCommandSpecificParameters(IDictionary<string, string> parameters)
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
        private TestAuthorisedCommand command = new TestAuthorisedCommand();

        public AuthorisedCommandTests()
        {
            this.command.Token = "sometoken";
        }

        [Fact]
        public void Authorisedcommand_must_require_a_token()
        {
            var exception = Assert.Throws<InvalidOperationException>(() =>
            {
                this.command.Token = null;
                this.command.ToQueryString();
            });

            exception.Message.ShouldEqual("No token was provided.");
        }

        [Fact]
        public void Authorisedcommand_includes_the_token_in_the_query_string()
        {
            this.command.ToQueryString().ShouldEqual("token=sometoken&cmd=testauthorised");
        }

        [Fact]
        public void Authorisedcommand_still_includes_other_parameters_in_the_query_string()
        {
            this.command.SomeOtherParam = "42";
            this.command.ToQueryString().ShouldEqual("someotherparam=42&token=sometoken&cmd=testauthorised");
        }
    }
}
