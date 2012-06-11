using System.Collections.Generic;

namespace Fourth.Tradesimple.Fogbugz
{
    public abstract class AuthorisedFogbugzCommand : FogbugzCommand
    {
        public string Token { get; set; }

        public AuthorisedFogbugzCommand()
        {
        }

        protected override void AddGenericParameters(IDictionary<string, string> parameters)
        {
            parameters.Add("token", this.Token);
        }
    }
}
