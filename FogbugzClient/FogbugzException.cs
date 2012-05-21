using System;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Fourth.Tradesimple.Fogbugz
{
    [Serializable]
    public class FogbugzException : Exception
    {
        public FogbugzException()
        {
        }

        public FogbugzException(string message) : base(message)
        {
        }

        public FogbugzException(XElement response) : base(ParseMessageFromResponse(response))
        {
        }

        private static string ParseMessageFromResponse(XElement response)
        {
            XElement messageElement = response.XPathSelectElement("/response/error");
            if (messageElement != null)
            {
                return messageElement.Value;    
            }

            return null;
        }

        public FogbugzException(string message, Exception inner) : base(message, inner)
        {
        }

        protected FogbugzException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
    }
}
