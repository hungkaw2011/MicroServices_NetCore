using IdentityModel;
using System.Security.Claims;
using static System.Net.WebRequestMethods;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;
using Duende.IdentityServer;

namespace IdentityServer
{
    public class Config
    {
        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                   new Client
                   {
                        ClientId = "discountClient",
                        AllowedGrantTypes = GrantTypes.ClientCredentials,
                        ClientSecrets =
                        {
                            new Secret("toiyeuem_secret".Sha256())
                        },
                        AllowedScopes = { "discountAPI" }
                   },
                   new Client
                   {
                       ClientId = "webapp_mvc_client",
                       ClientName = "MVC Web App",
                       AllowedGrantTypes = GrantTypes.Hybrid,
                       RequirePkce = false,
                       AllowRememberConsent = false,
                       RedirectUris = new List<string>()
                       {
                           "https://localhost:5002/signin-oidc",
                       },
                       PostLogoutRedirectUris = new List<string>()
                       {
                           "https://localhost:5002/signout-callback-oidc"
                       },
                       ClientSecrets = new List<Secret>
                       {
                           new Secret("toiyeuem_secret".Sha256())
                       },
                       AllowedScopes = new List<string>
                       {
                           IdentityServerConstants.StandardScopes.OpenId,
                           IdentityServerConstants.StandardScopes.Profile,
                           IdentityServerConstants.StandardScopes.Address,
                           IdentityServerConstants.StandardScopes.Email,
                           "discountAPI",
                           "roles"
                       }
                   }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
           new ApiScope[]
           {
               new ApiScope("discountAPI", "Discount API")
           };

        public static IEnumerable<ApiResource> ApiResources =>
          new ApiResource[]
          {
               new ApiResource("discountAPI", "Discount API"),
          };

        public static IEnumerable<IdentityResource> IdentityResources =>
          new IdentityResource[]
          {
              new IdentityResources.OpenId(),
              new IdentityResources.Profile(),
              new IdentityResources.Address(),
              new IdentityResources.Email(),
              new IdentityResource(
                    "roles",
                    "Your role(s)",
                    new List<string>() { "role" })
          };
    }
}
