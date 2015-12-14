using LiteCode;
using SecureSocketProtocol3;
using SecureSocketProtocol3.Network;
using SecureSocketProtocol3.Security.DataIntegrity;
using SecureSocketProtocol3.Security.Layers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleServer
{
    public class Peer : SSPClient
    {
        public Peer()
            : base()
        {

        }

        public override void onConnect()
        {
            Console.WriteLine("User \"" + base.Username + "\" connected, Peer connected " + base.RemoteIp);
        }

        public override void onDisconnect(DisconnectReason Reason)
        {
            Console.WriteLine("User \"" + base.Username + "\" disconnected");
        }

        public override void onException(Exception ex, ErrorType errorType)
        {

        }

        public override void onBeforeConnect()
        {
            base.RegisterOperationalSocket(new LiteCodeClient(this));
        }

        public override void onOperationalSocket_BeforeConnect(OperationalSocket OPSocket)
        {
            if (OPSocket as LiteCodeClient != null)
            {
                LiteCodeClient liteClient = OPSocket as LiteCodeClient;
                liteClient.ShareClass("SharedTest", typeof(SharedTest), false, int.MaxValue); //int.MaxValue = BAD, use 5-10 instead
            }
        }

        public override void onOperationalSocket_Disconnected(OperationalSocket OPSocket, DisconnectReason Reason)
        {

        }
        public override void onOperationalSocket_Connected(OperationalSocket OPSocket)
        {
        }

        public override void onApplyLayers(SecureSocketProtocol3.Security.Layers.LayerSystem layerSystem)
        {
            layerSystem.AddLayer(new QuickLzLayer());
            layerSystem.AddLayer(new AesLayer(base.Connection));
        }

        private HMacLayer hMacLayer;
        public override SecureSocketProtocol3.Security.DataIntegrity.IDataIntegrityLayer DataIntegrityLayer
        {
            get
            {
                if (hMacLayer == null)
                    hMacLayer = new HMacLayer(this);
                return hMacLayer;
            }
        }
    }
}