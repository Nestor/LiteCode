using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SampleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "LiteCode Client";

            Client client = new Client();

            Process.GetCurrentProcess().WaitForExit();
        }
    }
}