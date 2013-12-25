using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using LiteCode.SystemWrapper;
using SecureSocketProtocol2;
using SecureSocketProtocol2.Network;
using SecureSocketProtocol2.Network.Messages;
using LiteCode.Messages;

namespace LiteCode.Server
{
    public sealed class Client : SSPClient
    {
        public AClient aClient { get; internal set; }
        public Wrapper System { get; private set; }
        internal LiteServer server { get; set; }

        internal LiteServer.ConnectionCallback disconnectCallback;
        public Client(LiteServer server)
            : base(typeof(ClientChannel), new object[0], true)
        {
            this.aClient = new AClient(this);
            base.MultiThreadProcessing = true;
            this.System = new Wrapper(this);
        }

        public override void onClientConnect()
        {
            aClient.ReleaseLock("InitConnect");
        }

        public override void onDeepPacketInspection(byte[] data)
        {

        }

        public override void onDisconnect()
        {
            if (disconnectCallback != null)
                disconnectCallback(this);
        }

        public override void onException(Exception ex)
        {

        }

        public override void onKeepAlive()
        {

        }

        public override void onReceiveData(IMessage message)
        {
            aClient.ProcessPacket(message);
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