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

                var discoveryUrl = "https://localhost:6000";
                var clientId = "WidgetClient";
                var clientSecret = "p@ssw0rd";
                var discoveryClient = new DiscoveryClient(discoveryUrl);
                var discoveryResponse = await discoveryClient.GetAsync();
                if (discoveryResponse.IsError)
                {
                    throw new Exception("Failed to get discovery response!");
                }

                Console.WriteLine("Calling IdentityServer4 authorize endpoint to get request token...");
                Console.WriteLine(Environment.NewLine);

                var scope = "widgetapi";
                var tokenClient = new TokenClient(discoveryResponse.TokenEndpoint, clientId, clientSecret);
                var tokenResponse = await tokenClient.RequestClientCredentialsAsync(scope);
                if (tokenResponse.IsError)
                {
                    throw new Exception("Authentication failed!");
                }

                Console.WriteLine("Calling Ocelot endpoint with bearer token to get widgets...");
                Console.WriteLine(Environment.NewLine);

                var uri = "https://localhost:7000/venda/api/values";
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
    //public class Program
    //{
    //    private static async Task Main()
    //    {
    //        // discover endpoints from metadata
    //        var client = new HttpClient();

    //        var disco = await client.GetDiscoveryDocumentAsync("http://localhost:6000");
    //        if (disco.IsError)
    //        {
    //            Console.WriteLine(disco.Error);
    //            return;
    //        }

    //        // request token
    //        var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
    //        {
    //            Address = disco.TokenEndpoint,
    //            ClientId = "client", //Seria como o login 
    //            ClientSecret = "secret", //Seria como a senha

    //            Scope = "api1" // Client diz qual api quer acessar
    //        });

    //        if (tokenResponse.IsError)
    //        {
    //            Console.WriteLine(tokenResponse.Error);
    //            return;
    //        }

    //        Console.WriteLine(tokenResponse.Json);
    //        Console.WriteLine("\n\n");

    //        // call api
    //        var apiClient = new HttpClient();
    //        apiClient.SetBearerToken(tokenResponse.AccessToken);

    //        var response = await apiClient.GetAsync("http://localhost:5001/identity");
    //        if (!response.IsSuccessStatusCode)
    //        {
    //            Console.WriteLine(response.StatusCode);
    //        }
    //        else
    //        {
    //            var content = await response.Content.ReadAsStringAsync();
    //            Console.WriteLine(JArray.Parse(content));
    //        }
    //    }
    //}
}
