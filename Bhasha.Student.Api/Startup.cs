using Bhasha.Common.Api;
using Bhasha.Student.Api.Services;
using LazyCache;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bhasha.Student.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSingleton<IAuthorizedProfileLookup, AuthorizedProfileLookup>()
                .AddSingleton<IAppCache, CachingService>()
                .AddBhasha(_configuration);
       }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment _)
        {
            app.UseBhasha();
        }
    }
}