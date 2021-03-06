﻿using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Debugging;
using TCCC23.Library;

namespace TCCC23.DI.Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.MongoDB(MongoRepo.MongoConnectionString(), collectionName: MongoRepo.Log)
                .WriteTo.Console()
                .CreateLogger();

            SelfLog.Enable(System.Console.Error); // Include to get helpful messages of the logger itself

            // Setup pattern from https://msdn.microsoft.com/en-us/magazine/mt707534.aspx
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            try
            {
                // application logic here
                var log = serviceProvider.GetRequiredService<ILogger<Application>>();
                var helper = serviceProvider.GetRequiredService<IHelper>();
                var app = new Application(log, helper);
                app.Run(args);
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }

        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services
                .AddLogging(configure => configure.AddSerilog(dispose: true))
                .AddSingleton<IHelper, Helper>();
            // add other services here
        }

    }
}
