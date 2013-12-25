using SecureSocketProtocol2.Network.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiteCode.Messages
{
    internal class MsgGetSharedClass : IMessage
    {
        public string ClassName;
        public object[] ArgObjects;
        public int RequestId;

        public MsgGetSharedClass(string ClassName, object[] ArgObjects, int RequestId)
            : base()
        {
            this.ClassName = ClassName;
            this.ArgObjects = ArgObjects;
            this.RequestId = RequestId;
        }
        public MsgGetSharedClass()
            : base()
        {

        }
    }
}