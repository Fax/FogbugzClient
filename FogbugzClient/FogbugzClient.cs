using System;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Fourth.Tradesimple.Fogbugz
{
    public class FogbugzClient
    {
        public string Token { get; private set; }

        private IFogbugzHttpClient httpClient;

        private XElement errorElement;

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

        private bool ResponseContainsError(XDocument response)
        {
            this.errorElement = response.XPathSelectElement("/response/error");
            return this.errorElement != null;
        }

        public XDocument ExecuteCommand(FogbugzCommand command)
        {
            XDocument response;
            try
            {
                response = this.httpClient.ExecuteQuery(command.ToQueryString());
            }
            catch (Exception e)
            {
                throw new FogbugzException("An error occurred while communicating with Fogbugz", e);
            }

            if (this.ResponseContainsError(response))
            {
                throw new FogbugzException(this.errorElement);
            }

            return response;
        }

        public T ExecuteCommand<T>(FogbugzCommand command, Func<XDocument, T> conversionFunc)
        {
            var response = this.ExecuteCommand(command);
            return conversionFunc(response);
        }
    }
}
