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


            while (true)
            {
                Client client = new Client();
                Clients.Add(client);

                Stopwatch sw = Stopwatch.StartNew();
                int TimesCalled = 0;

                //while (true)
                {
                    //Console.ReadLine();
                    int returned = client.SharedTest.IntegerTest();
                    TimesCalled++;
                    //Console.WriteLine("Response from server: " + returned);

                    if (TimesCalled % 1000 == 0)
                    {
                        Console.Title = "LiteCode Client - Running For: " + sw.Elapsed + ", TimesCalled: " + TimesCalled;
                    }
                }
                Console.Title = "LiteCode Client - Clients: " + Clients.Count;
               // Thread.Sleep(10000);
            }

            Process.GetCurrentProcess().WaitForExit();
        }
    }
}