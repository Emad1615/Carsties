using Duende.IdentityServer.Models;

namespace IdentityService;

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
            new ApiScope("auctionApp","Auction app all access"),
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            // interactive client using code flow + pkce for postman testing
            new Client
            {
                ClientId = "postman",
                ClientName="Postman",
                ClientSecrets = { new Secret("NoASecret".Sha256()) },
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                RedirectUris = { "https://getpostman.com/oauth/callback" },
                AllowedScopes = { "openid", "profile", "auctionApp" }
            },
            // interactive client using code flow + pkce  for frontend application
            new Client{

                ClientName="nextApp",
                ClientId="nextApp",
                ClientSecrets={ new Secret("secret".Sha256())},
                AllowedGrantTypes=GrantTypes.CodeAndClientCredentials,
                RedirectUris={ "https://localhost:3000/api/auth/callback/id-server" },
                AllowedScopes={ "openid", "profile", "auctionApp" },
                AllowOfflineAccess=true,
                RequirePkce=false,
                AccessTokenLifetime=3600*24*30

            }
        };
}
