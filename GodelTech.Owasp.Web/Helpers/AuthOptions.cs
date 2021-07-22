using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace GodelTech.Owasp.Web.Helpers
{
    public class AuthOptions
    {
        public const string Issuer = "MyAuthServer";
        public const string Audience = "MyAuthClient";
        const string Key = "mysupersecret_secretkey!123";
        public const int Lifetime = 1; //token lifetime - 1 min
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}