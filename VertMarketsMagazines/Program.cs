using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace VertMarketsMagazines
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = Startup.ConfigureServices();
            var serviceProvider = services.BuildServiceProvider();

            serviceProvider.GetService<EntryPoint>().Run(args);
        }       
    }
}
