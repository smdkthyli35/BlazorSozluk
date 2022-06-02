using BlazorSozluk.WebApp.Infrastructure.Services;
using BlazorSozluk.WebApp.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.WebApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddHttpClient("WebApiClient", client =>
            {
                client.BaseAddress = new Uri("https://localhost:5001");
            });

            builder.Services.AddScoped(sp =>
            {
                var clientFactory = sp.GetRequiredService<IHttpClientFactory>();
                return clientFactory.CreateClient("WebApiClient");
            });

            builder.Services.AddTransient<IVoteService, VoteService>();
            builder.Services.AddTransient<IEntryService, EntryService>();

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            await builder.Build().RunAsync();
        }
    }
}
