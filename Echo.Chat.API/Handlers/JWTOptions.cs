namespace DomainCoreApi.Handlers;

public class JWTOptions
{
    public const string Position = "JwtSettings";

    public string Issuer { get; set; } = String.Empty;
    public string Audience { get; set; } = String.Empty;
    public List<string> Audiences { get; set; } = new List<string>();
    public int DefaultRefreshTokenLifeTimeDays { get; set; } = 0;
    public int DefaultAccessTokenLifeTimeHours { get; set; } = 0;
    public string Key { get; set; } = String.Empty;
    /*
     * "Issuer": "Https://localhost:7269/api",
        "Audience": "https://localhost:7283/",
        "DefaultRefreshTokenLifeTimeDays": 14, //14 days aka 336 hours
        "DefaultAccessTokenLifeTimeHours": 1, //1 hour
        "Key": "WeLoveBigBeautifulWomanAndWeCantLie"//You should not save the key here it should be save at a server that's can handle toke key
     */
}
