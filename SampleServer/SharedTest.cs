using LiteCode.Attributes;
using SampleLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleServer
{
    public class SharedTest : ISharedTest
    {
        public SharedTest()
        {

        }

        [RemoteExecution(30000, null)]
        public void CallTest()
        {
            Console.WriteLine("CallTest()");
        }

        [RemoteExecution(30000, null)]
        public string StringTest()
        {
            return "test";
        }

        [RemoteExecution(30000, 0)]
        public int IntegerTest()
        {
            return 8923;
        }

        [RemoteExecution(30000, null)]
        public byte[] ByteArrayTest()
        {
            return new byte[] { 1,3,3,7 };
        }

        [RemoteExecution(30000, null)]
        public void SecretShit()
        {

        }

        [RemoteExecution(30000, null)]
        public void DelegateTest(DelegateTestCallback Delly)
        {
            Delly("HelloWorld from Server :)");
        }

        [RemoteExecution(30000, null)]
        public void SendByteArray(byte[] data)
        {
            Console.WriteLine("SendByteArray, Length: " + data.Length);
        }
    }
}
