using LiteCode.Attributes;
using SampleLib;
using SecureSocketProtocol3.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleServer
{
    public class SharedTest : ISharedTest
    {
        FastRandom rnd = new FastRandom();
        public SharedTest()
        {

        }

        [RemoteExecution(30000, null)]
        public void CallTest()
        {
            Console.WriteLine("CallTest()");
        }

        [RemoteExecution(30000, null)]
        public void CallTest(int test)
        {
            Console.WriteLine("CallTest() " + test);
        }

        [RemoteExecution(30000, null)]
        public void CallTest(string test)
        {
            Console.WriteLine("CallTest() " + test);
        }

        [RemoteExecution(30000, null)]
        public void CallTest(object test)
        {
            Console.WriteLine("CallTest() " + test);
        }

        [RemoteExecution(30000, null)]
        public string StringTest()
        {
            return "test";
        }

        [RemoteExecution(30000, 0)]
        public int IntegerTest()
        {
            Console.WriteLine("IntegerTest");
            return rnd.Next();
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

        [UncheckedRemoteExecution()]
        public void DelegateTest(DelegateTestCallback Delly)
        {
            Delly("HelloWorld from Server :)");
        }

        [UncheckedRemoteExecution()]
        public void SendByteArray(byte[] data)
        {
            //Console.WriteLine("SendByteArray, Length: " + data.Length);
        }

    }
}
