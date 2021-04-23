using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceShop.Backend.IdentityServer
{
    public class IdentityServerConfig
    {

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
             new ApiScope[]
             {
                  new ApiScope("rookieshop.api", "Rookie Shop API")
             };
        public static IEnumerable<Client> GetClients(IConfiguration configuration) {
           return new List<Client>
            {
                // machine to machine client
                new Client
                {
                    ClientId = "client",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    // scopes that client has access to
                    AllowedScopes = { "rookieshop.api" }
                },

                // interactive ASP.NET Core MVC client
                new Client
                {
                    ClientId = "mvc",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,

                    RedirectUris = { configuration["IdentityServerConfig:Clients:MVC:RedirectUris"] },

                    PostLogoutRedirectUris = {configuration["IdentityServerConfig:Clients:MVC:PostLogoutRedirectUris"] },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "rookieshop.api"
                    }
                },
                new Client
                    {
                        ClientId = "react",
                        ClientSecrets = { new Secret("secret".Sha256()) },

                        AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                        AllowedScopes = new List<string>
                        {
                            IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Profile,
                            "rookieshop.api"
                        },

                        // AccessTokenLifetime = 90,
                        AllowOfflineAccess = true,
                },
                 new Client
                {
                    ClientId = "swagger",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,

                    RequireConsent = false,
                    RequirePkce = true,

                    RedirectUris =           { configuration["IdentityServerConfig:Clients:Swagger:RedirectUris"] },
                    PostLogoutRedirectUris = {  configuration["IdentityServerConfig:Clients:Swagger:PostLogoutRedirectUris"] },
                    AllowedCorsOrigins =     {  configuration["IdentityServerConfig:Clients:Swagger:AllowedCorsOrigins"] },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "rookieshop.api"
                    }
                }
            };
        }
           
    }
}
