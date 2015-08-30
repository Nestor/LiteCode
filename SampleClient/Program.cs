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

            Client client = new Client();

            Console.WriteLine("Press enter to see the response");

            Stopwatch sw = Stopwatch.StartNew();
            int TimesCalled = 0;


            while (true)
            {
                Console.ReadLine();
                int returned = client.SharedTest.IntegerTest();
                Console.WriteLine("Response from server: " + returned);
                Console.Title = "LiteCode Client - Running For: " + sw.Elapsed + ", TimesCalled: " + TimesCalled;
                TimesCalled++;
            }

            Process.GetCurrentProcess().WaitForExit();
        }
    }
}