using LiteCode.Shared;
using ProtoBuf;
using SecureSocketProtocol3.Network.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiteCode.Messages
{
    [ProtoContract]
    internal class MsgExecuteMethodResponse : IMessage
    {
        [ProtoMember(1)]
        public int RequestId { get; set; }

        [ProtoMember(2)]
        public byte[] ResultData { get; set; }

        public ReturnResult Result
        {
            get { return new ReturnResult().Deserialize(ResultData); }
            set { ResultData = value.Serialize(); }
        }

        public MsgExecuteMethodResponse()
            : base()
        {

        }
        public MsgExecuteMethodResponse(int RequestId, ReturnResult result)
            : base()
        {
            this.RequestId = RequestId;
            this.Result = result;
        }

        public override void ProcessPayload(SecureSocketProtocol3.SSPClient client, SecureSocketProtocol3.Network.OperationalSocket OpSocket)
        {
            LiteCodeClient Client = OpSocket as LiteCodeClient;
            lock (Client.Requests)
            {
                if (Client.Requests.ContainsKey(RequestId))
                {
                    Client.Requests[RequestId].Value = Result;
                    Client.Requests[RequestId].Pulse();
                    Client.Requests.Remove(RequestId);
                }
            }
        }
    }
}
