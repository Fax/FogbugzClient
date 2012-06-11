using System;
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
            if (string.IsNullOrEmpty(this.Token))
            {
                throw new InvalidOperationException();
            }

            parameters.Add("token", this.Token);
        }
    }
}
