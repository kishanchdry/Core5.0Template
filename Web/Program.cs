using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Context;
using Data.Context.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Services.IServices;
using Services.Services;
using Shared.Common;

namespace Web
{
    /// <summary>
    /// Web app program
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main program
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();
            //CreateHostBuilder(args).Build().Run();

            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();

                ExceptionLoggerExtentionMetod.service = services.GetRequiredService<IExceptionLogginService>();

                try
                {
                    var context = services.GetRequiredService<DataContext>();
                    await context.Database.MigrateAsync();
                    await DataContextSeed.SeedAsync(context);

                    //migrate indentity database
                    var identityContext = services.GetRequiredService<AppIdentityContext>();
                    await identityContext.Database.MigrateAsync();
                    //import seed identity data
                    await AppIdentityContextSeed.SeedAsync(services, identityContext);

                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError($"An error occurred during migration: {ex.Message}");
                }
            }
            host.Run();
        }

        /// <summary>
        /// Create host builder
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
