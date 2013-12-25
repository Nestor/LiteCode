using LiteCode.Shared;
using SecureSocketProtocol2.Network.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiteCode.Messages
{
    internal class MsgGetSharedClassResponse : IMessage
    {
        public ReturnResult Result;
        public int RequestId;
        public MsgGetSharedClassResponse(int RequestId, ReturnResult Result)
            : base()
        {
            this.RequestId = RequestId;
            this.Result = Result;
        }
        public MsgGetSharedClassResponse()
            : base()
        {

        }
    }
}