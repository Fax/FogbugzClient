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
        public override string FogbugzCommandName
        {
            get { return "test"; }
        }
    }

    public class CommandTests
    {
        [Fact]
        public void fogbugz_command_requires_you_to_specify_a_command_name()
        {
            var command = new TestCommand();
            command.FogbugzCommandName.ShouldEqual("test");
        }
    }
}
