using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Lamar;
using Lamar.Microsoft.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GenericRestService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>

                 Host.CreateDefaultBuilder(args)
                    .UseLamar()
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseStartup<Startup>();
                    });

    }
}