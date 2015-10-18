using LiteCode;
using SampleLib;
using SecureSocketProtocol3;
using SecureSocketProtocol3.Network;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace SampleClient
{
    public class Client : SSPClient
    {
        public ISharedTest SharedTest;

        public Client()
            : base(new ClientProps())
        {

        }

        public override void onConnect()
        {
            Console.WriteLine("Client successfully connected");

            //Add your own code here...
            LiteCodeClient liteCode = new LiteCodeClient(this);
            liteCode.Connect();

            SharedTest = liteCode.GetSharedClass<ISharedTest>("SharedTest");

            //SharedTest.CallTest();
            //SharedTest.CallTest(1234);
            //SharedTest.CallTest("test");
            //SharedTest.CallTest(ulong.MaxValue);

            //Console.WriteLine("ByteArrayTest");
            //byte[] ret_Array = SharedTest.ByteArrayTest();

            //Console.WriteLine("CallTest");
            //SharedTest.CallTest();
            //SharedTest.DelegateTest(new DelegateTestCallback(CallbackDelegate));

            //Console.WriteLine("IntegerTest");
            //int ret_Int = SharedTest.IntegerTest();

            //Console.WriteLine("SecretShit");
            //SharedTest.SecretShit();


            /*Console.WriteLine("SendByteArray");
            Stopwatch sw = Stopwatch.StartNew();
            byte[] DataTtest = ASCIIEncoding.ASCII.GetBytes("Some Data...");
            for (int i = 0; i < 100000; i++)
                SharedTest.SendByteArray(DataTtest);
            sw.Stop();
            Console.WriteLine(sw.Elapsed);
            */
            //Console.WriteLine("StringTest");
            //string ret_String = SharedTest.StringTest();
        }

        private void CallbackDelegate(string Value)
        {
            Console.WriteLine("[CallbackDelegate] Value: " + Value);
        }

        public override void onDisconnect(DisconnectReason Reason)
        {

        }

        public override void onException(Exception ex, ErrorType errorType)
        {

        }

        public override void onBeforeConnect()
        {
            base.RegisterOperationalSocket(new LiteCodeClient(this));
        }

        public override void onOperationalSocket_Connected(OperationalSocket OPSocket)
        {

        }

        public override void onOperationalSocket_BeforeConnect(OperationalSocket OPSocket)
        {

        }

        public override void onOperationalSocket_Disconnected(OperationalSocket OPSocket, DisconnectReason Reason)
        {

        }

        private class ClientProps : ClientProperties
        {

            public override string HostIp
            {
                get { return "127.0.0.1"; }
            }

            public override ushort Port
            {
                get { return 444; }
            }

            public override int ConnectionTimeout
            {
                get { return 30000; }
            }

            public override string Username
            {
                get { return "UserTest"; }
            }

            public override string Password
            {
                get { return "PassTest"; }
            }

            public override Stream[] PrivateKeyFiles
            {
                get
                {
                    List<MemoryStream> keys = new List<MemoryStream>();
                    keys.Add(new MemoryStream(File.ReadAllBytes(@".\Data\PrivateKey1.dat")));
                    keys.Add(new MemoryStream(File.ReadAllBytes(@".\Data\PrivateKey2.dat")));
                    return keys.ToArray();
                }
            }

            public override Stream PublicKeyFile
            {
                get
                {
                    return new MemoryStream(File.ReadAllBytes(@".\Data\PublicKey1.dat"));
                }
            }

            public override byte[] NetworkKey
            {
                get
                {
                    return new byte[]
                    {
                        80, 118, 131, 114, 195, 224, 157, 246, 141, 113,
                        186, 243, 77, 151, 247, 84, 70, 172, 112, 115,
                        112, 110, 91, 212, 159, 147, 180, 188, 143, 251,
                        218, 155
                    };
                }
            }

            public override uint Cipher_Rounds
            {
                get { return 1; }
            }

            public override EncAlgorithm EncryptionAlgorithm
            {
                get { return EncAlgorithm.HwAES; }
            }

            public override CompressionAlgorithm CompressionAlgorithm
            {
                get { return SecureSocketProtocol3.CompressionAlgorithm.QuickLZ; }
            }

            public override System.Drawing.Size Handshake_Maze_Size
            {
                get { return new System.Drawing.Size(128, 128); }
            }

            public override ushort Handshake_StepSize
            {
                get { return 10; }
            }

            public override ushort Handshake_MazeCount
            {
                get { return 1; }
            }
        }
    }
}
