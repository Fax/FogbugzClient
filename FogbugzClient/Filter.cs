using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Fourth.Tradesimple.Fogbugz
{
    public class Filter
    {
        public string FogbugzFilterDescriptor { get; private set; }

        public string Type { get; private set; }

        public string Description { get; private set; }

        public bool IsCurrent { get; private set; }

        public Filter(XElement source)
        {
            this.Type = source.Attribute("type").Value;
            this.Description = source.Value;
            this.FogbugzFilterDescriptor = source.Attribute("sFilter").Value;
            var statusAttribute = source.Attribute("status");
            if (statusAttribute != null)
            {
                this.IsCurrent = statusAttribute.Value == "current";
            }
        }
    }
}
