using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace StarDance.API.Infrastructure;

public class AuthOptions
{
    public const string ISSUER = "StarDanceApp";
        public const string AUDIENCE = "StarDanceClient";
        private const string KEY = "7VVUSPVGSC22QC374M6Y!";

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }