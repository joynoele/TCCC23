using System;
using Serilog;

namespace TCCC23.Console
{
    class Program
    {
        private static Serilog.ILogger _log;

        static void Main(string[] args)
        {
            _log = DemoLoggers.MinimumLogLevel();
            TestMessages();

            System.Console.ReadKey();
        }

        static void TestMessages()
        {
            _log.Verbose("hello verbose!");
            _log.Debug("hello debug!");
            _log.Information("hello information!");
            _log.Warning("hello warning!");
            _log.Error("hello error!");
            _log.Fatal("hello fatal!");
        }


    }
}
