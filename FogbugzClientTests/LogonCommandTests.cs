using Fourth.Tradesimple.Fogbugz;
using Should;
using Xunit;

namespace FogbugzClientTests
{
    public class LogonCommandTests
    {
        private LogonCommand command = new LogonCommand("simon.jefford@fourthhospitality.com", "password");

        [Fact]
        public void LogonCommand_has_a_fogbugz_command_name_of_logon()
        {
            this.command.FogbugzCommandName.ShouldEqual("logon");
        }

        [Fact]
        public void LogonCommand_passes_the_email_in_the_query_string()
        {
            string query = this.command.ToQueryString();
            query.ShouldContain("email=simon.jefford%40fourthhospitality.com");
        }

        [Fact]
        public void LogonCommand_passes_the_password_in_the_query_string()
        {
            string query = this.command.ToQueryString();
            query.ShouldContain("password=password");
        }
    }
}
