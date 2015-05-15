using SecureSocketProtocol3.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SampleServer
{
    class Program
    {
        static void Main(string[] args)
        {
            //SysLogger.onSysLog += SysLogger_onSysLog;
            Console.Title = "LiteCode Server";
            Server server = new Server();

            Process.GetCurrentProcess().WaitForExit();
        }

        static void SysLogger_onSysLog(string Message, SecureSocketProtocol3.SysLogType Type)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("[SysLogger][" + Type + "] " + Message);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}