﻿using System;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Debugging;

namespace TCCC23.DI.Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("log_DIConsole.txt")
                .CreateLogger();

            SelfLog.Enable(System.Console.Error); // Include to get helpful messages of the logger itself

            // Setup pattern from https://msdn.microsoft.com/en-us/magazine/mt707534.aspx
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            try
            {
                // application logic here
                System.Console.ReadKey();
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
                .AddLogging(configure => configure.AddSerilog(dispose: true));
                // add other services here
        }
    }
}