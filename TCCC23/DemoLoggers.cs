using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TCCC23.Console
{
    public static class DemoLoggers
    {
        public static Logger SimplestConsole()
        {
            return new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
        }

        public static Logger SimplestFile()
        {
            // logs to Source\Repos\TCCC23\TCCC23\bin\Debug\netcoreapp2.1\log.txt
            return new LoggerConfiguration()
                .WriteTo.File("log_SimplestFile.txt")
                .CreateLogger();
        }

        public static Logger DualSink()
        {
            return new LoggerConfiguration()
                .WriteTo.Console(theme: AnsiConsoleTheme.Code)
                .WriteTo.File("log_DualSink.txt")
                .CreateLogger();
        }

        public static Logger MinimumLogLevel()
        {
            // "if no MinimumLevel is specified, then Information level events and higher will be processed."
            return new LoggerConfiguration()
                .MinimumLevel.Warning()
                .WriteTo.Console(theme: AnsiConsoleTheme.Code)
                .WriteTo.File("log_MinimumLogLevel.txt")
                .CreateLogger();
        }

        public static Logger MinimumSinkLevel()
        {
            return new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Console(theme: AnsiConsoleTheme.Code)
                .WriteTo.File("log_MinimumSinkLevel.txt", restrictedToMinimumLevel: LogEventLevel.Warning)
                .CreateLogger();
        }

        public static Logger MinimumSinkAndLogLevel()
        {
            return new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console(theme: AnsiConsoleTheme.Code, restrictedToMinimumLevel: LogEventLevel.Debug)
                .WriteTo.File("log_MinimumSinkAndLogLevel.txt", restrictedToMinimumLevel: LogEventLevel.Warning)
                .CreateLogger();
        }

        public static Logger EnrichedEnvironmentConsole()
        {
            // Use custom enricher Serilog.Enrichers.Environment
            return new LoggerConfiguration()
                .Enrich.With(new EnvironmentEnricher())
                //.Enrich.WithProperty("Environment", "Production")
                .WriteTo.Console(
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss zzz} [{Environment}:{Level:u3}] {Message:lj}{NewLine}{Exception}")
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

        public static Logger SubLogger()
        {
            return new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Console(theme: AnsiConsoleTheme.Code, restrictedToMinimumLevel: LogEventLevel.Debug)
                .WriteTo.Logger(lc => lc
                    .Filter.ByIncludingOnly(x => x.Level >= LogEventLevel.Warning)
                    .WriteTo.File("log_SubLoggerIssues.txt"))
                .WriteTo.Logger(lc => lc
                    .Filter.ByExcluding(x => x.Level > LogEventLevel.Warning)
                    .WriteTo.File("log_SubLoggerGoodtoknow.txt"))
                .CreateLogger();
        }
    }
}
