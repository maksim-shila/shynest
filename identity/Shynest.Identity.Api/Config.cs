using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Shynest.Identity.Api;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new(name: "build-your-head-api", displayName: "Build Your Head Api")
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            new()
            {
                ClientId = "build-your-head-client",
                AllowedGrantTypes = GrantTypes.Code,
                ClientSecrets = { new Secret("TODO_hide_this".Sha256()) },
                RedirectUris = { "http://localhost:3000/" },
                PostLogoutRedirectUris = { "http://localhost:3000/" },
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "build-your-head-api"
                }
            }
        };
}