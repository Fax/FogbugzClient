using System;
using System.Net.Http;
using System.Xml.Linq;

namespace Fourth.Tradesimple.Fogbugz
{
    public class FogbugzHttpClient : IFogbugzHttpClient
    {
        private Uri baseUri;

        public FogbugzHttpClient(string baseUri)
        {
            this.baseUri = new Uri(baseUri);
        }

        public XDocument ExecuteQuery(string query)
        {
            HttpClient client = new HttpClient();
            UriBuilder builder = new UriBuilder(this.baseUri);
            builder.Query = query;

            XDocument doc = null;

            client.GetAsync(builder.Uri).ContinueWith(task =>
            {
                HttpResponseMessage message = task.Result;
                message.EnsureSuccessStatusCode();
                message.Content.ReadAsStringAsync().ContinueWith(readTask =>
                {
                    string resultString = readTask.Result;
                    doc = XDocument.Parse(resultString);
                }).Wait();
            }).Wait();

            return doc;
        }
    }
}
