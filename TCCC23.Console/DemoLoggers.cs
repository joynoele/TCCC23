﻿using System;
using System.Collections.Generic;
using System.Text;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace TCCC23.Console
{
    public static class DemoLoggers
    {
        private static string WriteLocation = @"C:\tmp\logs\backup";

        public static Logger DualSink()
        {
            return new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File($@"{WriteLocation}\log_DualSink.txt")
                .CreateLogger();
        }

        public static Logger MinimumSinkAndLogLevel()
        {
            return new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console(theme: AnsiConsoleTheme.Code, 
                    restrictedToMinimumLevel: LogEventLevel.Debug)
                .WriteTo.File($@"{WriteLocation}\log_MinimumSinkAndLogLevel.txt", 
                    restrictedToMinimumLevel: LogEventLevel.Warning)
                .CreateLogger();
        }

        public static Logger EnrichedMachineNameConsole()
        {
            // Use built-in enricher Serilog.Enrichers.Environment
            return new LoggerConfiguration()
                .Enrich.WithMachineName()
                .WriteTo.Console(
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss zzz} [{MachineName}:{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();
        }

        public static Logger EnrichedEnvironmentConsole()
        {
            
            return new LoggerConfiguration()
                .Enrich.With(new EnvironmentEnricher()) // Use custom enricher Serilog.Enrichers.Environment
                //.Enrich.WithProperty("Environment", "Production") // same result
                .WriteTo.Console(
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss zzz} [{Environment}:{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();
        }

        public static Logger EnrichedLogContextEnvironmentConsole()
        {
            return new LoggerConfiguration()
                .Enrich.FromLogContext() // now can use--> using(LogContext.Push(...)) {...}
                .WriteTo.Console(
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss zzz} [{username}:{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();
        }

        public static Logger SubLogger()
        {
            return new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Console(theme: AnsiConsoleTheme.Code, restrictedToMinimumLevel: LogEventLevel.Debug)
                .WriteTo.Logger(lc => lc
                    .Filter.ByIncludingOnly(x => x.Level >= LogEventLevel.Warning)
                    .WriteTo.File($@"{WriteLocation}\log_SubLoggerIssues.txt"))
                .WriteTo.Logger(lc => lc
                    .Filter.ByExcluding(x => x.Level > LogEventLevel.Warning)
                    .WriteTo.File($@"{WriteLocation}\log_SubLoggerGoodtoknow.txt"))
                .CreateLogger();
        }

        public static Logger MongoLogger()
        {
            System.Console.OutputEncoding = System.Text.Encoding.UTF8;
            return new LoggerConfiguration()
                .WriteTo.MongoDB("mongodb://localhost/TCCC23", collectionName: "logs")
                .Enrich.FromLogContext()
                .WriteTo.Console(theme: AnsiConsoleTheme.Code)
                .CreateLogger();
        }
    }
}
