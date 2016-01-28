using SecureSocketProtocol3;
using SecureSocketProtocol3.Network;
using SecureSocketProtocol3.Security.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SampleServer
{
    public class Server : SSPServer
    {
        SortedList<string, User.UserDbInfo> Users;
        public Server()
            : base(new ServerProps())
        {

        }

        public override SSPClient GetNewClient()
        {
            return new Peer();
        }

        private class ServerProps : ServerProperties
        {

            public override ushort ListenPort
            {
                get { return 444; }
            }

            public override string ListenIp
            {
                get { return "0.0.0.0"; }
            }

            public override Stream[] KeyFiles
            {
                get { return new Stream[0]; }
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
            public override TimeSpan ClientTimeConnected
            {
                get { return new TimeSpan(1, 0, 0, 0); }
            }

            public override string ListenIp6
            {
                get
                {
                    return "::";
                }
            }

            public override bool UseIPv4AndIPv6
            {
                get
                {
                    return true;
                }
            }

            public override SecureSocketProtocol3.Security.Serialization.ISerialization DefaultSerializer
            {
                get { return new ProtobufSerialization(); }
            }
        }
    }
}
