using System.Collections.Generic;

namespace Fourth.Tradesimple.Fogbugz
{
    public class LogonCommand : FogbugzCommand
    {
        public override string FogbugzCommandName
        {
            get { return "logon"; }
        }

        protected override void AddParameters(IDictionary<string, string> parameters)
        {
            parameters.Add("email", this.Email);
            parameters.Add("password", this.Password);
        }

        public string Email { get; private set; }

        public string Password { get; private set; }

        public LogonCommand(string email, string password)
        {
            this.Email = email;
            this.Password = password;
        }
    }
}
