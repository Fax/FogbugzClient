﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Fourth.Tradesimple.Fogbugz
{
    public abstract class FogbugzCommand
    {
        public abstract string FogbugzCommandName { get;  }

        protected abstract void AddCommandSpecificParameters(IDictionary<string, string> parameters);

        protected virtual void AddGenericParameters(IDictionary<string, string> parameters)
        {
        }

        public string ToQueryString()
        {
            Dictionary<string, string> paramsDictionary = this.GetParameters();
            paramsDictionary.Add("cmd", this.FogbugzCommandName);
            return ConvertParametersToQuery(paramsDictionary);
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
            this.AddCommandSpecificParameters(paramsDictionary);
            this.AddGenericParameters(paramsDictionary);
            return paramsDictionary;
        }
    }
}
