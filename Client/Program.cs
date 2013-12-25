using System;
using System.Collections.Generic;
using System.Text;
using LiteCode;
using LiteSharedCode;
using System.Diagnostics;
using System.IO;

namespace Client
{
    class Program
    {
        static void RefTest(int lol)
        {
            lol = 1337;
        }

        static void Main(string[] args)
        {
            int lol = 0;
            RefTest(lol);

            LiteClient client = new LiteClient("127.0.0.1", 539, null,
            (LiteClient c) => 
            {
                c.aClient.ShareClass("test", typeof(Dummy_Testy));
            });
            Process.GetCurrentProcess().WaitForExit();
        }
    }

    public class Dummy_Testy : ITest
    {
        [RemoteExecution]
        public string TestMe()
        {
            return "gsdgffdg";
        }

        [RemoteExecution]
        public string[] GetStrings()
        {
            return new string[]
            {
                "Is", "Your", "Mind", "Blown", "yet"
            };
        }

        [RemoteExecution]
        public int GetRandomNumber()
        {
            Console.WriteLine("AND YOU THOUGHT IT WILL RETURN 1337");
            Console.WriteLine("Strange that this method never got called... oh well");
            return 1337;
        }

        [RemoteExecution]
        public void TestDelegate(string SomeValue, [PacketQueue] [UncheckedRemoteExecution] TestDel CallMe)
        {
            while(true)
                CallMe("some string from Client Side :)");
        }

        [RemoteExecution]
        public void OutTest(out int SomeValue)
        {
            SomeValue = 1337;
        }

        [RemoteExecution]
        public string PropertyTest
        {
            [RemoteExecution]
            get;
            [RemoteExecution]
            set;
        }

        [UncheckedRemoteExecution]
        public void VoidTest()
        {

        }
    }
}