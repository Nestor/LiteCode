using System;
using System.Collections.Generic;
using System.Text;
using SecureSocketProtocol.Network;

namespace LiteCode
{
    [Serializable()]
    internal class Request
    {
        [NonSerialized]
        internal SyncObject syncObject;
        internal int RequestId { get; private set; }
        public byte[] PacketData { get; set; }
        public bool SendResultBack { get; private set; }

        [NonSerialized]
        public bool Async = false;

        [NonSerialized]
        private RequestCallback _Callback;

        public RequestCallback Callback
        {
            get { return _Callback; }
            private set { _Callback = value; }
        }

        public Request(int RequestId, byte[] data, bool SendResultBack, RequestCallback Callback = null)
        {
            this.RequestId = RequestId;
            this.syncObject = new SyncObject();
            this.PacketData = data;
            this.Async = Callback != null;
            this.Callback = Callback;
            this.SendResultBack = SendResultBack;
        }

        ~Request()
        {
            syncObject = null;
            PacketData = null;
            Callback = null;
        }
    }
}