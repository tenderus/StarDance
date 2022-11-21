using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace StarDance.API.Infrastructure;

public class AuthOptions
{
    public const string Issuer = "StarDanceApp";
    public const string Audience = "StarDanceClient";
    private const string Key = "7VVUSPVGSC22QC374M6Y!";

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
}