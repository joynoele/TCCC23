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
            return "Production";
        }
    }
}
