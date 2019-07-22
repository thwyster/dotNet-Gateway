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
        private const string ClientResource = "vendaApi";

        // scopes define the API resources in your system
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource(ClientResource, "VendaApi")
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
}
