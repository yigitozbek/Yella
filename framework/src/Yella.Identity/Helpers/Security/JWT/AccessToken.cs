namespace Yella.Identity.Helpers.Security.JWT;

public class AccessToken
{
    public AccessToken(string token, DateTime expiration)
    {
        Token = token;
        Expiration = expiration;
    }

    public string Token { get; init; }
    public DateTime Expiration { get; init; }
}