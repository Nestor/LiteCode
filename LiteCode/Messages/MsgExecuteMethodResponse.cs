using LiteCode.Shared;
using ProtoBuf;
using SecureSocketProtocol3;
using SecureSocketProtocol3.Network;
using SecureSocketProtocol3.Network.Messages;
using SecureSocketProtocol3.Security.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
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

        [ProtoMember(3)]
        public string ExceptionMessage;

        [ProtoMember(4)]
        public bool ExceptionOccured;

        [ProtoMember(5)]
        public bool UseTimeoutValue;

        public object Result
        {
            set
            {
                using (MemoryStream TempStream = new MemoryStream())
                {
                    Serializer.Serialize(TempStream, value);
                    ResultData = TempStream.ToArray();
                }
            }
        }

        public MsgExecuteMethodResponse()
            : base()
        {

        }
        public MsgExecuteMethodResponse(int RequestId, ReturnResult result)
            : base()
        {
            this.RequestId = RequestId;
            this.Result = result.ReturnValue;
            this.ExceptionMessage = result.exceptionMessage;
            this.ExceptionOccured = result.ExceptionOccured;
            this.UseTimeoutValue = result.UseTimeoutValue;
        }

        public object DeserializeObject(Type type)
        {
            if (type == typeof(void))
                return null;

            return Serializer.Deserialize(new MemoryStream(ResultData, 0, ResultData.Length), type);
        }

        public ReturnResult GetReturnResult(Type type)
        {
            ReturnResult ret = new ReturnResult();
            ret.exceptionMessage = this.ExceptionMessage;
            ret.ExceptionOccured = this.ExceptionOccured;
            ret.UseTimeoutValue = this.UseTimeoutValue;
            ret.ReturnValue = DeserializeObject(type);
            return ret;
        }

        public override void ProcessPayload(SSPClient client, OperationalSocket OpSocket)
        {
            LiteCodeClient Client = OpSocket as LiteCodeClient;
            lock (Client.Requests)
            {
                if (Client.Requests.ContainsKey(RequestId))
                {
                    Client.Requests[RequestId].Value = this;
                    Client.Requests[RequestId].Pulse();
                    Client.Requests.Remove(RequestId);
                }
            }
        }
    }
}
