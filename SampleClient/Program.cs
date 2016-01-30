using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace SampleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "LiteCode Client";
            List<Client> Clients = new List<Client>();


            //while (true)
            {
                Client client = new Client();
                Clients.Add(client);

                Stopwatch sw = Stopwatch.StartNew();
                Stopwatch PerSecSw = Stopwatch.StartNew();
                int TimesCalled = 0;
                int TimesCalledPerSec = 0;

                while (false)
                {
                    //Console.ReadLine();
                    client.SharedTest.CallTest(1234);
                    TimesCalled++;
                    TimesCalledPerSec++;
                    //Console.WriteLine("Response from server: " + returned);

                    if (PerSecSw.ElapsedMilliseconds >= 1000)
                    {
                        Console.Title = "LiteCode Client - Running For: " + sw.Elapsed + ", TimesCalled: " + TimesCalled;
                        Console.WriteLine("Speed per sec: " + TimesCalledPerSec);
                        TimesCalledPerSec = 0;
                        PerSecSw = Stopwatch.StartNew();
                    }
                }
                //client.Disconnect();
                Console.Title = "LiteCode Client - Clients: " + Clients.Count;
               // Thread.Sleep(10000);
            }

            Process.GetCurrentProcess().WaitForExit();
        }
    }
}