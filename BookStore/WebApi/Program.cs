using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DBOperations;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build(); //Host u build ediyoruz.

            using(var scope = host.Services.CreateScope()) //servis katmanını buluyoruz.
            {
                var services = scope.ServiceProvider; // servis katmanı için instance alıyoruz.
                DataGenerator.Initialize(services); // Data generatoru initialize ediyoruz.
            }
            host.Run(); // Ardından host u başlatıyoruz.
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
