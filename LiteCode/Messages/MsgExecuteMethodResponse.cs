using LiteCode.Shared;
using SecureSocketProtocol2.Network.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiteCode.Messages
{
    internal class MsgExecuteMethodResponse : IMessage
    {
        public int RequestId;
        public ReturnResult result;

        public MsgExecuteMethodResponse(int RequestId, ReturnResult result)
            : base()
        {
            this.RequestId = RequestId;
            this.result = result;
        }
        public MsgExecuteMethodResponse()
            : base()
        {

        }
    }
}