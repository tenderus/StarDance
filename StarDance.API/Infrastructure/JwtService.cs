using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace StarDance.API.Infrastructure;

public class JwtService
{
    public string Generate(string login, string role)
    {
        var symmetricSecurityKey = AuthOptions.GetSymmetricSecurityKey();
        var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
        var header = new JwtHeader(credentials);

        var payload = new JwtPayload(AuthOptions.ISSUER, AuthOptions.AUDIENCE, GetClaims(login, role).Claims, DateTime.Now, DateTime.Today.AddDays(1));
        var securityToken = new JwtSecurityToken(header, payload);
        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }

    private ClaimsIdentity GetClaims(string login, string role)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, login),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, role),
        };
        ClaimsIdentity claimsIdentity =
            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
        return claimsIdentity;
    }
}
