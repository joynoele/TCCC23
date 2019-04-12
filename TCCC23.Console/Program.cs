using System;
using System.Collections.Generic;
using System.Threading;
using Serilog;
using Serilog.Context;
using Serilog.Events;

namespace TCCC23.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //Log.Logger = DemoLoggers.DualSink();
            //TestMessages();

            TroubleshootingDemo();

            System.Console.ReadKey();
        }

        static void TestMessages()
        {
            Log.Logger.Verbose("hello verbose!");
            Log.Logger.Debug("hello debug!");
            Log.Logger.Information("hello information!");
            Log.Logger.Warning("hello warning!");
            Log.Logger.Error("hello error!");
            Log.Logger.Fatal("hello fatal!");
        }

        static void TestMessages2()
        {
            var foo = new List<(string greeting, string name, int age)> {
                ("Aloha", "Barack Obama",57),
                ("Hello", "Barack Obama",57),
                ("Привет", "Vladimir Putin",66),
                ("G'dag", "Erna Solberg",58)
            };

            foreach (var combo in foo)
            {
                // These lines are variations on writing out the same content
                Log.Logger.Information("{greeting}, {leader}({age})", combo.greeting, combo.name, combo.age);
                //Log.Logger.Information("{@user}", combo);
            }
        }

        static void TestMessages3()
        {
            var foo = new List<(string greeting, string name, int age)> {
                ("Aloha", "Barack Obama",57),
                ("Hello", "Barack Obama",57),
                ("Привет", "Vladimir Putin",66),
                ("G'dag", "Erna Solberg",58)
            };

            foreach (var combo in foo)
            {
                var username = string.Concat(combo.name[0], combo.name.Split(" ")[1]).ToLower();
                using (LogContext.Push(new LoggedInUser(username)))
                {
                    Log.Logger.Information("{greeting}, {leader}({age})", combo.greeting, combo.name, combo.age);
                }
            }
        }

        static void TroubleshootingDemo()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .Enrich.FromLogContext()
                .WriteTo.MongoDB("mongodb://localhost/TCCC23", collectionName: "troubleshootdata", restrictedToMinimumLevel: LogEventLevel.Information)
                .WriteTo.File("C:\\tmp\\TroubleshootExample.txt", restrictedToMinimumLevel: LogEventLevel.Debug)
                .WriteTo.ColoredConsole(
                    restrictedToMinimumLevel: LogEventLevel.Information,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Thread}:{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();

            Thread evezino = new Thread(delegate ()
                {
                    RunXTimes(40);
                })
                { Name = "evezino" };

            Thread nblumhardt = new Thread(delegate ()
                {
                    RunXTimes(35);
                })
                { Name = "nblumhardt" };

            Thread jatwood = new Thread(delegate ()
                {
                    RunXTimes(25);
                })
                { Name = "jatwood" };


            evezino.Start();
            nblumhardt.Start();
            jatwood.Start();
        }

        private static void RunXTimes(int runTimes)
        {
            using (LogContext.PushProperty("Thread", Thread.CurrentThread.Name))
            {
                var events = new CreateEvents();
                for (int i = 0; i < runTimes; i++)
                {
                    events.RunRandomQuery();
                }
            }
        }
    }

    public class CreateEvents
    {
        private Random rand;
        public CreateEvents()
        {
            rand = new Random();
        }

        public void RunRandomQuery()
        {
            var query = rand.Next(1, 6);
            switch (query)
            {
                case 1: Query1(); break;
                case 2: Query2(); break;
                case 3: Query3(); break;
                case 4: Query4(); break;
                case 5: Query5(); break;
            }
        }

        private void Query1()
        {
            using (LogContext.PushProperty("query", "Query1"))
            {
                var queryTime = rand.Next(1, 2000);
                ExecuteQuery(queryTime);
                Log.Logger.Debug("");
                Log.Logger.Information("{query} took {ms}ms to run", "Query1", queryTime);
            }
        }
        private void Query2()
        {
            using (LogContext.PushProperty("query", "Query2"))
            {
                var queryTime = rand.Next(1, 3000);
                ExecuteQuery(queryTime);
                Log.Logger.Debug("");
                Log.Logger.Information("{query} took {ms}ms to run", "Query2", queryTime);
            }
        }
        private void Query3()
        {
            using (LogContext.PushProperty("query", "Query3"))
            {
                var queryTime = rand.Next(1, 4000);
                ExecuteQuery(queryTime);
                Log.Logger.Debug("");
                Log.Logger.Information("{query} took {ms}ms to run", "Query3", queryTime);
            }
        }
        private void Query4()
        {
            using (LogContext.PushProperty("query", "Query4"))
            {
                var queryTime = rand.Next(1000, 5000);
                ExecuteQuery(queryTime);
                Log.Logger.Debug("");
                Log.Logger.Information("{query} took {ms}ms to run", "Query4", queryTime);
            }
        }
        private void Query5()
        {
            using (LogContext.PushProperty("query", "Query5"))
            {
                var queryTime = rand.Next(3000, 5000);
                ExecuteQuery(queryTime);
                Log.Logger.Debug("");
                Log.Logger.Information("{query} took {ms}ms to run", "Query5", queryTime);
            }
        }

        private void ExecuteQuery(int sleeptime)
        {
            System.Threading.Thread.Sleep(sleeptime);
            if (sleeptime > 2500 && sleeptime < 3500)
                Log.Logger.Error(new Exception($"Error Id: {Guid.NewGuid()}"), "\tQuery execution failed with execution time {sec}s", sleeptime/1000.0);
        }
    }
}
