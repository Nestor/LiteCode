using System;
using System.Collections.Generic;
using System.Text;
using LiteCode.SystemWrapper;
using SecureSocketProtocol2.SocksProxy;
using SecureSocketProtocol2;
using SecureSocketProtocol2.Network;
using System.Threading;
using System.Diagnostics;
using SecureSocketProtocol2.Network.Messages;
using LiteCode.Messages;

namespace LiteCode
{
    public class LiteClient : SSPClient
    {
        public event Action<LiteClient> onClientDisconnect;
        public AClient aClient { get; internal set; }

        public LiteClient(string HostIp, ushort HostPort, ProxySettings proxy = null, Action<LiteClient> BeforeConnect = null)
            : base(HostIp, HostPort, typeof(ClientChannel), new object[0], EncryptionType.None, CompressionType.None, true, proxy)
        {
            this.aClient = new AClient();

            if (BeforeConnect != null)
                BeforeConnect(this);

            aClient.Connection = this;
            //aClient.Connection.MultiThreadProcessing = true;
            aClient.ShareClass(typeof(Lite_Static_Cursor));
            aClient.ShareClass(typeof(Lite_Socket), true);
            aClient.ShareClass(typeof(Lite_Graphics), true);
            aClient.ShareClass(typeof(Lite_Process), true);
            aClient.ShareClass(typeof(Lite_FileStream), true);
            aClient.Lock("InitConnect"); //just wait till server side completed his part
        }

        public override void onClientConnect()
        {

        }

        public override void onDeepPacketInspection(byte[] data)
        {

        }

        public override void onDisconnect()
        {
            if(onClientDisconnect != null)
                onClientDisconnect(this);
        }

        public override void onException(Exception ex)
        {

        }

        public override void onKeepAlive()
        {

        }


        public override void onValidatingComplete()
        {

        }

        public override void onReconnect()
        {

        }

        public override void onNewChannelOpen(Channel channel)
        {

        }

        public override void onReceiveUdpData(byte[] data, int size)
        {

        }

        int requests = 0;
        Stopwatch sw = Stopwatch.StartNew();
        public override void onReceiveData(IMessage message)
        {
            requests++;
            aClient.ProcessPacket(message);

            if (sw.ElapsedMilliseconds >= 1000)
            {
                Console.WriteLine("Requests: " + requests);
                sw = Stopwatch.StartNew();
                requests = 0;
            }
        }

        public override void onRegisterMessages(MessageHandler messageHandler)
        {
            messageHandler.AddMessage(typeof(MsgGetSharedClass), "SharedClass_Get");
            messageHandler.AddMessage(typeof(MsgGetSharedClassResponse), "SharedClass_GetResponse");
            messageHandler.AddMessage(typeof(MsgReleaseLock), "ReleaseLock");
            messageHandler.AddMessage(typeof(MsgExecuteMethod), "ExecuteMethod");
            messageHandler.AddMessage(typeof(MsgExecuteMethodResponse), "ExecuteMethodresponse");
        }
    }
}