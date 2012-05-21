using System.Collections.Generic;

namespace Fourth.Tradesimple.Fogbugz
{
    public abstract class AuthorisedFogbugzCommand : FogbugzCommand
    {
        public abstract string Token { get; }

        protected override void AfterParametersAdded(IDictionary<string, string> parameters)
        {
            parameters.Add("token", this.Token);
        }
    }
}
