using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Serilog.Core;
using Serilog.Events;

namespace TCCC23.Console
{

    public class EnvironmentEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory) {
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(
                "Environment", GetEnvironment()));
        }

        private string GetEnvironment()
        {
            // Logic for determining environment goes here
            return "Production";
        }
    }

    public class LoggedInUser : ILogEventEnricher
    {
        private readonly string _username;
        public LoggedInUser(string username)
        {
            _username = username;
        }
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(
                "username", _username));
        }
    }
}
