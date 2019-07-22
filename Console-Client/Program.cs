using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Console_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Test client for Ocelot, IdentityServer4, and an internal ASP.NET Core 2.1 API");
            Console.WriteLine(Environment.NewLine);

            MainAsync().GetAwaiter().GetResult();
        }

        private static async Task MainAsync()
        {
            try
            {
                Console.WriteLine("Calling IdentityServer4 discovery endpoint...");
                Console.WriteLine(Environment.NewLine);

                var discoveryUrl = "http://localhost:5555/";
                var clientId = "thwyster";
                var clientSecret = "123456";
                var discoveryClient = new DiscoveryClient(discoveryUrl);
                var discoveryResponse = await discoveryClient.GetAsync();
                if (discoveryResponse.IsError)
                {
                    throw new Exception("Failed to get discovery response! - " + discoveryResponse.Error);
                }

                Console.WriteLine("Calling IdentityServer4 authorize endpoint to get request token...");
                Console.WriteLine(Environment.NewLine);

                var scope = "vendaApi";
                var tokenClient = new TokenClient(discoveryResponse.TokenEndpoint, clientId, clientSecret);
                var tokenResponse = await tokenClient.RequestClientCredentialsAsync(scope);
                if (tokenResponse.IsError)
                {
                    throw new Exception("Authentication failed!");
                }

                Console.WriteLine("Calling Ocelot endpoint with bearer token to get widgets...");
                Console.WriteLine(Environment.NewLine);

                var uri = "http://localhost:5000/venda/api/values";
                var gatewayClient = new HttpClient();
                gatewayClient.SetBearerToken(tokenResponse.AccessToken);
                var response = await gatewayClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Received HTTP 200 from API. Writing widgets to console...");
                    Console.WriteLine(Environment.NewLine);

                    var content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(content);
                }
                else
                {
                    Console.WriteLine("Response was unsuccessful with status code: " + response.StatusCode);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(Environment.NewLine);
            }

            Console.WriteLine("Press ENTER to quit.");
            Console.ReadLine();
        }
    }
}
