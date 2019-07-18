using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer
{
    public class Config
    {
        private const string ClientUsername = "thwyster";
        private const string ClientPassword = "123456";
        private const string ClientResource = "catalogoapi";

        // scopes define the API resources in your system
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource(ClientResource, "CatalogoApi")
            };
        }

        // clients want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients()
        {
            // client credentials client
            return new List<Client>
            {
                new Client
                {
                    ClientId = ClientUsername,
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    ClientSecrets =
                    {
                        new Secret(ClientPassword.Sha256())
                    },
                    AllowedScopes = { ClientResource }
                }
            };
        }
    }

    //public static class Config
    //{
    //    public static IEnumerable<IdentityResource> GetIdentityResources()
    //    {
    //        return new IdentityResource[]
    //        {
    //            new IdentityResources.OpenId()
    //        };
    //    }

    //    public static IEnumerable<ApiResource> GetApis()
    //    {
    //        return new List<ApiResource>
    //        {
    //            new ApiResource("api1", "Primeira Api")
    //        };
    //    }

    //    public static IEnumerable<Client> GetClients()
    //    {
    //        return new List<Client>
    //        {
    //            //Criando um novo cliente para acessar a aplicação
    //            new Client
    //            {
    //                ClientId = "client", //Seria como o username do login

    //                // no interactive user, use the clientid/secret for authentication
    //                AllowedGrantTypes = GrantTypes.ClientCredentials,

    //                // seria como o password do sistema
    //                ClientSecrets =
    //                {
    //                    new Secret("secret".Sha256())
    //                },

    //                // Identifica as api's que o cliente vai poder acessar
    //                AllowedScopes = { "api1" }
    //            }
    //        };
    //    }
    //}
}
