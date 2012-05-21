using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Fourth.Tradesimple
{
    public abstract class FogbugzCommand
    {
        public abstract string FogbugzCommandName { get;  }

        protected abstract void AddParameters(IDictionary<string, string> parameters);

        protected virtual void AfterParametersAdded(IDictionary<string, string> parameters)
        {
        }

        public string ToQueryString()
        {
            Dictionary<string, string> paramsDictionary = this.GetParameters();
            var paramstring = ConvertParametersToQuery(paramsDictionary);
            string query = string.Format("cmd={0}", this.FogbugzCommandName);
            return string.Join("&", query, paramstring);
        }

        private static string ConvertParametersToQuery(Dictionary<string, string> paramsDictionary)
        {
            var parameterPairs = paramsDictionary.Select(kvp => string.Format("{0}={1}", kvp.Key, HttpUtility.UrlEncode(kvp.Value)));
            var paramstring = string.Join("&", parameterPairs);
            return paramstring;
        }

        private Dictionary<string, string> GetParameters()
        {
            Dictionary<string, string> paramsDictionary = new Dictionary<string, string>();
            this.AddParameters(paramsDictionary);
            this.AfterParametersAdded(paramsDictionary);
            return paramsDictionary;
        }
    }
}
