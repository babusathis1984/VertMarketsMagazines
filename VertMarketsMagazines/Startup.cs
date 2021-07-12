using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration;
using VertMarketsMagazines.Interfaces;
using VertMarketsMagazines.APIFunctions;

namespace VertMarketsMagazines
{
    public static class Startup
    {
        public static IServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false);
            IConfiguration configuration = builder.Build();

            services.AddSingleton(configuration);
            services.AddTransient<IMagazine, MagazineStore>();

            services.AddTransient<EntryPoint>();

            return services;
        }
    }
}
