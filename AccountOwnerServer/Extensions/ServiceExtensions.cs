using Contracts;
using Data;
using Data.IRepositories;
using Data.Repository;
using LoggerService;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountOwnerServer.Extensions
{
    public static class ServiceExtensions
    {
        //// We will use Angular as a client-side on a different domain than the server’s domain, configuring CORS is mandatory.
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader());
            }
            );
        }
        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetSection("DbConnection").GetSection("connectionString");
            services.AddEntityFrameworkNpgsql()
                    .AddDbContext<Context>(options => options.UseNpgsql(connectionString.Value));
        }
        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddTransient<IOwnerRepository, OwnerRepository>();
        }
    }
}
