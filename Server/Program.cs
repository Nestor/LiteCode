using System;
using System.Collections.Generic;
using System.Text;
using LiteCode.Server;
using System.Diagnostics;
using LiteSharedCode;
using LiteCode;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;
using LiteCode.SystemWrapper.Interfaces;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using LiteCode.SystemWrapper;
using System.IO;
using SecureSocketProtocol2;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            LiteServer server = new LiteServer(539);
            server.onClientConnect += (LiteCode.Server.Client client) =>
            {
                ITest test = client.aClient.GetSharedClass<ITest>("test");
                Stopwatch sw = Stopwatch.StartNew();
                int calls = 0;
                int CallsPerSec = 0;

                while(true)
                {
                    test.TestDelegate("test", (string assd) =>
                    {

                    });

                    calls++;
                    CallsPerSec++;

                    if(sw.ElapsedMilliseconds >= 1000)
                    {
                        Console.WriteLine("milliseconds: " + sw.ElapsedMilliseconds + ", calls: " + calls + ", Calls Per Sec: " + CallsPerSec);
                        CallsPerSec = 0;
                        sw = Stopwatch.StartNew();
                    }
                }

                /*test.TestDelegate(":O", new TestDel((string val) =>
                {
                    calls++;
                    CallsPerSec++;

                    if(sw.ElapsedMilliseconds > 1000)
                    {
                        Console.WriteLine("milliseconds: " + sw.ElapsedMilliseconds + ", calls: " + calls + ", Calls Per Sec: " + CallsPerSec);
                        CallsPerSec = 0;
                        sw = Stopwatch.StartNew();
                    }
                }));*/
            };
            Console.WriteLine("Providing remote code at port 539");
            Process.GetCurrentProcess().WaitForExit();
        }
    }
}