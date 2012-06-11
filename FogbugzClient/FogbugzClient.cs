using System;
using System.Xml.Linq;
using System.Xml.XPath;
using Ninject;
using Ninject.Parameters;

namespace Fourth.Tradesimple.Fogbugz
{
    public class FogbugzClient
    {
        private IFogbugzHttpClient httpClient;

        private XElement errorElement;

        private IKernel container;

        public FogbugzClient(IFogbugzHttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.container = new StandardKernel();
        }

        public FogbugzClient(string baseUri) : this(new FogbugzHttpClient(baseUri))
        {
        }

        public string Logon(string email, string password)
        {
            LogonCommand command = new LogonCommand(email, password);
            XDocument response = this.ExecuteCommand(command);
            XElement element = response.XPathSelectElement("//token");

            return element.Value;
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
                throw new FogbugzException("An error occurred while communicating with Fogbugz.", e);
            }

            if (this.ResponseContainsError(response))
            {
                throw new FogbugzException(this.errorElement);
            }

            return response;
        }

        public TFormatted ExecuteCommand<TFormatted>(FogbugzCommand command, Func<XDocument, TFormatted> conversionFunc)
        {
            var response = this.ExecuteCommand(command);
            return conversionFunc(response);
        }

        public FogbugzCommand CreateCommand<TCommand>() where TCommand : FogbugzCommand
        {
            return this.container.Get<TCommand>();
        }
    }
}
