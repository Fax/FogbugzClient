namespace Fourth.Tradesimple.Fogbugz
{
    public abstract class AuthorisedFogbugzCommand : FogbugzCommand
    {
        public abstract string Token { get; }
    }
}
