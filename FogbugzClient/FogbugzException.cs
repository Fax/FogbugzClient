using System;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Fourth.Tradesimple.Fogbugz
{
    [Serializable]
    public class FogbugzException : Exception
    {
        public string ErrorCode { get; private set; }

        public FogbugzException()
        {
        }

        public FogbugzException(XElement response) : base(ParseMessageFromResponse(response))
        {
            var errorElement = response.XPathSelectElement("/response/error");
            if (errorElement != null)
            {
                var codeAttribute = errorElement.Attributes("code").FirstOrDefault();
                if (codeAttribute != null)
                {
                    this.ErrorCode = codeAttribute.Value;
                }
            }
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
