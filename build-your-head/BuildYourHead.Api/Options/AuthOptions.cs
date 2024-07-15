using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace BuildYourHead.Api.Options;

public static class AuthOptions
{
    public const string Issuer = "BuildYourHeadApi";
    public const string Audience = "BuildYourHeadClient";
    private const string Key = "mysupersecret_secretkey!123";
    public static readonly TimeSpan Lifetime = TimeSpan.FromMinutes(2);

    public static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
    }
}