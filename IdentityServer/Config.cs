using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId()
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1", "Primeira Api")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                //Criando um novo cliente para acessar a aplicação
                new Client
                {
                    ClientId = "client", //Seria como o username do login

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // seria como o password do sistema
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // Identifica as api's que o cliente vai poder acessar
                    AllowedScopes = { "api1" }
                }
            };
        }
    }
}
