using System;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using TCCC23.Library;

namespace TCCC23.DI.Console
{
    public class Application
    {
        private ILogger<Application> _log;
        private IHelper _helper;

        public Application(ILogger<Application> logger, IHelper helper)
        {
            _log = logger;
            _helper = helper;
        }

        public void Run(string[] args)
        {
            var patron = CreatePatron();

            using (LogContext.PushProperty("Patron", patron.Name))
            {
                _log.LogInformation("New patron created {@Patron} at {CreateTime}", patron, DateTime.Now);

                System.Console.Write("Do you want to play a game? (press q to quit)");
                var input = System.Console.ReadLine();
                while (input != "q")
                {
                    try
                    {
                        DivideThings();
                        System.Console.WriteLine("Press 'q' to quit, else anykey to continue");
                        input = System.Console.ReadLine();
                    }
                    catch (DivideByZeroException ex)
                    {
                        _log.LogError(ex, "Who did that!?");
                    }
                }
            }
        }

        private Patron CreatePatron()
        {
            System.Console.Write("Username:");
            string username = System.Console.ReadLine();

            System.Console.Write("Password:");
            string password = System.Console.ReadLine();

            return new Patron() {Name = username, Password = password, FirstLogin = DateTime.Now};
        }

        private void DivideThings()
        {
            System.Console.Write("input a number to divide: ");
            double a = Convert.ToDouble(System.Console.ReadLine());

            System.Console.Write("input another # to divide: ");
            double b = Convert.ToDouble(System.Console.ReadLine());
            if ((int)b == 0)
                _helper.TriggerDivisionByZero();

            _log.LogDebug("Recieved input a={numerator} and b={denomonator}", a, b);
            double dividend = a / b;

            _log.LogInformation("{numerator} divided by {denominator} = {dividend}", a, b, dividend);
        }
    }
}

