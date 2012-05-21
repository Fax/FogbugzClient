using System.Xml.Linq;
using System.Xml.XPath;

namespace Fourth.Tradesimple.Fogbugz
{
    public class FogbugzClient
    {
        public string Token { get; private set; }

        private IFogbugzHttpClient httpClient;

        public FogbugzClient(IFogbugzHttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public void Logon(string email, string password)
        {
            LogonCommand command = new LogonCommand(email, password);
            XDocument response = this.ExecuteCommand(command);
            XElement element = response.XPathSelectElement("//token");
            this.Token = element.Value;
        }

        public XDocument ExecuteCommand(FogbugzCommand command)
        {
            return this.httpClient.ExecuteQuery(command.ToQueryString());
        }
    }
}
