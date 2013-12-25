using LiteCode.Shared;
using SecureSocketProtocol2.Network.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiteCode.Messages
{
    internal class MsgExecuteMethod : IMessage
    {
        public ReturnResult Result;
        public int RequestId;
        public byte[] Data;
        public bool RequireResultBack;

        public MsgExecuteMethod(int RequestId, byte[] Data, bool RequireResultBack)
            : base()
        {
            this.RequestId = RequestId;
            this.Data = Data;
            this.RequireResultBack = RequireResultBack;
        }
        public MsgExecuteMethod(int RequestId)
            : base()
        {
            this.RequestId = RequestId;
            this.Data = new byte[0];
        }
        public MsgExecuteMethod()
            : base()
        {

        }
    }
}