using System.Xml.Linq;

namespace Fourth.Tradesimple.Fogbugz
{
    public interface IFogbugzHttpClient
    {
        XDocument ExecuteQuery(string query);
    }
}
