using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphClient
{
    class Program
    {
        private const string _clientId = "";
        private const string _tenantId = "";

        public static async Task Main(string[] args)
        {
            var app = PublicClientApplicationBuilder
                        .Create(_clientId)
                        .WithAuthority(AzureCloudInstance.AzurePublic, _tenantId)
                        .WithRedirectUri("http://localhost")
                        .Build();

            var scopes = new List<string>
            {
                "user.read"
            };

            var result = await app.AcquireTokenInteractive(scopes).ExecuteAsync();

            Console.WriteLine($"Token:\t{result.AccessToken}");

            var provider = new DeviceCodeProvider(app, scopes);

            var client = new GraphServiceClient(provider);

            var profile = await client.Me.Request().GetAsync();

            Console.WriteLine($"Name:\t{profile.DisplayName}");
            Console.WriteLine($"ADD Id:\t{profile.Id}");
        }
    }
}
