using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fourth.Tradesimple
{
    public abstract class FogbugzCommand
    {
        public abstract string FogbugzCommandName { get;  }

        public string ToQueryString()
        {
            return string.Format("cmd={0}", this.FogbugzCommandName);
        }
    }
}
