#nullable enable
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Yella.Framework.Identity.Entities;
using Yella.Framework.Identity.Extensions;
using Yella.Framework.Utilities.Security.Encryption;

namespace Yella.Framework.Identity.Helpers.Security.JWT;

public class JwtHelper<TUser, TRole> : ITokenHelper<TUser, TRole>
    where TUser : IdentityUser<TUser, TRole>
    where TRole : IdentityRole<TUser, TRole>
{
    private readonly TokenOptions _tokenOptions;
    private readonly DateTime _accessTokenExpiration;

    public JwtHelper(IConfiguration configuration)
    {
        _tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();
        _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
    }

    public AccessToken CreateToken(TUser user, IEnumerable<TRole> roles, IEnumerable<Permission<TUser, TRole>> permissions, List<Claim> claims)
    {
        var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
        var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
        var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, roles, permissions, claims);
        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var token = jwtSecurityTokenHandler.WriteToken(jwt);

        return new AccessToken(token, _accessTokenExpiration);
    }

    private JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, TUser user,
        SigningCredentials signingCredentials, IEnumerable<TRole> roles,
        IEnumerable<Permission<TUser, TRole>> permissions, List<Claim> claims)
    {
        if (signingCredentials == null)
            throw new ArgumentNullException(nameof(signingCredentials));

        var key = Encoding.ASCII.GetBytes(_tokenOptions.SecurityKey);

        var jwt = new JwtSecurityToken(
            issuer: tokenOptions.Issuer,
            notBefore: new DateTimeOffset(DateTime.Now).DateTime,
            audience: tokenOptions.Audience,
            expires: DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration),
            claims: SetClaims(user, roles, permissions, claims),
            signingCredentials: new SigningCredentials
                (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        );
        return jwt;
    }

     

    private static IEnumerable<Claim> SetClaims(TUser user, IEnumerable<TRole> roles,
        IEnumerable<Permission<TUser, TRole>> permissions, List<Claim> list)
    {
        var claims = new List<Claim>();
        claims.AddNameIdentifier(user.Id.ToString());
        claims.AddRoles(roles.Select(c => c.Name).ToArray());
        claims.AddPermissions(permissions.Select(c => c.Name).ToArray());
        claims.AddFullName($"{user.Name}  {user.Surname}");
        claims.AddUsername($"{user.UserName}");
        claims.AddEmail($"{user.Email}");

        if (claims is { Count: > 0 }) claims.AddRange(list);

        return claims;
    }

    public class TokenOptions
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int AccessTokenExpiration { get; set; }
        public string SecurityKey { get; set; }
    }

    public class SessionOption
    {
        public string CookieName { get; set; }
        public int IdleTimeout { get; set; }

    }
}