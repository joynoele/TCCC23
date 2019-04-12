using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace TCCC23.DI.Console
{
    public class Application
    {
        private ILogger<Application> _log;
        public Application(ILogger<Application> logger)
        {
            _log = logger;
        }

        public void Run(string[] args)
        {
            string input = args.ToString();

            do
            {
                _log.LogInformation("Running with input parameter {parameters}", input);
                System.Console.Write("More input! (press 'q' to quit)");
                input = System.Console.ReadLine();
            } while (input != "q");
        }
    }
}
