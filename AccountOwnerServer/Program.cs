using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AccountOwnerServer
{
    public class Program
    {
        // Absolutely Begin First There
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)     // Use this to configure app the app configuration, logging, and dependency injection contrainer
                .ConfigureWebHostDefaults(webBuilder =>  // Use Kestrel Configuration, and using the Startup Class, middleware pipeline
                {
                    webBuilder.UseStartup<Startup>(); // Startup class which we configure embedded or custom services that our application needs. 
                });
    }
}
