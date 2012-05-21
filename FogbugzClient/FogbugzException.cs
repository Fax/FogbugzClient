using System;

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
