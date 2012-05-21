using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Fourth.Tradesimple;
using Should;

namespace FogbugzClientTests
{
    class TestCommand : FogbugzCommand
    {
        private string param1;

        private int param2;

        public TestCommand(string param1, int param2)
        {
            this.param1 = param1;
            this.param2 = param2;
        }

        protected override void AddParameters(IDictionary<string, string> parameters)
        {
            parameters.Add("param1", param1);
            parameters.Add("param2", param2.ToString());
        }

        public override string FogbugzCommandName
        {
            get { return "test"; }
        }
    }

    public class CommandTests
    {
        private TestCommand command;

        public CommandTests()
        {
            this.command = new TestCommand(param1: "foo", param2: 42);
        }

        [Fact]
        public void fogbugz_command_requires_you_to_specify_a_command_name()
        {
            this.command.FogbugzCommandName.ShouldEqual("test");
        }

        [Fact]
        public void fogbugz_command_will_create_a_query_string_containing_the_command_name()
        {
            string query = command.ToQueryString();
            query.ShouldContain("cmd=test");
        }

        [Fact]
        public void fogbugz_command_allows_you_to_add_parameters_to_the_produced_query_string()
        {
            string query = command.ToQueryString();
            query.ShouldEqual("cmd=test&param1=foo&param2=42");
        }

        [Fact]
        public void fogbugz_command_will_encode_its_parameters()
        {
            this.command = new TestCommand(param1: "value with \"space\"", param2: 42);
            string query = this.command.ToQueryString();
            query.ShouldEqual("cmd=test&param1=value+with+%22space%22&param2=42");
        }
    }
}
