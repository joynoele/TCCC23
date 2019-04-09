using System;
using System.Collections.Generic;
using System.Threading;
using Serilog;
using Serilog.Context;

namespace TCCC23.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            // Specify the logger and test message to use
            Log.Logger = DemoLoggers.Troubleshooting();
            //TestMessages3();

            Thread evezino = new Thread(delegate ()
            {
                RunXTimes(50);
            })
            { Name = "evezino" };

            Thread nblumhardt = new Thread(delegate ()
            {
                RunXTimes(30);
            })
            { Name = "nblumhardt" };

            Thread jatwood = new Thread(delegate ()
                {
                    RunXTimes(20);
                })
            { Name = "jatwood" };


            evezino.Start();
            nblumhardt.Start();
            /////////

            System.Console.WriteLine("Press any key to exit ...");
            System.Console.ReadKey();
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
                //Log.Logger.Information("{greeting}, {leader}({age})", combo.greeting, combo.name, combo.age);
                Log.Logger.Information("{@user}", combo);
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
            var query = rand.Next(1, 5);
            switch (query)
            {
                case 1:
                    Query1();
                    break;
                case 2:
                    Query2();
                    break;
                case 3:
                    Query3();
                    break;
                case 4:
                    Query4();
                    break;
                case 5:
                    Query5();
                    break;
            }
        }

        private void Query1()
        {
            using (LogContext.PushProperty("query", "Query1"))
            {
                var queryTime = rand.Next(1, 2000);
                Query(queryTime);
                Log.Logger.Debug("");
                Log.Logger.Information("{query} took {sec}ms to run", "query1", queryTime);
            }
        }
        private void Query2()
        {
            using (LogContext.PushProperty("query", "Query2"))
            {
                var queryTime = rand.Next(1, 3000);
                Query(queryTime);
                Log.Logger.Debug("");
                Log.Logger.Information("{query} took {sec}ms to run", "query2", queryTime);
            }
        }
        private void Query3()
        {
            using (LogContext.PushProperty("query", "Query3"))
            {
                var queryTime = rand.Next(1, 4000);
                Query(queryTime);
                Log.Logger.Debug("");
                Log.Logger.Information("{query} took {sec}ms to run", "query3", queryTime);
            }
        }
        private void Query4()
        {
            using (LogContext.PushProperty("query", "Query4"))
            {
                var queryTime = rand.Next(1000, 5000);
                Query(queryTime);
                Log.Logger.Debug("");
                Log.Logger.Information("{query} took {sec}ms to run", "query4", queryTime);
            }
        }
        private void Query5()
        {
            using (LogContext.PushProperty("query", "Query5"))
            {
                var queryTime = rand.Next(3000, 5000);
                Query(queryTime);
                Log.Logger.Debug("");
                Log.Logger.Information("{query} took {sec}ms to run", "query5", queryTime);
            }
        }

        private void Query(int sleeptime)
        {
            if (sleeptime > 2500 && sleeptime < 3500)
                Log.Logger.Error(new Exception("Uh Oh! This query failed."), "Error Id: {ErrorGuid}: ", Guid.NewGuid());
            System.Threading.Thread.Sleep(sleeptime);
        }
    }
}
