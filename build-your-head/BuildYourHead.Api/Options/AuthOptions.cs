using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BuildYourHead.Api.Options
{
    public class AuthOptions
    {
        public const string Issuer = "BuildYourHeadApi";
        public const string Audience = "BuildYourHeadClient";
        const string Key = "mysupersecret_secretkey!123";
        public static readonly TimeSpan Lifetime = TimeSpan.FromMinutes(2);

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}
