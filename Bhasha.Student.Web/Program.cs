using System;
using System.Net.Http;
using System.Threading.Tasks;
using Bhasha.Student.Web.Services;
using LazyCache;
using MatBlazor;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Bhasha.Student.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<ISettingsProvider, SettingsProvider>();
            builder.Services.AddScoped<IStudentApiClient, StudentApiClient>();
            builder.Services.AddSingleton<IAppCache, CachingService>();
            builder.Services.AddMatBlazor();
            await builder.Build().RunAsync();
        }
    }
}