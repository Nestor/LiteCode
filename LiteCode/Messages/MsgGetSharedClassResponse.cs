using LiteCode.Shared;
using ProtoBuf;
using SecureSocketProtocol3.Network.Messages;
using SecureSocketProtocol3.Security.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiteCode.Messages
{
    [ProtoContract]
    internal class MsgGetSharedClassResponse : IMessage
    {
        [ProtoMember(1)]
        public byte[] ResultData { get; set; }

        [ProtoMember(2)]
        public int RequestId { get; set; }

        public ReturnResult Result
        {
            get { return new ReturnResult().Deserialize(ResultData); }
            set { ResultData = value.Serialize(); }
        }

        public MsgGetSharedClassResponse()
            : base()
        {

        }
        public MsgGetSharedClassResponse(int RequestId, ReturnResult Result)
            : base()
        {
            this.RequestId = RequestId;
            this.ResultData = Result.Serialize();
        }

        public override void ProcessPayload(SecureSocketProtocol3.SSPClient client, SecureSocketProtocol3.Network.OperationalSocket OpSocket)
        {
            LiteCodeClient Client = OpSocket as LiteCodeClient;
            lock (Client.Requests)
            {
                if (Client.Requests.ContainsKey(RequestId))
                {
                    //Console.WriteLine("result is null ? " + (Result == null));
                    Client.Requests[RequestId].Value = Result;
                    Client.Requests[RequestId].Pulse();
                    Client.Requests.Remove(RequestId);
                }
            }
        }
    }
}
