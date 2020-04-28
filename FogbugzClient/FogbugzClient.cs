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
            this.Token = element.Value;
            return this.Token;
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
                var queryString = command.ToQueryString();
                // FR For the love of the coding god, the token! We have it in the client, why we are not adding it automatically to the querystring??
                if (!String.IsNullOrWhiteSpace(this.Token))
                {
                    queryString += $"&token={this.Token}";
                }

                response = this.httpClient.ExecuteQuery(queryString);

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

        public TCommand CreateCommand<TCommand>() where TCommand : AuthorisedFogbugzCommand
        {
            if (string.IsNullOrEmpty(this.Token))
            {
                throw new InvalidOperationException("You cannot create a command before logging in.");
            }

            var command = this.container.Get<TCommand>();
            command.Token = this.Token;
            return command;
        }

        public string Token { get; set; }
    }
}
