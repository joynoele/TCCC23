using System;
using System.Collections.Generic;
using Serilog;

namespace TCCC23.Console
{
    class Program
    {
        private static Serilog.ILogger _log;

        static void Main(string[] args)
        {
            _log = DemoLoggers.MongoLogger();
            TestMessages();

            System.Console.ReadKey();
        }

        static void TestMessages()
        {
            //_log.Verbose("hello verbose!");
            //_log.Debug("hello debug!");
            //_log.Information("hello information!");
            //_log.Warning("hello warning!");
            //_log.Error("hello error!");
            //_log.Fatal("hello fatal!");

            var foo = new List<(string greeting,string name, int age)> {
                ("Aloha", "Barack Obama",57),
                ("Hello", "Barack Obama",57),
                ("Привет", "Vladimir Putin",66),
                ("G'dag", "Erna Solberg",58)
            };
            foreach (var combo in foo) {
                TestStructuredMessages(combo.greeting, combo.name, combo.age);
            }
        }

        static void TestStructuredMessages(string salutation, string name, int age)
        {
            _log.Information("{greeting}, {guest}({age})", salutation, name, age);
        }

    }
}
